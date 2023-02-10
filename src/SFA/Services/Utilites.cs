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
        static string workingDirectory = Environment.CurrentDirectory;

        //Function SendEmailDGMDIncomingSchedule uses these
        static string DGMDIncomingScheduleStandard = "2d6f.32b15e784a472135.k1.84a47570-96de-11ed-908a-525400e3c1b1.185c2e0e647";
        static string DGMDIncomingScheduleSensitiveNation = "2d6f.32b15e784a472135.k1.7f84b3b0-96df-11ed-908a-525400e3c1b1.185c2e7526b";
        static string DGMDIncomingScheduleR1Status = "2d6f.32b15e784a472135.k1.114db760-96e0-11ed-908a-525400e3c1b1.185c2eb0dd6";
        static string DGMDIncomingScheduleCombinedStatus = "2d6f.32b15e784a472135.k1.5d428640-9bff-11ed-9725-525400e3c1b1.185e47cc4a4";

        //Function SendEmailToMissionaryConfirmedMacro uses this
        static string EmailToMissionaryConfirmedMacro = "2d6f.32b15e784a472135.k1.dc345170-9c01-11ed-9725-525400e3c1b1.185e48d20077";

        //Function SendEmailToPastorNewServiceSchedule uses this
        static string EmailToPastorWithNewService = "2d6f.32b15e784a472135.k1.bd121560-9c02-11ed-9725-525400e3c1b1.185e492e1b6";
        static string EmailToPastorWithNewServiceOfferingOnly = "2d6f.32b15e784a472135.k1.4f1dc760-9c03-11ed-9725-525400e3c1b1.185e4969ed6";

        //Templates 1,1a,1b,1c
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

        //Template 5
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

        //Template 6 and 7(Offering Only)
        public void SendEmailToPastorNewServiceSchedule(string district, string missionaryFirstName, string missionaryLastName, string userSalutation, string pastorFirstName, string pastorLastName, string churchName, string missionaryCountry, string numbe3rtraveling, string travelingVia, string dayOfWeek, string eventDate, string eventTime, string typeOfEvent, string MissionaryMobile,string missionaryEmail,string dgmdFirstName,string dgmdLastName,string dgmdMobile, string dgmdEmail, string dgmdPrimaryPhone, string pastorEmail, bool offeringOnly,string dgmdName, string dgmdAddress)
        {

            string districtWebsite = "https://gmdeputation.com/";
            //{ { district} }
            //{ { MissionaryFirstName} }
            //{ { MissionaryLastName} }
            //{ { UserSalutation} }
            //{ { PastorFirstName} }
            //{ { PastorLastName} }
            //{ { ChurchName} }
            //{ { MissionaryCountry} }
            //{ { NumberTraveling} }
            //{ { TravelingVia} }
            //{ { DayOfWeek} }
            //{ { EventDate} }
            //{ { EventTime} }
            //{ { TypeOfEvent} }
            //{ { MissionaryMobile} }
            //{ { MissionaryEmail} }
            //{ { DGMDFirstName} }
            //{ { DGMDLastName} }
            //{ { DGMDMobile} }
            //{ { DGMDEmail} }
            //{ { DistrictWebsite} }
            //{ { DGMDPrimaryPhone} }
            //  {{DGMDName}} Offering Only
            //{ { DGMDAddress}} Offering Only
            string jsonString = "";
            string apiTemplateKey = "";
            if (offeringOnly)
            {
                //for template 7
                apiTemplateKey = EmailToPastorWithNewServiceOfferingOnly;
                jsonString = "{'template_key':'" + apiTemplateKey + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + pastorEmail + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','UserSalutation':'" + userSalutation + "','PastorFirstName':'" + pastorFirstName + "','PastorLastName':'" + pastorLastName + "','ChurchName':'" + churchName + "','MissionaryCountry':'" + missionaryCountry + "','NumberTraveling':'" + numbe3rtraveling + "','TravelingVia':'" + travelingVia + "','DayOfWeek':'" + dayOfWeek + "','EventDate':'" + eventDate + "','EventTime':'" + eventTime + "','TypeOfEvent':'" + typeOfEvent + "','MissionaryMobile':'" + MissionaryMobile + "','MissionaryEmail':'" + missionaryEmail + "','DGMDFirstName':'" + dgmdFirstName + "','DGMDLastName':'" + dgmdLastName + "','DGMDMobile':'" + dgmdMobile + "','DGMDEmail':'" + dgmdEmail + "','DistrictWebsite':'" + districtWebsite + "','DGMDPrimaryPhone':'" + dgmdPrimaryPhone + "','DGMDName':'" + dgmdName + "','DGMDAddress':'" + dgmdAddress + "'}}";
            }
            else
            {
                //for template 6
                apiTemplateKey = EmailToPastorWithNewService;
                jsonString = "{'template_key':'" + apiTemplateKey + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + pastorEmail + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','UserSalutation':'" + userSalutation + "','PastorFirstName':'" + pastorFirstName + "','PastorLastName':'" + pastorLastName + "','ChurchName':'" + churchName + "','MissionaryCountry':'" + missionaryCountry + "','NumberTraveling':'" + numbe3rtraveling + "','TravelingVia':'" + travelingVia + "','DayOfWeek':'" + dayOfWeek + "','EventDate':'" + eventDate + "','EventTime':'" + eventTime + "','TypeOfEvent':'" + typeOfEvent + "','MissionaryMobile':'" + MissionaryMobile + "','MissionaryEmail':'" + missionaryEmail + "','DGMDFirstName':'" + dgmdFirstName + "','DGMDLastName':'" + dgmdLastName + "','DGMDMobile':'" + dgmdMobile + "','DGMDEmail':'" + dgmdEmail + "','DistrictWebsite':'" + districtWebsite + "','DGMDPrimaryPhone':'" + dgmdPrimaryPhone + "'}}";
            }

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");
           //jsonString = "{'template_key':'" + apiTemplateKey + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + pastorEmail   + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','UserSalutation':'" + userSalutation + "','PastorFirstName':'" + pastorFirstName + "','PastorLastName':'" + pastorLastName + "','ChurchName':'" + churchName + "','MissionaryCountry':'" + missionaryCountry + "','NumberTraveling':'" + numbe3rtraveling + "','TravelingVia':'" + travelingVia + "','DayOfWeek':'" + dayOfWeek + "','EventDate':'" + eventDate + "','EventTime':'" + eventTime + "','TypeOfEvent':'" + typeOfEvent + "','MissionaryMobile':'" + MissionaryMobile + "','MissionaryEmail':'" + missionaryEmail + "','DGMDFirstName':'" + dgmdFirstName + "','DGMDLastName':'" + dgmdLastName + "','DGMDMobile':'" + dgmdMobile + "','DGMDEmail':'" + dgmdEmail + "','DistrictWebsite':'" + districtWebsite + "','DGMDPrimaryPhone':'" + dgmdPrimaryPhone + "'}}";
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
