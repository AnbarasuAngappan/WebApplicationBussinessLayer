using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessLayer;

namespace WebApplicationBussinessLayer.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmployeeBussinessLayer employeeBussinessLayer = new EmployeeBussinessLayer();
        public ActionResult Index()
        {
            
            List<Employee> employees = employeeBussinessLayer.Employees.ToList();                       
            return View(employees);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost()//[Bind(Include = "EmployeeID,EmployeeName,EmployeeAge,EmployeeGender,EmployeeCity,EmpDepartmentID,DepartmentID")] Employee employee
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee employee = new Employee();
                    TryUpdateModel<Employee>(employee); //UpdateModel<Employee>(employee); -> this Throws the Exception when the validation fails(UpdateViewModel)
                    //This Function inspects all the HttpRequest such as posted form data, 
                    //querry string, cookies and server variables and populate in the Employee Object.
                    employeeBussinessLayer.AddEmployee(employee);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

    }
}