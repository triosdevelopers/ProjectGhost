using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ghost_Db.Models
{
    public class GhostProtocols
    {
        public int GhostProtocolsID { get; set; }

        // camera brightness
        public int CameraBrightness { get; set; }
        // camera contrast
        public int CameraContrast { get; set; }
        // camera on / off
        public Boolean CameraState { get; set; }

        // LED schema
        public int LedBrightness { get; set; }
        // LED on / off
        public Boolean LedState { get; set; }

        // Audio Volume
        // default 25%
        public int Volume { get; set; }

        // Speakers on / off
        public Boolean SpeakerState { get; set; }

        // Motion sensor on / off
        public Boolean MotionSensorState { get; set; }

        // Mic sensitivity
        // public int MicSensitivity { get; set; }

        // GHOST FK
        public int GhostID { get; set; }
        public Ghost Ghost { get; set; }

    }
}
