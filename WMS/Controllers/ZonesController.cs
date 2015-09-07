using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WMS.CustomClass;
using WMS.Models;

namespace WMS.Controllers
{   
    public class ZonesController : Controller
    {
        private TAS2013Entities context = new TAS2013Entities();
        CustomFunc myClass = new CustomFunc();
        //
        // GET: /Zones/

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            User LoggedInUser = HttpContext.Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            string query = qb.QueryForUserAccess(LoggedInUser, "Zone");
            DataTable dt = qb.GetValuesfromDB("Select * FROM Zone where " + query);
            var zone = dt.ToList<Zone>().AsQueryable();
           

            if (!String.IsNullOrEmpty(searchString))
            {
                zone = zone.Where(s => s.ZoneName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    zone = zone.OrderByDescending(s => s.ZoneName);
                    break;
                default:
                    zone = zone.OrderBy(s => s.ZoneName);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(zone.ToPagedList(pageNumber, pageSize));

        }

        //
        // GET: /Zones/Details/5

        public ViewResult Details(short id)
        {
            Zone zone = context.Zones.Single(x => x.ZoneID == id);
            return View(zone);
        }

        //
        // GET: /Zones/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Zones/Create

        [HttpPost]
        public ActionResult Create(Zone zone)
        {
            if (ModelState.IsValid)
            {
                List<Zone> zo2ne = context.Zones.Where(aa => aa.ZoneName == "Central Zone").ToList();
                context.Zones.Add(zone);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(zone);
        }
        
        //
        // GET: /Zones/Edit/5
 
        public ActionResult Edit(short id)
        {
            Zone zone = context.Zones.Single(x => x.ZoneID == id);
            return View(zone);
        }

        //
        // POST: /Zones/Edit/5

        [HttpPost]
        public ActionResult Edit(Zone zone)
        {
            if (ModelState.IsValid)
            {
                List<Region> region = new List<Region>();
                region = context.Regions.Where(aa => aa.ZoneID == zone.ZoneID).ToList();
                foreach (var re in region)
                {
                     
                    re.ZoneName = zone.ZoneName;
                    context.SaveChanges();
                
                }
                context.Entry(zone).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(zone);
        }

        //
        // GET: /Zones/Delete/5
 
        public ActionResult Delete(short id)
        {
              
            Zone zone = context.Zones.Single(x => x.ZoneID == id);
            return View(zone);
        }

        //
        // POST: /Zones/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(short id)
        {
            Zone zone = context.Zones.Single(x => x.ZoneID == id);
            context.Zones.Remove(zone);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}