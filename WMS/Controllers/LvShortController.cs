using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WMS.Models;
using PagedList;
using WMS.CustomClass;
using WMS.Controllers.Filters;
using WMS.HelperClass;
namespace WMS.Controllers
{
     [CustomControllerAttributes]
    public class LvShortController : Controller
    {
        private TAS2013Entities db = new TAS2013Entities();

        // GET: /LvShort/
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            User LoggedInUser = Session["LoggedUser"] as User;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TypeSortParm = sortOrder == "LvType" ? "LvType_desc" : "LvType";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForUserAccess(LoggedInUser, qb.CheckForUserRole(LoggedInUser));

            DateTime dt1 = DateTime.Today;
            DateTime dt2 = new DateTime(dt1.Year, 1, 1);
            string date = dt2.Year.ToString() + "-" + dt2.Month.ToString() + "-" + dt2.Day.ToString() + " ";
            DataTable dt = qb.GetValuesfromDB("select * from ViewSLData where " + query);
            List<ViewSLData> lvapplications = dt.ToList<ViewSLData>();

            ViewBag.CurrentFilter = searchString;
            //var lvapplications = db.LvApplications.Where(aa=>aa.ToDate>=dt2).Include(l => l.Emp).Include(l => l.LvType1);
            if (!String.IsNullOrEmpty(searchString))
            {
                lvapplications = lvapplications.Where(s => s.EmpName.ToUpper().Contains(searchString.ToUpper())
                     || s.EmpNo.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lvapplications = lvapplications.OrderByDescending(s => s.EmpName).ToList();
                    break;
                case "Date_desc":
                    lvapplications = lvapplications.OrderByDescending(s => s.DutyDate).ToList();
                    break;
                case "Date":
                    lvapplications = lvapplications.OrderBy(s => s.DutyDate).ToList();
                    break;
                default:
                    lvapplications = lvapplications.OrderBy(s => s.EmpName).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(lvapplications.OrderByDescending(aa => aa.DutyDate).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult EmpList(string sortOrder, string searchString, string currentFilter, int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            List<EmpView> listOfEmps = new List<EmpView>();

            listOfEmps = db.EmpViews.Where(aa => aa.Status == true).OrderBy(s => s.EmpName).ToList();
            if (ViewBag.CurrentFilter != null || searchString != null)
                page = 1;
            else { searchString = currentFilter; }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                listOfEmps = listOfEmps.Where(s => s.EmpName.ToUpper().Contains(searchString.ToUpper())
                     || s.EmpNo.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            return View(listOfEmps.ToPagedList(pageNumber, pageSize));
        }
        // GET: /LvShort/Details/5
             [CustomActionAttribute]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LvShort lvshort = db.LvShorts.Find(id);
            if (lvshort == null)
            {
                return HttpNotFound();
            }
            return View(lvshort);
        }

        // GET: /LvShort/Create
             [CustomActionAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /LvShort/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionAttribute]
        public ActionResult Create([Bind(Include = "SlID,EmpID,DutyDate,EmpDate,SHour,EHour,THour,Remarks,CreatedBy,ApprovedBy,Status")] LvShort lvshort,string SHour,string EHour)
        {
            string STimeIn = SHour;
            string STimeOut = EHour;
            string STimeInH = STimeIn.Substring(0, 2);
            string STimeInM = STimeIn.Substring(2, 2);
            string STimeOutH = STimeOut.Substring(0, 2);
            string STimeOutM = STimeOut.Substring(2, 2);
            lvshort.SHour = new TimeSpan(Convert.ToInt16(STimeInH), Convert.ToInt16(STimeInM), 0);
            lvshort.EHour = new TimeSpan(Convert.ToInt16(STimeOutH), Convert.ToInt16(STimeOutM), 0);
            if (lvshort.EHour < lvshort.SHour)
            {
                ModelState.AddModelError("EHour", "End hour required");
            }
            if (Request.Form["EmpNo"].ToString() == "")
            {
                ModelState.AddModelError("EmpNo", "Emplyee No is required!");
            }
            else
            {
                string _EmpNo = Request.Form["EmpNo"].ToString();
                List<Emp> _emp = db.Emps.Where(aa => aa.EmpNo == _EmpNo).ToList();
                if (_emp.Count == 0)
                {
                    ModelState.AddModelError("EmpNo", "Emp No not exist");
                }
                else
                {
                    lvshort.EmpID = _emp.FirstOrDefault().EmpID;
                    lvshort.EmpDate = _emp.FirstOrDefault().EmpID.ToString() + lvshort.DutyDate.Value.ToString("yyMMdd");
                    lvshort.CreatedDate = DateTime.Today;
                    lvshort.THour = lvshort.EHour - lvshort.SHour;
                }
            }
            if (lvshort.DutyDate == null)
            {
                ModelState.AddModelError("DutyDate", "DutyDate is required!");
            }
            if (lvshort.SHour == null)
            {
                ModelState.AddModelError("SHour", "Start Time is required!");
            }
            if (lvshort.EHour == null)
            {
                ModelState.AddModelError("EHour", "Ending Time is required!");
            }
            if (ModelState.IsValid)
            {
                LeaveController LvProcessController = new LeaveController();
                int _userID = Convert.ToInt32(Session["LogedUserID"].ToString());
                User LoggedInUser = Session["LoggedUser"] as User;
                lvshort.CreatedBy = _userID;
                lvshort.CompanyID = LoggedInUser.CompanyID;
                lvshort.Status = true;
               
                //The below LvShort checks if there is an already existing Lvshort for
                //this date. If there is an existing LvShort remove it from the database 
                //, check if he has balance for causal leave and then add a causal leave
                //
               
                if (db.Options.FirstOrDefault().TwoShortLToOneCausal == true)
                {

                    if (db.LvShorts.Where(aa => aa.EmpDate == lvshort.EmpDate).Count() > 0)
                    {
                        if (CheckForLQuota(lvshort) == true)
                        {
                            ShortLRemove(lvshort.EmpDate);
                            switch (AddCausalLeave(lvshort))
                            {
                                case 1: ModelState.AddModelError("Remarks", "Causal Leave Already exists");
                                    break;
                                case 2: db.SaveChanges();
                                    break;
                                case 3: ModelState.AddModelError("Remarks", "No Quota Defined for this Employee");
                                    break;
                            
                            
                            }
                          
                            
                            return RedirectToAction("Index");
                        }
                        else 
                        {
                            ModelState.AddModelError("Remarks", "No Quota Defined for this Employee");
                            return View(lvshort);
                        }
                        
                    }

               }
                        //Check If its already made
                    TimeSpan ehour = new TimeSpan(Convert.ToInt32(STimeOutH), Convert.ToInt32(STimeOutM), 0);
                    TimeSpan shour = new TimeSpan(Convert.ToInt32(STimeInH), Convert.ToInt32(STimeInM), 0);
                    if (db.LvShorts.Where(aa => aa.EmpDate == lvshort.EmpDate && (aa.SHour == shour || aa.EHour == ehour)).Count() > 0)
                        ModelState.AddModelError("Remarks", "Short Leave Already Exists for this time duration");
                    else
                    {
                        
                        db.LvShorts.Add(lvshort);
                        db.SaveChanges();
                        LvProcessController.AddShortLeaveToAttData(lvshort);
                        HelperClass.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.ShortLeave, (byte)MyEnums.Operation.Add, DateTime.Now);
                        return RedirectToAction("Index");
                    }
                

                
                
                
               
            }

            return View(lvshort);
        }

        private bool CheckForLQuota(LvShort lvshort)
        {
            string EmpLvType = lvshort.EmpID + "A";
         
            List<LvConsumed> consumed = db.LvConsumeds.Where(aa => aa.EmpLvType == EmpLvType).ToList();
            if (consumed.Count() > 0)
                return true;
            else
                return false;
          
        }

        private void ShortLRemove(string p)
        {
            List<LvShort> removeShort = db.LvShorts.Where(aa => aa.EmpDate == p).ToList();
            foreach(LvShort removes in removeShort)
            {
                db.LvShorts.Remove(removes);
            
            
            }
            db.SaveChanges();
        }

        private int AddCausalLeave(LvShort lvshort)
        {
            //find that bastard through the causal leave field in EmpLvType
            string EmpLvType= lvshort.EmpID+"A";
            //if there is some freak accident and now he has two or more records 
            //cater for that by going through a list
            List<LvConsumed> consumed = db.LvConsumeds.Where(aa => aa.EmpLvType == EmpLvType).ToList();
            if (consumed.Count() > 0)
            {
                foreach (LvConsumed consume in consumed)
                {
                    consume.GrandTotalRemaining = consume.GrandTotalRemaining - 1;
                    consume.YearRemaining = consume.YearRemaining - 1;
                    LvConsumed refresh = new LvConsumed();
                    refresh = CheckMonthAndAddOneLeave(consume);
                    if (db.LvApplications.Where(aa => aa.LvDate == lvshort.DutyDate && aa.EmpID == lvshort.EmpID && aa.LvType == "A").Count() > 0)
                    {
                        return 1;

                    }
                    else
                    {
                        LvApplication lvapplication = new LvApplication();
                        lvapplication.EmpID = (int)lvshort.EmpID;
                        lvapplication.LvDate = (DateTime)lvshort.DutyDate;
                        lvapplication.LvType = "A";
                        lvapplication.FromDate = (DateTime)lvshort.DutyDate;
                        lvapplication.ToDate = (DateTime)lvshort.DutyDate;
                        lvapplication.NoOfDays = 1;
                        lvapplication.IsHalf = false;
                        lvapplication.HalfAbsent = false;
                        lvapplication.LvReason = lvshort.Remarks;
                        lvapplication.CreatedBy = lvshort.CreatedBy;
                        lvapplication.LvStatus = "P";
                        lvapplication.CompanyID = lvshort.CompanyID;
                        lvapplication.Active = true;
                        LeaveController LvProcessController = new LeaveController();
                        if (LvProcessController.HasLeaveQuota(lvapplication.EmpID, lvapplication.LvType))
                        {
                            if (lvapplication.IsHalf != true)
                            {
                              
                                if (LvProcessController.CheckDuplicateLeave(lvapplication))
                                {
                                    //Check leave Balance
                                    if (LvProcessController.CheckLeaveBalance(lvapplication))
                                    {
                                        lvapplication.LvDate = DateTime.Today;
                                        int _userID = Convert.ToInt32(Session["LogedUserID"].ToString());
                                        lvapplication.CreatedBy = _userID;
                                db.LvApplications.Add(lvapplication);
                                        if (db.SaveChanges() > 0)
                                        {
                                            HelperClass.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Leave, (byte)MyEnums.Operation.Add, DateTime.Now);
                                            LvProcessController.AddLeaveToLeaveData(lvapplication);
                                            LvProcessController.AddLeaveToLeaveAttData(lvapplication);
                                             return 2;
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("Remarks", "There is an error while creating leave.");
                                        }

                                    }
                                    else
                                        ModelState.AddModelError("Remarks", "Leave Balance Exceeds, Please check the balance");
                                }
                                else
                                    ModelState.AddModelError("Remarks", "This Employee already has leave of this date ");
                            }
                      }
                        else
                            ModelState.AddModelError("Remarks", "Leave Quota does not exist");
                       

                       
                        
                    
                    
                    }
                   


                }
                return 2;
            }
            else
                return 3;
            
        }

        private LvConsumed CheckMonthAndAddOneLeave(LvConsumed consume)
        {
            DateTime today = DateTime.Now; 
            switch (today.Month)
            {

                case 1: consume.JanConsumed = consume.JanConsumed + 1;
                    break;
                case 2: consume.FebConsumed = consume.FebConsumed + 1;
                    break;
                case 3: consume.MarchConsumed = consume.MarchConsumed + 1;
                    break;
                case 4: consume.AprConsumed = consume.AprConsumed + 1;
                    break;
                case 5: consume.MayConsumed = consume.MayConsumed + 1;
                    break;
                case 6: consume.JuneConsumed = consume.JuneConsumed + 1;
                    break;
                case 7: consume.JulyConsumed = consume.JulyConsumed + 1;
                    break;
                case 8: consume.AugustConsumed = consume.AugustConsumed + 1;
                    break;
                case 9: consume.AugustConsumed = consume.AugustConsumed + 1;
                    break;
                case 10: consume.SepConsumed = consume.SepConsumed + 1;
                    break;
                case 11: consume.OctConsumed = consume.OctConsumed + 1;
                    break;
                case 12: consume.DecConsumed = consume.DecConsumed + 1;
                    break;
            
            }
            return consume;
        }

        // GET: /LvShort/Edit/5
             [CustomActionAttribute]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LvShort lvshort = db.LvShorts.Find(id);
            if (lvshort == null)
            {
                return HttpNotFound();
            }
            return View(lvshort);
        }

