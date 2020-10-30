using System;
using System.Collections.Generic;
using System.Linq;
using Mvc.Models;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<mvcEmployeeModel> emplist;
            HttpResponseMessage response = GlobalVariables.webapiClient.GetAsync("Employee").Result;
            emplist = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;

            return View(emplist);
        }
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
         
            return View(new mvcEmployeeModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.webapiClient.GetAsync("Employee/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcEmployeeModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(mvcEmployeeModel emp)
        {
            if (emp.EmployeeId == 0)
            { 
            HttpResponseMessage response = GlobalVariables.webapiClient.PostAsJsonAsync("Employee", emp).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.webapiClient.PutAsJsonAsync("Employee/"+emp.EmployeeId, emp).Result;

            }

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.webapiClient.DeleteAsync("Employee/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}