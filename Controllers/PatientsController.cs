using apbd_cw5.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController(IDbService dbService) : ControllerBase
    {
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetPatientDetails(int id)
        {
            var result = dbService.GetPatientDetailsAsync(id);
            if (result == null)
            {
                return NotFound(new { message = $"Patient with ID={id} not found." });
            }
            return Ok(result);
        }
    }
}