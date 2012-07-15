using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcApplicationForMobile.Models
{
    public class User : Stamp
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [DataAnnotationsExtensions.Email]
        public string Email { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

       
    }
}