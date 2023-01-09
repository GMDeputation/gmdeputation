using System;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace SFA.Services
{
    public class Utilites
    {
        // This will get the current WORKING directory (i.e. \bin\Debug)
        static string workingDirectory = Environment.CurrentDirectory;
        static string templateKey = "2d6f.32b15e784a472135.k1.372ab3e0-8c93-11ed-80c6-525400fa05f6.1857f6a2e1e";

        private static string MacroEmailToDGMD = System.IO.File.ReadAllText(workingDirectory+ "\\wwwroot\\styles\\emailTemplates\\MacroScheduleEmailToDGMD.html");
        public void SendEmail(string dgmd, string district, string missionary, string startDate, string link, string toEmail)
        {

            //Replacing the html teamplate with the values needed for the email.
            MacroEmailToDGMD = MacroEmailToDGMD.Replace("{{DGMD}}", dgmd);
            MacroEmailToDGMD = MacroEmailToDGMD.Replace("{{district}}", district);
            MacroEmailToDGMD = MacroEmailToDGMD.Replace("{{missionary}}", missionary);
            MacroEmailToDGMD = MacroEmailToDGMD.Replace("{{startDate}}", startDate);
            MacroEmailToDGMD = MacroEmailToDGMD.Replace("{{link}}", link);

            //This is needed to make the HTML a valid JSON Object
            MacroEmailToDGMD = MacroEmailToDGMD.Replace("\n","\\n");
            MacroEmailToDGMD = MacroEmailToDGMD.Replace("'", "''");
            //string test = "I ' am testing ' ' ' ";



            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");
            string jsonString = "{'template_key':'"+ templateKey +"','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + toEmail + "','name': 'Troy Reynolds'}}],'merge_info':{'DGMD':'Test','{{district}}':'test','{{missionary}}':'test','{{startDate}}':'test','{{link}}':'test'}}";
            JObject parsedContent = null;
            try
            {
                parsedContent = JObject.Parse(jsonString);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine(parsedContent.ToString());
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent.ToString());

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();           
            try
            {
                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                Console.WriteLine(content);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           

          
            
        }
    }
}
