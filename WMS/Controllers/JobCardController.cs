using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WMS.Models;

namespace WMS.Controllers
{
    public class JobCardController : Controller
    {
        private TAS2013Entities db = new TAS2013Entities();

        // GET: /JobCard/
        public ActionResult Index()
        {
            ViewData["JobDateFrom"] = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            ViewData["JobDateTo"] = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            ViewBag.JobCardType = new SelectList(db.JobCards, "WorkCardID", "WorkCardName");
            return View(db.JobCardApps.ToList());

        }

        [HttpPost]
        public ActionResult EditAttJobCard()
        {
            User LoggedInUser = Session["LoggedUser"] as User;
            try
            {
                string _EmpNo = "";
               // int CompID = Convert.ToInt16(Request.Form["CompanyID"].ToString());
                List<Emp> _Emp = new List<Emp>();
                short _WorkCardID = Convert.ToInt16(Request.Form["JobCardType"].ToString());
                //First Save Job Card Application
                JobCardApp jobCardApp = new JobCardApp();
                jobCardApp.CardType = _WorkCardID;
                jobCardApp.DateCreated = DateTime.Now;
                jobCardApp.DateStarted = Convert.ToDateTime(Request.Form["JobDateFrom"]);
                jobCardApp.DateEnded = Convert.ToDateTime(Request.Form["JobDateTo"]);
                jobCardApp.Status = false;
                //switch (Request.Form["cars"].ToString())
                //{                    case "employee":
                //        if (Request.Form["cars"].ToString() == "employee")
                //        {
                            _EmpNo = Request.Form["JobEmpNo"];
                            _Emp = db.Emps.Where(aa => aa.EmpNo == _EmpNo).ToList();
                            if (_Emp.Count > 0)
                            {
                                jobCardApp.CriteriaData = _Emp.FirstOrDefault().EmpID;
                                jobCardApp.JobCardCriteria = "E";
                                db.JobCardApps.Add(jobCardApp);
                                if (db.SaveChanges() > 0)
                                {
                                    AddJobCardAppToJobCardData();
                                }
                            }
                       // }
                        //break;
                //}

                //Add Job Card to JobCardData and Mark Legends in Attendance Data if attendance Created
                Session["EditAttendanceDate"] = DateTime.Today.Date.ToString("yyyy-MM-dd");
                //ViewBag.JobCardType = new SelectList(db.JobCards, "WorkCardID", "WorkCardName");
                //ViewBag.ShiftList = new SelectList(db.Shifts, "ShiftID", "ShiftName");
                //ViewBag.CrewList = new SelectList(db.Crews, "CrewID", "CrewName");
                //ViewBag.SectionList = new SelectList(db.Sections, "SectionID", "SectionName");
                ViewBag.CMessage = "Job Card Created sucessfully";
                ViewData["datef"] = Session["EditAttendanceDate"].ToString();
                ViewData["JobDateFrom"] = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
                ViewData["JobDateTo"] = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
                ViewBag.JobCardType = new SelectList(db.JobCards, "WorkCardID", "WorkCardName");
              //  ViewBag.CompanyID = new SelectList(db.Companies, "CompID", "CompName", LoggedInUser.CompanyID);
                return View("Index");
            }
            catch (Exception ex)
            {
                //ViewData["datef"] = HttpContext.Session["EditAttendanceDate"].ToString();
                ViewData["JobDateFrom"] = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
                ViewData["JobDateTo"] = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
                ViewBag.JobCardType = new SelectList(db.JobCards, "WorkCardID", "WorkCardName");
                //ViewBag.ShiftList = new SelectList(db.Shifts, "ShiftID", "ShiftName");
                //ViewBag.CrewList = new SelectList(db.Crews, "CrewID", "CrewName");
                //ViewBag.SectionList = new SelectList(db.Sections, "SectionID", "SectionName");
                ViewBag.CMessage = "An Error occured while creating Job Card of" + Request.Form["JobCardType"].ToString();
                return View("Index");
            }
        }

        private void AddJobCardAppToJobCardData()
        {
            using (var ctx = new TAS2013Entities())
            {
                List<JobCardApp> _jobCardApp = new List<JobCardApp>();
                _jobCardApp = ctx.JobCardApps.Where(aa => aa.Status == false).ToList();
                User LoggedInUser = Session["LoggedUser"] as User;
                List<Emp> _Emp = new List<Emp>();
                foreach (var jcApp in _jobCardApp)
                {
                    jcApp.Status = true;
                    switch (jcApp.JobCardCriteria)
                    {

                       case "E":
                            int _EmpID = (int)jcApp.CriteriaData;
                            _Emp = ctx.Emps.Where(aa => aa.EmpID == _EmpID).ToList();
                           break;
                    }
                    foreach (var selectedEmp in _Emp)
                    {
                        //AddJobCardData(selectedEmp, (short)jcApp.CardType, jcApp.DateStarted.Value, jcApp.DateEnded.Value);
                    }
                }
                ctx.SaveChanges();
                ctx.Dispose();
            }
        }










        // GET: /JobCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCardApp jobcardapp = db.JobCardApps.Find(id);
            if (jobcardapp == null)
            {
                return HttpNotFound();
            }
            return View(jobcardapp);
        }

        // GET: /JobCard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /JobCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="JobCardID,DateCreated,DateStarted,DateEnded,CardType,UserID,JobCardCriteria,CriteriaData,Status,TimeIn,TimeOut,WorkMin")] JobCardApp jobcardapp)
        {
            if (ModelState.IsValid)
            {
                db.JobCardApps.Add(jobcardapp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobcardapp);
        }

        // GET: /JobCard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCardApp jobcardapp = db.JobCardApps.Find(id);
            if (jobcardapp == null)
            {
                return HttpNotFound();
            }
            return View(jobcardapp);
        }

        // POST: /JobCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="JobCardID,DateCreated,DateStarted,DateEnded,CardType,UserID,JobCardCriteria,CriteriaData,Status,TimeIn,TimeOut,WorkMin")] JobCardApp jobcardapp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobcardapp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobcardapp);
        }

        // GET: /JobCard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCardApp jobcardapp = db.JobCardApps.Find(id);
            if (jobcardapp == null)
            {
                return HttpNotFound();
            }
            return View(jobcardapp);
        }

        // POST: /JobCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobCardApp jobcardapp = db.JobCardApps.Find(id);
            db.JobCardApps.Remove(jobcardapp);
            db.SaveChanges();
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
