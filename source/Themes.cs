using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Configuration;
using System.Collections.Specialized;


namespace Sytel.SyFastPage
{
	/// <summary>
	/// Summary description for Themes.
	/// </summary>
	public class Themes
	{
		public static string RelativeThemeFolderPath
		{
			get
			{
				return (string) ConfigurationSettings.AppSettings["RelativeThemeFolderPath"];
			}
		}
		
		public static string[] GetThemeNames()
		{
			return Directory.GetDirectories(RelativeThemeFolderPath);
		}

		public static string CurrentThemeName
		{
			get
			{
				return ((string) HttpContext.Current.Session["CurrentThemeName"]) + String.Empty;
			}
			set
			{
				HttpContext.Current.Session["CurrentThemeName"]= value;
			}
		}
		
		public static ITemplate GetTemplate(string templateName)
		{
			string cachKeyForTemplate = GetCacheKeyForTemplate(CurrentThemeName, templateName);
			ITemplate template = (ITemplate) WebCommonFunctions.CurrentCache.Get(cachKeyForTemplate);
			
			if (template == null)
			{
				string themeFolderPath = Path.Combine(RelativeThemeFolderPath, CurrentThemeName);
				if ((themeFolderPath != string.Empty) && !(themeFolderPath.EndsWith(@"\")==true))
					themeFolderPath += @"\";
				string templateFileName =  themeFolderPath + templateName + ".ascx";
				
				template = WebCommonFunctions.CurrentPage.LoadTemplate(templateFileName);

				WebCommonFunctions.CurrentCache.Insert(cachKeyForTemplate, template, 
						new CacheDependency(WebCommonFunctions.GetFullPathOnServer(templateFileName)), 
						Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30), 
						CacheItemPriority.Low, null);
			}

			return template;
		}

		private static string GetCacheKeyForTemplate(string themeName, string templateName)
		{
			return "Theme Template Object - " + themeName + " - " + templateName;
		}
	}

	public interface IThemedControl
	{
		string ThemeTemplateName {get;set;}
	}

	public class ControlTemplateBuilder : System.Web.UI.ITemplate 
	{
		private string m_template = string.Empty;
		public string Template
		{
			get
			{
				return m_template;
			}
			set
			{
				m_template = value;
			}
		}

		public ControlTemplateBuilder()
		{
		}

		public ControlTemplateBuilder(string template)
		{
			m_template = template;
		}

		void System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control container)
		{
			//PlaceHolder content = new PlaceHolder();
			Literal subContent = new Literal();
			subContent.Text = m_template;
			//content.Controls.Add (subContent);
			container.Controls.Add(container.Page.ParseControl(m_template));
		}
	}
}
