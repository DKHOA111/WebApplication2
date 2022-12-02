using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        public double Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public int CategoryID { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
