using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ghost_Db.Models
{
    public class GhostContext : DbContext
    {
        public GhostContext(DbContextOptions<GhostContext> options)
             : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Capture> Capture { get; set; }
        public DbSet<Ghost> Ghost { get; set; }
        public DbSet<GhostType> GhostType { get; set; }
        public DbSet<CameraSchedule> CameraSchedule { get; set; }
        public DbSet<GhostProtocols> GhostProtocol { get; set; }
    
        
    }

}
