using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentClassLibrary;
using StudentMvcApp.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMvcApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly StudentMvcDbContext _context;
        public CoursesController(StudentMvcDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.Courses.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ID == id);

            return course == null ? NotFound() : View(course);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,DepartmentName,LecturerName")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var course = await _context.Courses.FindAsync(id);
            return course == null ? NotFound() : View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,DepartmentName,LecturerName")] Course course)
        {
            if (id != course.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(e => e.ID == course.ID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.ID == id);
            return course == null ? NotFound() : View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null) _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
