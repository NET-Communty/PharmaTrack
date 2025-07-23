using Domain.Entities;
using Domain.Interfaces.Repositories;   
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;
using Service.Abstractions.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMedicineBatchRepository _medicineBatchRepository;
        private readonly ILowStockNotifier _notifier;

        public StockService(
            IStockRepository stockRepository,
            IMedicineBatchRepository medicineBatchRepository,
            ILowStockNotifier notifier)
        {
            _stockRepository = stockRepository;
            _medicineBatchRepository = medicineBatchRepository;
            _notifier = notifier;
        }

        public async Task AddStockAsync(int medicineBatchId, int quantity, StockMovementType stockMovementType)
        {
            var medicineBatch = await _medicineBatchRepository.GetByIdAsync(medicineBatchId);
            if (medicineBatch == null)
                throw new Exception("medicine batch not found");

            var stock = new Stock
            {
                MedicineBatchId = medicineBatchId,
                Quantity = quantity,
                TimeStamp = DateTime.Now,
                StockMovementType = stockMovementType
            };

            await _stockRepository.AddAsync(stock);
            await _stockRepository.SaveAsync();

            medicineBatch.Quantity += quantity;
            await _medicineBatchRepository.SaveAsync();

            await CheckAndNotifyIfLowAsync(medicineBatch);
        }

        public async Task DeleteStockAsync(int stockId)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);
            if (stock == null)
                throw new Exception("Stock not found");

            stock.IsDeleted = true;
            _stockRepository.Update(stock);
            await _stockRepository.SaveAsync();

            var batch = await _medicineBatchRepository.GetByIdAsync(stock.MedicineBatchId);
            if (batch != null)
            {
                await CheckAndNotifyIfLowAsync(batch);
            }
        }

        public async Task<long> GetAvailableQuantityAsync(int batchId)
        {
            var medicineBatch = await _medicineBatchRepository.GetByIdAsync(batchId);
            if (medicineBatch == null)
                throw new Exception("medicine batch not found");

            return medicineBatch.Quantity;
        }

        public async Task<List<StockDto>> GetStockHistoryAsync(int batchId)
        {
            var medicineBatch = await _medicineBatchRepository.GetByIdAsync(batchId);
            if (medicineBatch == null)
                throw new Exception("medicine batch not found");

            var stocks = medicineBatch.stocks ?? new List<Stock>();
            return stocks.Select(s => new StockDto
            {
                Quantity = s.Quantity,
                timestamp = s.TimeStamp,
                StockMovementType = s.StockMovementType
            }).ToList();
        }

        private async Task CheckAndNotifyIfLowAsync(MedicineBatch batch)
        {
            var med = batch.Medicine;
            if (med == null) 
                return;
            

            long available = batch.Quantity; 
            if (available <= med.LowStockThreshold)
            {
                await _notifier.NotifyLowStockAsync(med.Id, med.Name, available, med.LowStockThreshold);
            }
        }
    }
}
