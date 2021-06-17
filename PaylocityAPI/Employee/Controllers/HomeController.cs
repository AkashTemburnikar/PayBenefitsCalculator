using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Employee.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Employees> empList = new List<Employees>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44343/api/Employee"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    empList = JsonConvert.DeserializeObject<List<Employees>>(apiResponse);
                }
            }
            if (empList != null)
            {
                return View(empList);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ViewResult GetEmployee() => View();

        [HttpPost]
        public async Task<IActionResult> GetEmployee(int id)
        {
            Employees emp = new Employees();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44343/api/Employee/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employees>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            if (emp != null)
            {
                return View(emp);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ViewResult AddEmployee() => View();

        [HttpPost]
        public async Task<IActionResult> AddEmployee(IFormCollection formCollection, Employees emp1)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employees();
                emp = AddDependents(formCollection, emp);

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44343/api/Employee", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (apiResponse == null)
                        {
                            return RedirectToAction("Error");
                        }
                        TempData["StatusCode"] = apiResponse;
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> UpdateEmployee(int id)
        {
            Employees emp = new Employees();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44343/api/Employee/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    emp = JsonConvert.DeserializeObject<Employees>(apiResponse);
                }
            }
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(IFormCollection formCollection, Employees emp1)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employees();
                emp = AddDependents(formCollection, emp);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync("https://localhost:44343/api/Employee/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (apiResponse == null)
                        {
                            return RedirectToAction("Error");
                        }
                        TempData["StatusCode"] = apiResponse;
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44343/api/Employee/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == null)
                    {
                        return RedirectToAction("Error");
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public Employees AddDependents(IFormCollection formCollection, Employees emp)
        {
            if (Convert.ToInt32(formCollection["EmployeeId"]) > 0)
            {
                emp.EmployeeId = Convert.ToInt32(formCollection["EmployeeId"]);
            }
            emp.FirstName = formCollection["FirstName"];
            emp.LastName = formCollection["LastName"];
            emp.EmailID = formCollection["EmailID"];
            emp.childDependents = Convert.ToInt32(formCollection["ChildDependent"]);
            emp.spouseDependents = Convert.ToInt32(formCollection["SpouseDependent"]);

            List<Dependents> dList = new List<Dependents>();

            foreach (string key in formCollection.Keys)
            {
                if (key.Contains("ChildName"))
                {
                    var dChildObj = new Dependents();
                    dChildObj.Name = formCollection[key];
                    dChildObj.DepTypeId = (int)DepConstants.Child;
                    dList.Add(dChildObj);
                }
                if (key.Contains("SpouseName"))
                {
                    var dSpouseObj = new Dependents();
                    dSpouseObj.Name = formCollection[key];
                    dSpouseObj.DepTypeId = (int)DepConstants.Spouse;
                    dList.Add(dSpouseObj);
                }
            }
            emp.Dependents = dList;
            return emp;
        } 
    }
}
