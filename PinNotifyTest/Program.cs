using System;
using PinNotify;

namespace PinNotifyTest
{
    class Program
    {
        static void Main()
        {
            string[] vals = new string[] {
                "QQBsAGUAeAAgAEgAbwByAHQAbwBuAA==", //To person
                "YQBsAGUAeAAuAGQALgBoAG8AcgB0AG8AbgBAAGgAbwB0AG0AYQBpAGwALgBjAG8AbQA=", //To Address
                "aABvAHIAdABvAG4AbABsAGMAMQA5ADcANgBAAG8AdQB0AGwAbwBvAGsALgBjAG8AbQA=", //From Address
                "cwBtAHQAcAAtAG0AYQBpAGwALgBvAHUAdABsAG8AbwBrAC4AYwBvAG0A", //SMTP Server
                "aABvAHIAdABvAG4AbABsAGMAMQA5ADcANgBAAG8AdQB0AGwAbwBvAGsALgBjAG8AbQA=", //Username
                "RABhAHIAbgBlAGwAbAA3ADYA" //password
            };

            var notify = new NotifyAction(vals);

            //display the new 6 digit pin number as an encrypted string
            Console.WriteLine("New Pin number Has been Sent to email!");
        }
    }
}
