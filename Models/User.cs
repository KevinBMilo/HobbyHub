using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace ExamThree.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [Display(Name = " ")]
        [MinLength(2, ErrorMessage="The first name should be longer than 2 characters.")]
        public string FirstName {get;set;}

        [Required]
        [Display(Name = " ")]
        [MinLength(2, ErrorMessage="The last name should be longer than 2 characters.")]
        public string LastName {get;set;}

        [Required]
        [Display(Name = " ")]
        [MinLength(3, ErrorMessage="The username should be longer than 3 characters.")]
        [MaxLength(15, ErrorMessage="The username cannot be longer than 15 characters.")]
        public string Username {get;set;}

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = " ")]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}
        
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // Will not be mapped to your users table!
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public List<Passion> CreatedPassions {get;set;}
        public List<Fan> Fans {get;set;}
    }

    public class LoginUser
    {
        [Required]
        [Display(Name = " ")]
        [MinLength(3, ErrorMessage="The username should be longer than 3 characters.")]
        [MaxLength(16, ErrorMessage="The username cannot be longer than 15 characters.")]
        public string LoginUsername {get; set;}
        [Required]
        [Display(Name = " ")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string LoginPassword { get; set; }
    }

    public class Passion
    {
        [Key]
        public int PasId {get;set;}

        [Required]
        [Display(Name = " ")]
        [MinLength(5, ErrorMessage="The hobby name should be longer than 5 characters.")]
        public string PasName {get;set;}

        [Required]
        [Display(Name = " ")]
        [MinLength(5, ErrorMessage="The description should be longer than 5 characters.")]
        public string PasContent {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public int UserId { get; set; }
        public User Creator {get;set;}
        public List<Fan> Fans {get;set;}
        public Passion()
                {
                    Fans = new List<Fan>();
                    CreatedAt = DateTime.Now;
                    UpdatedAt = DateTime.Now;
                }
    }
    public class Fan
    {
        [Key]
        public int FanId {get;set;}
        public int UserId {get;set;}
        public int PasId {get;set;}
        public string ProLevel {get;set;}
        public User User {get;set;}
        public Passion Passion {get;set;}

        
    }


}