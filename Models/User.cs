using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public string Password {get;set;}
        [Required]
        [DataType(DataType.Password)]
        //NOTMAPPED = MYSQL WILL SKIP AND NOT PUT INTO DB
        [NotMapped]
        [Compare("Password")]
        [Display(Name="Confirm Password")]
        public string ComparePassword {get;set;}
        //________________________________________________________________________
        //Navagational
        public List<Wedding> CreatedWeddings {get;set;}
        public List<Reservation> Weddings {get;set;}
        //_______________________________________________________________________
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}