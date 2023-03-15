using CarTestTask.Models;
using CarTestTask.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public CarController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Cars.GetAllAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetByRecordNumbers/{first}/{last}")]
        public async Task<IActionResult> GetRecords(int first, int last)
        {
            var data = await unitOfWork.Cars.GetByRecordNumbersAsync(first, last);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Cars.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Car car)
        {
            var data = await unitOfWork.Cars.AddAsync(car);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Cars.DeleteAsync(id);
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Car car)
        {
            var data = await unitOfWork.Cars.UpdateAsync(car);
            return Ok(data);
        }
    }
}
