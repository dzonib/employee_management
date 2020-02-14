using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            // can do it like this, it creates tight cuppling error prone
            //_employeeRepository = new MockEmployeeRepository();
            _employeeRepository = employeeRepository;
        }


        public ViewResult Index()
        {
            //return _employeeRepository.GetEmployee(1).Name;
            var model = _employeeRepository.GetAllEmployees();

            return View(model);
        }

        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);

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
    }
}
