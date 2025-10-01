using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.MessageDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _mapper.Map<List<ResultMessageDto>>(_messageService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            _messageService.TAdd(new Message
            {
                NameSurname = createMessageDto.NameSurname,
                Mail=createMessageDto.Mail,
                Phone = createMessageDto.Phone,
                Subject = createMessageDto.Subject,
                MessageContent = createMessageDto.MessageContent,
                Date= DateTime.Now,
                Status = false
            });
            return Ok("Mesaj şekilde eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var value = _messageService.TGetByID(id);
            _messageService.TDelete(value);
            return Ok("Mesaj silindi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage(int id)
        {
            var value = _messageService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            _messageService.TUpdate(new Message
            {
                MessageId= updateMessageDto.MessageId,
                NameSurname = updateMessageDto.NameSurname,
                Mail = updateMessageDto.Mail,
                Phone = updateMessageDto.Phone,
                Subject = updateMessageDto.Subject,
                MessageContent = updateMessageDto.MessageContent,
                Date = updateMessageDto.Date,
                Status = false
            });
            return Ok("Mesaj şekilde güncellendi.");
        }


        [HttpGet("MessageStatusChangeToFalse/{id}")]
        public IActionResult MessageStatusChangeToFalse(int id)
        {
            _messageService.TMessageStatusChangeToFalse(id);
            return Ok("Güncelleme Yapıldı");
        }


        [HttpGet("MessageStatusChangeToTrue/{id}")]
        public IActionResult MessageStatusChangeToTrue(int id)
        {
            _messageService.TMessageStatusChangeToTrue(id);
            return Ok("Güncelleme Yapıldı");
        }

    }
}
