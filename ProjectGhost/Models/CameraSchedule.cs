using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ghost_Db.Models
{
    public class CameraSchedule
    {

        public int CameraScheduleID { get; set; }

        // day of week
        [Required]
        public int DayOfWeek { get; set; }

        // time camera turns on for live streaming
        // can be turned on manualy in protocols
        [Required]
        public int OnTime { get; set; }

        // time camera turns off
        // can be turned off manualy in protocols
        [Required]
        public int OffTime { get; set; }

        // camera on / off
        // default off
        public Boolean CameraState { get; set; }

        // video or snapshot
        [Required]
        public int CaptureType { get; set; }

        // length of recording time
        public int RecordingDuration { get; set; }
        // length of down time until next recording
        public int RecordingDelay { get; set; }
    
        // how many snapshots??
        public int SnapshotCount { get; set; }
        // length of downtime until next snapshot group
        public int SnapshotDelay { get; set; }

    }
}
