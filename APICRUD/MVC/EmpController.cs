using APICRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace APICRUD.MVC
{
    public class EmpController : Controller
    {
        // GET: Emp
        public ActionResult Index()
        {
            IEnumerable<APIemp> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61273/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Emp");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<APIemp>>();
                    readTask.Wait();

                    employees = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    employees = Enumerable.Empty<APIemp>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employees);
        }

        public ActionResult CreateEmp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmp(APIemp emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61273/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("Emp", emp);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(emp);
        }
        public ActionResult Edit(int id)
        {
            APIemp employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61273/");
                //HTTP GET
                var responseTask = client.GetAsync("GetById?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<APIemp>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(APIemp emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61273/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync("update", emp);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(emp);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61273/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Emp/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }


    }
}