using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectGhost.Pages
{
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
            var UserID = Program.Manager.UserID;
            var GhostID = Program.Manager.GhostID;
          
        }

        public void OnPostMiscOptions(int brightness, int contrast, bool ledToggle, int volume,
                                    bool micToggle, bool proximityToggle, bool audioToggle) {

            int brit = brightness;
            int con = contrast;
            int vol = volume;
            bool led = ledToggle;
            bool mic = micToggle;
            bool prox = proximityToggle;
            bool audio = audioToggle;


            Program.Manager.AddOptions(brit, con, led, vol, mic, prox, audio);
            

        }
    }
}