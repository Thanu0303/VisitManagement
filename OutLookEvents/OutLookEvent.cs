using Microsoft.Office.Interop.Outlook;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace OutLookEvents
{
    public class OutLookEvents
    {

        public string OutLookEvent(eAppointment appointment, Boolean SendMail)
        {
            try
            {
                Application outlookapp = new Application();
                AppointmentItem agendaMeeting = (AppointmentItem)outlookapp.CreateItem(OlItemType.olAppointmentItem);
                if (agendaMeeting != null)
                {
                    agendaMeeting.MeetingStatus = appointment.MeetingStatus;
                    agendaMeeting.Location = appointment.Location;
                    agendaMeeting.Subject = appointment.Subject;
                    agendaMeeting.Body = appointment.Body;
                    agendaMeeting.Start = appointment.Start;
                    agendaMeeting.Duration = appointment.Duration;
                    // save the appointment
                    agendaMeeting.Save();
                    if (SendMail)
                    {
                        Recipient recipient = null;
                        foreach (var mails in appointment.Email)
                        {
                            recipient = agendaMeeting.Recipients.Add(mails);
                        }
                        recipient.Type = (int)OlMeetingRecipientType.olRequired;
                        try
                        {
                            ((_AppointmentItem)agendaMeeting).Send();
                            return "Success";
                        }
                        catch (System.Exception ex)
                        {
                            return "Fail";
                        }
                    }
                }
            }
            catch (System.Exception ex) { }
            return string.Empty;
        }

        public string OutLookEventMail(eMail mail)
        {
            try
            {
                Application outlookapp = new Application();
                MailItem mailitem = (MailItem)outlookapp.CreateItem(OlItemType.olMailItem);
                if (mailitem != null)
                {
                    mailitem.BodyFormat = mail.mailformat;
                    mailitem.Subject = mail.Subject;
                    mailitem.Body = mail.Body;
                    Recipient recipient = null;
                    foreach (var receipient in mail.Receipients)
                    {
                        recipient = mailitem.Recipients.Add(receipient);
                    }

                    try
                    {
                        mailitem.Send();
                        return "Success";
                    }
                    catch (System.Exception ex)
                    {
                        return "Fail";
                    }
                }
            }
            catch (System.Exception ex) { }
            return string.Empty;
        }

        public string SMTPMail(eMail mail)
        {
            if (mail.Receipients.Count() > 0)
            {
                MailMessage message = new MailMessage();
                foreach (var receipient in mail.Receipients)
                {
                    message.To.Add(receipient); // Add Receiver mail Address  
                }
                message.From = new MailAddress("canarys.admin@canarys.com", "Canarys Automation Pvt ltd."); // Sender address  
                message.Subject = mail.Subject;


                message.IsBodyHtml = true; //HTML email  
                message.Body = mail.Body;


                try
                {
                    System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient();
                    //smtpclient.Host = "smtp.gmail.com"; //-------this has to given the Mailserver IP
                    //smtpclient.Port = 587;
                    //smtpclient.EnableSsl = true;
                    //smtpclient.Credentials = new System.Net.NetworkCredential("visitcanarys@gmail.com", "Canarys123");
                    smtpclient.Send(message);
                    return "Success";
                }
                catch (System.Exception)
                {
                    return "Fail";
                }
            }
            return "No Recepient Found";
        }

        public string SMTPEvent(eAppointment appointment)
        {
            if (appointment.Email.Count() > 0)
            {
                MailMessage msg = new MailMessage();
                foreach (var recepient in appointment.Email)
                {
                    msg.To.Add(recepient);
                }
                msg.From = new MailAddress("canarys.admin@canarys.com", "Canarys Automation Pvt ltd.");
                msg.Subject = appointment.Subject;
                msg.Body = appointment.Body;
                msg.IsBodyHtml = true;


                StringBuilder str = new StringBuilder();

                str.AppendLine("BEGIN:VCALENDAR");
                str.AppendLine("PRODID:-//Schedule a Meeting");
                str.AppendLine("VERSION:2.0");
                str.AppendLine("METHOD:REQUEST");
                str.AppendLine("BEGIN:VEVENT");
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", appointment.Start));
                str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
                str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", appointment.End));
                str.AppendLine("LOCATION: " + appointment.Location);
                str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
                str.AppendLine(string.Format("DESCRIPTION:{0}", msg.Body));
                str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
                str.AppendLine(string.Format("SUMMARY:{0}", "Visit Created"));
                str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", "canarys.admin@canarys.com"));
                str.AppendLine("BEGIN:VALARM");
                str.AppendLine("TRIGGER:-PT15M");
                str.AppendLine("ACTION:DISPLAY");
                str.AppendLine("DESCRIPTION:Reminder");
                str.AppendLine("END:VALARM");
                str.AppendLine("END:VEVENT");
                str.AppendLine("END:VCALENDAR");


                byte[] byteArray = Encoding.ASCII.GetBytes(str.ToString());
                MemoryStream stream = new MemoryStream(byteArray);


                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(stream, "Visit Invite.ics");
                msg.Attachments.Add(attach);


                System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
                contype.Parameters.Add("method", "REQUEST");
                //  contype.Parameters.Add("name", "Meeting.ics");
                AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype);
                AlternateView HTMLV = AlternateView.CreateAlternateViewFromString(msg.Body, new System.Net.Mime.ContentType("text/html"));
                msg.AlternateViews.Add(avCal);
                msg.AlternateViews.Add(HTMLV);


                try
                {
                    //Now sending a mail with attachment ICS file.                     
                    System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient();
                    smtpclient.Host = "smtp.gmail.com"; //-------this has to given the Mailserver IP
                    smtpclient.Port = 587;
                    smtpclient.EnableSsl = true;
                    smtpclient.Credentials = new System.Net.NetworkCredential("visitcanarys@gmail.com", "Canarys123");
                    smtpclient.Send(msg);
                    return "Success";
                }
                catch (System.Exception)
                {
                    return "Fail";
                }
            }
            return "No Recepient Found";
        }

        public class eAppointment
        {
            public string[] Email { set; get; }
            public string Location { set; get; }
            public string Body { set; get; }
            public DateTime Start { set; get; }
            public DateTime End { set; get; }
            public int Duration { set; get; }
            public string Subject { set; get; }
            public OlMeetingStatus MeetingStatus { get; set; }

        }

        public class eMail
        {
            public string[] Receipients { set; get; }
            public string Body { set; get; }
            public string Subject { set; get; }
            public OlBodyFormat mailformat { get; set; }
        }

    }
}








