using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVista.Models
{
    public class ResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
