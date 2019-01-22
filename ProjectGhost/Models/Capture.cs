using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ghost_Db.Models
{
    public class Capture
    {
        public int CaptureID { get; set; }

        // time stamp
        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public string Name { get; set; }

        // video or snapshot
        [Required]
        public Boolean Type { get; set; }


        // GHOST FK
        public int GhostID { get; set; }
        public Ghost Ghost { get; set; }
    }
}
