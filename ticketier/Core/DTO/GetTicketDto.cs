namespace ticketier.Core.DTO
{
    public class GetTicketDto
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string PassengerName { get; set; }
        public long PassengerSSn { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Price { get; set; }
    }
}
