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
        }

        public void OnPostMiscOptions(int brightness, int contrast, bool ledToggle, int volume, bool micToggle, bool proximityToggle, bool audioToggle)
        {
            Program.Manager.AddOptions(brightness, contrast, volume, ledToggle, micToggle, proximityToggle, audioToggle);
        }

        public void OnPostSchedule(
            bool mondayCheck, int onTime1, int offTime1, int picVid1, int vLength1, int vDelay1, int sDelay1,
            bool teusdayCheck, int onTime2, int offTime2, int picVid2, int vLength2, int vDelay2, int sDelay2,
            bool WednesdayCheck, int onTime3, int offTime3, int picVid3, int vLength3, int vDelay3, int sDelay3,
            bool thursdayCheck, int onTime4, int offTime4, int picVid4, int vLength4, int vDelay4, int sDelay4,
            bool fridayCheck, int onTime5, int offTime5, int picVid5, int vLength5, int vDelay5, int sDelay5,
            bool saturdayCheck, int onTime6, int offTime6, int picVid6, int vLength6, int vDelay6, int sDelay6,
            bool sundayCheck, int onTime7, int offTime7, int picVid7, int vLength7, int vDelay7, int sDelay7)
        {
           
            if(mondayCheck == true)
            {               
                Program.Manager.updateSchedule(0, onTime1, offTime1, picVid1, vLength1, vDelay1, sDelay1);
            } 

            if(teusdayCheck == true)
            {                
                Program.Manager.updateSchedule(1, onTime2, offTime2, picVid2, vLength2, vDelay2, sDelay2);                
            }

            if(WednesdayCheck == true)
            {
                Program.Manager.updateSchedule(2, onTime3, offTime3, picVid3, vLength3, vDelay3, sDelay3);
            }

            if(thursdayCheck == true)
            {
                Program.Manager.updateSchedule(3, onTime4, offTime4, picVid4, vLength4, vDelay4, sDelay4);
            }

            if(fridayCheck == true)
            {
                Program.Manager.updateSchedule(4, onTime5, offTime5, picVid5, vLength5, vDelay5, sDelay5);
            }

            if(saturdayCheck == true)
            {
                Program.Manager.updateSchedule(5, onTime6, offTime6, picVid6, vLength6, vDelay6, sDelay6);
            }

            if(sundayCheck == true)
            {
                Program.Manager.updateSchedule(6, onTime7, offTime7, picVid7, vLength7, vDelay7, sDelay7);
            }                                    
        }// end onPostSchedule 

        public void ChangeBrightness()
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