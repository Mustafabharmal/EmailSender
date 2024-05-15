using Assignment_3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Assignment_3.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private DegreeEntities db = new DegreeEntities();

        // CREATE (Insert)
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                // Create a new t_Employee instance and map properties from employeeModel
                var employee = new t_Employee
                {
                    EmpName = employeeModel.EmpName,
                    Age = employeeModel.Age
                };

                db.t_Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeModel);
        }


        // READ (View)
        public ActionResult Index()
        {
            var employees = db.t_Employee.Select(e => new EmployeeModel
            {
                EmployeeId = e.EmployeeId,
                EmpName = e.EmpName,
                Age = (int)(e.Age)
            }).ToList();

            return View(employees);
        }


        // UPDATE (Edit)
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            t_Employee employee = db.t_Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            // Create an EmployeeModel and map properties from t_Employee
            var employeeModel = new EmployeeModel
            {
                EmployeeId = employee?.EmployeeId ?? 0, // Convert nullable int to int with a default value of 0
                EmpName = employee?.EmpName,
                Age = (int)(employee?.Age)
            };


            return View(employeeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                // Find the corresponding t_Employee entity
                var employee = db.t_Employee.Find(employeeModel.EmployeeId);

                if (employee != null)
                {
                    // Update properties of the t_Employee entity
                    employee.EmpName = employeeModel.EmpName;
                    employee.Age = employeeModel.Age;

                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(employeeModel);
        }

        // DELETE (Delete)
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            t_Employee employee = db.t_Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            // Create an EmployeeModel and map properties from t_Employee
            var employeeModel = new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                EmpName = employee.EmpName,
                Age = (int)(employee?.Age)
            };

            return View(employeeModel);
        }

        // DELETE action for employees
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            t_Employee employee = db.t_Employee.Find(id);
            if (employee != null)
            {
                // First, delete associated salary records in t_Salary
                var salaryRecords = db.t_Salary.Where(s => s.EmpId == id);
                db.t_Salary.RemoveRange(salaryRecords);

                // Then, delete the employee
                db.t_Employee.Remove(employee);

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // VIEW salary records for an employee
        public ActionResult Salary(int id)
        {
            t_Employee employee = db.t_Employee.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            var salaryRecords = db.t_Salary.Where(s => s.EmpId == id).ToList();

            ViewBag.Employee = employee;
            return View(salaryRecords);
        }

        // ADD salary record for an employee
        public ActionResult AddSalary(int empId)
        {
            t_Employee employee = db.t_Employee.Find(empId);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var newSalaryRecord = new t_Salary { EmpId = empId, MonthName = "Jan", Salary = (decimal?)50000.0 }; // Set default values

            ViewBag.Employee = employee;
            return View(newSalaryRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSalary(t_Salary salaryRecord)
        {
            if (ModelState.IsValid)
            {
                db.t_Salary.Add(salaryRecord);
                db.SaveChanges();
                return RedirectToAction("Salary", new { id = salaryRecord.EmpId });
            }

            ViewBag.Employee = db.t_Employee.Find(salaryRecord.EmpId);
            return View(salaryRecord);
        }

        // EDIT salary record for an employee
        public ActionResult EditSalary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            t_Salary salaryRecord = db.t_Salary.Find(id);
            if (salaryRecord == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee = db.t_Employee.Find(salaryRecord.EmpId);
            return View(salaryRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSalary(t_Salary salaryRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salaryRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Salary", new { id = salaryRecord.EmpId });
            }

            ViewBag.Employee = db.t_Employee.Find(salaryRecord.EmpId);
            return View(salaryRecord);
        }


        // DELETE salary record for an employee
        public ActionResult DeleteSalary(int id)
        {
            t_Salary salaryRecord = db.t_Salary.Find(id);
            if (salaryRecord == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee = db.t_Employee.Find(salaryRecord.EmpId);
            return View(salaryRecord);
        }

        [HttpPost, ActionName("DeleteSalary")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSalaryConfirmed(int id)
        {
            t_Salary salaryRecord = db.t_Salary.Find(id);
            if (salaryRecord != null)
            {
                db.t_Salary.Remove(salaryRecord);
                db.SaveChanges();
            }

            return RedirectToAction("Salary", new { id = salaryRecord.EmpId });
        }


    }
}