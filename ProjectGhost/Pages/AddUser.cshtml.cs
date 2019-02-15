using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectGhost.Pages
{
    public class AddUserModel : PageModel
    {
        
        public List<string> GhostNames = new List<string>();
        public bool firstAttemptFail;

        public void OnGet()
        {
            // get all the ghost names from db
            GhostNames = Program.Manager.ReturnGhostNames();
        }

        public void OnPostCreateAccount(string email, string password, string serialNum, string ghostName)
        {

            string newSerial = serialNum.ToUpper();
            string pattern = "[0-9][0-9][A-Z][A-Z][0-9][0-9]-AD";

            Regex regex = new Regex(pattern);
            Match isMatch = regex.Match(newSerial);

            GhostNames.Clear();
            Program.Manager.GhostNames.Clear();

            if (isMatch.Success)
            {
                firstAttemptFail = false;
                Program.Manager.CreateAccount(email, password, newSerial, ghostName);
                Response.Redirect("./Index");
                
            }
            else
            {
                firstAttemptFail = true;
                //alert if cereal has no milk
            }

        }
    }
}