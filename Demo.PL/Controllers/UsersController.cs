﻿using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Demo.PL.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.PL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                return View(userManager.Users);
            }
            else
            {
                var user = await userManager.FindByEmailAsync(SearchValue);
                return View(new List<ApplicationUser>() { user});
            }

        }



        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var User = await userManager.FindByIdAsync(id);
            if (User == null)
                return NotFound();
            return View(ViewName, User);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUser UpdatedUser)
        {
            if (id != UpdatedUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    user.UserName = UpdatedUser.UserName;
                    user.NormalizedUserName = UpdatedUser.UserName.ToUpper();
                    user.PhoneNumber = UpdatedUser.PhoneNumber;

                    var result = await userManager.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    throw;
                }
            }           
            return View(UpdatedUser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, ApplicationUser deletedUser)
        {
            if (id != deletedUser.Id)
                return BadRequest();
            try
            {

                var user = await userManager.FindByIdAsync(id);
                var result = await userManager.DeleteAsync(user);
                if(result.Succeeded)
                   return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(deletedUser);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
