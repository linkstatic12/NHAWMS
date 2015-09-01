using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS.Models;

namespace WMS.Controllers
{   
    public class ZonesController : Controller
    {
        private TAS2013Entities context = new TAS2013Entities();

        //
        // GET: /Zones/

        public ViewResult Index()
        {
            return View(context.Zones.ToList());
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
                context.Entry(zone).State = EntityState.Modified;
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