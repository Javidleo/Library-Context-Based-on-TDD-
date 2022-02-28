using BookApplication.Dto;
using DomainModel.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.ServiceContract;

namespace BookApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly AdminValidation _validation;
        public AdminController(IAdminService service)
        {
            _service = service;
            _validation = new AdminValidation();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AdminDto dto)
        {
            if (dto is null)
                throw new NotAcceptableException("Null Input");

            await _service.Create(dto.Name, dto.Family, dto.DateofBirth, dto.NationalCode, dto.UserName, dto.Email, dto.Password);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            return Ok(response);
        }

        [HttpGet("GetByNationalCode/{NationalCode}")]
        public async Task<IActionResult> GetByNationalCode([FromQuery] string nationalCode)
        {
            var response = await _service.GetByNationalCode(nationalCode);
            return Ok(response);
        }

    }
}
