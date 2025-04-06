using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CloudPOE.Models;

namespace CloudPOE.Data
{
    public class CloudPOEContext : DbContext
    {
        public CloudPOEContext (DbContextOptions<CloudPOEContext> options)
            : base(options)
        {
        }

        public DbSet<CloudPOE.Models.Venues> Venues { get; set; } = default!;
        public DbSet<CloudPOE.Models.Event> Event { get; set; } = default!;
        public DbSet<CloudPOE.Models.Bookings> Bookings { get; set; } = default!;
    }
}
