using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using ExamThree.Models;

namespace ExamThree.Models
{
    public class ViewModel
    {
        public User Username {get;set;}
        public int SessionData {get;set;}
        public User User {get;set;}
        public int UserId { get; set; }
        public int SessionId { get; set; }
        
        public int PasId { get; set; }
        public string PasName {get;set;}
        public string PasContent {get;set;}
        public User Creator {get;set;}
        public List<User> AllUsers {get;set;}
        public List<Passion> AllPassions {get;set;}
        public List<Fan> AllFans {get;set;}

    }
}