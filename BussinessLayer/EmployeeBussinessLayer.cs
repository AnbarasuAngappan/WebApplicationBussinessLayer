using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace BussinessLayer
{
   public class EmployeeBussinessLayer
    {
        public IEnumerable<Employee> Employees
        {
            get
            {
                string connectionstring = ConfigurationManager.ConnectionStrings["EmployeeContext"].ConnectionString;
                List<Employee> employeeslist = new List<Employee>();
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("spToGetAllEmployees", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while(sqlDataReader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = Convert.ToString(sqlDataReader["EmployeeID"]);
                        employee.EmployeeName = Convert.ToString(sqlDataReader["EmployeeName"]);
                        employee.EmployeeGender = Convert.ToString(sqlDataReader["EmployeeGender"]);
                        employee.EmployeeAge = Convert.ToString(sqlDataReader["EmployeeAge"]);
                        employee.EmployeeCity = Convert.ToString(sqlDataReader["EmployeeCity"]);
                        employee.DepartmentID = Convert.ToString(sqlDataReader["DepartmentID"]);
                        employeeslist.Add(employee);
                    }
                }

                return employeeslist;
            }

        }

        public void AddEmployee(Employee employee)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["EmployeeContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand sqlCommand = new SqlCommand("spAddemployee", con);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sqlParameterID = new SqlParameter();
                sqlParameterID.ParameterName = "@EmployeeID";
                sqlParameterID.Value = employee.EmployeeID;
                sqlCommand.Parameters.Add(sqlParameterID);

                SqlParameter sqlParameterName = new SqlParameter();
                sqlParameterName.ParameterName = "@EmployeeName";
                sqlParameterName.Value = employee.EmployeeName;
                sqlCommand.Parameters.Add(sqlParameterName);

                SqlParameter sqlParameterAge = new SqlParameter();
                sqlParameterAge.ParameterName = "@EmployeeAge";
                sqlParameterAge.Value = employee.EmployeeAge;
                sqlCommand.Parameters.Add(sqlParameterAge);

                SqlParameter sqlParameterGender = new SqlParameter();
                sqlParameterGender.ParameterName = "@EmployeeGender";
                sqlParameterGender.Value = employee.EmployeeGender;
                sqlCommand.Parameters.Add(sqlParameterGender);

                SqlParameter sqlParameterCity = new SqlParameter();
                sqlParameterCity.ParameterName = "@EmployeeCity";
                sqlParameterCity.Value = employee.EmployeeCity;
                sqlCommand.Parameters.Add(sqlParameterCity);

                SqlParameter sqlParameterEmpDepID = new SqlParameter();
                sqlParameterEmpDepID.ParameterName = "@EmpDepartmentID";
                sqlParameterEmpDepID.Value = employee.EmpDepartmentID;
                sqlCommand.Parameters.Add(sqlParameterEmpDepID);

                SqlParameter sqlParameterDepartment = new SqlParameter();
                sqlParameterDepartment.ParameterName = "@DepartmentID";
                sqlParameterDepartment.Value = employee.DepartmentID;
                sqlCommand.Parameters.Add(sqlParameterDepartment);

                con.Open();
                sqlCommand.ExecuteNonQuery();

            }
        }

        }      
    }

