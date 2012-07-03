using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcApplicationForMobile.Models
{
    public class Address : Stamp
    {
        public int AddressID {get; set;}

        public int UserID {get; set;}

        [Required(ErrorMessage = "First line of address is required.")]
        [Display(Name = "Address Line 1")]
        [MaxLength(50, ErrorMessage = "Address Line cannot be longer than 50 characters.")]
        public string Line1 {get; set;}

        [Display(Name = "Address Line 2")]
        [MaxLength(50, ErrorMessage = "Address Line cannot be longer than 50 characters.")]
        public string Line2 {get; set;}

        [Required(ErrorMessage = "Post Code is required.")]
        [Display(Name = "Post Code")]
        [MaxLength(10, ErrorMessage = "Post Code cannot be longer than 10 characters.")]
        public string PostCode {get; set;}

        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(50, ErrorMessage = "Country cannot be longer than 10 characters.")]
        public string Country {get; set;}

        public virtual User User { get; set; }
    }
}