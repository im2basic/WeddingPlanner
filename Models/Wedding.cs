using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WeddingPlanner.Models
{

public class Wedding
    {
        [Key]
        public int WeddingId {get;set;}
        [Required]
        public string WedderOne {get;set;}
        [Required]
        public string WedderTwo {get;set;}
        [Required]
        public string Address {get;set;}
        [Required]
        [FutureDate]
        public DateTime WeddingDate {get;set;} 

        //foreign key
        public int UserId {get;set;}
        //-------------------------------------------------
        //navagational property 
        public User Planner {get;set;}
        public List<Reservation> GuestList {get;set;}
        //-------------------------------------------------
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
    // Custom Validation
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;
            if(value is DateTime)
            {
                date = (DateTime)value;
            }
            else
            {
                return new ValidationResult("Invalid datetime!");
            }
            if(date < DateTime.Now)
            {
                return new ValidationResult("Date must be in the future");
            }
            return ValidationResult.Success;
        }

    }

}