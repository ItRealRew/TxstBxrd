using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EVENT_SERVICE.Models;

namespace EVENT_SERVICE.Services
{
    public partial class EventDBContext : DbContext
    {
        public EventDBContext()
        {
        }

        public EventDBContext(DbContextOptions<EventDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;port=5432;Username=postgres;Password=root;Database=EventDB");
            }
        }

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
