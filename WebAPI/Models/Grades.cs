using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Grades
    {
        public string Nombres { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        private DateTime _fechaNacimiento;

        public int Grado { get; set; }

        public string Grupo { get; set; }

        public float Calificaciones { get; set; }

        private int _edad;

        private string _clave;

        private string Abecedario ="ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";

        public DateTime FechaNacimiento
        {
            get
            {
                return _fechaNacimiento;
            }
            set
            {
                _fechaNacimiento = value;
            }
        }
        public int Edad
        {
            get //Encapsulación nivel abierto, por defecto es publico porque la propiedad es publico.
            {
                return _edad;
            }
            private set //Encapsulación nivel cerrado- Privado
            {
                _edad = value;
            }
        }

        public string Clave
        {
            get //Encapsulación nivel abierto, por defecto es publico porque la propiedad es publico.
            {
                return _clave;
            }
            private set //Encapsulación nivel cerrado- Privado
            {
                _clave = value;
            }
        }
        //Métodos
        public void getAge()
        {
            calculateAge(FechaNacimiento);
        }
        public void calculateAge(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Now;
            Edad = fechaActual.Year - fechaNacimiento.Year;
        }
        public void getKey()
        {
            generateKey(Nombres,ApellidoMaterno);
        }
        public void generateKey(string names, string lastName)
        {
            
            int cName = names.Length;
            int cLastName = lastName.Length;
            string wordsLastName= lastName.Substring((cLastName-2),2).ToUpper();
            string wordsNombre = names.Substring(0,2).ToUpper();
            string firstLetterName = suffleLetter(Abecedario.IndexOf(wordsNombre[0].ToString()));
            string secondLetterName = suffleLetter(Abecedario.IndexOf(wordsNombre[1].ToString()));
            string firstLetterLastName = suffleLetter(Abecedario.IndexOf(wordsLastName[0].ToString()));
            string secondLetterLastName = suffleLetter(Abecedario.IndexOf(wordsLastName[1].ToString()));
            Clave = firstLetterName + secondLetterName + firstLetterLastName + secondLetterLastName + Edad;
         
            
        }

        private string suffleLetter(int letter)
        {
            switch (letter)
            {
                case 0:
                    return Abecedario.Substring((26 - 2), 1);

                case 1:
                    return Abecedario.Substring((25 - 2), 1);
                   
                case 2:
                    return Abecedario.Substring((24 - 2), 1);       
                  
                default:
                    return Abecedario.Substring((letter -3), 1);                   
            }

        }
    }

}
