using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult NotificationList()
        {
            var value = _mapper.Map<List<ResultNotificationDto>>(_notificationService.TGetListAll());
            return Ok(value);
        }

        [HttpGet("{id}")]
        public IActionResult GetNotification(int id)
        {
            var value = _notificationService.TGetByID(id);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            _notificationService.TAdd(new Notification()
            {
                Type = createNotificationDto.Type,
                Icon = createNotificationDto.Icon,
                Link = createNotificationDto.Link,
                Description = createNotificationDto.Description,
                Date = DateTime.Now,
                Status = false,

            });
            return Ok("Bildirim eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var value = _notificationService.TGetByID(id);
            _notificationService.TDelete(value);
            return Ok("Bildirim silindi");
        }


        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            _notificationService.TUpdate(new Notification()
            {
                NotificationID = updateNotificationDto.NotificationID,
                Type = updateNotificationDto.Type,
                Icon = updateNotificationDto.Icon,
                Link = updateNotificationDto.Link,
                Description = updateNotificationDto.Description,
                Date = DateTime.Now,
                Status = true
            });
            return Ok("Bildirim güncellendi.");
        }

        [HttpGet("NotificationCountByStatusFalse")]
        public IActionResult NotificationCountByStatusFalse()
        {
            return Ok(_notificationService.TNotificationCountByStatusFalse());
        }


        [HttpGet("GetAllNotificationByFalseList")]
        public IActionResult GetAllNotificationByFalseList()
        {
            return Ok(_notificationService.TGetAllNotificationByFalseList());
        }

        [HttpGet("NotificationStatusChangeToFalse/{id}")]
        public IActionResult NotificationStatusChangeToFalse(int id)
        {
            _notificationService.TNotificationStatusChangeToFalse(id);
            return Ok("Güncelleme Yapıldı");
        }


        [HttpGet("NotificationStatusChangeToTrue/{id}")]
        public IActionResult NotificationStatusChangeToTrue(int id)
        {
            _notificationService.TNotificationStatusChangeToTrue(id);
            return Ok("Güncelleme Yapıldı");
        }
    }
}
