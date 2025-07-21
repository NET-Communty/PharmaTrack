using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Dtos.SupplierDto;
using Service.Abstractions.HandlerResponse;
using Service.Abstractions.IServices;
namespace PharmaTrack.Controllers
{

    public class SupplierController : APIController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("GetAllSuppliers")]
        public async Task<ActionResult<ServiceResponse<List<SupplierReadDto>>>> GetAllSuppliers()
        {
            var response = await _supplierService.GetAllSuppliers();
            if (response.Success)
                return Ok(response.Data);
            return NotFound(response.Message);
        }

        [HttpGet("GetSupplierById/{id}")]
        public async Task<ActionResult<ServiceResponse<SupplierReadDto>>> GetSupplierById(int id)
        {
            var response = await _supplierService.GetSupplierById(id);
            if (response.Success)
                return Ok(response.Data);
            return NotFound(response.Message);
        }

        [HttpPost("CreateSupplier")]
        public async Task<ActionResult<ServiceResponse<SupplierReadDto>>> AddSupplier([FromBody] SupplierAddDto supplierAddDto)
        {
            var response = await _supplierService.AddSupplier(supplierAddDto);
            if (response.Success)
                return Ok(response.Data);
            return BadRequest(response.Message);
        }

        [HttpPut("UpdateSupplier")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateSupplier([FromBody] SupplierUpdateDto supplierUpdateDto)
        {
            var response = await _supplierService.UpdateSupplier(supplierUpdateDto);
            if (response.Success)
                return Ok(response.Message);
            return BadRequest(response.Message);
        }

        [HttpDelete("DeleteSupplier/{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteSupplier(int id)
        {
            var response = await _supplierService.DeleteSupplier(id);
            if (response.Success)
                return Ok(response.Message);
            return BadRequest(response.Message);
        }
    }
}

