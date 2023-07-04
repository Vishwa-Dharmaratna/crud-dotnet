using AutoMapper;
using ticketier.Core.DTO;
using ticketier.Core.Entities;

namespace ticketier.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            //tickets
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<Ticket, GetTicketDto>();
            CreateMap<UpdateTicketDto, Ticket>();
            
        }
    }
}
