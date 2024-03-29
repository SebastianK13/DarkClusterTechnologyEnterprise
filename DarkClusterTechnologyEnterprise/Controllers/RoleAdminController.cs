﻿using DarkClusterTechnologyEnterprise.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Controllers
{
    //[Authorize(Roles = "Admins")]
    public class RoleAdminController : Controller
    {

        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleMgr,
            UserManager<IdentityUser> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        public ViewResult Index() => View(roleManager.Roles);

        public ViewResult UserToRole() => View();

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {

            if (ModelState.IsValid)
            {
                IdentityResult result
                    = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Redirect("/Board/MainBoard");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Nie znaleziono roli.");
            }
            return View("Index", roleManager.Roles);
        }

        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<IdentityUser> members = new List<IdentityUser>();
            List<IdentityUser> nonMembers = new List<IdentityUser>();
            foreach (IdentityUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleChangerModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    result = await userManager.AddToRoleAsync(user,
                        model.RoleName);
                    if (!result.Succeeded)
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }

            return View("UserToRole");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

    }
}
