using System;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.Entities;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        //Function SendEmailToMissionaryConfirmedService Uses this
        static string EmailToMissionaryConfirmedService = "2d6f.32b15e784a472135.k1.6c44c850-9c05-11ed-9725-525400e3c1b1.185e4a47955";

        static string EmatilToHQTwoWeekOut = "2d6f.32b15e784a472135.k1.af0d2570-9c04-11ed-9725-525400e3c1b1.185e49fa147";

        static string EmailForMacroScheduleCancel = "2d6f.32b15e784a472135.k1.fb1565d0-9c00-11ed-9725-525400e3c1b1.185e4875cad";

        static string EmailForAppointCancel = "2d6f.32b15e784a472135.k1.de4db970-9d9f-11ed-ab84-525400e3c1b1.185ef265e87";

        private readonly SFADBContext _context = null;

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

        //Template 12
        public void SendEmailToMissionaryConfirmedService(string district, string missionaryFirstName, string missionaryLastName, string pastorFirstName, string pastorLastName, string churchCity, string TypeOfEvent, string DayOfWeek, string EventTime, string PastorMobile, string PastorEmail, string churchName, string Churchaddress, string ChurchPhone, string SpecialPastorText, string UserSalutation, string DGMDFirstName, string DGMDLastName, string DGMDPrimaryPhone, string DGMDemail,string missionaryEmail)
        {

            string districtWebsite = "https://gmdeputation.com/";
            //{ { district} }
            //{ { MissionaryFirstName} }
            //{ { MissionaryLastName} }
            //{ { PastorFirstName} }
            //{ { PastorLastName} }
            //{ { ChurchCity} }
            //{ { TypeOfEvent} }
            //{ { DayOfWeek} }
            //{ { EventTime} }
            //{ { PastorMobile} }
            //{ { PastorEmail} }
            //{ { ChurchName} }
            //{ { Churchaddress} }
            //{ { ChurchPhone} }
            //{ { SpecialPastorText} }
            //{ { UserSalutation} }
            //{ { DGMDFirstName} }
            //{ { DGMDLastName} }
            //{ { DGMDPrimaryPhone} }
            //{ { DGMDemail} }
            //{ { DGMDEmail} }
            //{ { DistrictWebsite} }

            string jsonString = "";
            string apiTemplateKey = "";

            //for template 12
            apiTemplateKey = EmailToMissionaryConfirmedService;
            jsonString = "{'template_key':'" + apiTemplateKey + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + missionaryEmail + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','PastorFirstName':'" + pastorFirstName + "','PastorLastName':'" + pastorLastName + "','ChurchCity':'" + churchCity + "','TypeOfEvent':'" + TypeOfEvent + "','DayOfWeek':'" + DayOfWeek + "','EventTime':'" + EventTime + "','PastorMobile':'" + PastorMobile + "','PastorEmail':'" + PastorEmail + "','ChurchName':'" + churchName + "','Churchaddress':'" + Churchaddress + "','ChurchPhone':'" + ChurchPhone + "','SpecialPastorText':'" + SpecialPastorText + "','UserSalutation':'" + UserSalutation + "','DGMDFirstName':'" + DGMDFirstName + "','DGMDLastName':'" + DGMDLastName + "','DGMDPrimaryPhone':'" + DGMDPrimaryPhone + "','DGMDEmail':'" + DGMDemail + "','DistrictWebsite':'" + districtWebsite + "'}}";



            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");
       
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
        //Template 11
        //This is called from Program.cs in the main class. Called once a day and returns all schedules that are two weeks away exactly to the email template
        //As an excel file. 
        public void SendEmailToHQStaff2weekschedules(object source, ElapsedEventArgs e)
        {
            DateTime currentTim = DateTime.Now;
            //Linq Query Right now looking at everything greater than two weeks. When we go liev >= need to just be == Wre are just doing this for testing. 
            var appointmentQuery = _context.TblAppointmentNta.Where(s=> s.EventDate >= currentTim.AddDays(-14)).Include(m => m.Church).ThenInclude(m => m.District).ThenInclude(m => m.TblUserNta)
                       .Include(m => m.AcceptByPastorByNavigation).Include(m => m.MacroScheduleDetail).ThenInclude(m => m.MacroSchedule)
                       .Include(m => m.MacroScheduleDetail).ThenInclude(m => m.District).Include(m => m.AcceptMissionaryByNavigation).Include(m => m.MacroScheduleDetail)
                      .ThenInclude(m => m.User).AsNoTracking().AsQueryable();

            string districtWebsite = "https://gmdeputation.com/";
            //{ { district} }
            //{ { MissionaryFirstName} }
            //{ { MissionaryLastName} }
            //{ { District} }
            //{ { UserSalutation} }
            //{ { LastName} }
            //{ { DGMDFirstName} }
            //{ { DGMDLastName} }
            //{ { DGMDPrimaryPhone} }
            //{ { DGMDEmail} }
            //{ { DistrictWebsite} }

            string jsonString = "";
            string apiTemplateKey = "";

            //for template 11
            apiTemplateKey = EmatilToHQTwoWeekOut;
           // jsonString = "{'template_key':'" + apiTemplateKey + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + missionaryEmail + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','PastorFirstName':'" + pastorFirstName + "','PastorLastName':'" + pastorLastName + "','ChurchCity':'" + churchCity + "','TypeOfEvent':'" + TypeOfEvent + "','DayOfWeek':'" + DayOfWeek + "','EventTime':'" + EventTime + "','PastorMobile':'" + PastorMobile + "','PastorEmail':'" + PastorEmail + "','ChurchName':'" + churchName + "','Churchaddress':'" + Churchaddress + "','ChurchPhone':'" + ChurchPhone + "','SpecialPastorText':'" + SpecialPastorText + "','UserSalutation':'" + UserSalutation + "','DGMDFirstName':'" + DGMDFirstName + "','DGMDLastName':'" + DGMDLastName + "','DGMDPrimaryPhone':'" + DGMDPrimaryPhone + "','DGMDEmail':'" + DGMDemail + "','DistrictWebsite':'" + districtWebsite + "'}}";



            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");

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
        //Template 17
        //This is called from Program.cs in the main class. Called once a day and returns all schedules that are two weeks away exactly to the email template
        //As an excel file. 
        public void SendEmailForCancelledAppointments(object source, ElapsedEventArgs e)
        {
            //TODO NEED TO FIND OUT HOW TO GET THESE DETAILS
            //{ { district} }
            //{ { MissionaryFirstName} }
            //{ { MissionaryLastName} }
            //{ { UserSalutation} }
            //{ { PastorFirstName} }
            //{ { PastorLastName} }
            //{ { DGMDFirstName} }
            //{ { DGMDLastName} }
            //{ { DayOfWeek} }
            //{ { EventDate} }
            //{ { MissionaryCountry} }
            //{ { ChurchName} }
            //{ { DGMDMobile} }
            //{ { DGMDEmail} }
            //{ { DistrictWebsite} }
            //{ { DGMDPrimaryPhone} }

            DateTime currentTim = DateTime.Now;
            //Linq Query Right now looking at everything greater than two weeks. When we go liev >= need to just be == Wre are just doing this for testing. 
            var appointmentQuery = _context.TblAppointmentNta.Where(s => s.EventDate >= currentTim.AddDays(-14)).Include(m => m.Church).ThenInclude(m => m.District).ThenInclude(m => m.TblUserNta)
                       .Include(m => m.AcceptByPastorByNavigation).Include(m => m.MacroScheduleDetail).ThenInclude(m => m.MacroSchedule)
                       .Include(m => m.MacroScheduleDetail).ThenInclude(m => m.District).Include(m => m.AcceptMissionaryByNavigation).Include(m => m.MacroScheduleDetail)
                      .ThenInclude(m => m.User).AsNoTracking().AsQueryable();

            string districtWebsite = "https://gmdeputation.com/";

            string jsonString = "";
            string apiTemplateKey = "";

            //for template 17
            apiTemplateKey = EmailForAppointCancel;
            // jsonString = "{'template_key':'" + apiTemplateKey + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + missionaryEmail + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','PastorFirstName':'" + pastorFirstName + "','PastorLastName':'" + pastorLastName + "','TypeOfEvent':'" + TypeOfEvent + "','DayOfWeek':'" + DayOfWeek + "','EventTime':'" + EventTime + "','PastorMobile':'" + PastorMobile + "','PastorEmail':'" + PastorEmail + "','ChurchName':'" + churchName + "','Churchaddress':'" + Churchaddress + "','ChurchPhone':'" + ChurchPhone + "','SpecialPastorText':'" + SpecialPastorText + "','UserSalutation':'" + UserSalutation + "','DGMDFirstName':'" + DGMDFirstName + "','DGMDLastName':'" + DGMDLastName + "','DGMDPrimaryPhone':'" + DGMDPrimaryPhone + "','DGMDEmail':'" + DGMDemail + "','DistrictWebsite':'" + districtWebsite + "'}}";



            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");

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

        //Template 3
        //This is called from Program.cs in the main class. Called once a day and returns all schedules that are two weeks away exactly to the email template
        //As an excel file. 
        public void SendEmailForCancelledMacroSchedule(object source, ElapsedEventArgs e)
        {
            // TODO: FIND OUT HOW TO GET THESE DETAILS
            //{ { district} }
            //{ { MssionaryFirstName} }
            //{ { MissionaryLastName} }
            //{ { UserSalutation} }
            //{ { DGMDLastName} }
            //{ { start date} }
            //{ { EndDate} }
            //{ { status} }
            //{ { ReasonText} }
            //{ { DGMDEmail} }
            //{ { DistrictWebsite} }
            //{ { DGMDPrimaryPhone} }

            DateTime currentTim = DateTime.Now;
            //Linq Query Right now looking at everything greater than two weeks. When we go liev >= need to just be == Wre are just doing this for testing. 
            var appointmentQuery = _context.TblAppointmentNta.Where(s => s.EventDate >= currentTim.AddDays(-14)).Include(m => m.Church).ThenInclude(m => m.District).ThenInclude(m => m.TblUserNta)
                       .Include(m => m.AcceptByPastorByNavigation).Include(m => m.MacroScheduleDetail).ThenInclude(m => m.MacroSchedule)
                       .Include(m => m.MacroScheduleDetail).ThenInclude(m => m.District).Include(m => m.AcceptMissionaryByNavigation).Include(m => m.MacroScheduleDetail)
                      .ThenInclude(m => m.User).AsNoTracking().AsQueryable();

            string districtWebsite = "https://gmdeputation.com/";


            string jsonString = "";
            string apiTemplateKey = "";

            //for template 3
            apiTemplateKey = EmailForMacroScheduleCancel;
            // jsonString = "{'template_key':'" + apiTemplateKey + "','bounce_address':'bounceback@bounce.gmdeputation.com','from': { 'address': 'noreply@gmdeputation.com','name':'Troy'},'to': [{'email_address': {'address': '" + missionaryEmail + "','name': 'Missionary'}}],'merge_info':{'district':'" + district + "','MissionaryFirstName':'" + missionaryFirstName + "','MissionaryLastName':'" + missionaryLastName + "','PastorFirstName':'" + pastorFirstName + "','PastorLastName':'" + pastorLastName + "','ChurchCity':'" + churchCity + "','TypeOfEvent':'" + TypeOfEvent + "','DayOfWeek':'" + DayOfWeek + "','EventTime':'" + EventTime + "','PastorMobile':'" + PastorMobile + "','PastorEmail':'" + PastorEmail + "','ChurchName':'" + churchName + "','Churchaddress':'" + Churchaddress + "','ChurchPhone':'" + ChurchPhone + "','SpecialPastorText':'" + SpecialPastorText + "','UserSalutation':'" + UserSalutation + "','DGMDFirstName':'" + DGMDFirstName + "','DGMDLastName':'" + DGMDLastName + "','DGMDPrimaryPhone':'" + DGMDPrimaryPhone + "','DGMDEmail':'" + DGMDemail + "','DistrictWebsite':'" + districtWebsite + "'}}";



            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var baseAddress = "https://api.zeptomail.com/v1.1/email/template";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.PreAuthenticate = true;
            http.Headers.Add("Authorization", "Zoho-enczapikey wSsVR60irB74DaZ+zzL/Lu5umg5RA1ugE0R6jVWiuXH6HvvD98c9wkKcUVOlFPAaEzNqEjdGo7krzUxR2jVa24t+ng5WCyiF9mqRe1U4J3x17qnvhDzKWW9alxONJY4MzgtukmdpEMgn+g==");

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
