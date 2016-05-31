﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WorkFlowManager.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkFlowManager.Controllers
{
    public class JobController : Controller
    {
        readonly JobDataContext _dataContext;

        public JobController(JobDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var jobs = _dataContext.Jobs.OrderByDescending(x => x.Id).ToArray();
            return View(jobs);
        }

        [HttpGet]
        public ActionResult Details(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            Job job = _dataContext.Jobs.SingleOrDefault(x => x.Id == Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public class CreateJobRequest
        {
            public string Serial { get; set; }
            public string Name { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            if (!ModelState.IsValid)
            {
                return View(job);
            }

            job.ECMS = job.ECMS;
            job.Name = job.Name;
            job.Make = job.Make;
            job.Model = job.Model;
            job.Type = job.Type;

            _dataContext.Jobs.Add(job);

            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            Job job = _dataContext.Jobs.SingleOrDefault(x => x.Id == Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Job job)
        {
            if (!ModelState.IsValid)
            {
                return View(job);
            }

            job.ECMS = job.ECMS;
            job.Name = job.Name;
            job.Make = job.Make;
            job.Model = job.Model;
            job.Type = job.Type;

            _dataContext.Jobs.Update(job);

            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(long? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            Job job = _dataContext.Jobs.SingleOrDefault(x => x.Id == Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long Id)
        {
            Job job = _dataContext.Jobs.SingleOrDefault(x => x.Id == Id);
            _dataContext.Jobs.Remove(job);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Job(long Id)
        {
            var db = new JobDataContext();
            var job = db.Jobs.SingleOrDefault(x => x.Id == Id);
            return View(job);
        }
    }
}

