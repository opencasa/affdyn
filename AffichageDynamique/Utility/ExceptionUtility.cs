using AffichageDynamique.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AffichageDynamique.Utility
{
    public class ExceptionUtility
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ExceptionUtility(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExceptionUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static IConfigurationRoot configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
        string connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];

        public static string SendCriticalErrorToMail(IApplicationBuilder app, HttpContext context)
        {

            try
            {
                var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

                string server = configuration.GetSection("Smtp")["ServerName"];
                string mailerror = configuration.GetSection("SendMail")["MailError"];
                string mailit = configuration.GetSection("SendMail")["MailIt"];
                string exceptionmailsubject = context.Request.Host + " " + configuration.GetSection("SendMail")["ExceptionMailSubject"];

                //split liste des personnes à prévenir
                String[] substrings = mailerror.Split(";");

                //string webRootPath = _hostingEnvironment.WebRootPath;
                //string contentRootPath = _hostingEnvironment.ContentRootPath;

                using (var message = new MailMessage())
                {
                    var httpConnectionFeature = context.Features.Get<IHttpConnectionFeature>();
                    var localIpAddress = httpConnectionFeature?.LocalIpAddress;

                    var exception = context.Features.Get<IExceptionHandlerFeature>();
                    var request = context.Features.Get<IHttpRequestFeature>();

                    if (exception != null && request != null)
                    {
                        message.Subject = "ErrorMVCCoreCritical";

                        message.Body = $"<h1>Error: {exception.Error.Message.Replace(Environment.NewLine, "<br />")}</h1>{exception.Error.Source.Replace(Environment.NewLine, "<br />")}<hr />";

                        message.Body += $"<b>Date: " + DateTime.Now.ToString() + "</b><br />";
                        message.Body += $"CurrentCulture.DisplayName: {CultureInfo.CurrentCulture.DisplayName}<hr />";
                        message.Body += $"Path: {request.Path}<br />";
                        message.Body += $"QueryString: {request.QueryString}<br />";
                        message.Body += $"RawTarget:<a href='{context.Request.Host}{request.RawTarget}'> {request.RawTarget}</a><br />";
                        message.Body += $"Protocol: {request.Protocol}<br />";
                        message.Body += $"StatusCode: {context.Response.StatusCode}<hr />";

                        message.Body += $"User Identity: {context.User.Identity.Name}<br />";
                        message.Body += $"Host: {context.Request.Host}<br />";
                        message.Body += $"Method: {context.Request.Method}<hr />";

                        message.Body += $"<b>Exception:</b><br />";
                        message.Body += $"HelpLink: {exception.Error.HelpLink }<br />";
                        message.Body += $"Type: {exception.Error.GetType() }<br />";
                        message.Body += $"Values: {exception.Error.Data.Values}<br />";
                        message.Body += $"TargetSite : {exception.Error.TargetSite  }<hr />";

                        message.Body += $"Referer: {context.Request.Headers["Referer"]}<hr />";

                        message.Body += $"User-Agent: {context.Request.Headers["User-Agent"]}<br />";
                        message.Body += $"IP Address: {httpConnectionFeature?.LocalIpAddress}<br />";
                        //////////////////////////ATTENTION CECI NE FONCTIONNE PAS SUR IIS///////////////////////
                        //message.Body += $"Server Name: {System.Security.Principal.WindowsIdentity.GetCurrent().Name} <hr /> ";
                        //////////////////////////ATTENTION CECI NE FONCTIONNE PAS SUR IIS///////////////////////
                        if (exception.Error.StackTrace != null)
                            message.Body += $"<b>Stack Trace</b><br />{exception.Error.StackTrace.Replace(Environment.NewLine, "<br />")}";
                        if (exception.Error.InnerException != null)
                            message.Body += $"<hr /><b>Inner Exception</b><br />{exception.Error.InnerException?.Message.Replace(Environment.NewLine, "<br />")}<hr />";
                        // This bit here to check for a form collection!
                        if (context.Request.HasFormContentType && context.Request.Form.Any())
                        {
                            message.Body += "<table border=\"1\"><tr><td colspan=\"2\">Form collection:</td></tr>";
                            foreach (var form in context.Request.Form)
                            {
                                message.Body += $"<tr><td>{form.Key}</td><td>{form.Value}</td></tr>";
                            }
                            message.Body += "</table><hr />";
                        }
                        if (context.Session != null)
                        {
                            message.Body += $"<hr /><b>Session:</b><br />";
                            message.Body += $"Id: {context.Session.Id}<br />";
                            message.Body += $"IsAvailable: {context.Session.IsAvailable}<br />";
                            message.Body += $"Keys: {context.Session.Keys}<br />";
                        }

                        message.IsBodyHtml = true;

                    }
                    foreach (var substring in substrings)
                    {
                        message.To.Add(new MailAddress(substring));
                    }
                    message.From = new MailAddress(mailit, exceptionmailsubject);

                    using (var smtpClient = new SmtpClient(server))
                    {
                        smtpClient.Send(message);
                    }
                    return message.Body;
                }

            }
            catch (Exception em)
            {
                em.ToString();
                return null;
            }

        }

        public static void SendSQLErrorToMail(SqlException ex, SqlError exmail, string filename)
        {
            
            try
            {
                var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

                string server = configuration.GetSection("Smtp")["ServerName"];
                string mailerror = configuration.GetSection("SendMail")["MailError"];
                string mailit = configuration.GetSection("SendMail")["MailIt"];
                string exceptionmailsubject = configuration.GetSection("SendMail")["ExceptionMailSubject"];

                //split liste des personnes à prévenir
                String[] substrings = mailerror.Split(";");

                string ErrorlineNo = exmail.LineNumber.ToString();
                string Errormsg = exmail.GetType().Name.ToString();
                string extype = exmail.GetType().ToString();
                string sp = exmail.Procedure.ToString();

                string ErrorLocation = exmail.Message.ToString();



                using (var message = new MailMessage())
                {


                    message.Subject = "ErrorMVCCoreSql";

                    message.Body = $"<h1>Error: {ex.Message.Replace(Environment.NewLine, "<br />")}</h1>{ex.Source.Replace(Environment.NewLine, "<br />")}<hr />";

                    message.Body += $"<b>Date: " + DateTime.Now.ToString() + "</b><br />";
                    message.Body += $"CurrentCulture.DisplayName: {CultureInfo.CurrentCulture.DisplayName}<hr />";


                    //////////////////////////ATTENTION CECI NE FONCTIONNE PAS SUR IIS///////////////////////
                    //message.Body += $"Server IIS: {System.Security.Principal.WindowsIdentity.GetCurrent().Name} <br /> ";
                    //////////////////////////ATTENTION CECI NE FONCTIONNE PAS SUR IIS///////////////////////
                    message.Body += $"Server SQL: {ex.Server }<hr />";

                    message.Body += $"File: {filename}<br />";
                    message.Body += $"Procedure: {sp}<br />";
                    message.Body += $"Line: {ErrorlineNo}<br />";
                    message.Body += $"ErrorMessage: {Errormsg}<br />";
                    message.Body += $"Exception Type: {extype}<br />";
                    message.Body += $"Error Details: {ErrorLocation}<br />";
                    message.Body += $"Error Page Url: <hr />";

                    //message.Body += $"Path: {request.Path}<br />";
                    //message.Body += $"QueryString: {request.QueryString}<br />";
                    //message.Body += $"RawTarget: {request.RawTarget}<br />";
                    //message.Body += $"Protocol: {request.Protocol}<br />";
                    //message.Body += $"StatusCode: {context.Response.StatusCode}<hr />";

                    //message.Body += $"User Identity: {context.User.Identity.Name}<br />";
                    //message.Body += $"Host: {context.Request.Host}<br />";
                    //message.Body += $"Method: {context.Request.Method}<hr />";

                    message.Body += $"<b>Error:</b><br />";
                    message.Body += $"HelpLink: {ex.HelpLink }<br />";
                    message.Body += $"Type: {ex.GetType() }<br />";
                    message.Body += $"Values: {ex.Data.Values}<br />";
                    message.Body += $"TargetSite : {ex.TargetSite  }<hr />";

                    //message.Body += $"Referer: {context.Request.Headers["Referer"]}<hr />";

                    //message.Body += $"User-Agent: {context.Request.Headers["User-Agent"]}<br />";
                    //message.Body += $"IP Address: {httpConnectionFeature?.LocalIpAddress}<br />";

                    if (ex.StackTrace != null)
                        message.Body += $"<b>Stack Trace</b><br />{ex.StackTrace.Replace(Environment.NewLine, "<br />")}";
                    if (ex.InnerException != null)
                        message.Body += $"<hr /><b>Inner Exception</b><br />{ex.InnerException?.Message.Replace(Environment.NewLine, "<br />")}<hr />";

                    message.IsBodyHtml = true;

                    foreach (var substring in substrings)
                    {
                        message.To.Add(new MailAddress(substring));
                    }
                    message.From = new MailAddress(mailit, exceptionmailsubject);

                    using (var smtpClient = new SmtpClient(server))
                    {
                        smtpClient.Send(message);
                    }
                }
            }
            catch (Exception em)
            {
                em.ToString();
            }
        }

    }
}
