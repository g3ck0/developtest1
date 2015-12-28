using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Bookstore.Solution.Models
{
    public class BookModel
    {
        public BookModel()
        {
            int[] BookAuthor ;
            int[] BookCategory ;
        }

        [Key]
        public Int64 Id { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public int[] BookAuthor { get; set; }
        public int[] BookCategory { get; set; }
    }

    public class BookDBContext : DbContext
    {
        public DbSet<BookModel> Books{ get; set; }
    }
    
    
}
