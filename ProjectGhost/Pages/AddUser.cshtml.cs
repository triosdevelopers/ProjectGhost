using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectGhost.Pages
{
    public class AddUserModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPostCreateAccount(string email, string password, string serialNum, string ghostName)
        {
            string newSerial = serialNum.ToUpper();
            string pattern = "[0-9][0-9][A-Z][A-Z][0-9][0-9]-AD";

            Regex regex = new Regex(pattern);
            Match isMatch = regex.Match(newSerial);

            if (isMatch.Success)
            {
                Program.Manager.CreateAccount(email, password, newSerial, ghostName);
                Response.Redirect("./Index");
            }
            else
            {
                //alert if cereal has no milk
            }

        }
    }
}