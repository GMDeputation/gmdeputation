

// SFA.Services.NotificationService
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.Entities;
using SFA.Models;
using SFA.Services;


namespace SFA.Services
{
	public interface INotificationService
	{
		Task<NotificationView> GetUnOpenByUserId(int userId);

		Task<bool> OpenByUserId(int id);

		Task<bool> Add(Notification notification);

		Task<bool> Add(List<Notification> notifications);

		Task<bool> Edit(Notification notification);
	}
	public class NotificationService : INotificationService
	{
		private readonly SFADBContext _context;

		public NotificationService(SFADBContext context)
		{
			_context = context;
		}

		public async Task<NotificationView> GetUnOpenByUserId(int userId)
		{
			List<Notification> list = (await (from m in _context.TblNotificationNta
											  where m.EventUser == userId && !m.IsOpened
											  orderby m.InsertDatetime descending
											  select m).ToListAsync()).Select((TblNotificationNta m) => new Notification
											  {
												  Id = m.Id,
												  EventUrl = m.EventUrl,
												  Description = m.Description,
												  EventUser = m.EventUser,
												  InsertUser = m.InsertUser,
												  InsertDatetime = m.InsertDatetime
											  }).ToList();
			return new NotificationView
			{
				Notifications = list,
				Count = list.Count()
			};
		}

		public async Task<bool> OpenByUserId(int id)
		{
			TblNotificationNta notificationEntity = await _context.TblNotificationNta.FirstOrDefaultAsync((TblNotificationNta m) => m.Id == id);
			foreach (TblNotificationNta item in await _context.TblNotificationNta.Where((TblNotificationNta m) => m.EventUser == notificationEntity.EventUser && m.EventUrl == notificationEntity.EventUrl && !m.IsOpened).ToListAsync())
			{
				item.IsOpened = true;
				//item.OpenedOn = DateTime.Now;
				//item.OpenedBy = notificationEntity.EventUser;
			}
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public async Task<bool> Add(List<Notification> notifications)
		{
			List<TblNotificationNta> notificationEntities = new List<TblNotificationNta>();
			foreach (Notification notification in notifications)
			{
				TblNotificationNta item = new TblNotificationNta
				{
					//CreatedBy = notification.CreatedBy,
					//CreatedOn = DateTime.Now,
					Description = notification.Description,
					EventUrl = notification.EventUrl,
					EventUser = notification.EventUser,
					IsOpened = false
				};
				notificationEntities.Add(item);
				TblUserNta tblUser = await _context.TblUserNta.FirstOrDefaultAsync((TblUserNta m) => m.Id == notification.EventUser);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("<p> Hi " + tblUser.FirstName + " " + tblUser.MiddleName + " " + tblUser.LastName + "</p>"));
				stringBuilder.AppendLine(string.Format("<p style='margin-left:30px'>UPCI GLOBAL MISSIONS DEPUTATION</p><br><p>" + notification.Description + "<br><p>Kindly check <a href='https://gmdeputation.com/" + notification.EventUrl + "'>UPCI GLOBAL MISSIONS DEPUTATION</a> .</p>"));
				try
				{
					//MimeMessage emailMessage = new MimeMessage();
					//emailMessage.get_From().Add((InternetAddress)new MailboxAddress("UPCI GLOBAL MISSIONS DEPUTATION", "notify@realchurch.eu"));
					//emailMessage.get_To().Add((InternetAddress)new MailboxAddress("Verify Email", tblUser.Email));
					//emailMessage.set_Subject("UPCI GLOBAL MISSIONS DEPUTATION Notification");
					//TextPart val = new TextPart("html");
					//val.set_Text(stringBuilder.ToString());
					//emailMessage.set_Body((MimeEntity)val);
					//SmtpClient client = new SmtpClient();
					//try
					//{
					//	client.set_LocalDomain("smtp.easyname.com");
					//	await ((MailService)client).ConnectAsync("smtp.easyname.com", 465, (SecureSocketOptions)1, default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
					//	await ((MailService)client).AuthenticateAsync((ICredentials)new NetworkCredential("notify@realchurch.eu", "0.etx6z6qcup"), default(CancellationToken));
					//	await ((MailTransport)client).SendAsync(emailMessage, default(CancellationToken), (ITransferProgress)null).ConfigureAwait(continueOnCapturedContext: false);
					//	await ((MailService)client).DisconnectAsync(true, default(CancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
					//}
					//finally
					//{
					//	((IDisposable)client)?.Dispose();
					//}
				}
				catch (Exception)
				{
				}
			}
			try
			{
				_context.TblNotificationNta.AddRange(notificationEntities);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public async Task<bool> Add(Notification notification)
		{
			TblNotificationNta notificationEntity = new TblNotificationNta
			{
				//CreatedBy = notification.CreatedBy,
				//CreatedOn = DateTime.Now,
				Description = notification.Description,
				EventUrl = notification.EventUrl,
				EventUser = notification.EventUser,
				IsOpened = false
			};
			TblUserNta tblUser = await _context.TblUserNta.FirstOrDefaultAsync((TblUserNta m) => m.Id == notification.EventUser);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("<p> Hi " + tblUser.FirstName + " " + tblUser.MiddleName + " " + tblUser.LastName + "</p>"));
			stringBuilder.AppendLine(string.Format("<p style='margin-left:30px'>UPCI GLOBAL MISSIONS DEPUTATION</p><br><p>" + notification.Description + "<br><p>Kindly check <a href='https://gmdeputation.com/" + notification.EventUrl + "'>UPCI GLOBAL MISSIONS DEPUTATION</a> .</p>"));
			try
			{
				//mimemessage emailmessage = new mimemessage();
				//emailmessage.get_from().add((internetaddress)new mailboxaddress("upci global missions deputation", "notify@realchurch.eu"));
				//emailmessage.get_to().add((internetaddress)new mailboxaddress("verify email", tbluser.email));
				//emailmessage.set_subject("upci global missions deputation notification");
				//textpart val = new textpart("html");
				//val.set_text(stringbuilder.tostring());
				//emailmessage.set_body((mimeentity)val);
				//smtpclient client = new smtpclient();
				//try
				//{
				//	client.set_localdomain("smtp.easyname.com");
				//	await ((mailservice)client).connectasync("smtp.easyname.com", 465, (securesocketoptions)1, default(cancellationtoken)).configureawait(continueoncapturedcontext: false);
				//	await ((mailservice)client).authenticateasync((icredentials)new networkcredential("notify@realchurch.eu", "0.etx6z6qcup"), default(cancellationtoken));
				//	await ((mailtransport)client).sendasync(emailmessage, default(cancellationtoken), (itransferprogress)null).configureawait(continueoncapturedcontext: false);
				//	await ((mailservice)client).disconnectasync(true, default(cancellationtoken)).configureawait(continueoncapturedcontext: false);
				//}
				//finally
				//{
				//	((idisposable)client)?.dispose();
				//}
			}
			catch (Exception)
			{
			}
			try
			{
				_context.TblNotificationNta.Add(notificationEntity);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		public async Task<bool> Edit(Notification notification)
		{
			(await _context.TblNotificationNta.FirstOrDefaultAsync((TblNotificationNta m) => m.Id == notification.Id)).Description = notification.Description;
			try
			{
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}