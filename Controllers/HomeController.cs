using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zamara.Models;

namespace Zamara.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    string baseUrl="https://dummyjson.com/";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
      public IActionResult SignUp()
    {
        return View();
    }

      public async Task<IActionResult> Posts()
    {
        Console.WriteLine("Posts ");
        DataTable dt=new DataTable();
        using(var client =new HttpClient()){
            client.BaseAddress=new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage getData=await client.GetAsync("posts?limit=8");//
               
            if(getData.IsSuccessStatusCode){
                string results=getData.Content.ReadAsStringAsync().Result;
                Console.WriteLine("results "+results);
                dt=JsonConvert.DeserializeObject<DataTable>(results);
                Console.WriteLine("dt "+dt);

                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(results);
                Console.WriteLine("dataSet "+dataSet);

                DataTable dataTable = dataSet.Tables["posts"];
                Console.WriteLine("dataTable "+dataTable);

Console.WriteLine(dataTable.Rows.Count);
// 2

foreach (DataRow row in dataTable.Rows)
{
    Console.WriteLine(row["id"] );//+ " - " + row["item"]
}
            }else{
Console.WriteLine("Error calling web api");
            }
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
