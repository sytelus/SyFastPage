using System;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace Sytel
{
	namespace SyFastPage
	{
		/// <summary>
		/// Summary description for SyCommonPage.
		/// </summary>
		public class PageTemplate: Page
		{
			
			
			private string m_pageTitle = null;	//TODO : set this for all properties
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String PageTitle
			{
				get
				{
					return m_pageTitle;
				}	
				set
				{
					m_pageTitle = value;
					//TODO : DO below for all properties
					if (m_pageTitle != null)
					{
						ReplacePageLiteralControlText("[page_title]", m_pageTitle);
					}
				}
			}
			
		
			private String m_htmlTemplateFileName = "page_template.htm";
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String HtmlTemplateFileName
			{
				get
				{
					return m_htmlTemplateFileName;
				}
				set
				{
					m_htmlTemplateFileName = value;
				}
			}
						
			private String m_siteTitle = SiteTemplateSettings.SiteTitle;
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String SiteTitle 
			{
				get
				{
					return m_siteTitle;
				}
				set
				{
					m_siteTitle = value;
				}
			}
			
			private String m_siteIcon = SiteTemplateSettings.SiteIcon;
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String SiteIcon
			{
				get
				{
					return m_siteIcon;
				}
				set
				{
					m_siteIcon = value;
				}
			}

			private String m_pageIcon = String.Empty;
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String PageIcon
			{
				get
				{
					return m_pageIcon;
				}
				set
				{
					m_pageIcon = value;
				}
			}

			private String m_pageHeading = null;
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String PageHeading
			{
				get
				{
					return m_pageHeading;
				}
				set
				{
					m_pageHeading = value;
					if (m_pageHeading != null)
					{
						ReplacePageLiteralControlText("[page_heading]", m_pageHeading);
					}
				}
			}

			private String m_siteNavigationMenu = String.Empty;
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String SiteNavigationMenu
			{
				get
				{
					return m_siteNavigationMenu;
				}
				set
				{
					m_siteNavigationMenu = value;
				}
			}

			private String m_CurrentActiveNavigationMenuName = String.Empty;
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String CurrentActiveNavigationMenuName
			{
				get
				{
					return m_CurrentActiveNavigationMenuName;
				}
				set
				{
					m_CurrentActiveNavigationMenuName = value;
				}
			}

			private String m_CurrentActiveNavigationSubMenuName = String.Empty;
			[System.ComponentModel.CategoryAttribute("SyPage")]
			public String CurrentActiveNavigationSubMenuName
			{
				get
				{
					return m_CurrentActiveNavigationMenuName;
				}
				set
				{
					m_CurrentActiveNavigationMenuName = value;
				}
			}

			private void ReplacePageLiteralControlText(string oldText, string newText)
			{
				for(int iControlIndex = Page.Controls.Count-1; iControlIndex >= 0 ; iControlIndex--)
				{
					if (Page.Controls[iControlIndex] is LiteralControl)
					{
						(Page.Controls[iControlIndex] as LiteralControl).Text = CommonFunctions.ReplaceString((Page.Controls[iControlIndex] as LiteralControl).Text, oldText, newText, true);
					}
				}
			}

			private void InjectTemplate()
			{
				//Read the page template
				StreamReader oTemplateFileStream = File.OpenText(Server.MapPath(HtmlTemplateFileName));
				String sTemplateContent = oTemplateFileStream.ReadToEnd();
				oTemplateFileStream.Close();
				
				//Replace site specific parameters
				sTemplateContent = CommonFunctions.ReplaceString(sTemplateContent, "[site_title]", SiteTemplateSettings.SiteTitle, true);
				sTemplateContent = CommonFunctions.ReplaceString(sTemplateContent, "[site_icon]", SiteTemplateSettings.SiteIcon, true);
				
				//Replace page specific parameters
				sTemplateContent = CommonFunctions.ReplaceString(sTemplateContent, "[page_icon]", PageIcon, true);
				if (PageTitle != null) sTemplateContent = CommonFunctions.ReplaceString(sTemplateContent, "[page_title]", PageTitle, true);
				if (PageHeading != null) sTemplateContent = CommonFunctions.ReplaceString(sTemplateContent, "[page_heading]", PageHeading, true);
				
				//Divide the page in to two parts
				String[] aPageParts = CommonFunctions.SplitString(sTemplateContent, "[page_content]", true);
				if (aPageParts.Length == 2)
				{
					if (Page.Controls.Count == 3)
					{
						Page.Controls.RemoveAt(2);
						Page.Controls.RemoveAt(0);
						Page.Controls.AddAt(0,new LiteralControl( aPageParts[0]));
						Page.Controls.AddAt(2,new LiteralControl( aPageParts[1]));
					}
					else
					{
						throw new Exception("Page has more then 3 controls which is unexpected");
					} ;
				}
				else {} ; // marker doesn't exist
				
				//For each control look for left navigation menu marker
				for(int iControlIndex = Page.Controls.Count-1; iControlIndex >= 0 ; iControlIndex--)
				{
					Control oControlInPage = Page.Controls[iControlIndex];
					if (oControlInPage is LiteralControl)
					{
						LiteralControl oLiteralControlInPage  = (LiteralControl) oControlInPage;
						String[] aLiteralControlParts = CommonFunctions.SplitString(oLiteralControlInPage.Text, "[site_navigation_menu]", true);
						if (aLiteralControlParts.Length != 1)
						{
							Page.Controls.Remove(oLiteralControlInPage);
							Page.Controls.AddAt(iControlIndex, new LiteralControl(aLiteralControlParts[0]));
							SySimpleMenu oMenu = new SySimpleMenu();
							oMenu.CurrentActiveMenu = this.CurrentActiveNavigationMenuName;
							oMenu.CurrentActiveSubMenu = this.CurrentActiveNavigationSubMenuName;
							Page.Controls.AddAt(iControlIndex+1, oMenu);
							Page.Controls.AddAt(iControlIndex+2, new LiteralControl(aLiteralControlParts[1]));
						}
					}
				}
			}
			
		
			override protected void OnInit(EventArgs e)
			{
				InjectTemplate();
				base.OnInit(e);
			}
		}
		
		internal class SiteTemplateSettings
		{
			public static String SiteTitle
			{
				get
				{
					return ConfigurationSettings.AppSettings["SiteTitle"] + "";
				}
			}
			public static String SiteIcon
			{
				get
				{
					return ConfigurationSettings.AppSettings["SiteIcon"] + String.Empty;
				}
			}
		}
	}
}
