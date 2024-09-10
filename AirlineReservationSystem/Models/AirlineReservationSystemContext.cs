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

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=AirlineReservationSystem;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.AirlineId).HasName("PK__Airlines__DC4582734630199F");

            entity.Property(e => e.AirlineId).HasColumnName("AirlineID");
            entity.Property(e => e.AirlineName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Cities__F2D21A96F1E46D70");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(100);
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A098C39105");

            entity.HasIndex(e => e.ClassName, "UQ__Classes__F8BF561B4437269A").IsUnique();

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__8A9E148E83259FF4");

            entity.HasIndex(e => e.FlightNumber, "UQ__Flights__2EAE6F50063DFE9E").IsUnique();

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.AirlineId).HasColumnName("AirlineID");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.AvailableSeats).HasDefaultValue(0);
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.DestinationCityId).HasColumnName("DestinationCityID");
            entity.Property(e => e.FlightNumber).HasMaxLength(20);
            entity.Property(e => e.OriginCityId).HasColumnName("OriginCityID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirlineId)
                .HasConstraintName("FK__Flights__Airline__2DE6D218");

            entity.HasOne(d => d.Class).WithMany(p => p.Flights)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Flights__ClassID__30C33EC3");

            entity.HasOne(d => d.DestinationCity).WithMany(p => p.FlightDestinationCities)
                .HasForeignKey(d => d.DestinationCityId)
                .HasConstraintName("FK__Flights__Destina__2FCF1A8A");

            entity.HasOne(d => d.OriginCity).WithMany(p => p.FlightOriginCities)
                .HasForeignKey(d => d.OriginCityId)
                .HasConstraintName("FK__Flights__OriginC__2EDAF651");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC9A83A0CC");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateTimestamp"));

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053429C98DDA").IsUnique();

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
