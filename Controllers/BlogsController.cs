using Microsoft.AspNetCore.Mvc;
using Diploma.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    public class BlogsController : Controller
    {
        private readonly DiplomaContext _context;

        public BlogsController(DiplomaContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            var blog = await _context.Blogs.ToListAsync();
            return View(blog);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Blogs.FirstOrDefaultAsync(m => m.ID == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] Blog blogPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(blogPost);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Blogs.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: Blogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] Blog blogPost)
        {
            if (id != blogPost.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.ID))
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

            return View(blogPost);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Blogs.FirstOrDefaultAsync(m => m.ID == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.Blogs.Any(e => e.ID == id);
        }
    }
}

