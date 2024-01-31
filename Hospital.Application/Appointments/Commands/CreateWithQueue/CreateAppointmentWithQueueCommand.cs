using Hospital.Application.Common.Helpers.Queue;
using Hospital.Application.Common.Models;
using Hospital.Application.Common.Models.Queue;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Appointments.Commands.CreateWithQueue;

public class CreateAppointmentWithQueueCommand : IRequest<BaseResponseModel<Unit>>
{
    public long CategoryId { get; set; }
    public long StaffId { get; set; }
    public long PatientId { get; set; }
    public long WorkCalendarId { get; set; }
    
    public class Handler : IRequestHandler<CreateAppointmentWithQueueCommand, BaseResponseModel<Unit>>
    {
        private readonly QueueHelper _queueHelper;
        private readonly ILogger<CreateAppointmentWithQueueCommand> _logger;

        public Handler(QueueHelper queueHelper, ILogger<CreateAppointmentWithQueueCommand> logger)
        {
            _queueHelper = queueHelper;
            _logger = logger;
        }
        
        public async Task<BaseResponseModel<Unit>> Handle(CreateAppointmentWithQueueCommand request, CancellationToken cancellationToken)
        {
            _queueHelper.Send(new AddAppointmentRequestModel
            {
                CategoryId = request.CategoryId,
                PatientId = request.PatientId,
                StaffId = request.StaffId,
                WorkCalendarId = request.WorkCalendarId
            });
            
            _logger.LogCritical("Randevu eklenmesi için başarılı bir şekilde sıraya eklendi.");
            return BaseResponseModel<Unit>.Success(Unit.Value,"Randevu eklenmesi için başarılı bir şekilde sıraya eklendi.");
        }
    }
}