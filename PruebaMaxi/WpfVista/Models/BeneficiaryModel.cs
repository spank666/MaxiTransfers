using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfVista.NotificationProperty;

namespace WpfVista.Models
{
    public class BeneficiaryModel : NotificationPropertyChange, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime Fecha { get; set; }
        public int Porcentaje { get; set; }
        public string CURP { get; set; }
        public string SSN { get; set; }
        public string Telefono { get; set; }
        public string Nacionalidad { get; set; }
        public int IdEmployee { get; set; }

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;

                switch (columnName)
                {
                    case "Nombres":
                        if (string.IsNullOrWhiteSpace(Nombres) || Nombres.Length < 1)
                        {
                            result = "El nombre es requerido";
                        }
                        break;
                    case "Apellidos":
                        if (string.IsNullOrWhiteSpace(Nombres) || Nombres.Length < 1)
                        {
                            result = "El apellido es requerido";
                        }
                        break;

                    case "Fecha":
                        if (CalculateAge(Fecha) < 18)
                        {
                            result = "El empleado debe tener almenos 18 años";
                        }
                        break;
                    case "CURP":
                        if (string.IsNullOrWhiteSpace(CURP) || CURP.Length < 1)
                        {
                            result = "La curp es requerida";
                        }
                        break;
                    case "SSN":
                        if (string.IsNullOrWhiteSpace(SSN) || SSN.Length < 1)
                        {
                            result = "El numero de seguro social es requerido";
                        }
                        break;
                    case "Telefono":
                        if (string.IsNullOrWhiteSpace(Telefono) || Telefono.Length < 9)
                        {
                            result = "El  telefono debe contener 10 digitos";
                        }
                        break;
                    case "Nacionalidad":
                        if (string.IsNullOrWhiteSpace(Nacionalidad) || Nacionalidad.Length < 1)
                        {
                            result = "La nacionalidad es requerida";
                        }
                        break;
                }

                return result;
            }
        }

        [JsonIgnore]
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                Boolean isValid = true;
                PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo p in properties)
                {
                    if (this[p.Name].Length > 0)
                    {
                        return false;
                    }
                }
                return isValid;
            }
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Today;
            int Age = currentDate.Year - dateOfBirth.Year;
            if (dateOfBirth.Month > currentDate.Month || (dateOfBirth.Month == currentDate.Month && dateOfBirth.Day > currentDate.Day))
            {
                --Age;
            }
            return Age;
        }
    }
}
