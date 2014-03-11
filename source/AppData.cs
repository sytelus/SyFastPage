using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.IO;


namespace Sytel.SyFastPage
{
	/// <summary>
	/// Summary description for AppData.
	/// </summary>
	public class AppData
	{
		private string m_ConectionString = null;
		public AppData():this("MainData")
		{
		}
		public AppData(string instanceName)
		{
			m_ConectionString = CommonFunctions.GetConnectionStringForAccess(WebCommonFunctions.GetFullPathOnServer( (string) ConfigurationSettings.AppSettings[instanceName]));
		}

		public DataSet GetTableData(string tableName, string dataSetTableName )
		{
			return GetTableData(tableName, null, null, null, dataSetTableName);
		}
		public DataSet GetTableData(string tableName)
		{
			return GetTableData(tableName, null, null, null,tableName);
		}
		public DataSet GetTableData(string tableName, string[] columnNames, object[] columnValues)
		{
			return GetTableData(tableName, null, columnNames, columnValues,tableName);
		}
		public DataSet GetTableData(string tableName, DataSet dataSetToFill, string[] columnNames, object[] columnValues, string dataSetTableName)
		{
			DataSet dataSetToUse = dataSetToFill;
			if (dataSetToUse == null) dataSetToUse = new DataSet();
			OleDbDataAdapter adapter = GetAdapterForSelect(tableName, columnNames, columnValues);
			adapter.Fill(dataSetToUse, dataSetTableName);

			return dataSetToUse;
		}
		
		public void UpdateTableData(DataSet paramDataSet, string tableName)
		{
			OleDbDataAdapter adapter = GetAdapterForSelect(tableName, null, null);
			using (OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter))
			{
				adapter.Update(paramDataSet, tableName);
			}
		}

		public OleDbDataAdapter GetAdapterForSelect(string tableName, string[] columnNames, object[] columnValues)
		{
			OleDbCommand selectCommand = new OleDbCommand();
			string selectCommandText;
			if (tableName.StartsWith("sql:") )
			{
				selectCommandText = tableName.Substring(4);
			}
			else
			{
				selectCommandText = "SELECT * FROM " + tableName;
			}

			if (columnNames != null)
			{
				string whereClause = "";
				for(int parameterIndex =0;parameterIndex < columnNames.Length;parameterIndex++)
				{
					if (whereClause != "") whereClause += " AND ";
					whereClause += "(" + columnNames[parameterIndex] + "=" + "@" + columnNames[parameterIndex] + ")";
					OleDbParameter newParam = selectCommand.Parameters.Add("@" + columnNames[parameterIndex], null);
					newParam.SourceColumn = columnNames[parameterIndex];
					newParam.Value = columnValues[parameterIndex];
				}
				if (whereClause != "") 	selectCommandText += " WHERE " + whereClause;
			}
			selectCommand.CommandText = selectCommandText;
			selectCommand.Connection = new OleDbConnection(m_ConectionString);
			return new OleDbDataAdapter(selectCommand);
		}
	}
}
