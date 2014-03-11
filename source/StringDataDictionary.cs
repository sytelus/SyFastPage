using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Sytel.SyFastPage
{
	/// <summary>
	/// Summary description for StringDataDictionary.
	/// </summary>
	public class StringDataDictionary : StringDictionary 
	{
		private bool m_isDataLoaded = false;
		public bool IsDataLoaded
		{
			get
			{
				return m_isDataLoaded;
			}
			set
			{
				m_isDataLoaded = value;
			}
		}

		public void LoadData(string xmlFullFilePath, string tableName, string keyColumnName, string valueColumnName)
		{
			m_isDataLoaded = false;
			this.Clear();
			
			DataSet dataSetToLoadXml = new DataSet();
			dataSetToLoadXml.ReadXml(xmlFullFilePath);
			
			foreach (DataRow eachRow in dataSetToLoadXml.Tables[tableName].Rows)
			{
				this.Add((string) eachRow[keyColumnName], (string) eachRow[valueColumnName]);
			}
			m_isDataLoaded = true;
		}
	}
}
