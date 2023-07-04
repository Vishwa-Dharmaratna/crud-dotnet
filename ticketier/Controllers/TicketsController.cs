using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketier.Core.Context;
using ticketier.Core.DTO;
using ticketier.Core.Entities;

namespace ticketier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        //we need database so we inject it using constructor
        //we need AutoMapper so we inject it using constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TicketsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTicket([FromBody]CreateTicketDto createTicketDto)
        {
            var newTicket = new Ticket();
            _mapper.Map(createTicketDto, newTicket);

            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();

            return Ok("Ticket Saved Successfully");


        }
        //Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTicketDto>>> GetTickets(string? q)
        {
            //Get Tickets from Context
            //var tickets = await _context.Tickets.ToListAsync();

            //var convertedTickets = _mapper.Map<IEnumerable<GetTicketDto>>(tickets);
            //return Ok(convertedTickets);


            //check if we have search parameter or not
            IQueryable<Ticket> query = _context.Tickets;
            if(q is not null)
            {
                query = query.Where(t =>t.PassengerName.Contains(q));
            }

            var tickets = await query.ToListAsync();
            var convertedTickets = _mapper.Map<IEnumerable<GetTicketDto>>(tickets);
            return Ok(convertedTickets);
        }

        //Read one by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetTicketDto>> GetTicketById([FromRoute]long id)
        {
            //Get ticket from context and check if it exists
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if(ticket is null)
            {
                return NotFound("Tickets is not found");
            }
            var convertedTicket = _mapper.Map<GetTicketDto>(ticket);
            return Ok(convertedTicket); 
        }

        //Update
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditTicket([FromRoute]long id, [FromBody]UpdateTicketDto updateTicketDto)
        {
            //Get ticket from context and check if it exists
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket is null)
            {
                return NotFound("Tickets is not found");
            }
            _mapper.Map(updateTicketDto, ticket);
            ticket.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("ticket updated succesfull");

        }

        //Delete
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute]long id)
        {
            //Get ticket from context and check if it exists
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket is null)
            {
                return NotFound("Tickets is not found");
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok("ticket deleted succesfull");

        }
    }
}
