using Microsoft.EntityFrameworkCore;
using EVENT_SERVICE.Models;

namespace EVENT_SERVICE.Services
{
    public partial class EventDBContext : DbContext
    {
        public EventDBContext(DbContextOptions<EventDBContext> options) : base(options) { }

        public virtual DbSet<Event> Events { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.EventId)
                    .ValueGeneratedNever()
                    .HasColumnName("event_id");

                entity.Property(e => e.Customer)
                    .HasColumnType("character varying(255)[]")
                    .HasColumnName("customer");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying(255)[]")
                    .HasColumnName("description");

                entity.Property(e => e.EventDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("event_date");

                entity.Property(e => e.EventType).HasColumnName("event_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
