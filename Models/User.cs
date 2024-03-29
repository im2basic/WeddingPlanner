using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId{get;set;}
        [Required (ErrorMessage="Please enter a first name")]
        [Display(Name="First Name")]
        public string FirstName {get;set;}
        [Required(ErrorMessage="Please enter a Last name")]
        [Display(Name="Last Name")]
        public string LastName {get;set;}
        [Required (ErrorMessage="Please enter a Email")]
        [EmailAddress]
        public string Email {get;set;}
        [Required(ErrorMessage="Please enter a password")]
        [DataType(DataType.Password)]
        [PasswordVali]
        public string Password {get;set;}
        [Required]
        [DataType(DataType.Password)]
        //NOTMAPPED = MYSQL WILL SKIP AND NOT PUT INTO DB
        [NotMapped]
        [Compare("Password")]
        [Display(Name="Confirm Password")]
        [PasswordVali]
        public string ComparePassword {get;set;}
        //________________________________________________________________________
        //Navagational
        public List<Wedding> CreatedWeddings {get;set;}
        public List<Reservation> Weddings {get;set;}
        //_______________________________________________________________________
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
    public class PasswordValiAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool validPassword = false;
            string Error = String.Empty;
            string Password = value == null ? String.Empty : value.ToString();
            if (String.IsNullOrEmpty(Password) || Password.Length < 8)
            {
                return new ValidationResult("Your new password must be at least 8 characters long.");
            }
            else
            {
                Regex reSymbol = new Regex("[^a-zA-Z0-9]");
                if (!reSymbol.IsMatch(Password))
                {
                    Error += "Your new password must contain at least 1 symbol character.";
                }
                else
                {
                    validPassword = true;
                }
            }
            if (validPassword)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(Error);
            }
        }
    }
}