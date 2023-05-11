using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PairSoftMVC.Models;
using PairSoftMVC.Models.ViewModel;
using System.Diagnostics;
using System.Text;

namespace PairSoftMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index()
        {
            List<ToDoList> lst = new List<ToDoList>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7027/api/ToDo/GetToDoList"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<ToDoList>>(apiResponse);
                }
            }
            return View(lst);
        }
        public  ViewResult Search()=>View();
        [HttpPost]
        public async Task<IActionResult> Search(SearchList search)
        {
            List<ToDoList> lst = new List<ToDoList>();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(search), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7027/api/ToDo/SearchList", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        lst = JsonConvert.DeserializeObject<List<ToDoList>>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(lst);
        }
        public ViewResult GetListByID() => View();
        [HttpPost]
        public async Task<IActionResult> GetListByID(int id)
        {
            ToDoList lst = new ToDoList();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7027/api/ToDo/GetListById" + "?" + "id=" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        lst = JsonConvert.DeserializeObject<ToDoList>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(lst);
        }

        public ViewResult InsertList() => View();

        [HttpPost]
        public async Task<IActionResult> InsertList(ToDoList toDoList)
        {
            ToDoList receivedlst = new ToDoList();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(toDoList), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7027/api/ToDo/InsertList", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedlst = JsonConvert.DeserializeObject<ToDoList>(apiResponse);
                }
            }
            return View(receivedlst);
        }

        public async Task<IActionResult> UpdateList(int id)
        {
            ToDoList lst = new ToDoList();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7027/api/ToDo/GetListById" + "?" + "id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<ToDoList>(apiResponse);
                }
            }
            return View(lst);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateList(ToDoList toDoList)
        {
            ToDoList receivedlst = new ToDoList();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(toDoList), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:7027/api/ToDo/Updatelist", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedlst = JsonConvert.DeserializeObject<ToDoList>(apiResponse);
                }
            }
            return View(receivedlst);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteList(int Id)
        {
            ToDoList lst = new ToDoList();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7027/api/ToDo/DeleteList" + "?" + "id=" + Id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        lst = JsonConvert.DeserializeObject<ToDoList>(apiResponse);
                        return RedirectToAction("Index");

                    }

                }
            }
            return RedirectToAction("Index");

        }
        public ViewResult AddRecord() => View();

        [HttpGet]
        public async Task<IActionResult> GetRecord()
        {
			List<ToDoList> lst = new List<ToDoList>();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync("https://localhost:7027/api/ToDo/GetToDoList"))
				{
					string apiResponse = await response.Content.ReadAsStringAsync();
					lst = JsonConvert.DeserializeObject<List<ToDoList>>(apiResponse);
				}
			}
			return View(lst);
		}

        [HttpGet] 
        public async Task<IActionResult> UpdateStatus(int id)
        {
            ToDoList lst = new ToDoList();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7027/api/ToDo/UpdateStatus" + "?" + "id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<ToDoList>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
       
    }
}