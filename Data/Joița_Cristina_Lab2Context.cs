using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Joița_Cristina_Lab2.Models;

namespace Joița_Cristina_Lab2.Data
{
    public class Joița_Cristina_Lab2Context : DbContext
    {
        public Joița_Cristina_Lab2Context (DbContextOptions<Joița_Cristina_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Joița_Cristina_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Joița_Cristina_Lab2.Models.Publisher> Publisher { get; set; }

        public DbSet<Joița_Cristina_Lab2.Models.Author> Author { get; set; }

        public DbSet<Joița_Cristina_Lab2.Models.Category> Category { get; set; }
    }
}
