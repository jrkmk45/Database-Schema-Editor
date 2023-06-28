using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Attribute = Domain.Models.Attribute;

namespace Repository
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Connection>()
                .HasOne(c => c.AttributeFrom)
                .WithMany(a => a.ConnectionsTo)
                .HasForeignKey(c => c.AttributeFromId);
            
            builder.Entity<Connection>()
                .HasOne(c => c.AttributeTo)
                .WithMany(a => a.ConnectionsFrom)
                .HasForeignKey(c => c.AttributeToId);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Scheme> Schemes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<DataType> DataTypes { get; set; }
    }
}
