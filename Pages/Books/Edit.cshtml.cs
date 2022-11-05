using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Joița_Cristina_Lab2.Data;
using Joița_Cristina_Lab2.Models;

namespace Joița_Cristina_Lab2.Pages.Books
{
    public class EditModel :  BookCategoriesPageModel
    {
        private readonly Joița_Cristina_Lab2.Data.Joița_Cristina_Lab2Context _context;

        public EditModel(Joița_Cristina_Lab2.Data.Joița_Cristina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book =  await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
            Book = await _context.Book
.Include(b => b.Publisher)
.Include(b=>b.Author)
.Include(b => b.BookCategories).ThenInclude(b => b.Category)
.AsNoTracking()
.FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            PopulateAssignedCategoryData(_context, Book);
            var authorlist = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");
            ViewData["AuthorID"] = new SelectList(authorlist, "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookToUpdate = await _context.Book
            .Include(i => i.Publisher)
            .Include(i=>i.Author)
            .Include(i => i.BookCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            i => i.Title, i => i.AuthorID,
            i => i.Price, i => i.PublishingDate, i => i.Publisher))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }
    }

   /* private bool BookExists(int id)
        {
          return _context.Book.Any(e => e.ID == id);
        }
    }
   */
}
