﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectGhost.Pages
{
    public class AdminModel : PageModel
    {

        string userEmail;
        string ghostSerial; //serial #

        public void OnGet()
        {

        }
    }
}