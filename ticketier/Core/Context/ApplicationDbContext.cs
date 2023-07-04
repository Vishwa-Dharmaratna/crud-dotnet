using Microsoft.EntityFrameworkCore;
using ticketier.Core.Entities;

namespace ticketier.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //we need to a have a DbSet for our Database table
        public DbSet<Ticket> Tickets { get; set; }
    }
}
