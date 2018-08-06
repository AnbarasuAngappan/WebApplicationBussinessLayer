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

        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";           

            var emp = from e in employeeBussinessLayer.Employees select e;
            switch (sortOrder)
            {
                case "name_desc":
                    emp = emp.OrderByDescending(s => s.EmployeeName);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                emp = emp.Where(s => s.EmployeeName.Contains(searchString)
                                       || s.EmployeeCity.Contains(searchString));
            }
            //List<Employee> employees = employeeBussinessLayer.Employees.ToList();                       
            return View(emp.ToList());
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

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Employee employee = employeeBussinessLayer.Employees.Single(emp => emp.EmployeeID == id);
            return View(employee);
        }

        //[HttpPost, ActionName("Edit")]
        //public ActionResult EditPost(string id)
        //{
        //    try
        //    {
        //        Employee employee = employeeBussinessLayer.Employees.Single(x => x.EmployeeID == id);
        //        if (ModelState.IsValid)
        //        {                    
        //            TryUpdateModel(employee, new string[] { "EmployeeID","EmployeeAge", "EmployeeGender",
        //                                                    "EmployeeCity", "EmpDepartmentID", "DepartmentID" }); //This is the White List and Black List----

        //            employee.EmpDepartmentID = employee.DepartmentID;
        //            employeeBussinessLayer.UpdateEmployee(employee);
        //            return RedirectToAction("Index");
        //        }
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }     
        //}

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(string id)
        {
            try
            {
                Employee employee = employeeBussinessLayer.Employees.Single(x => x.EmployeeID == id);
                employee.EmpDepartmentID = employee.DepartmentID;
                TryUpdateModel<Employee>(employee);
                if (ModelState.IsValid)
                {                 
                    employeeBussinessLayer.UpdateEmployee(employee);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                Employee employee = employeeBussinessLayer.Employees.Single(y => y.EmployeeID == id);
                if(employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            catch (Exception)
            {

                throw;
            }           
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletepost(string ID)
        {
            try
            {
                employeeBussinessLayer.DeleteEmployee(ID);
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}