using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Abstractions.Dtos;
using Domain.Entities;

namespace Service.Abstractions.Services
{
    public interface IStockService
    {


        Task<List<StockDto>> GetStockHistoryAsync(int batchId);
        Task<long> GetAvailableQuantityAsync(int batchId);

        Task AddStockAsync(int medicineBatchId, int quantity, StockMovementType stockMovementType); // Added at DateTime.Now
        Task DeleteStockAsync(int StockId);

    }
}
