using Microsoft.AspNetCore.SignalR;
using PharmaTrack.Hubs;
using Service.Abstractions.Notifications;

namespace PharmaTrack.Notifications
{
    public class SignalRLowStockNotifier: ILowStockNotifier
    {
        private readonly IHubContext<NotificationHub> _hub;

        public SignalRLowStockNotifier(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        public async Task NotifyLowStockAsync(int medicineId, string medicineName, long availableQty, int threshold)
        {
            var payload = new
            {
                MedicineId = medicineId,
                MedicineName = medicineName,
                Available = availableQty,
                Threshold = threshold,
                Warning = "LOW_STOCK"
            };

            await _hub.Clients.All.SendAsync("ReceiveNotification", payload);
        }
    }
}
