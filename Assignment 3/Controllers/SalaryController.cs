using Assignment_3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Assignment_3.Controllers
{
    public class SalaryController : Controller
    {
        // GET: Salary
        public ActionResult CalculateSalary()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CalculateSalary(EmployeeSalaryModel model)
        {
            if (ModelState.IsValid)
            {
                double dailySalary = model.FixedSalary / 30;
                double hourlyRate = model.FixedSalary / (30 * 8);

                model.BasicSalary = dailySalary * model.PresentDays;
                model.LeaveDeduction = dailySalary * model.LeaveDays;
                model.OvertimeEarnings = hourlyRate * model.OvertimeHours;

                model.TotalSalary = model.BasicSalary - model.LeaveDeduction + model.OvertimeEarnings;

                return View("Result", model);
            }

            return View(model);
        }

        public ActionResult DownloadExcel(EmployeeSalaryModel model)
        {
            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Employee Salary");

                // Define the column headers
                worksheet.Cells["A1"].Value = "Basic Salary";
                worksheet.Cells["B1"].Value = "Leave Deduction";
                worksheet.Cells["C1"].Value = "Overtime Earnings";
                worksheet.Cells["D1"].Value = "Total Salary";

                // Populate the data
                worksheet.Cells["A2"].Value = model.BasicSalary;
                worksheet.Cells["B2"].Value = model.LeaveDeduction;
                worksheet.Cells["C2"].Value = model.OvertimeEarnings;
                worksheet.Cells["D2"].Value = model.TotalSalary;

                // Apply styles (optional)
                worksheet.Cells["A1:D1"].Style.Font.Bold = true;
                worksheet.Cells["A1:D2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                // Save the Excel package to a memory stream
                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);

                // Set the response headers for the file download
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=EmployeeSalary.xlsx");

                // Write the Excel file to the response
                memoryStream.WriteTo(Response.OutputStream);

                // Close and flush the response
                Response.Flush();
                Response.End();
            }

            return View("CalculateSalary", model);
        }

    }
}