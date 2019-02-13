using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ghost_Db.Models
{
    public class GhostContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Filename=./Ghost.db");
        }

        public DbSet<User> user { get; set; }
        public DbSet<Capture> capture { get; set; }
        public DbSet<Ghost> ghost { get; set; }
        public DbSet<GhostType> ghostType { get; set; }
        public DbSet<CameraSchedule> cameraSchedule { get; set; }
        public DbSet<GhostProtocols> ghostProtocol { get; set; }
    
        
    }

}
