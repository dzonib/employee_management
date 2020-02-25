using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{

    //The controller route template is Not combined with action method route template
    //    if the route tempalte of the action method begins with / or ~/

    //parent route, all other child routes start from here
    //[Route("Home")]
    //can use token like this (like default behaviour)
    //[Route("[controller]")]

    //now we dont need action in method routes
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            // can do it like this, it creates tight cuppling error prone
            //_employeeRepository = new MockEmployeeRepository();
            _employeeRepository = employeeRepository;
        }


        //if we navigate to any of the following routes, execute Index method
        [Route("~/Home")]
        //[Route("index")]
        //[Route("[action]")]
        [Route("~/")]
        public ViewResult Index()
        {   
            //return _employeeRepository.GetEmployee(1).Name;
            var model = _employeeRepository.GetAllEmployees();


            //if the view and the method do not have the same name you can put the path to the wanted view
            //as the first arg
            //return View("~/Views/Home/index.cshtml", model);
            return View(model);
        }

        //getting the parametar in bracket
        //[Route("details/{id?}")]
        //[Route("[action]/{id?}")]
        [Route("{id?}")]
        public ViewResult Details(int? id)
        {
            Employee model = _employeeRepository.GetEmployee(id??1);

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                PageTitle = "Employee Details"
            };

            //ViewData["Employee"] = model;
            //ViewBag.Employee = model;
            //ViewData["PageTitle"] = "Employee Details";
            //ViewBag.PageTitle = "Employee Details";

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Create(Employee employee)
        {
            Employee newEmployee = _employeeRepository.Add(employee);

            return RedirectToAction("details", new { id = newEmployee.Id});
        }
    }
}
