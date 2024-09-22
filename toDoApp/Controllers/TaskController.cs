using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using toDoApp.Models;

namespace toDoApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly TodoappdbContext _context;

        public TaskController(TodoappdbContext context)
        {
            _context = context;
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var task = await _context.Tasks
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var task = await _context.Tasks
                .Include(d => d.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);
            ////Console.Write(task);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Task/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Tags"] = await _context.Tags.ToListAsync();
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Task1,Isfinish")] Models.Task task, string[] tagOfTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                if (tagOfTask != null)
                {
                    foreach(var tag in tagOfTask)
                    {
                        Tag t = _context.Tags.Find(long.Parse(tag));
                        task.Tags.Add(t);
                        //Console.WriteLine(task.Id);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["Tags"] = await _context.Tags.ToListAsync();
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Task1,Isfinish")] Models.Task task ,string[] tagOfTask)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // query task in database.
                    Models.Task taskCurrent = await _context.Tasks.Include(t => t.Tags).FirstOrDefaultAsync(t => t.Id == id);
                    // delete all tags of task queried.
                    taskCurrent.Tags.Clear();

                    taskCurrent.Title = task.Title;
                    taskCurrent.Task1 = task.Task1;
                    taskCurrent.Isfinish = task.Isfinish;


                    if (tagOfTask != null)
                    {
                        foreach (var tag in tagOfTask)
                        {
                            Tag t = _context.Tags.Find(long.Parse(tag));
                            taskCurrent.Tags.Add(t);
                            //Console.WriteLine(task.Id);
                        }
                    }
                    _context.Update(taskCurrent);
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
            return View(task);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(long id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
