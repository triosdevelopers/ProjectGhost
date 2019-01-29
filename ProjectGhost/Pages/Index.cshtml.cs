using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectGhost.Pages
{
	public class IndexModel : PageModel
	{
		public void OnGet()
		{

		}

        public void OnPostLogin(string email, string password)
        {
            Program.Manager.CheckID(email, password);
            Response.Redirect("./admin");
        }
    }
}