        // POST: /LvShort/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomActionAttribute]
        public ActionResult Edit([Bind(Include = "SlID,EmpID,DutyDate,EmpDate,SHour,EHour,THour,Remarks,CreatedBy,ApprovedBy,Status")] LvShort lvshort)
        {
            if (lvshort.EHour < lvshort.SHour)
            {
                ModelState.AddModelError("EHour", "End hour required");
            }
            if (lvshort.DutyDate == null)
            {
                ModelState.AddModelError("DutyDate", "DutyDate is required!");
            }
            if (ModelState.IsValid)
            {
                lvshort.CreatedDate = DateTime.Today;
                lvshort.THour = lvshort.EHour - lvshort.SHour;
                db.Entry(lvshort).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                int _userID = Convert.ToInt32(Session["LogedUserID"].ToString());
                HelperClass.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.ShortLeave, (byte)MyEnums.Operation.Edit, DateTime.Now);
                return RedirectToAction("Index");
            }
            return View(lvshort);
        }

        // GET: /LvShort/Delete/5
             [CustomActionAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LvShort lvshort = db.LvShorts.Find(id);
            if (lvshort == null)
            {
                return HttpNotFound();
            }
            return View(lvshort);
        }

        // POST: /LvShort/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomActionAttribute]
        public ActionResult DeleteConfirmed(int id)
        {
            LvShort lvshort = db.LvShorts.Find(id);
            db.LvShorts.Remove(lvshort);
            db.SaveChanges();
            int _userID = Convert.ToInt32(Session["LogedUserID"].ToString());
            HelperClass.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.ShortLeave, (byte)MyEnums.Operation.Delete, DateTime.Now);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
