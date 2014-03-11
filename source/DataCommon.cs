using System;
using System.Data;

namespace Sytel.SyFastPage
{
	/// <summary>
	/// Summary description for DataCommon.
	/// </summary>
	public class DataCommon
	{

		public static void LoadRowValues(DataRow row, string[] columnNames, object[] columnValues)
		{
			for(int columnIndex=0; columnIndex < columnNames.Length; columnIndex++)
			{
				row[columnNames[columnIndex]] = columnValues[columnIndex];
			}
		}
	}
}
