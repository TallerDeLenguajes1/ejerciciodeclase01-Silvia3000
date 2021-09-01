using Ejercicios_tp01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Ejercicios_tp01.Entities;

namespace Ejercicios_tp01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Problema1()
        {
            return View();
        }

        private static bool VerificarEntero(string n)
        {
            try
            {
                int i = Convert.ToInt32(n);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static bool VerificaCero(string n)
        {
            try
            {
                return (Convert.ToDecimal(n) == 0)? true : false;
                
            }
            catch (Exception)
            {
                return true;
            }
        }

        private static bool VerificarDecimal(string n)
        {
            try
            {
                decimal d = Convert.ToDecimal(n);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        public string Problema1(string n)
        {
            try
            {
                return (VerificarEntero(n) && !VerificaCero(n)) ? $"El cuadrado de {Convert.ToInt32(n)} es {Math.Pow(Convert.ToInt32(n), 2)}" : "No es un numero valido!!!";
               
            }
            catch (Exception e)
            {
                return $"ERROR: { e.Message}";
               
            }

        }
        public IActionResult Problema2()
        {
            return View();

        }

        [HttpPost]
        public string Problema2(string n1, string n2)
        {
            try
            {
                if (VerificarDecimal(n1) && VerificarDecimal(n2) && !VerificaCero(n2))
                {
                    return $"La division entre {Convert.ToDecimal(n1)} y {Convert.ToDecimal(n2)} es {Math.Round((Convert.ToDecimal(n1) / Convert.ToDecimal(n2)),2)}";
                }
                else
                {
                    return "No es posible la division por cero";
                }
            }
            catch (Exception e)
            {
                return $"ERROR: {e.Message} ";
            }
            
        }
        

        public IActionResult Problema3()
        {
            var url = $"https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre ";
            
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            //string cadena = "";
            List<Provincia> ListaProv;

            try
            {
                using WebResponse response = request.GetResponse();
                using Stream strReader = response.GetResponseStream();
                using StreamReader objReader = new(strReader);
                string respondeBody = objReader.ReadToEnd();
                ListaProv = System.Text.Json.JsonSerializer.Deserialize<ProvinciaArgentina>(respondeBody).Provincias;

            }
            catch (Exception e)
            {
                return View($"Error {e.Message}");
                
            }
            return View(ListaProv);
                
        }
        public IActionResult Problema4()
        {
            return View();

        }

        [HttpPost]
        public string Problema4(string a, string b)
        {
            return Problema2(a,b);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
