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