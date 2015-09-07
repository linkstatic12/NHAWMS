using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using WMS.Models;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using WMS.Controllers.Filters;

namespace WMS.Controllers
{
    [CustomControllerAttributes]
    public class UserController : Controller
    {
        private TAS2013Entities db = new TAS2013Entities();

        // GET: /User/
        public ActionResult Index()
        {
            User LoggedInUser = Session["LoggedUser"] as User;
            var users = db.Users.Include(u => u.UserRole);
            return View(users.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        public ActionResult ListOfADUser()
        {

            return View(GetADUsers());
        }
        public ActionResult Create()
        {
            //for (int i = 0; i < 7; i++)
            //{
            //    string Time = Request.Form["StudentList[" + i.ToString() + "].Date"].ToString();
            //}

            ViewBag.CompanyID = new SelectList(db.Companies, "CompID", "CompName");
            ViewBag.EmpID = new SelectList(db.Emps, "EmpID", "EmpNo");
            ViewBag.LocationID = new SelectList(db.Locations, "LocID", "LocName");
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName");
            return View();
        }


        private ADUsersModel GetADUsers()
        {
            ADUsersModel _objstudentmodel = new ADUsersModel();
            _objstudentmodel._ADUsersAttributes = new List<ADUsersAttributes>();
            //using (var context = new PrincipalContext(ContextType.Domain, "fatima-group.com", "ffl.ithelpdesk@fatima-group.com", "fatima@0202"))
            using (var context = new PrincipalContext(ContextType.Domain, "fatima-group.com", "wms.ffl@fatima-group.com", "fflWMS.net"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    int i = 1;
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        string name = result.Name;
                        string displayName = result.DisplayName;
                        string userPrincipleName = result.UserPrincipalName;
                        string samAccountName = result.SamAccountName;
                        string distinguishedName = result.DistinguishedName;
                        //label1.Text += "Name:    " + result.Name;
                        //label1.Text += "      account name   :    " + result.UserPrincipalName;
                        //label1.Text += "      Server:    " + result.Context.ConnectedServer + "\r";
                        _objstudentmodel._ADUsersAttributes.Add(new ADUsersAttributes
                        {
                            ID = i,
                            UserName = name,
                            DisplayName = displayName,
                            PrincipleName = userPrincipleName,
                            DistingushedName = distinguishedName,
                            SAMName = samAccountName
                        });
                        i++;
                    }
                }
            }
            return _objstudentmodel;
        }
        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,Password,EmpID,DateCreated,Name,Status,Department,CanEdit,CanDelete,CanAdd,CanView,CompanyID,RoleID,MHR,MDevice,MLeave,MDesktop,MEditAtt,MUser,MOption,MRoster,MRDailyAtt,MRLeave,MRMonthly,MRAudit,MRManualEditAtt,MREmployee,MRDetail,MRSummary,MRGraph,ViewPermanentStaff,ViewPermanentMgm,ViewContractual,ViewLocation,LocationID")] User user)
        {
            int count;
            String requestform = "";
            int role = (int)user.RoleID;
            switch ((int)user.RoleID)
            {
                case 4: requestform = Request.Form["uZoneCount"]; break;
                case 5: requestform = Request.Form["uRegionCount"]; break;
                case 6: requestform = Request.Form["uCityCount"]; break;
                case 7: requestform = Request.Form["uLocationCount"]; break;
                default: requestform = "-1"; break;
             }

             bool isNumeric = int.TryParse(requestform, out count);
            if(isNumeric)
            {     
            
            if (count > -2)
            {
                bool check = false;
                string _EmpNo = Request.Form["EmpNo"].ToString();
                List<Emp> _emp = db.Emps.Where(aa => aa.EmpNo == _EmpNo).ToList();
                if (_emp.Count == 0)
                {
                    user.EmpID = null;
                    check = true;
                }
                if (user.UserName == null)
                    check = true;

                if (Request.Form["Status"] == "1")
                    user.Status = true;
                else
                    user.Status = false;

                if (Request.Form["CanEdit"] == "1")
                    user.CanEdit = true;
                else
                    user.CanEdit = false;

                if (Request.Form["CanDelete"] == "1")
                    user.CanDelete = true;
                else
                    user.CanDelete = false;

                if (Request.Form["CanAdd"] == "1")
                    user.CanAdd = true;
                else
                    user.CanAdd = false;

                if (Request.Form["CanView"] == "1")
                    user.CanView = true;
                else
                    user.CanView = false;
                if (Request.Form["MUser"] == "1")
                    user.MUser = true;
                else
                    user.MUser = false;
                if (Request.Form["MUser"] == "1")
                    user.MUser = true;
                else
                    user.MUser = false;
                if (Request.Form["MHR"] == "1")
                    user.MHR = true;
                else
                    user.MHR = false;
                if (Request.Form["MOption"] == "1")
                    user.MOption = true;
                else
                    user.MOption = false;
                if (Request.Form["MDevice"] == "1")
                    user.MDevice = true;
                else
                    user.MDevice = false;
                if (Request.Form["MDesktop"] == "1")
                    user.MDesktop = true;
                else
                    user.MDesktop = false;
                if (Request.Form["MEditAtt"] == "1")
                    user.MEditAtt = true;
                else
                    user.MEditAtt = false;
                if (Request.Form["MLeave"] == "1")
                    user.MLeave = true;
                else
                    user.MLeave = false;
                if (Request.Form["MRoster"] == "1")
                    user.MRoster = true;
                else
                    user.MRoster = false;
                if (Request.Form["MRLeave"] == "1")
                    user.MRLeave = true;
                else
                    user.MRLeave = false;
                if (Request.Form["MRDailyAtt"] == "1")
                    user.MRDailyAtt = true;
                else
                    user.MRDailyAtt = false;
                if (Request.Form["MRMonthly"] == "1")
                    user.MRMonthly = true;
                else
                    user.MRMonthly = false;
                if (Request.Form["MRAudit"] == "1")
                    user.MRAudit = true;
                else
                    user.MRAudit = false;
                if (Request.Form["MRManualEditAtt"] == "1")
                    user.MRManualEditAtt = true;
                else
                    user.MRManualEditAtt = false;
                if (Request.Form["MREmployee"] == "1")
                    user.MREmployee = true;
                else
                    user.MREmployee = false;
                if (Request.Form["MRDetail"] == "1")
                    user.MRDetail = true;
                else
                    user.MRDetail = false;
                if (Request.Form["MRSummary"] == "1")
                    user.MRSummary = true;
                else
                    user.MRSummary = false;
                if (Request.Form["MRGraph"] == "1")
                    user.MRGraph = true;
                else
                    user.MRGraph = false;

                if (Request.Form["ViewPermanentStaff"] == "1")
                    user.ViewPermanentStaff = true;
                else
                    user.ViewPermanentStaff = false;
                if (Request.Form["ViewPermanentMgm"] == "1")
                    user.ViewPermanentMgm = true;
                else
                    user.ViewPermanentMgm = false;
                if (Request.Form["ViewContractual"] == "1")
                    user.ViewContractual = true;
                else
                    user.ViewContractual = false;
               

               // if (check == false)
               // {
                  //  string _dpName = FindADUser(user.UserName);
                  //  if (_dpName != "No")
                  //  {
                if (!check)
                {
                    user.Name = _emp.FirstOrDefault().EmpName;
                    user.EmpID = _emp.FirstOrDefault().EmpID;
                }
                        user.DateCreated = DateTime.Today;
                       
                        db.Users.Add(user);
                        db.SaveChanges();
                        //Save UserLoc
                        switch ((int)user.RoleID)
                        {
                            case 4: SaveUserAccess(user, "Zone",count);
                                    break;
                            case 5: SaveUserAccess(user, "Region", count);
                                    break;
                            case 6: SaveUserAccess(user, "City", count);
                                    break;
                            case 7: SaveUserAccess(user, "Location", count);
                                    break;
                            default: SaveUserAccess(user, "SuperUser", count);
                                    break;
                        }
                       
                        return RedirectToAction("Index");
                   // }
              //  }
            }}
            ViewBag.CompanyID = new SelectList(db.Companies, "CompID", "CompName", user.CompanyID);
            ViewBag.EmpID = new SelectList(db.Emps, "EmpID", "EmpNo", user.EmpID);
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName", user.RoleID);
           // ViewBag.LocationID = new SelectList(db.Locations, "LocID", "LocName", user.LocationID);
            return View(user);
        }

        private void SaveUserAccess(Models.User user, string p,int count)
        {
            switch (p)
            {
                case "Zone": List<Zone> locs = new List<Zone>();
                    locs = db.Zones.ToList();
                    for (int i = 1; i <= count; i++)
                    {
                        string uZoneID = "uZone" + i;
                        string ZoneName = Request.Form[uZoneID].ToString();
                        int zoneID = locs.Where(aa => aa.ZoneName == ZoneName).FirstOrDefault().ZoneID;
                        UserAccess uAcc = new UserAccess();
                        uAcc.UserID = user.UserID;
                        uAcc.Criteria = "Z";
                        
                        uAcc.CriteriaData = zoneID;
                        db.UserAccesses.Add(uAcc);
                        db.SaveChanges();
                    }
                                         break;
                case "Region": List<Region> region = new List<Region>();
                                         region = db.Regions.ToList();
                                         for (int i = 1; i <= count; i++)
                                         {
                                             string uRegionID = "uRegion" + i;
                                             string RegionName = Request.Form[uRegionID].ToString();
                                             int regionID = region.Where(aa => aa.RegionName == RegionName).FirstOrDefault().RegionID;
                                             UserAccess uAcc = new UserAccess();
                                             uAcc.UserID = user.UserID;
                                             uAcc.Criteria = "R";
                                             
                                             uAcc.CriteriaData = regionID;
                                             db.UserAccesses.Add(uAcc);
                                             db.SaveChanges();
                                         }
                                         break;
                case "City": List<City> city = new List<City>();
                                         city = db.Cities.ToList();
                                         for (int i = 1; i <= count; i++)
                                         {
                                             string uCityID = "uCity" + i;
                                             string CityName = Request.Form[uCityID].ToString();
                                             int cityID = city.Where(aa => aa.CityName == CityName).FirstOrDefault().CityID;
                                             UserAccess uAcc = new UserAccess();
                                             uAcc.UserID = user.UserID;
                                             uAcc.Criteria = "C";
                                           
                                             uAcc.CriteriaData = cityID;
                                             db.UserAccesses.Add(uAcc);
                                             db.SaveChanges();
                                         }
                                         break;
                case "Location": List<Location> loc = new List<Location>();
                                         loc = db.Locations.ToList();
                                         for (int i = 1; i <= count; i++)
                                         {
                                             string uLocationID = "uLocation" + i;
                                             string LocationName = Request.Form[uLocationID].ToString();
                                             int locationID = loc.Where(aa => aa.LocName == LocationName).FirstOrDefault().LocID;
                                             UserAccess uAcc = new UserAccess();
                                             uAcc.UserID = user.UserID;
                                             uAcc.Criteria = "L";
                                            
                                             uAcc.CriteriaData = locationID;
                                             db.UserAccesses.Add(uAcc);
                                             db.SaveChanges();
                                         }
                                         break;
                case "SuperUser":            UserAccess uAcc1 = new UserAccess();
                                             uAcc1.UserID = user.UserID;
                                             uAcc1.Criteria = "S";
                                            
                                             uAcc1.CriteriaData = -1;
                                             db.UserAccesses.Add(uAcc1);
                                             db.SaveChanges();
                                         
                                         break;            
            
            
            
            }



        }
        private void EditUserAccess(Models.User user, string p, int count)
        {
            
            List<UserAccess> aCC = db.UserAccesses.Where(aa => aa.UserID == user.UserID).ToList();
            foreach (var ac in aCC)
            {
                UserAccess uc = db.UserAccesses.Where(aa => aa.UserID == ac.UserID).FirstOrDefault();
                db.UserAccesses.Remove(uc);
                db.SaveChanges();

            }
            switch (p)
            {
                case "Zone": 
                    List<Zone> locs = new List<Zone>();
                    locs = db.Zones.ToList();
                    for (int i = 1; i <= count; i++)
                    {
                        string uZoneID = "uZone" + i;
                        string ZoneName = Request.Form[uZoneID].ToString();
                        int zoneID = locs.Where(aa => aa.ZoneName == ZoneName).FirstOrDefault().ZoneID;
                        UserAccess uAcc = new UserAccess();
                        uAcc.UserID = user.UserID;
                        uAcc.Criteria = "Z";

                        uAcc.CriteriaData = zoneID;
                        db.UserAccesses.Add(uAcc);
                        db.SaveChanges();
                    }
                    break;
                case "Region": List<Region> region = new List<Region>();
                    region = db.Regions.ToList();
                    for (int i = 1; i <= count; i++)
                    {
                        string uRegionID = "uRegion" + i;
                        string RegionName = Request.Form[uRegionID].ToString();
                        int regionID = region.Where(aa => aa.RegionName == RegionName).FirstOrDefault().RegionID;
                        UserAccess uAcc = new UserAccess();
                        uAcc.UserID = user.UserID;
                        uAcc.Criteria = "R";

                        uAcc.CriteriaData = regionID;
                        db.UserAccesses.Add(uAcc);
                        db.SaveChanges();
                    }
                    break;
                case "City": List<City> city = new List<City>();
                    city = db.Cities.ToList();
                    for (int i = 1; i <= count; i++)
                    {
                        string uCityID = "uCity" + i;
                        string CityName = Request.Form[uCityID].ToString();
                        int cityID = city.Where(aa => aa.CityName == CityName).FirstOrDefault().CityID;
                        UserAccess uAcc = new UserAccess();
                        uAcc.UserID = user.UserID;
                        uAcc.Criteria = "C";

                        uAcc.CriteriaData = cityID;
                        db.UserAccesses.Add(uAcc);
                        db.SaveChanges();
                    }
                    break;
                case "Location": List<Location> loc = new List<Location>();
                    loc = db.Locations.ToList();
                    for (int i = 1; i <= count; i++)
                    {
                        string uLocationID = "uLocation" + i;
                        string LocationName = Request.Form[uLocationID].ToString();
                        int locationID = loc.Where(aa => aa.LocName == LocationName).FirstOrDefault().LocID;
                        UserAccess uAcc = new UserAccess();
                        uAcc.UserID = user.UserID;
                        uAcc.Criteria = "L";

                        uAcc.CriteriaData = locationID;
                        db.UserAccesses.Add(uAcc);
                        db.SaveChanges();
                    }
                    break;
                case "SuperUser": UserAccess uAcc1 = new UserAccess();
                    uAcc1.UserID = user.UserID;
                    uAcc1.Criteria = "S";

                    uAcc1.CriteriaData = -1;
                    db.UserAccesses.Add(uAcc1);
                    db.SaveChanges();

                    break;



            }



        }

        private string FindADUser(string adUserName)
        {
            string displayName = "No";
            ADUsersModel adModel = GetADUsers();
            foreach (var item in adModel._ADUsersAttributes)
            {
                if (item.SAMName.ToUpper() == adUserName.ToUpper())
                {
                    displayName = item.DisplayName;
                }
            }
            return displayName;
        }


        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompID", "CompName", user.CompanyID);
            ViewBag.EmpID = new SelectList(db.Emps, "EmpID", "EmpNo", user.EmpID);
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName", user.RoleID);
           // ViewBag.LocationID = new SelectList(db.Locations, "LocID", "LocName", user.LocationID);
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Password,EmpID,DateCreated,Name,Status,Department,CanEdit,CanDelete,CanAdd,CanView,CompanyID,RoleID,MHR,MDevice,MLeave,MDesktop,MEditAtt,MUser,MOption,MRDailyAtt,MRLeave,MRMonthly,MRAudit,MRManualEditAtt,MREmployee,MRDetail,MRSummary,MRGraph,ViewPermanentStaff,ViewPermanentMgm,ViewContractual,ViewLocation,LocationID")] User user)
        {
            bool check = false;
            
            if (Request.Form["Status"].ToString() == "true")
                user.Status = true;
            else
                user.Status = false;

            if (Request.Form["CanEdit"].ToString() == "true")
                user.CanEdit = true;
            else
                user.CanEdit = false;

            if (Request.Form["CanDelete"].ToString() == "true")
                user.CanDelete = true;
            else
                user.CanDelete = false;

            if (Request.Form["CanAdd"].ToString() == "true")
                user.CanAdd = true;
            else
                user.CanAdd = false;

            if (Request.Form["CanView"].ToString() == "true")
                user.CanView = true;
            else
                user.CanView = false;
            if (Request.Form["MUser"].ToString() == "true")
                user.MUser = true;
            else
                user.MUser = false;
            if (Request.Form["MRoster"].ToString() == "true")
                user.MRoster = true;
            else
                user.MUser = false;
            if (Request.Form["MHR"].ToString() == "true")
                user.MHR = true;
            else
                user.MHR = false;
            //if (Request.Form["MOption"].ToString() == "true")
            //    user.MOption = true;
            //else
            //    user.MOption = false;
            if (Request.Form["MDevice"].ToString() == "true")
                user.MDevice = true;
            else
                user.MDevice = false;
            if (Request.Form["MDesktop"].ToString() == "true")
                user.MDesktop = true;
            else
                user.MDesktop = false;
            if (Request.Form["MEditAtt"].ToString() == "true")
                user.MEditAtt = true;
            else
                user.MEditAtt = false;
            if (Request.Form["MLeave"].ToString() == "true")
                user.MLeave = true;
            else
                user.MLeave = false;
            if (Request.Form["MRLeave"].ToString() == "true")
                user.MRLeave = true;
            else
                user.MRLeave = false;
            if (Request.Form["MRDailyAtt"].ToString() == "true")
                user.MRDailyAtt = true;
            else
                user.MRDailyAtt = false;
            if (Request.Form["MRMonthly"].ToString() == "true")
                user.MRMonthly = true;
            else
                user.MRMonthly = false;
            if (Request.Form["MRAudit"].ToString() == "true")
                user.MRAudit = true;
            else
                user.MRAudit = false;
            if (Request.Form["MRManualEditAtt"].ToString() == "true")
                user.MRManualEditAtt = true;
            else
                user.MRManualEditAtt = false;
            if (Request.Form["MREmployee"].ToString() == "true")
                user.MREmployee = true;
            else
                user.MREmployee = false;
            if (Request.Form["MRDetail"].ToString() == "true")
                user.MRDetail = true;
            else
                user.MRDetail = false;
            if (Request.Form["MRSummary"].ToString() == "true")
                user.MRSummary = true;
            else
                user.MRSummary = false;
            if (Request.Form["MRGraph"].ToString() == "true")
                user.MRGraph = true;
            else
                user.MRGraph = false;
            string requestform;
            

            switch ((int)user.RoleID)
            {
                case 4: requestform = Request.Form["uZoneCount"]; break;
                case 5: requestform = Request.Form["uRegionCount"]; break;
                case 6: requestform = Request.Form["uCityCount"]; break;
                case 7: requestform = Request.Form["uLocationCount"]; break;
                default: requestform = "-1"; break;
            }

            
            user.RoleID = Convert.ToByte(Request.Form["RoleID"].ToString());
            if (check == false)
            {

                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
               
                switch ((int)user.RoleID)
                {
                    case 4: EditUserAccess(user, "Zone", Convert.ToInt32(requestform));
                        break;
                    case 5:EditUserAccess(user, "Region", Convert.ToInt32(requestform));
                        break;
                    case 6: EditUserAccess(user, "City", Convert.ToInt32(requestform));
                        break;
                    case 7: EditUserAccess(user, "Location", Convert.ToInt32(requestform));
                        break;
                    default: EditUserAccess(user, "SuperUser", Convert.ToInt32(requestform));
                        break;
                }
                //int count = Convert.ToInt32(Request.Form["uLocationCount"]);
                //List<Location> locs = new List<Location>();
                //List<UserLocation> userLocs = db.UserLocations.Where(aa=>aa.UserID==user.UserID).ToList();
                //locs = db.Locations.ToList();
                //List<int> currentLocIDs = new List<int>();
                //foreach (var uloc in userLocs)
                //{
                //    UserLocation ul = db.UserLocations.First(aa=>aa.UserLocID==uloc.UserLocID);
                //    db.UserLocations.Remove(ul);
                //    db.SaveChanges();
                //}
                //for (int i = 1; i <= count; i++)
                //{
                //    string uLocID = "uLocation" + i;
                //    string LocName = Request.Form[uLocID].ToString();
                //    int locID = locs.Where(aa => aa.LocName == LocName).FirstOrDefault().LocID;
                //    currentLocIDs.Add(locID);
                //    if(userLocs.Where(aa=>aa.LocationID==locID).Count()>0)
                //    {
                        
                //    }
                //    else
                //    {
                //        UserLocation uloc = new UserLocation();
                //        uloc.UserID = user.UserID;
                //        uloc.LocationID = (short)locID;
                //        db.UserLocations.Add(uloc);
                //        db.SaveChanges();
                //    }   
                //}
               
                return RedirectToAction("Index");

            }

            ViewBag.CompanyID = new SelectList(db.Companies, "CompID", "CompName", user.CompanyID);
            ViewBag.EmpID = new SelectList(db.Emps, "EmpID", "EmpNo", user.EmpID);
            ViewBag.RoleID = new SelectList(db.UserRoles, "RoleID", "RoleName", user.RoleID);
        //    ViewBag.LocationID = new SelectList(db.Locations, "LocID", "LocName", user.LocationID);
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
        public ActionResult UserZoneList()
        {
            var states = db.Zones.ToList();
            return Json(new SelectList(
                            states.ToArray(),
                            "ZoneID",
                            "ZoneName")
                       , JsonRequestBehavior.AllowGet);
        
        
        }
        public ActionResult UserRegionList()
        {
            var states = db.Regions.ToList();
            return Json(new SelectList(
                            states.ToArray(),
                            "RegionID",
                            "RegionName")
                       , JsonRequestBehavior.AllowGet);


        }
        public ActionResult UserCityList()
        {
            var states = db.Cities.ToList();
            return Json(new SelectList(
                            states.ToArray(),
                            "CityID",
                            "CityName")
                       , JsonRequestBehavior.AllowGet);
        
        
        }

        public ActionResult UserLocationList()
        {
            var states = db.Locations.ToList();
                return Json(new SelectList(
                                states.ToArray(),
                                "LocID",
                                "LocName")
                           , JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectedUserZoneList(int id)
        {
            List<UserAccess> userLoc = db.UserAccesses.Where(aa => aa.UserID == id).ToList();
            List<Zone> _locs = db.Zones.ToList();
            List<Zone> locs = new List<Zone>();
            if(userLoc.FirstOrDefault().Criteria.Contains("Z"))
            foreach (var loc in userLoc)
            {
                Zone ll = db.Zones.FirstOrDefault(aa => aa.ZoneID == loc.CriteriaData);
                locs.Add(ll);
            }
            return Json(new SelectList(
                           locs.ToArray(),
                           "ZoneID",
                           "ZoneName")
                      , JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectedUserCityList(int id)
        {
            List<UserAccess> userLoc = db.UserAccesses.Where(aa => aa.UserID == id).ToList();
            List<City> _locs = db.Cities.ToList();
            List<City> locs = new List<City>();
            if (userLoc.FirstOrDefault().Criteria.Contains("C"))
                foreach (var loc in userLoc)
                {
                    City ll = db.Cities.FirstOrDefault(aa => aa.CityID == loc.CriteriaData);
                    locs.Add(ll);
                }
            return Json(new SelectList(
                           locs.ToArray(),
                           "CityID",
                           "CityName")
                      , JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectedUserRegionList(int id)
        {
            List<UserAccess> userLoc = db.UserAccesses.Where(aa => aa.UserID == id).ToList();
            List<Region> _locs = db.Regions.ToList();
            List<Region> locs = new List<Region>();
            if (userLoc.FirstOrDefault().Criteria.Contains("R"))
                foreach (var loc in userLoc)
                {
                    Region ll = db.Regions.FirstOrDefault(aa => aa.RegionID == loc.CriteriaData);
                    locs.Add(ll);
                }
            return Json(new SelectList(
                           locs.ToArray(),
                           "RegionID",
                           "RegionName")
                      , JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectedUserLocList(int id)
        {
            List<UserAccess> userLoc = db.UserAccesses.Where(aa => aa.UserID == id).ToList();
            List<Location> _locs = db.Locations.ToList();
            List<Location> locs = new List<Location>();

            if (userLoc.FirstOrDefault().Criteria.Contains("L"))
            foreach (var loc in userLoc)
            {
                Location ll = db.Locations.FirstOrDefault(aa => aa.LocID == loc.CriteriaData);
                locs.Add(ll);
            }
            return Json(new SelectList(
                           locs.ToArray(),
                           "LocID",
                           "LocName")
                      , JsonRequestBehavior.AllowGet);
        }
    }

    public class ADUsersAttributes
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string PrincipleName { get; set; }
        public string SAMName { get; set; }
        public string DistingushedName { get; set; }
        public bool Checked { get; set; }

    }
    public class ADUsersModel
    {
        public List<ADUsersAttributes> _ADUsersAttributes { get; set; }
    }

}
