using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Services;

namespace PharmaTrack.Controllers
{
 
    public class StockController : APIController
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("available/{batchId}")]
        public async Task<IActionResult> GetAvailableQuantity(int batchId)
        {
            var quantity = await _stockService.GetAvailableQuantityAsync(batchId);
            return Ok(new { BatchId = batchId, AvailableQuantity = quantity });
        }


        [HttpGet("history/{batchId}")]
        public async Task<IActionResult> GetStockHistory(int batchId)
        {
            var history = await _stockService.GetStockHistoryAsync(batchId);
            return Ok(history);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddStock([FromBody] AddStockRequest request)
        {
            await _stockService.AddStockAsync(request.MedicineBatchId, request.Quantity, request.StockMovementType);
            return Ok("Stock added successfully");
        }

        [HttpDelete("{stockId}")]
        public async Task<IActionResult> DeleteStock(int stockId)
        {
            await _stockService.DeleteStockAsync(stockId);
            return Ok("Stock deleted successfully");
        }
    }

    public class AddStockRequest
    {
        public int MedicineBatchId { get; set; }
        public int Quantity { get; set; }
        public Domain.Entities.StockMovementType StockMovementType { get; set; }
    }
}

