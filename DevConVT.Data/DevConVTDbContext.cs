using DevConVT.Data.Models;
using System;
using System.Data.Entity;

namespace DevConVT.Data
{
    public class DevConVTDbContext : DbContext
    {
        public DevConVTDbContext() : base("DevConVTDbContext")
        {


        }

        public DbSet<ContactForm> ContactForm { get; set; }

        public DbSet<EventRegistration> EventRegistrations { get; set; }
    }
}
