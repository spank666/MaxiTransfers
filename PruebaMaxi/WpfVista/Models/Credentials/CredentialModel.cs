using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using WpfVista.NotificationProperty;

namespace WpfVista.Models.Credentials
{
    public class CredentialModel : NotifyPropertyBase, IDataErrorInfo
    {
        #region Private
        private string _User;
        private string _Password;
        #endregion

        #region Public
        public string User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        #endregion

        [JsonIgnore]
        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;

                switch (columnName)
                {
                    case "User":
                        if (string.IsNullOrWhiteSpace(User) || User.Length < 1)
                        {
                            result = "El nombre es requerido";
                        }
                        break;
                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password) || Password.Length < 1)
                        {
                            result = "El apellido es requerido";
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
    }
}
