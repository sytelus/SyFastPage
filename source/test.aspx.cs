using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Sytel;
using Sytel.SyFastPage;

namespace SyFastPage.Private
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : Sytel.SyFastPage.PageTemplate 
	{
		protected Sytel.SyFastPage.TemplatedPageFragment TemplatedPageFragment2;
		protected Sytel.SyFastPage.ListSet ListSet1;
		protected Sytel.SyFastPage.TemplatedPageFragment TemplatedPageFragment1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.PageHeading = "Test page";
			this.PageTitle = "Page for test";

//			ListSet1.ThemeTemplateName = "TestListSetTemplate";
//			ListSet1.XmlRelativeFileName = "sitedata.xml";
//			ListSet1.XmlDataTableName = "Things2Try";

			
			TemplatedPageFragment1.Values.Add("main", ".Net Rules");
			
			ITemplate it =  (ITemplate) Page.Cache["it"];
			if (it == null)
			{
				it =Page.LoadTemplate("a.ascx");
				Page.Cache.Add("it", it, new CacheDependency(Server.MapPath("a.ascx")), Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(3), CacheItemPriority.High, null);
			}

			TemplatedPageFragment1.Template =  it; //new Sytel.SyFastPage.ControlTemplateBuilder(@"<%@ Register TagPrefix=""cc2"" Namespace=""Sytel.SyFastPage"" Assembly=""SyFastPage"" %>" + @"yoyo <asp:TextBox runat=""server"" Text=""ert""></asp:TextBox> <asp:TextBox runat=""server"" Text=""ert""></asp:TextBox> pro rata <cc2:templatedpagefragment runat=""server""><template>ya ko <%# ""dd"" %> hita</template></cc2:templatedpagefragment>");
			TemplatedPageFragment1.Text = "ru1q-";
			
			it = Themes.GetTemplate("a");

			TemplatedPageFragment2.Template =  it; //new Sytel.SyFastPage.ControlTemplateBuilder(@"<%@ Register TagPrefix=""cc2"" Namespace=""Sytel.SyFastPage"" Assembly=""SyFastPage"" %>" + @"yoyo <asp:TextBox runat=""server"" Text=""ert""></asp:TextBox> <asp:TextBox runat=""server"" Text=""ert""></asp:TextBox> pro rata <cc2:templatedpagefragment runat=""server""><template>ya ko <%# ""dd"" %> hita</template></cc2:templatedpagefragment>");
			TemplatedPageFragment2.Text = " <br>yes++++ishital";
			
			DataBind();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
