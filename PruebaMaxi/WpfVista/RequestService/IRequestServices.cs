using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfVista.Models;

namespace WpfVista.RequestService
{
    public interface IRequestServices
    {
        Task RequestMethod(
            string Uri,
            object s,
            HttpMethod verb,
            Action<ResponseModel> success,
            Action error
            );

        Task RequestMethod(
            string Uri,
            HttpMethod verb,
            Action<ResponseModel> success,
            Action error
            );
    }
}
