using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Students
    {
        public List<Grades> calificaciones { get; set; }
        public string MejorCalificacion { get; set; }
        public string PeorCalificacion { get; set; }
        public float Promedio { get; set; }

    }
}
