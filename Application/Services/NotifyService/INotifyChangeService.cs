using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NotifyService
{
    public interface INotifyChangeService
    {
        Task NotifyCourseChangeAsync(int courseId, NotifyOperationEnum operation);
        Task NotifyTrailChangeAsync(int trailId, NotifyOperationEnum operation);
        Task NotifyContentChangeAsync(int contentId, NotifyOperationEnum operation);
    }
}
