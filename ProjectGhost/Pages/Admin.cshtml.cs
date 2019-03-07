using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ProjectGhost.Pages
{
    public class AdminModel : PageModel
    {
        public string[] timeOfDay = {"12:00", "12:15", "12:30", "12:45", "1:00", "1:15", "1:30", "1:45", "2:00", "2:15", "2:30", "2:45", "3:00",
            "3:15", "3:30", "3:45", "4:00", "4:15", "4:30", "4:45", "5:00", "5:15", "5:30", "5:45", "6:00", "6:15", "6:30", "6:45",
            "7:00", "7:15", "7:30", "7:45", "8:00", "8:15", "8:30", "8:45", "9:00", "9:15", "9:30", "9:45", "10:00", "10:15", "10:30",
            "10:45", "11:00", "11:15", "11:30", "11:45"};
        public string[] dayOfWeek = {"Sunday", "Monday", "Teusday", "Wednesday", "Thursday", "Friday", "Saturday"};

        public int OptionsID;
        public int Brightness = Program.Manager.Brightness;
        public int Contrast = Program.Manager.Contrast;
        public int Led = Program.Manager.Led;
        public int Volume = Program.Manager.Volume;
        public int Mic = Program.Manager.Microphone;
        public int Prox = Program.Manager.Proximity;
        public int Audio = Program.Manager.Audio;


        public void OnGet()
        {
            var UserID = Program.Manager.UserID;
            var GhostID = Program.Manager.GhostID;
            ReloadOptions();
        }

        public void OnPostMiscOptions(int brightness, int contrast, int ledToggle, int volume,
                                    int micToggle, int proximityToggle, int audioToggle) {
            

            // uses Gpio Pin 17
            //Program.Connections.LedToggle(ledToggle);

            // Adds all the options to DB
            //Program.Manager.AddOptions(brightness, contrast, volume, ledToggle, micToggle, proximityToggle, audioToggle);

            // Changes the camera Options on the PI
            Program.Connections.Send_cmd("br", brightness);
            Program.Connections.Send_cmd("co", contrast);

            ReloadOptions();
        }

        

        public void ReloadOptions()
        {
            Program.Manager.ReturnLastOptions();
            OptionsID = Program.Manager.OptionsID;
            Brightness = Program.Manager.Brightness;
            Contrast = Program.Manager.Contrast;
            Led = Program.Manager.Led;
            Volume = Program.Manager.Volume;
            Mic = Program.Manager.Microphone;
            Prox = Program.Manager.Proximity;
            Audio = Program.Manager.Audio;
        }


    }
}