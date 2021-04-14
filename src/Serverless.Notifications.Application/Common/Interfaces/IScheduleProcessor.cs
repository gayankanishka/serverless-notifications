using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface IScheduleProcessor
    {
        Task ProcessQueueAsync();
    }
}
