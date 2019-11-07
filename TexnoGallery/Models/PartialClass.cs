using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TexnoGallery.Models
{
    [MetadataType(typeof(ApplicationUserMetadata))]
    public partial class ApplicationUser
    {
        public virtual string ConfirmPassword { get; set; }

        private class ApplicationUserMetadata
        {
            [Required]
            public string FirstName { get; set; }
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string UserName { get; set; }

            [Required]
            public string Phone{ get; set; }

            [Required, MinLength(7)]
            public string Password { get; set; }

            [Compare(nameof(Password))]
            public virtual string ConfirmPassword { get; set; }
        }
    }

    public class UserLogin
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}