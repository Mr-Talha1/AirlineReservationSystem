using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Models;

public partial class AirlineReservationSystemContext : DbContext
{
    public AirlineReservationSystemContext()
    {
    }

    public AirlineReservationSystemContext(DbContextOptions<AirlineReservationSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=AirlineReservationSystem;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.AirlineId).HasName("PK__Airlines__DC4582732DA8B322");

            entity.Property(e => e.AirlineId).HasColumnName("AirlineID");
            entity.Property(e => e.AirlineName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Cities__F2D21A96245B25FD");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.CoachId).HasName("PK__Coach__F411D9A19B4A2CC1");

            entity.ToTable("Coach");

            entity.HasIndex(e => e.CoachName, "UQ__Coach__459C3992A084AFCF").IsUnique();

            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.CoachName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__8A9E148E8F1B17CC");

            entity.HasIndex(e => e.FlightNumber, "UQ__Flights__2EAE6F5046B6004B").IsUnique();

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.AirlineId).HasColumnName("AirlineID");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.AvailableSeats).HasDefaultValue(0);
            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.DestinationCityId).HasColumnName("DestinationCityID");
            entity.Property(e => e.FlightNumber).HasMaxLength(20);
            entity.Property(e => e.FlightType).HasMaxLength(55);
            entity.Property(e => e.OriginCityId).HasColumnName("OriginCityID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.ReturnArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("Return_ArrivalTime");
            entity.Property(e => e.ReturnDepartureTime)
                .HasColumnType("datetime")
                .HasColumnName("Return_DepartureTime");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.StatusDate).HasColumnType("datetime");

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirlineId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Flights__Airline__0697FACD");

            entity.HasOne(d => d.Coach).WithMany(p => p.Flights)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Flights__CoachID__09746778");

            entity.HasOne(d => d.DestinationCity).WithMany(p => p.FlightDestinationCities)
                .HasForeignKey(d => d.DestinationCityId)
                .HasConstraintName("FK__Flights__Destina__0880433F");

            entity.HasOne(d => d.OriginCity).WithMany(p => p.FlightOriginCities)
                .HasForeignKey(d => d.OriginCityId)
                .HasConstraintName("FK__Flights__OriginC__078C1F06");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACB50C953E");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateTimestamp"));

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A15B3592").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.SkyMiles).HasDefaultValue(0);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
