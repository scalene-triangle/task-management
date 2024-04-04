using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using task_management.Data;
using task_management.DTOs.MyTasks;
using task_management.Models;
using task_management.Services;

namespace task_management.Controllers
{
    public class MyTasksController : Controller
    {
        private readonly IMyTaskService _taskService;
        private readonly AppDbContext _context;

        public MyTasksController(AppDbContext context, IMyTaskService taskService)
        {
            _context = context;
            _taskService = taskService;
        }

        // GET: MyTasks
        public async Task<IActionResult> Index()
        {
            return _context.Task != null ?
                          View(await _context.Task.Include(t => t.Project).Include(t => t.Story).ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Task'  is null.");
        }

        // GET: MyTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.Project)
                .Include(t => t.Story)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: MyTasks/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Name");
            ViewData["StoryId"] = new SelectList(_context.Set<Story>(), "Id", "Name");
            return View();
        }

        // POST: MyTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMyTaskDto task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTask(task);
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Name", task.ProjectId);
            ViewData["StoryId"] = new SelectList(_context.Set<Story>(), "Id", "Name", task.StoryId);

            return View(task);
        }

        // GET: MyTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Name", task.ProjectId);
            ViewData["StoryId"] = new SelectList(_context.Set<Story>(), "Id", "Name", task.StoryId);
            return View(task);
        }

        // POST: MyTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,DueDate,estimate,ProjectId,StoryId,Id")] MyTask task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "Name", task.ProjectId);
            ViewData["StoryId"] = new SelectList(_context.Set<Story>(), "Id", "Name", task.StoryId);
            return View(task);
        }

        // GET: MyTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.Project)
                .Include(t => t.Story)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: MyTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Task == null)
            {
                return Problem("Entity set 'AppDbContext.MyTask'  is null.");
            }
            var task = await _context.Task.FindAsync(id);
            if (task != null)
            {
                _context.Task.Remove(task);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
          return (_context.Task?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
