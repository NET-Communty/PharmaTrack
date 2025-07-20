using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Dtos.MedicineDto;
using Service.Abstractions.HandlerResponse;
using Service.Abstractions.IServices;

namespace PharmaTrack.Controllers
{

    public class MedicineController : APIController
    {
        private readonly IMedicineSercive _medicineSercive;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MedicineController(IMedicineSercive medicineSercive, IWebHostEnvironment webHostEnvironment)
        {
            _medicineSercive=medicineSercive;
            _webHostEnvironment=webHostEnvironment;
        }

        [HttpGet("GetAllMedicines")]
        public async Task<ActionResult<ServiceResponse<MedicineReadDto>>> GetAllMedicines()
        {
            var response = await _medicineSercive.GetAllMedicine();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpGet("GetMedicineById/{id}")]
        public async Task<ActionResult<ServiceResponse<MedicineReadDto>>> GetMedicineById(int id)
        {
            var response = await _medicineSercive.GetMedicineById(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
        [HttpGet("GetMedicineByCategoryId/{id}")]
        public async Task<ActionResult<ServiceResponse<MedicineReadDto>>> GetMedicineByCategoryId(int id)
        {
            var response = await _medicineSercive.GetMedicineByIdCategoryId(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpGet("GetMedicineBySupplirId/{id}")]
        public async Task<ActionResult<ServiceResponse<MedicineReadDto>>> GetMedicineBySupplirId(int id)
        {
            var response = await _medicineSercive.GetMedicineByIdSupplierId(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpPost("CreateMedicine")]
        public async Task<ActionResult<ServiceResponse<MedicineReadDto>>> CreateMedicine(MedicineAddDto medicineAddDto)
        {
            var response = await _medicineSercive.AddMedicine(medicineAddDto, _webHostEnvironment.WebRootPath);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("DeleteMedicine/{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteMedicine(int id)
        {
            var response = await _medicineSercive.DeleteMedidne(id);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);

        }
        [HttpPut("UpdateMedicine")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateMedicine(MedicineUpdateDto medicineUpdateDto)
        {
            var response = await _medicineSercive.UpdateMedicine(medicineUpdateDto, _webHostEnvironment.WebRootPath);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }
    }
}
