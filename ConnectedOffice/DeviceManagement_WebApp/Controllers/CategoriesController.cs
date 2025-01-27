﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        
        //private readonly ConnectedOfficeContext _context;
        private readonly ICategoriesRepository  _categoriesRepository;
       

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
           _categoriesRepository = categoriesRepository;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
           
            return View(_categoriesRepository.GetAll());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _categoriesRepository.Add(category);
            _categoriesRepository.Save();

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            try
            {
                _categoriesRepository.Update(category);
                _categoriesRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        [HttpPost,ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriesRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = _categoriesRepository.GetById(id);
            _categoriesRepository.Remove(category);
            _categoriesRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(Guid id)
        {
            Category category = _categoriesRepository.GetById(id);
            
            if ( category != null)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }
    }
}
