using Microsoft.EntityFrameworkCore;

namespace DUR_Application.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }

        public DbSet<Lane> Lanes { get; set; }

        public DbSet<Machine> Machines {get; set;}

        public DbSet<MalfunctionRequest>  MalfunctionRequests {get; set;}

        public DbSet<RequestType> RequestTypes {get; set;}

        public DbSet<Role> Roles {get; set;}

        public DbSet<Magazine> Magazines { get; set;}

        public DbSet<SparePart> SpareParts{get; set;}

        public DbSet<RequestStatus> RequestStatus { get; set;}

    }
}
