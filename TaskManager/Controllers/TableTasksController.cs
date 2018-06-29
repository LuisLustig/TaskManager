using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TableTasksController : Controller
    {
        private readonly TasksContext _context;

        public TableTasksController(TasksContext context)
        {
            _context = context;
        }

        // GET: TableTasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.TableTasks.ToListAsync());
        }

        // GET: TableTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableTasks = await _context.TableTasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tableTasks == null)
            {
                return NotFound();
            }

            return View(tableTasks);
        }

        // GET: TableTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TableTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsDone")] TableTasks tableTasks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tableTasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tableTasks);
        }

        // GET: TableTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableTasks = await _context.TableTasks.SingleOrDefaultAsync(m => m.Id == id);
            if (tableTasks == null)
            {
                return NotFound();
            }
            return View(tableTasks);
        }

        public async Task<IActionResult> Edit(int? id, bool? isDone)
        {

            var tableTasks = await _context.TableTasks.SingleOrDefaultAsync(m => m.Id == id);
            if (tableTasks == null)
            {
                return NotFound();
            }
            tableTasks.IsDone = (bool)isDone;
            //return View(tableTasks);

            if (id != tableTasks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tableTasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableTasksExists(tableTasks.Id))
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
            return View(tableTasks);
        }

        // POST: TableTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsDone")] TableTasks tableTasks)
        {
            if (id != tableTasks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tableTasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableTasksExists(tableTasks.Id))
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
            return View(tableTasks);
        }

      

        // GET: TableTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableTasks = await _context.TableTasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tableTasks == null)
            {
                return NotFound();
            }

            return View(tableTasks);
        }

        // POST: TableTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tableTasks = await _context.TableTasks.SingleOrDefaultAsync(m => m.Id == id);
            _context.TableTasks.Remove(tableTasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableTasksExists(int id)
        {
            return _context.TableTasks.Any(e => e.Id == id);
        }
    }
}
