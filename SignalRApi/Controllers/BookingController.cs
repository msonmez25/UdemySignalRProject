using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.BookingDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult BookingList()
        {
            var values = _bookingService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto createBookingDto)
        {
            Booking booking = new Booking();
            {
                booking.Name = createBookingDto.Name;
                booking.Phone = createBookingDto.Phone;
                booking.Mail = createBookingDto.Mail;
                booking.PersonCount = createBookingDto.PersonCount;
                booking.Date = createBookingDto.Date;
                booking.Status = true;
                booking.Description = "Rezervasyon Alındı";
            };
            _bookingService.TAdd(booking);
            return Ok("Rezervasyon alındı.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            _bookingService.TDelete(value);
            return Ok("Rezervasyon silindi.");
        }

        [HttpPut]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            Booking booking = new Booking();
            {
                booking.BookingID = updateBookingDto.BookingID;
                booking.Name = updateBookingDto.Name;
                booking.Phone = updateBookingDto.Phone;
                booking.Mail = updateBookingDto.Mail;
                booking.PersonCount = updateBookingDto.PersonCount;
                booking.Date = updateBookingDto.Date;
                booking.Status = true;
            };
            _bookingService.TUpdate(booking);
            return Ok("Rezervasyon güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            return Ok(value);
        }

        [HttpGet("BookingStatusApproved/{id}")]
        public IActionResult BookingStatusApproved(int id)
        {
            _bookingService.TBookingStatusApproved(id);
            return Ok("Rezervasyon Onaylandı Olarak Değiştirildi");
        }

        [HttpGet("BookingStatusCanceled/{id}")]
        public IActionResult BookingStatusCanceled(int id)
        {
            _bookingService.TBookingStatusCanceled(id);
            return Ok("Rezervasyon İptal Edildi Olarak Değiştirildi");
        }
    }
}
