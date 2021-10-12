using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Globalization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GradesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult List(List<Grades> grades = null)
        {
            grades = grades == null ? new List<Grades>() : grades;

            return new JsonResult(grades);
        }

        [HttpPost]
        public IActionResult List(IFormFile file)
        {

            //List<Grades> calificaciones = new List<Grades>();
            Students students = new Students();
            Grades grades = new Grades();
            
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                students.calificaciones = new List<Grades>();
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    
                    while (reader.Read())
                    {
                        int count = 0;
                        count++;
                     
                          if (
                            reader.GetValue(0).ToString() != "Nombres" 
                            && reader.GetValue(2).ToString() != "Apellido Paterno"
                            && reader.GetValue(1).ToString() != "Apellido Materno"
                            && reader.GetValue(3).ToString() != "Fecha de Nacimiento"
                            && reader.GetValue(4).ToString() != "Grado"
                            && reader.GetValue(5).ToString() != "Grupo"
                            && reader.GetValue(6).ToString() != "Calificacion"
                            )
                          {
                            grades = new Grades();
                            var stringDate = DateTime.Now.ToString(reader.GetValue(3).ToString());
                            grades.Nombres = reader.GetValue(0).ToString();
                            grades.ApellidoPaterno = reader.GetValue(2).ToString();
                            grades.ApellidoMaterno = reader.GetValue(1).ToString();
                            grades.FechaNacimiento = DateTime.ParseExact(stringDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            grades.Grado = Int32.Parse(reader.GetValue(4).ToString());
                            grades.Grupo = reader.GetValue(5).ToString();
                            grades.Calificaciones = float.Parse(reader.GetValue(6).ToString());
                            grades.getAge();
                            grades.getKey();
                            students.calificaciones.Add(grades);
                            int gradesCount = students.calificaciones.Count;
                            float sumGrades = 0;
                            float betterGrade = 0;
                            float lowerGrande = 0;
                            List<float> betterGradesList = new List<float>();
                            List<float> lowerGradesList = new List<float>();
                            var counter = 0;
                            foreach (var item in students.calificaciones)
                            {
                                sumGrades+= item.Calificaciones;
                                betterGradesList.Add(item.Calificaciones);
                                lowerGradesList.Add(item.Calificaciones);
                                if (counter == 0)
                                {
                                    betterGrade = item.Calificaciones;
                                    lowerGrande = item.Calificaciones;
                                } else
                                {
                                    betterGrade = betterGrade >= item.Calificaciones ? betterGrade : item.Calificaciones;
                                    lowerGrande = lowerGrande <= item.Calificaciones ? lowerGrande : item.Calificaciones;
                                }
                                counter++;

                            }
                            var lowerIndex = lowerGradesList.IndexOf(lowerGrande);
                            var betterIndex = lowerGradesList.IndexOf(betterGrade);
                            students.MejorCalificacion = students.calificaciones[betterIndex].Nombres + " " + students.calificaciones[betterIndex].ApellidoPaterno + " " +students.calificaciones[betterIndex].ApellidoMaterno;
                            students.PeorCalificacion = students.calificaciones[lowerIndex].Nombres + " " +students.calificaciones[lowerIndex].ApellidoPaterno + " "+students.calificaciones[lowerIndex].ApellidoMaterno;
                            students.Promedio = (sumGrades / gradesCount);

                            // students.MejorCalificaci[on = betterGrade.Min().to;
                        }
                        
                    }
       

                    }
                return Ok(students);

            }
        }
    }
}
