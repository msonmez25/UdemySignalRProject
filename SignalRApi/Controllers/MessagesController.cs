using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.CategoryDto;
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
            createMessageDto.Date = DateTime.Now;
            createMessageDto.Status = false;
            var value = _mapper.Map<Message>(createMessageDto);
            _messageService.TAdd(value);
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
            return Ok(_mapper.Map<GetMessageDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            updateMessageDto.Status = false;
            var value = _mapper.Map<Message>(updateMessageDto);
            _messageService.TUpdate(value);
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
