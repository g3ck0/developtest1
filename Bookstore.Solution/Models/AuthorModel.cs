using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Bookstore.Solution.Models
{
    public class AuthorModel
    {
        public AuthorModel()
        {
            int[] BookAuthor;
        }
        [Key]
        public Int64 Id { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        public int[] BookAuthor { get; set; }
    }

    public class AuthorDBContext : DbContext
    {
        public DbSet<AuthorModel> Authors { get; set; }
    }
}
