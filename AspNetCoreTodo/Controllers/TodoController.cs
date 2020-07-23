using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

    namespace AspNetCoreTodo.Controllers
    {   
       [Authorize]
        public class TodoController : Controller
        {
            private readonly ITodoItemService _todoItemService;
            private readonly UserManager<ApplicationUser> _userManager;
            public TodoController(ITodoItemService todoItemService , UserManager<ApplicationUser> userManager)
            {
                _todoItemService = todoItemService;
                _userManager = userManager;
            }

            public async Task<IActionResult> Index()
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Challenge();
                } 

                var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

                var model = new TodoViewModel()
                {
                     Items = items
                };
                return View(model);
            // Get to-do items from database
            // Put items into a model
            // Render view using the model
            }

            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddItem(TodoItem newItem){
                Console.WriteLine ("entrei no AddItem");

                if(!ModelState.IsValid){
                    Console.WriteLine ("entrei no AddItem invalido");
                    return RedirectToAction("Index");
                }

               

                 var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoItemService.AddItemAsync(newItem, currentUser);

                if (!successful)
                {
                    return BadRequest("Could not add item.");
                }
                return RedirectToAction("Index");
            }

            [ValidateAntiForgeryToken]
            public async Task<IActionResult> MarkDone(Guid id){
                
                Console.WriteLine ("entrei no markdone");

                if (id == Guid.Empty)
                {
                    return RedirectToAction("Index");
                }
                
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Index");                    
                }

                var successful = await _todoItemService.MarkDoneAsync(id,currentUser);

                if (!successful)
                {
                    return BadRequest("Could not mark item as done.");
                }
                return RedirectToAction("Index");
            }

        }
    }
