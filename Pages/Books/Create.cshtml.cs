using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Joița_Cristina_Lab2.Data;
using Joița_Cristina_Lab2.Models;

namespace Joița_Cristina_Lab2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Joița_Cristina_Lab2.Data.Joița_Cristina_Lab2Context _context;

        public CreateModel(Joița_Cristina_Lab2.Data.Joița_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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

        [BindProperty]
        public Book Book { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
