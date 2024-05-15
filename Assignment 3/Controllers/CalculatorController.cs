using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_3.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Calculate(string expression)
        {
            double result = 0;

            // Replace any potential security risks in the expression
            string cleanedExpression = new DataTable().Compute(expression, null).ToString();

            if (double.TryParse(cleanedExpression, out result))
            {
                ViewBag.Result = result;
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid input or expression. Please enter a valid mathematical expression.";
            }

            return View("Index");
        }


        //public ActionResult Calculate(string firstNumber, string secondNumber, string operation)
        //{
        //    double result = 0;
        //    double num1, num2;

        //    if (double.TryParse(firstNumber, out num1) && double.TryParse(secondNumber, out num2))
        //    {
        //        switch (operation)
        //        {
        //            case "Add":
        //                result = num1 + num2;
        //                break;
        //            case "Subtract":
        //                result = num1 - num2;
        //                break;
        //            case "Multiply":
        //                result = num1 * num2;
        //                break;
        //            case "Divide":
        //                if (num2 != 0)
        //                {
        //                    result = num1 / num2;
        //                }
        //                else
        //                {
        //                    ViewBag.ErrorMessage = "Cannot divide by zero!";
        //                    return View("Index");
        //                }
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Invalid input. Please enter valid numbers.";
        //        return View("Index");
        //    }

        //    ViewBag.Result = result;
        //    return View("Index");
        //}
    }
}