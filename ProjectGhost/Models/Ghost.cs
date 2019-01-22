using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ghost_Db.Models
{
    public class Ghost
    {
        public int GhostID { get; set; }

        [Required]
        public int SerialNumber { get; set; }

        // TYPE FK
        public int GhostTypeID { get; set; }
        public GhostType GhostType { get; set; }


        // camera schedule FK
        public int CameraScheduleID { get; set; }
        public CameraSchedule CameraSchedule { get; set; }

    }
}
