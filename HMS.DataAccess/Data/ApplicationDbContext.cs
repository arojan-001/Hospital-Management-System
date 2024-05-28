using System;
using System.Collections.Generic;
using System.Text;
using HMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patients> Patients { get; set; }

        public DbSet<Nurses> Nurses { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<OnCall> OnCalls { get; set; }

        public DbSet<Usage> Usage { get; set; }

        public DbSet<Doctors> Doctors { get; set; }

        public DbSet<Appointments> Appointments { get; set; }

        public DbSet<Medication> Medications { get; set; }

        public DbSet<Perscribtion> Perscribtions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Affiliated> Affiliated { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Undergoes> Undergoes { get; set; }

        public DbSet<ApplicationUser> applicationUser { get; set; }
    }
}
