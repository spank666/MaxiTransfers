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
    public class BeneficiaryController : ControllerBase
    {
        private IBeneficiaryService beneficiaryService;

        public BeneficiaryController(IBeneficiaryService _beneficiaryService)
        {
            beneficiaryService = _beneficiaryService;
        }

        // GET api/<BeneficiaryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ResponseDto<List<BeneficiaryDto>> response = new ResponseDto<List<BeneficiaryDto>>();
            var list = await beneficiaryService.AllBeneficiariesOfEmployee(id);
            if (list is not null)
            {
                response.Code = 100;
                response.Message = "Lista de Beneficiarios obtenida correctamente";
                response.Result = list;
            }
            else
            {
                response.Code = 200;
                response.Message = "Ocurrio un problema al obtener la lista";
            }
            return Ok(response);
        }

        // POST api/<BeneficiaryController>
        [HttpPost]
        public async Task<IActionResult> Post(BeneficiaryDto beneficiary)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();

            if (await beneficiaryService.Add(beneficiary))
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
        public async Task<IActionResult> Put(BeneficiaryDto beneficiary)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();

            if (await beneficiaryService.Update(beneficiary))
            {
                response.Code = 100;
                response.Message = "El Beneficiario se actualizo correctamente";
            }
            else
            {
                response.Code = 200;
                response.Message = "No se pudo actualizar el beneficiario";
            }

            return Ok(response);
        }

        // PUT
        [HttpPut("{action}")]
        public async Task<IActionResult> Percentage(List<BeneficiaryDto> items)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();

            if (await beneficiaryService.UpdatePercentage(items))
            {
                response.Code = 100;
                response.Message = "Los porcentajes se actualizaron correctamente correctamente";
            }
            else
            {
                response.Code = 200;
                response.Message = "No se pudieron actualizar los porcentajes";
            }

            return Ok(response);
        }

        // DELETE api/<BeneficiaryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();

            if (await beneficiaryService.Delete(id))
            {
                response.Code = 100;
                response.Message = "Beneficiario eliminado correctamente";
            }
            else
            {
                response.Code = 200;
                response.Message = "No se pudo eliminar al beneficiario";
            }

            return Ok(response);
        }
    }
}
