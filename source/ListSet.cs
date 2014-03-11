using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sytel.SyFastPage
{
	/// <summary>
	/// Summary description for ListSet.
	/// </summary>
	public class ListSet : System.Web.UI.WebControls.DataGrid, IThemedControl 
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnableViewState = false;
		}

		private void SetDataGridPropertiesToEnableTemplate(bool isEnableTemplate)
		{
			this.AutoGenerateColumns = !isEnableTemplate;
			this.ShowHeader = !isEnableTemplate;
			this.CellPadding = 0;
			this.CellSpacing = 0;
			this.BorderStyle = (isEnableTemplate?BorderStyle.None:BorderStyle.Dashed);
			this.GridLines = (isEnableTemplate?GridLines.None:GridLines.Both);
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
			if (template != null)
			{
				SetDataGridPropertiesToEnableTemplate(true);
				TemplateColumn cell = new TemplateColumn();
				cell.ItemTemplate = template;

				this.Columns.Add(cell);
			}
			else
			{
				this.Columns.Clear();
				SetDataGridPropertiesToEnableTemplate(false);
			}
			
			if (isThemeUsed == false) 
				m_themeTemplateName = string.Empty;
		}

		public void LoadDataFromXmlFile(string xmlRelativeFileName, string tableName)
		{
			string xmlFullFilePath = WebCommonFunctions.GetFullPathOnServer(xmlRelativeFileName);
			
			DataSet dataSetToLoadXml = new DataSet();
			dataSetToLoadXml.ReadXml(xmlFullFilePath);

			this.DataSource = dataSetToLoadXml;
			this.DataMember = tableName;
			this.DataBind();
		}

		public void LoadDataFromOleDbDataBase(string oldDbConnectionString, string selectSql)
		{
			DataSet dataSetToFill = new DataSet();
			CommonFunctions.FillDataSetUsingSql(oldDbConnectionString, dataSetToFill, "DefaultTable", selectSql);
			
			//<TODO> : check for no rows/no tables
			this.DataSource = dataSetToFill;
			this.DataMember = "DefaultTable";
			this.DataBind();
		}

	}
}

