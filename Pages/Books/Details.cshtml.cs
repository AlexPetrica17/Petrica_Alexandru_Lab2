﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab2.Data;
using Lab2.Models;

namespace Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Lab2.Data.Lab2Context _context;

        public DetailsModel(Lab2.Data.Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public IList<Category> Categories { get; set; } = new List<Category>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            Categories = Book.BookCategories.Select(bc => bc.Category).ToList();

            return Page();
        }
    }
}
