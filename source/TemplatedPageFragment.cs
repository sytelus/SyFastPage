using System;
using System.Web;
using System.Web.UI;
using System.Collections;

namespace Sytel.SyFastPage
{
	[ParseChildren(true)]
	public class TemplatedPageFragment :  System.Web.UI.WebControls.WebControl, INamingContainer
	{
		private ITemplate m_template;
		private string m_text = null;
		private Hashtable m_values = new Hashtable();
		private bool m_isDataBound = false;

		public bool IsDataBound
		{
			get
			{
				return m_isDataBound;
			}
		}


		public Hashtable Values
		{
			get
			{
				return m_values;
			}
			set
			{
				m_values = value;
			}
		}

		            
		[System.ComponentModel.BrowsableAttribute(false), 
		System.ComponentModel.DefaultValueAttribute(""), 
		System.Web.UI.PersistenceModeAttribute(PersistenceMode.InnerProperty), 
		TemplateContainer(typeof(TemplatedPageFragment))]
		public ITemplate Template
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
		            
		public String Text
		{
			get 
			{
				return m_text;
			}
			set
			{
				m_text = value;
			}
		}

		protected override void OnDataBinding(EventArgs e)
		{
			m_isDataBound = true;
			this.EnsureChildControls();
			base.OnDataBinding(e);
		}

		protected override void CreateChildControls ()
		{
			Controls.Clear();
			if (Template != null)
			{
				Template.InstantiateIn(this);
			}
			else
			{
				Controls.Add(new LiteralControl(Text));
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			if (m_isDataBound == false) this.DataBind();
			base.OnPreRender(e);
		}

		private string m_themeTemplateName = string.Empty;
		public string ThemeTemplateName
		{
			get
			{
				return m_themeTemplateName;
			}
			set
			{
				m_themeTemplateName = value;
				
				if (WebCommonFunctions.IsInDesignMode(this)==false)
				{
					if (m_themeTemplateName == string.Empty)
						ApplyTemplate(null, false); //remove theme
					else
						ApplyTemplate(Themes.GetTemplate(m_themeTemplateName), true);
				}
			}
		}
		public void ApplyTemplate(ITemplate template)
		{
			ApplyTemplate(template, false);
		}
		private void ApplyTemplate(ITemplate template, bool isThemeUsed)
		{
			this.Template = template;
			if (isThemeUsed == false) 
				m_themeTemplateName = string.Empty;
		}

	
	}
}

