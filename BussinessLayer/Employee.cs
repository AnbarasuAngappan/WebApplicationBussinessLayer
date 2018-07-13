using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Employee
    {
        //[Display(Name = "ID :")]
        [Required]
        public string EmployeeID { get; set; }

        [Display(Name = "Employee Name :")]
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Employee Age :")]
        public string EmployeeAge { get; set; }

        [Required]
        [Display(Name = "Employee Gender :")]
        public string EmployeeGender { get; set; }

        [Required]
        [Display(Name = "Employee City :")]
        public string EmployeeCity { get; set; }

        [Required]
        [Display(Name = "Employee Department ID :")]
        public string EmpDepartmentID { get; set; }

        [Required]
        [Display(Name = "Department ID :")]
        public string DepartmentID { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
