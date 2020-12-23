using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace myWebApp
{
        public class Variable    {
            public string path { get; set; } 
            public string value { get; set; } 
            public string valueType { get; set; } 
    }

    public class Root123    {
        public List<Variable> variables { get; set; } 
    }

    public class Program
    {
        public static void Main(string[] args)
        {

            // API SECTION

            Console.WriteLine("Hello World!");

             var handler = new HttpClientHandler() 
            { 
              ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var handler1 = new HttpClientHandler() 
            { 
              ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            Console.WriteLine("Making API Call...  GETting inputs");
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://192.168.10.10/_pxc_api/api/variables?paths=Arp.Plc.Eclr/wInputs");
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Result: " + result);
            }

            Console.WriteLine("Making API Call...  PUTing outputs");
        
            var test = new Variable();
            test.path = "Arp.Plc.Eclr/wOutputs";
            test.value = "255";
            test.valueType = "Constant";

            List<Variable> testlist = new List<Variable>();
            testlist.Add(test);

            var testobject = new Root123();
            testobject.variables = testlist;

            string body = JsonConvert.SerializeObject(testobject); 
            
            var content = new StringContent(body, Encoding.UTF8, "text/plain");

            Console.WriteLine(body);

            using (var client = new HttpClient(handler1))      
            {         

                client.BaseAddress = new Uri("https://192.168.10.10/_pxc_api/api/variables");      
                var response = client.PutAsync("https://192.168.10.10/_pxc_api/api/variables", content).Result;      
                if (response.IsSuccessStatusCode)    
                {      
                    Console.WriteLine("Success");
                  //  Console.WriteLine(response);      
                }      
                else      
                    Console.WriteLine("Error");
                    Console.WriteLine(response);     

            }

            CreateHostBuilder(args).Build().Run();
        
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });    
    }
}