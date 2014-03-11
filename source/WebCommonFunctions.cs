using System;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.ComponentModel;
using Sytel.SyFastPage;

namespace Sytel
{
	/// <summary>
	/// Summary description for WebCommonFunctions.
	/// </summary>
	public class WebCommonFunctions
	{
		public static string GetFullPathOnServer(string relativePathOnSite)
		{
			if (HttpContext.Current != null)
			{
				return HttpContext.Current.Request.MapPath(relativePathOnSite);
			}
			else
			{
				throw new Exception("Current HttpContext is null. Can not virtual map path '+ relativePathOnSite +'");
			}
		}

		public static Page CurrentPage
		{
			get
			{
				return (Page) HttpContext.Current.Handler;
			}
		}

		public static Cache CurrentCache
		{
			get
			{
				return HttpContext.Current.Cache;
			}
		}
		
		public static bool IsInDesignMode(Control hostedComponent)
		{
			if (hostedComponent.Site!=null) return hostedComponent.Site.DesignMode;
			else return false;
		}
		
	}
}
