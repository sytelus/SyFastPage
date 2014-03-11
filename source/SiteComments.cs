using System;
using System.Data;
using System.IO;
using System.Web.Mail;

namespace Sytel.SyFastPage
{
	/// <summary>
	/// Summary description for SiteComments.
	/// </summary>
	public class SiteComments
	{
		private string m_commentFile = "(no comment XML file path specified)";
		public SiteComments()
		{
			m_commentFile = WebCommonFunctions.GetFullPathOnServer("sitecomments.xml");
		}
		public SiteComments(string commentFile)
		{
			m_commentFile = commentFile;
		}

		//This method can not be re-entered
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
		public void AddComment(string comment, string senderName, string senderEmail, string userHostAddress, string userHostName, string userAgent, string urlReferrer, string headers, DateTime commentTime)
		{
			DataSet commentsDataSet = new DataSet();
			commentsDataSet.ReadXml(m_commentFile);

			DataRow newComment = commentsDataSet.Tables["SiteComments"].Rows.Add(new object[] {});
			newComment["Comment"] = comment;
			newComment["SenderName"] = senderName;
			newComment["SenderEmail"] = senderEmail;
			newComment["UserHostAddress"] = userHostAddress;
			newComment["UserHostName"] = userHostName;
			newComment["UserAgent"] = userAgent;
			newComment["UrlReferrer"] = urlReferrer;
			newComment["Headers"] = headers;
			newComment["CommentTime"] = commentTime;
			
			commentsDataSet.WriteXml(m_commentFile, XmlWriteMode.WriteSchema);

			if (senderEmail == "") senderEmail = "not specified";
			try
			{
				SmtpMail.SmtpServer = "mail.shitalshah.com";
				SmtpMail.Send(senderEmail, "shital@ShitalShah.com", "Site Comments", "comment");
			}
			catch
			{ //stay silent
			}
		}
	}
}
