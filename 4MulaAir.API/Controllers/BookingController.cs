using System;
using _4MulaAir.API.Models;
using _4MulaAir.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace _4MulaAir.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        //In-Memo-db
        public readonly static List<Booking> _bookingsDB = new();

        private readonly ILogger<BookingController> _logger;
        private readonly IMessageProducer _messageProducer;

        public BookingController(ILogger<BookingController> logger, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking booking)
        {
            if (!ModelState.IsValid) return BadRequest();

            _bookingsDB.Add(booking);

            _messageProducer.SendingMessage<Booking>(booking);

            return Ok();
        }

    }
}

