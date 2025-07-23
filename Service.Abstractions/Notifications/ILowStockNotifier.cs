using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Notifications
{
    public interface ILowStockNotifier
    {
        Task NotifyLowStockAsync(int medicineId, string medicineName, long availableQty, int threshold);

    }
}
