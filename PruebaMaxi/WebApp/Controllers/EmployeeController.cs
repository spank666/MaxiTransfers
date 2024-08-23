using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dtos;
using WebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ResponseDto<List<EmployeeDto>> response = new ResponseDto<List<EmployeeDto>>();
            var list = await employeeService.AllEmployees();
            if(list is not null)
            {
                response.Code = 100;
                response.Message = "Lista de Empleados obtenida correctamente";
                response.Result = list;
            }
            else
            {
                response.Code = 200;
                response.Message = "Ocurrio un problema al obtener la lista";
            }
            return Ok(response);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDto employee)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();

            if (await employeeService.Add(employee))
            {
                response.Code = 100;
                response.Message = "Empleado creado correctamente";
            }
            else
            {
                response.Code = 200;
                response.Message = "No se pudo crear el empleado";
            }

            return Ok(response);
        }

        // PUT 
        [HttpPut]
        public async Task<IActionResult> Put(EmployeeDto employee)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();

            if (await employeeService.Update(employee))
            {
                response.Code = 100;
                response.Message = "El Empleado se actualizo correctamente";
            }
            else
            {
                response.Code = 200;
                response.Message = "No se pudo actualizar el empleado";
            }

            return Ok(response);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();

            if (await employeeService.Delete(id))
            {
                response.Code = 100;
                response.Message = "Empleado eliminado correctamente";
            }
            else
            {
                response.Code = 200;
                response.Message = "No se pudo eliminar al empleado";
            }

            return Ok(response);
        }
    }
}
