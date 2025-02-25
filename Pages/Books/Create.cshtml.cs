﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab2.Data;
using Lab2.Models;

namespace Lab2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Lab2.Data.Lab2Context _context;

        public CreateModel(Lab2.Data.Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");
            var authors = _context.Author
            .Select(a => new {
                AuthorID = a.AuthorID,
                FullName = a.FirstName + " " + a.LastName
            })
            .ToList();

            ViewData["AuthorID"] = new SelectList(authors, "AuthorID", "FullName");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
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