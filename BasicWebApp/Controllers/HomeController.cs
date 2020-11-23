using BasicWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BasicWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult GeneralDetails()
        {
            //dbcontext
            WebAppEntities empList = new WebAppEntities();
            var data = empList.Employees;

            return View(data);

        }

        // GET: Home
        public ActionResult Details()
        {
            //dbcontext
            WebAppEntities empList = new WebAppEntities();
            var data = empList.Employees;

            return View(data);

        }

        [HttpGet]
        [Route("EditDetails/{Id:string}")]
        [Authorize(Roles = "Admin")]
        public ActionResult EditDetails(string Id)
        {

            WebAppEntities empList = new WebAppEntities();

            var data = empList.Employees.Where(x => x.EmployeeID == Id.ToString()); ;

                if (data != null)
                {
                Employee model = new Employee();

                    foreach (var item in data)
                    {
                        model.FirstName = item.FirstName;
                        model.EmployeeID = item.EmployeeID;
                        model.DOB = item.DOB;
                        model.LastName = item.LastName;
                        model.Department = item.Department;
                    }

                    return View(model);
                }

                return View();
            }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public ActionResult UpdateEmployeeDetails(Employee employee)
        {


            if (!ModelState.IsValid)
            {
                return View("EditDetails", employee);
            }
            else
            {
                using (WebAppEntities empList = new WebAppEntities())
                {
                    // validate employeeID
                    var userDetails = empList.Employees.Where(x => x.EmployeeID == employee.EmployeeID).FirstOrDefault();
                    if (userDetails != null)
                    {
                        userDetails.FirstName = employee.FirstName;
                        userDetails.LastName = employee.LastName;
                        userDetails.Department = employee.Department;
                        userDetails.DOB = employee.DOB;
                        //save changes to db
                        empList.SaveChanges();
                        // redirect to details page
                        return RedirectToAction("Details", "Home");
                    }

                }

            }
            return RedirectToAction("Details", "Home");
        }
    }
}