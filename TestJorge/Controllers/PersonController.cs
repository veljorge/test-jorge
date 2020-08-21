using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestJorge.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [Route("getPeople")]
        [HttpGet]

        public async Task<IActionResult> GetPeople()
        {
            try
            {
                var people = await _unitOfWork.GetAll<Person>();
                return Ok(people);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}