using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmailSender.SMTP
{
    public partial class MainWindow : Window
    {
        public MailAddress FromAddress { get; set; }
        public MailAddress ToAddress { get; set; }
        public SmtpClient SmtpClient { get; set; }

        public MainWindow()
        {
            DataContext = this;
            FromAddress = new MailAddress("nigarrmustafazada@gmail.com", "Nigar Mustafazada");
            SmtpClient = new SmtpClient("smtp.gmail.com", 587);

            SmtpClient.Credentials = new NetworkCredential("nigarrmustafazada@gmail.com", "nkmosjmsltgyilva");
            SmtpClient.EnableSsl = true;
            InitializeComponent();
        }
        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ToAddress = new MailAddress(Send.Text);
                var subject = Subject.Text;
                var body = Message.Text;

                Task.Run(() =>
                {
                    MailMessage mailMessage = new MailMessage(FromAddress, ToAddress);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    SmtpClient.Send(mailMessage);
                    MessageBox.Show("Your message sent");
                    Dispatcher.BeginInvoke(() =>
                    {
                        Send.Clear();
                        Subject.Clear();
                        Message.Clear();
                    });

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("You made some mistake. Try again!! ");
            }
        }
    }
}
