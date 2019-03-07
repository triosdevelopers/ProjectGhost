using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectGhost
{
   
    public class Connections
    {

        public void Send_cmd(string command, int value)
        {
            string cmd = command + " " + value.ToString();

            try
            {
                Process.Start("/bin/bash", "sendCmd \"" + cmd + "\"");
            }
            catch (System.IO.IOException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Stack Trace: /t {ex} /n");
            }
           
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
