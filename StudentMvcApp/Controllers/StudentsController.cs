using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentClassLibrary;
using StudentMvcApp.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMvcApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentMvcDbContext _context;
        public StudentsController(StudentMvcDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.Students.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.ID == id);

            return student == null ? NotFound() : View(student);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Age,EmailAddress")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.FindAsync(id);
            return student == null ? NotFound() : View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Age,EmailAddress")] Student student)
        {
            if (id != student.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(e => e.ID == student.ID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var student = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            return student == null ? NotFound() : View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null) _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
