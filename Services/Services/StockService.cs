using Domain.Entities;
using Domain.Interfaces.Repositories;
using Service.Abstractions.Dtos;
using Service.Abstractions.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Services.Services
{
    public class StockService : IStockService
    {
        IStockRepository stockRepository;
        IMedicineBatchRepository medicineBatchRepository;
        public StockService(IStockRepository _stockRepository , IMedicineBatchRepository _medicineBatchRepository)
        {
            this.stockRepository = _stockRepository;
            this.medicineBatchRepository = _medicineBatchRepository;
        }
        public async Task AddStockAsync(int medicineBatchId, int quantity, Domain.Entities.Type type)
        {
           MedicineBatch medicineBatch = await medicineBatchRepository.GetByIdAsync(medicineBatchId);
            if(medicineBatch == null)
            {
                throw new Exception("medicine batch not found");
            }
            Stock stock = new Stock
            {
                MedicineBatchId=medicineBatchId,
                Quantity=quantity,
                TimeStamp=DateTime.Now,
                type=type
            };

           await stockRepository.AddAsync(stock);
           await stockRepository.SaveAsync();
            

        }


        public async Task DeleteStockAsync(int StockId)
        {
            Stock stock =await stockRepository.GetByIdAsync(StockId);
            if (stock == null)
            {
                throw new Exception("Stock not found");
            }

            stock.IsDeleted = true;
            stockRepository.Update(stock);
            await stockRepository.SaveAsync();
                 
            
        }


        public async Task<long> GetAvailableQuantityAsync(int batchId)
        {
            MedicineBatch medicineBatch = await medicineBatchRepository.GetByIdAsync(batchId);
            if (medicineBatch == null)
            {
                throw new Exception("medicine batch not found");
            }
            long quantity = medicineBatch.Quantity;

            return quantity;

        }

        public async Task<List<StockDto>> GetStockHistoryAsync(int batchId)
        {
            MedicineBatch medicineBatch = await medicineBatchRepository.GetByIdAsync(batchId);

            if (medicineBatch == null)
            {
                throw new Exception("medicine batch not found");

            }

            var stocks = medicineBatch.stocks;
            List<StockDto> stockDtos = new List<StockDto>();


            foreach(var stock in stocks)
            {
                stockDtos.Add(new StockDto
                {
                   Quantity=stock.Quantity,
                   timestamp=stock.TimeStamp,
                   Type=stock.type

                }
                );
            }

            return stockDtos;
        }


    }
}
