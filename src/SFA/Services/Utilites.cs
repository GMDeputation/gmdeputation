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

        static string DGMDIncomingScheduleStandard = "2d6f.32b15e784a472135.k1.84a47570-96de-11ed-908a-525400e3c1b1.185c2e0e647";
        static string DGMDIncomingScheduleSensitiveNation = "2d6f.32b15e784a472135.k1.7f84b3b0-96df-11ed-908a-525400e3c1b1.185c2e7526b";
        static string DGMDIncomingScheduleR1Status = "2d6f.32b15e784a472135.k1.114db760-96e0-11ed-908a-525400e3c1b1.185c2eb0dd6";
        static string DGMDIncomingScheduleCombinedStatus = "2d6f.32b15e784a472135.k1.5d428640-9bff-11ed-9725-525400e3c1b1.185e47cc4a4";
        static string EmailToMissionaryConfirmedMacro = "2d6f.32b15e784a472135.k1.dc345170-9c01-11ed-9725-525400e3c1b1.185e48d20077";

        public void SendEmailDGMDIncomingSchedule( string district, string missionaryFirstName, string missionaryLastName, string userSalutation, string missionaryEmail, string dgmdEmail, string dgmdPrimaryPhone, string startDate,string  endDate, string type)
        {
            string apiTemplateKey = "";
            if("standard".Equals(type))
            {
                apiTemplateKey = DGMDIncomingScheduleStandard;
            }
            else if ("R1".Equals(type))
            {
                apiTemplateKey = DGMDIncomingScheduleR1Status;
            }
            else if ("sensitiveNation".Equals(type))
            {
                apiTemplateKey = DGMDIncomingScheduleSensitiveNation;
            }
            else if("combined".Equals(type))
            {
                apiTemplateKey = DGMDIncomingScheduleCombinedStatus;
            }
            string districtWebsite = "https://gmdeputation.com/";
            //Variables for these Emails are
            //{{district}}
            //{{MissionaryFirstName}}
            //{{MissionaryLastName}}
            //{{UserSalutation}}
            //{{email}}
            //{{DGMDEmail}}
            //{{DistrictWebsite}}
            //{{DGMDPrimaryPhone}}
            //{{StartDate}}
            //{{EndDate}}

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");
            string jsonString = "{'template_key':'"+ apiTemplateKey + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + dgmdEmail + "','name': 'DGMD'}}],'merge_info':{'district':'"+district+ "','MissionaryFirstName':'"+missionaryFirstName+ "','MissionaryLastName':'"+missionaryLastName+ "','UserSalutation':'"+userSalutation+ "','email':'"+missionaryEmail+ "','DGMDEmail':'"+dgmdEmail+ "','DistrictWebsite':'"+districtWebsite+ "','DGMDPrimaryPhone':'"+dgmdPrimaryPhone+ "','StartDate':'"+startDate+ "','EndDate':'"+endDate+"'}}";
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
        public void SendEmailToMissionaryConfirmedMacro(string district, string missionaryFirstName, string missionaryLastName, string missionaryEmail, string specialDGMDText, string dgmdFirstName, string dgmdlastName, string dgmdPrimaryPhone, string dgmdEmail)
        {

            string districtWebsite = "https://gmdeputation.com/";
            //{{district}}
            //{{MMissionaryFirstName }}
            //{{MissionaryLastName}}
            //{{SpecialDGMDText}} ??
            //{{DGMDFirstName}}
            //{{DGMDLastName}}
            //{{DGMDPrimaryPhone}}
            //{{DGMDEmail}}
            //{{DistrictWebsite}}

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");
            string jsonString = "{'template_key':'" + EmailToMissionaryConfirmedMacro + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + missionaryEmail + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','SpecialDGMDText':'" + specialDGMDText + "','DGMDFirstName':'" + dgmdFirstName + "','DGMDEmail':'" + dgmdEmail + "','DistrictWebsite':'" + districtWebsite + "','DGMDPrimaryPhone':'" + dgmdPrimaryPhone + "','DGMDLastName':'" + dgmdlastName + "'}}";
            JObject parsedContent = null;
            try
            {
                parsedContent = JObject.Parse(jsonString);
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SendEmailToPastorNewServiceSchedule(string district, string missionaryFirstName, string missionaryLastName, string missionaryEmail, string specialDGMDText, string dgmdFirstName, string dgmdlastName, string dgmdPrimaryPhone, string dgmdEmail)
        {

            string districtWebsite = "https://gmdeputation.com/";
            //{{district}}
            //{{MMissionaryFirstName }}
            //{{MissionaryLastName}}
            //{{SpecialDGMDText}} ??
            //{{DGMDFirstName}}
            //{{DGMDLastName}}
            //{{DGMDPrimaryPhone}}
            //{{DGMDEmail}}
            //{{DistrictWebsite}}

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");
            string jsonString = "{'template_key':'" + EmailToMissionaryConfirmedMacro + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + missionaryEmail + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','SpecialDGMDText':'" + specialDGMDText + "','DGMDFirstName':'" + dgmdFirstName + "','DGMDEmail':'" + dgmdEmail + "','DistrictWebsite':'" + districtWebsite + "','DGMDPrimaryPhone':'" + dgmdPrimaryPhone + "','DGMDLastName':'" + dgmdlastName + "'}}";
            JObject parsedContent = null;
            try
            {
                parsedContent = JObject.Parse(jsonString);
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
