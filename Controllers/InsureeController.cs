﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth," +
            "CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                // calculate age of insuree
                DateTime insureeBday = insuree.DateOfBirth;
                DateTime currentDate = DateTime.Now;
                var insureeAge = currentDate.Year - insureeBday.Year;
                //Console.WriteLine(insureeAge);

                //Calculate Quote
                //age restrictions
                if (insureeAge < 18)
                { insuree.Quote = insuree.Quote + 100; }
                else if (insureeAge >= 18 && insureeAge < 26)
                { insuree.Quote = insuree.Quote + 50; }
                else
                { insuree.Quote = insuree.Quote + 25; }

                //car year restrictions
                if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
                { insuree.Quote = insuree.Quote + 25; }

                //car make restrictions
                if (insuree.CarMake == "Porsche")
                { insuree.Quote = insuree.Quote + 25; }

                if (insuree.CarMake == "Porsche" && insuree.CarModel == "911 Carrera")
                { insuree.Quote = insuree.Quote + 25; }

                //Speeding ticket amount

                if (insuree.SpeedingTickets>0)
                {
                    insuree.Quote = insuree.Quote + (insuree.SpeedingTickets * 10);
                }

                //DUI bool
                if (insuree.DUI = true)
                {
                    insuree.Quote = Decimal.Multiply(insuree.Quote, 1.25m);
                }

                //Coverage type bool
                if (insuree.CoverageType = true)

                { insuree.Quote = Decimal.Multiply(insuree.Quote, 1.5m); }




                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
      

            return View(insuree);
        }

        
        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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
    
        public ActionResult Admin()
        {
            using (InsuranceEntities db = new InsuranceEntities())
            {
                //var person = db.Insuree();
                //var insuree = new List<Insuree>();
              //  foreach (var insuree in Insurees)
                //{
                   // insuree.FirstName;
                    //insuree.LastName;
                    //insuree.EmailAddress;
                    //insuree.Quote;
                    //insurees.Add(insuree);

               // }
         
              


                return View();
            }
        }
    
    }
}
