using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        [ValidateNever]
        public Book Book { get; set; }
        [ValidateNever]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        //public ApplicationUser ApplicationUser { get; set; }
        public int Count { get; set; }
    }
}
