using System;
using PinNotify;

namespace PinNotifyTest
{
    class Program
    {
        static void Main()
        {
            string[] vals = new string[] {
                "", //To person
                "", //To Address
                "", //From Address
                "", //SMTP Server
                "", //Username
                "" //password
            };

            var notify = new NotifyAction(vals);

            //display the new 6 digit pin number as an encrypted string
            Console.WriteLine("New Pin number Has been Sent to email!");
        }
    }
}
