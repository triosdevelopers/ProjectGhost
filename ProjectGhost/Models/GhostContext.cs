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

        public DbSet<User> user { get; set; }
        public DbSet<Capture> capture { get; set; }
        public DbSet<Ghost> ghost { get; set; }
        public DbSet<GhostType> ghostType { get; set; }
        public DbSet<CameraSchedule> cameraSchedule { get; set; }
        public DbSet<GhostProtocols> ghostProtocol { get; set; }
    
        
    }

}
