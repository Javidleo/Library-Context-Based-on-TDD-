using Microsoft.AspNetCore.Mvc;
using testAPIProject.DTO;
using testProject;

namespace testAPIProject.Controllers
{
    /// <summary>
    /// This Controller provides The Request for Person Domain Model
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class PersonController : ControllerBase
    {

        /// <summary>
        /// This Controller Need For Some service to Bind the Informations and Requests 
        /// So We Got the Iperson Service in here to do this 
        /// </summary>
        private readonly IPersonService _service;
        public PersonController(IPersonService service)
        {
            _service = service;
        }

        /// <summary>
        /// This Method Provide post Requests from client and add a new person to Database finally 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(PersonDTO dto)
        {
            Person person = Person.Create(dto.name, dto.family);
            _service.Add(person);
            return Ok();
        }

        /// <summary>
        /// This Method Provide for Updating Person Informations in Database finally 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="Id"></param>
        /// <returns>200 Ok Status Code</returns>
        [HttpPut]
        public async Task<IActionResult> Put(PersonDTO dto, int Id)
        {
            Person person = Person.Create(dto.name, dto.family);
            _service.Update(Id, person);
            return Ok();
        }
    }
}
