using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Bookstore.Solution.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            int[] BookCategory;
        }
        [Key]
        public Int64 Id { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        public virtual int[] BookCategory { get; set; }
    }

    public class CategoryDBContext : DbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }
    }
}
