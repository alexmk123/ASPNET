using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Administrator")).ToArray();

            var everyone = await _userManager.Users.ToArrayAsync();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View(model);
        }
        public async Task<IActionResult> DelUserAsync(ApplicationUser user)
        {
              var user2 = await _userManager.FindByIdAsync(user.Id);
              await _userManager.DeleteAsync(user2);

              if( user!=null)
                 {
                    Console.WriteLine("entrei no if user é true "+user2.Email);
                    return RedirectToAction("Index");;
                 }
            Console.WriteLine("entrei no metodo");
            return  RedirectToAction("Index");;
        
        }
    }
}