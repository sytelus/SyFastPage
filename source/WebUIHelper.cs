using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace Sytel
{
	/// <summary>
	/// Summary description for UIHelper.
	/// </summary>
	public class WebUIHelper
	{
		public static String GetSelectedListItemValue(ListControl vlstListControl)
		{
			String sSelectedItemValue;
			if (vlstListControl.SelectedItem != null)
				sSelectedItemValue = vlstListControl.SelectedItem.Value;
			else
				sSelectedItemValue = String.Empty;
			
			return sSelectedItemValue;	
		}
		public static void SetSelectedListItemSafe(ListControl vlstListControl, String vsListItemKeyValue, bool vbIsSelected)
		{
			ListItem lsiSelectedListItem = vlstListControl.Items.FindByValue(vsListItemKeyValue);
			if (lsiSelectedListItem != null)
			{
				lsiSelectedListItem.Selected = vbIsSelected;
			}		
			//return lsiSelectedListItem;
		}  
		
	
		public static Hashtable GetSelectedListItems(ListControl  vlstListControl)
		{
			Hashtable  hstSelectedListItems = new Hashtable();
			for(int iListItemIndex = 0; iListItemIndex < vlstListControl.Items.Count; iListItemIndex++)
			{
				if (vlstListControl.Items[iListItemIndex].Selected == true)
					hstSelectedListItems.Add (vlstListControl.Items[iListItemIndex].Value, vlstListControl.Items[iListItemIndex].Text );
			}
			return hstSelectedListItems;
		}
		
		public static void SetSelectedListItemSafe(ListControl vlstListControl, IDictionary vdeItemsToSelect)
		{
			foreach(String sListItemToSelectKey in vdeItemsToSelect.Keys)
			{
				ListItem liItemToSelect = vlstListControl.Items.FindByValue(sListItemToSelectKey);
				if (liItemToSelect != null)
					liItemToSelect.Selected = true;
			}
		}

		public static void SelectAllListItems(ListControl vlstListControl, bool vbIsItemsToBeSelected)
		{
			foreach(ListItem liListItem in vlstListControl.Items)
			{
				liListItem.Selected = vbIsItemsToBeSelected;
			}
		}
		
		public static void SetControlValuesFromRow(DataRow row)
		{
			for (int columnIndex = 0; columnIndex < row.Table.Columns.Count; columnIndex++)
			{
				Control foundControl = WebCommonFunctions.CurrentPage.FindControl("db_" + row.Table.Columns[columnIndex].ColumnName);

				if (foundControl != null)
				{
					if(foundControl is TextBox)
					{
						(foundControl as TextBox).Text = row[columnIndex].ToString();
					}
					else if(foundControl is  DropDownList)
					{
						SetSelectedListItemSafe((foundControl as  DropDownList), row[columnIndex].ToString(), true);
					}
				}
			}
		}

		public static void SetControlValuesToRow(DataRow row)
		{
			for (int columnIndex = 0; columnIndex < row.Table.Columns.Count; columnIndex++)
			{
				Control foundControl = WebCommonFunctions.CurrentPage.FindControl("db_" + row.Table.Columns[columnIndex].ColumnName);

				if (foundControl != null)
				{
					if(foundControl is TextBox)
					{
						if ((foundControl as TextBox).Text=="")
						{
							if (row.Table.Columns[columnIndex].DataType == typeof(string))
							{
								row[columnIndex] = "";
							}
							else
							{
								row[columnIndex] = DBNull.Value;
							}
						}
						else
							row[columnIndex] = (foundControl as TextBox).Text;
					}
					else if(foundControl is  DropDownList)
					{
						row[columnIndex] = GetSelectedListItemValue((foundControl as  DropDownList));
					}
				}
			}
		}

		public static void FillListFromDataBase(ListControl paramListControl, Sytel.SyFastPage.AppData dataInstance, string tableName, string keyColumn, string valueColumn)
		{
			DataSet listData = dataInstance.GetTableData(tableName);
			if (listData.Tables.Contains(tableName)==true)
			{
				paramListControl.DataSource=listData;
				paramListControl.DataMember=tableName;
				paramListControl.DataTextField = valueColumn;
				paramListControl.DataValueField = keyColumn;
				paramListControl.DataBind();
			}
		}
	}
}
