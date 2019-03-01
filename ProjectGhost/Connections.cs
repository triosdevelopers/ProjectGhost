using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectGhost
{
   
    public class Connections
    {
        List<string> myQ = new List<string>();

        public void send_cmd(int brightness, int contrast)
        {
            string bright = "brightness ";
            string contra = "contrast ";

            string brightnes_value = brightness.ToString();
            string contrast_value = contrast.ToString();

            string brightness_cmd = bright + brightnes_value;
            string contrast_cmd = contra + contrast.ToString();

            string flip_horizontal = "hflip true";
            string flip_vertical = "vflip true";

            myQ.Add(flip_horizontal);
            myQ.Add(flip_vertical);
            myQ.Add(brightness_cmd);
            myQ.Add(contrast_cmd);

            try
            {
                //==================================================== RUNNING LOCAL ===============>>>>
                File.WriteAllLines("C:/FakeEditFile/fake_uconfig.txt", myQ);
                ReadConfig("C:/FakeEditFile/fake_raspimjpg.txt");

                //===================================================== RUNNING ON RASPBERRY PI ====>>>>
                //File.WriteAllLines("/var/www/html/uconfig", myQ);
                //ReadConfig("/var/www/html/raspimjpeg");
            }
            catch (System.IO.IOException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Stack Trace: /t {ex} /n");
            }
        }

        public void ReadConfig(string fileName)
        {
            StringBuilder newFile = new StringBuilder();
            string temp = "";
            string[] file = File.ReadAllLines(fileName);
            
            foreach (string line in file) { 
           
                foreach(var cmd in myQ)
                {
                    var comd_check = cmd.Substring(0, 5);
                    if (line.Contains(comd_check))
                    {
                        temp = line.Replace(line, cmd);
                        newFile.Append(temp + "\r\n");
                        continue;
                    }
                }
                if(temp == "")
                {
                    newFile.Append(line + "\r\n");
                }
                else
                {
                    temp = "";
                }
                
                
            }
            File.WriteAllText(fileName, newFile.ToString());
        }

        public void LedToggle(int dir)
        {
            if(dir == 1)
            {
                using (var led = new GpioPin(17, Direction.Out))
                {
                    // ON
                    led.Value = PinValue.High;
                }
            }
            else
            {
                using (var led = new GpioPin(17, Direction.Out))
                { 
                    // OFF
                    led.Value = PinValue.Low;
                }
            }
        }





    }
}
