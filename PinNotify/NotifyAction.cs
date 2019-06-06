using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Text;
using System.IO;

namespace PinNotify
{
    interface IEmailActions
    {
        bool SendEmail(string[] args);
        int RandomNumberGen();
        string EncryptString(string val);
        string DecryptString(string val);
        void KeyFile();
    }

    public class NotifyAction : IEmailActions
    {
        private int _randomNumber = 0;
        private string _toPerson = string.Empty;
        private string _toAddress = string.Empty;
        private string _fromAddress = string.Empty;
        private string _SMTPServer = string.Empty;
        private string _userName = string.Empty;
        private string _password = string.Empty;

        public NotifyAction(string[] args)
        {
            SendEmail(args);
        }

        public bool SendEmail(string[] args)
        {
            try
            {
                //decrypt the strings passed
                _toPerson = DecryptString(args[0]);
                _toAddress = DecryptString(args[1]);
                _fromAddress = DecryptString(args[2]);
                _SMTPServer = DecryptString(args[3]);
                _userName = DecryptString(args[4]);
                _password = DecryptString(args[5]);
                _randomNumber = RandomNumberGen();

                var message = new MimeMessage();
                message.To.Add(new MailboxAddress(_toPerson, _toAddress));
                message.From.Add(new MailboxAddress("Reset Pin Email", _fromAddress));
                message.Subject = "Testing message subject";
                message.Body = new TextPart("plain")
                {
                    Text = @"New PIN Number is " + _randomNumber 
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    //Connect to SMTPServer
                    client.Connect(_SMTPServer, 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(_userName, _password);
                    client.Send(message);
                    client.Disconnect(true);

                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                KeyFile();
            }
        }

        public int RandomNumberGen()
        {
            //random 6 digit number generator
            Random random = new Random();
            _randomNumber = random.Next(99999999);
            return _randomNumber;
        }

        public string EncryptString(string val)
        {
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(val));
        }


        public string DecryptString(string val)
        {
            //Base64 String decryption method
            return Encoding.Unicode.GetString(Convert.FromBase64String(val));
        }

        public void KeyFile()
        {
            string FileLocation = @"C:\Users\Public\Keyfile.txt";
            string FileContent = "Pin number is " + _randomNumber.ToString();

            using(var sw = new StreamWriter(FileLocation))
            {
                sw.WriteLine(EncryptString(FileContent));
            }

        }
    }

}
