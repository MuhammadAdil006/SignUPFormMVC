using Microsoft.AspNetCore.Mvc;
using Sign_Up_Form.Models;
using System.Diagnostics;

namespace Sign_Up_Form.Controllers
{
    public class HomeController : Controller
    {
        UserMangemet usrmng;
        private readonly ILogger<HomeController> _logger;
        //reverse scenario db to model to view to controller
       //public ViewResult getData(User a)
       // {
       //     //User c=  usrmng.GetUser(a);
       //     //  //ViewBag.bag = c;
       //     //  //ViewBag.title = "this post title";
       //     //  return View("NewsFeed",c);//this will send modal to newsfeed
       //     return null;
       // }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            usrmng = new UserMangemet();
        }
        
        //Sign in form
        [HttpPost]
        public ViewResult SignIn(User u)
        {

            int id = usrmng.CheckCredentials(u);
            if(id==-1)
            {
                //credentials not matched
                
                User temp=new User();
                temp.id = -1;
                
                return View("Index",temp);
            }
            else
            {
                usrmng.GetUsers();//this will fill the list wiht user
                //ViewBag.curUser = SignInUser;
                return View("Table",UserMangemet.u);
            }
        }
        //sign up form 1st time getting
        [HttpGet]
        public IActionResult SignUp()
        {
            //after sign up again to index
            User temp = new User();
            temp.id = -1;

            return View("Index", temp);
        }
        //sign up form getting user data
        [HttpPost]

        //WITHOUT BINDING manual

        //public IActionResult SignUp(string firstname, string lastname, string email, string New_password, string gender)
        //{
        //    //get the information from form

        //    User u = new User();
        //    u.firstName = firstname;
        //    Console.WriteLine(u.firstName);
        //    u.LastName = lastname;
        //    u.Email = email;
        //    u.Password = New_password;
        //    u.gender = gender;
        //    //DB CODE HERE 

        //    a.addUser(u);
        //    return View();
        //}
        //modal binding
        [HttpPost]
        public ViewResult SignUp(User u)
        {
            u.date =Convert.ToDateTime( usrmng.MakeDate(u));
            if (ModelState.IsValid)
            {
                //got the information automatically without writing

                //User u = new User();
                //u.firstName = firstname;
                //Console.WriteLine(u.firstName);
                //u.LastName = lastname;
                //u.Email = email;
                //u.Password = New_password;
                //u.gender = gender;
                //DB CODE HERE 

                //if adding true then added successfuly else email already exits

                bool adding = usrmng.addUser(u);
                User temp = new User();
                temp.firstname = "";
                temp.New_password = "";
                temp.id = -2;
                //further display message
                return View("Index",temp);
            }
            else
            {
                User temp = new User();
                temp.firstname = "";
                temp.New_password = "";
                temp.id = -2;
                return View("Index",temp);
            }
            
        }
        public IActionResult Index()
        {
            //after sign up again to index
            User temp = new User();
            temp.firstname = "";
            temp.New_password = "";
            temp.id = -1;

            return View("Index", temp);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}