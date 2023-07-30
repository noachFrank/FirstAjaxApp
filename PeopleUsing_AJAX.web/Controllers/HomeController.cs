using Microsoft.AspNetCore.Mvc;
using PeopleUsing_AJAX.data;
using PeopleUsing_AJAX.web.Models;
using System.Diagnostics;

namespace PeopleUsing_AJAX.web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=PeopleAndCars;Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPeople()
        {
            var manager = new DbMAnager(_connectionString);
            var people = manager.GetAll();

            return Json(people);
        }

        [HttpPost]
        public void Add(Person p)
        {
            var manager = new DbMAnager(_connectionString);
            manager.AddPerson(p);
        }

        public IActionResult Update(int id)
        {
            var manager = new DbMAnager(_connectionString);

            var p = manager.GetPersonById(id);

            return Json(p);
        }

        [HttpPost]
        public void Update(Person p)
        {
            var manager = new DbMAnager(_connectionString);
            manager.Update(p);
        }

        [HttpPost]
        public void Delete(int id)
        {
            var manager = new DbMAnager(_connectionString);
            manager.Delete(id);
        }
    }
}