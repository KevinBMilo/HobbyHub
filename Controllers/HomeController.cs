using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamThree.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ExamThree.Controllers
{
    public class HomeController : Controller
    {
        private int? UserSessionData
        {
            get { return HttpContext.Session.GetInt32("UserId");}
            set { HttpContext.Session.SetInt32("UserId", (int)value);}
        }
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet("edit/{PasId}")]
        public IActionResult Edit(int PasId)
        {
            Passion RetrievedPassion = dbContext.Passions.FirstOrDefault(Passion => Passion.PasId == PasId);
            return View("Update", RetrievedPassion);
        }




        [HttpGet("new")]
        public IActionResult NewAct()
        {
            return View("New");
        }


        [HttpGet("Hobby")]
        public IActionResult GetAll()
        {
        if(UserSessionData != null)
        {
            var SessionId = (int) UserSessionData;
            ViewModel PasGroup = new ViewModel()
            {
                AllPassions = dbContext.Passions.ToList(),
                AllUsers = dbContext.Users.ToList(),
                AllFans = dbContext.Fans.ToList(),
                SessionData = SessionId
            };
            return View("Dashboard", PasGroup);
        }
        ModelState.AddModelError("Name", "You are not logged in.");
        return View("Index");
        }

        [HttpGet("Hobby/{PasId}")]
        public IActionResult ShowPas(int PasId)
        {
            if(UserSessionData != null)
            {
            var HobbyWithPeople = dbContext.Passions
                .Include(passion => passion.Fans)
                .ThenInclude(fa => fa.User)
                .Include(passion => passion.Creator)
                .FirstOrDefault(passion => passion.PasId == PasId);
            User oneUser = dbContext.Users
            .OrderBy(u => u.CreatedAt)
            .FirstOrDefault(User => User.UserId == (int) UserSessionData);
            ViewBag.UserId = oneUser;
            return View("Show", HobbyWithPeople);
            }
            ModelState.AddModelError("Name", "You are not logged in.");
            return View("Index");
        }

        [HttpPost("plan")]
        public IActionResult Plan(Passion newPassion)
        {
            if(ModelState.IsValid)
            {
                Passion submittedPassion = newPassion;
                if(dbContext.Passions.Any(p => p.PasName == newPassion.PasName))
                {
                    ModelState.AddModelError("PasName", "Hobby names must be unique!");
                    return View("New");
                }
                newPassion.UserId = (int) UserSessionData;
                dbContext.Add(newPassion);
                dbContext.SaveChanges();
                return RedirectToAction("GetAll", newPassion);
            }
            return View("New");
        }


        [HttpGet("love/{PasId}")]
        public IActionResult Love(int PasId, Fan newFan)
        {
            Fan subFan = newFan;
            newFan.UserId = (int) UserSessionData;
            newFan.PasId = (int) PasId;
            dbContext.Add(newFan);
            dbContext.SaveChanges();
            return RedirectToAction("GetAll");
        }

        [HttpPost("change/{PasId}")]
        public IActionResult Change(int PasId, Passion formPassion)
        {
            Passion RetrievedPassion = dbContext.Passions.FirstOrDefault(Passion => Passion.PasId == PasId);
            if(ModelState.IsValid)
            {
                RetrievedPassion.PasName = formPassion.PasName;
                RetrievedPassion.PasContent = formPassion.PasContent;
                RetrievedPassion.UserId = RetrievedPassion.UserId;
                RetrievedPassion.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("ShowPas", new {PasId = PasId});
            }
            return View("Update", RetrievedPassion);
        }




        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            User submittedUser = newUser;
            if(dbContext.Users.Any(u => u.Username == newUser.Username))
            {
                ModelState.AddModelError("Username", "That username has been taken.");
                return View("Index");
            }
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                UserSessionData = newUser.UserId;
                return RedirectToAction("GetAll");
            }
            return View("Index");
        }

        [HttpPost("verify")]
        public IActionResult Verify(LoginUser userSubmission)
        {
            LoginUser submittedUser = userSubmission;
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Username == userSubmission.LoginUsername);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Username", "Invalid Username.");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Your current information does not match anything in our database.");
                    return View("Index");
                }
                UserSessionData = userInDb.UserId;
                return RedirectToAction("GetAll");
            }
            return View("Index");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
