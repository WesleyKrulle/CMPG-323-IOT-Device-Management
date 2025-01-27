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
    public class DevicesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IDeviceRepository    _devicesRepsoitory;
        private readonly IZoneRepository      _zoneRepository;

        public DevicesController(ICategoriesRepository categoriesRepository , IDeviceRepository devicesRepsoitory, IZoneRepository zoneRepository)
        {
            _categoriesRepository = categoriesRepository;
            _devicesRepsoitory = devicesRepsoitory;
            _zoneRepository = zoneRepository;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        { 
            return View(_categoriesRepository.GetAll());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _devicesRepsoitory.GetById(id);
               
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoriesRepository.GetAll(), "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_zoneRepository.GetAll(), "ZoneId", "ZoneName");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();

            _devicesRepsoitory.Add(device);
            _devicesRepsoitory.Save();

            return RedirectToAction(nameof(Index));


        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById(id);

            if (zone == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_categoriesRepository.GetAll(), "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_zoneRepository.GetAll(), "ZoneId", "ZoneName");
            return View(zone);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }
            try
            {
                _devicesRepsoitory.Update(device);
                _devicesRepsoitory.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
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

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = _devicesRepsoitory.GetById(id);
                
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var device = _devicesRepsoitory.GetById(id);
            _devicesRepsoitory.Remove(device);
            _devicesRepsoitory.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(Guid id)
        {
            Device device = _devicesRepsoitory.GetById(id);

            if (device != null )
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
