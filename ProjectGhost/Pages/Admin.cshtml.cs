using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectGhost.Pages
{
    public class AdminModel : PageModel
    {
        public string[] timeOfDay = {"12:15", "12:30", "12:45", "1:00", "1:15", "1:30", "1:45", "2:00", "2:15", "2:30", "2:45", "3:00",
            "3:15", "3:30", "3:45", "4:00", "4:15", "4:30", "4:45", "5:00", "5:15", "5:30", "5:45", "6:00", "6:15", "6:30", "6:45",
            "7:00", "7:15", "7:30", "7:45", "8:00", "8:15", "8:30", "8:45", "9:00", "9:15", "9:30", "9:45", "10:00", "10:15", "10:30",
            "10:45", "11:00", "11:15", "11:30", "11:45", "12:00"};
        public string[] dayOfWeek = {"Sunday", "Monday", "Teusday", "Wednesday", "Thursday", "Friday", "Saturday"};

        public void OnGet()
        {
            var UserID = Program.Manager.UserID;
            var GhostID = Program.Manager.GhostID;
          
        }

        public void OnPostMiscOptions(int brightness, int contrast, bool ledToggle, int volume,
                                    bool micToggle, bool proximityToggle, bool audioToggle) {

            Program.Manager.AddOptions(brightness, contrast, volume, ledToggle, micToggle, proximityToggle, audioToggle);

        }


        public void ChangeBrightness()
        {

        }
    }
}