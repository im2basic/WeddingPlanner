using System.ComponentModel.DataAnnotations;
using System;

namespace WeddingPlanner.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId {get;set;}

        //Foreign Keys
        public int UserId {get;set;}
        public int WeddingId {get;set;}

        //Navagational Properties
        public User Guest {get;set;}
        public Wedding Attending {get;set;}


    }
}