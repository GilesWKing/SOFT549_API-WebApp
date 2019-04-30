﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SOFT549_ASD_MIS_DemoWebApp.Models;

namespace SOFT549_ASD_MIS_DemoWebApp.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly GilesContext _context;

        public ActivitiesController(GilesContext context)
        {
            _context = context;
        }

        // Retrieves all records in the Activity table and generates a table of data on Activities Index View.
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetApiCall<List<Activity>>("Activities"));
        }


        // Retrieves a specific activity through use of the id. Displays that data on an individual view.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.GetApiCall<Activity>(string.Concat("Activities", "/", id));
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }


        // GET: Activities/Create
        public IActionResult Create()
        {
            return View();
        }


        // My post method sending data to a model and onto the Post API Call method that will send data to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,ProjectId,ActivityName,StaffId,PredictedStartDate,ActualStartDate,PredictedCompletionDate,ActualCompletionDate,PredictedCost,ActualCost,ActivitySequence")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.PostApiCall<Activity>("Activities", activity);

                return RedirectToAction(nameof(Index));
            }
            return View(activity);
        }


        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.GetApiCall<Activity>(string.Concat("Activities", "/", id));

            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }


        // POST: Activities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityId,ProjectId,ActivityName,StaffId,PredictedStartDate,ActualStartDate,PredictedCompletionDate,ActualCompletionDate,PredictedCost,ActualCost,ActivitySequence")] Activity activity)
        {
            if (id != activity.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var result = await _context.PutApiCall<Activity>(string.Concat("Activities", "/", id), activity);

                return RedirectToAction(nameof(Index));
            }

            return View(activity);
        }


        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.GetApiCall<Activity>(string.Concat("Activities", "/", id));

            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }


        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await _context.DeleteApiCall<Activity>(string.Concat("Activities", "/", id));
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(int id)
        {
            var task = _context.GetApiCall<Activity>(string.Concat("Activities", "/", id)).Result;

            return (task.ActivityId > 0);
        }
    }
}