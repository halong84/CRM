#region Imports
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

#endregion

namespace N_MicrosoftExcelClient
{
	/// <summary>
	/// A client which interfaces to excel work books
	/// </summary>
	public class MicrosoftExcelClient
	{
		
		#region Variable Declarations

		/// <summary>
		/// Current message from the client
		/// </summary>
		string m_CurrentMessage = "";

		/// <summary>
		/// The file path for the source excel book
		/// </summary>
		string m_SourceFileName;

		/// <summary>
		/// Connects to the source excel workbook
		/// </summary>
		OleDbConnection m_ConnectionToExcelBook;

		/// <summary>
		/// Reads the data from the document to a System.Data object
		/// </summary>
		OleDbDataAdapter m_AdapterForExcelBook;

		#endregion
		
		#region Constructor Logic

		/// <summary>
		/// Parameterized constructor .. specifies path
		/// </summary>
		/// <param name="iSourceFileName">The source filename</param>
		public MicrosoftExcelClient(string iSourceFileName)
		{
		
			this.m_SourceFileName = iSourceFileName;

		}

		
		#endregion

		#region Properties

		/// <summary>
		/// Gets he current messages from the client
		/// </summary>
		public string CurrentMessage
		{
		
			get
			{
				return this.m_CurrentMessage;
			}

		}

		/// <summary>
		/// Property that gets / sets the current source excel book
		/// </summary>
		public string FileName
		{
		
			get
			{
				return this.m_SourceFileName;
			}
			set
			{
				this.m_SourceFileName = value;
			}
		
		}

		#endregion

		#region Methods

		/// <summary>
		/// Runs a non query database command such as UPDATE , INSERT or DELETE
		/// </summary>
		/// <param name="iQuery">The required query</param>
		/// <returns></returns>
		public bool runNonQuery(string iQuery)
		{
		
			try
			{
			
				
				OleDbCommand nonQueryCommand = new OleDbCommand(iQuery);
				
				nonQueryCommand.Connection = this.m_ConnectionToExcelBook;
				nonQueryCommand.CommandText = iQuery;
				
				int rowsAffected = nonQueryCommand.ExecuteNonQuery();

				this.m_CurrentMessage = "SUCCESS - " + rowsAffected.ToString() + " Rows affected ";				

				
				return true;
			
			}
			catch(Exception ex)
			{
				
				this.m_CurrentMessage = "ERROR "  + ex.Message;
				MessageBox.Show(ex.Message,"Error Editing Source",MessageBoxButtons.OK,MessageBoxIcon.Error);		
				return false;
			}

			
		}

		/// <summary>
		/// Reads data as per the user query
		/// </summary>
		/// <param name="iQuery">The speicfic Query</param>
		/// <returns></returns>
		public DataTable readForSpecificQuery(string iQuery)
		{
			try
			{			
				DataTable returnDataObject = new DataTable();
				OleDbCommand selectCommand = new OleDbCommand(iQuery);
				selectCommand.Connection = this.m_ConnectionToExcelBook;
				this.m_AdapterForExcelBook = new OleDbDataAdapter();			
				this.m_AdapterForExcelBook.SelectCommand = selectCommand;
				this.m_AdapterForExcelBook.Fill(returnDataObject);
				this.m_CurrentMessage = "SUCCESS - " +  returnDataObject.Rows.Count + " Records Loaded ";				
				return returnDataObject;			
			}
			catch(Exception ex)
			{
				this.m_CurrentMessage = "ERROR "  + ex.Message;
				MessageBox.Show(ex.Message,"Error Reading Source",MessageBoxButtons.OK,MessageBoxIcon.Error);		
				return null;
			}	
		}

		/// <summary>
		/// Reads an entire excel sheet from an opened excel workbook
		/// </summary>
		/// <param name="iSheetName"></param>
		/// <returns></returns>
		public DataTable readEntireSheet(string iSheetName)
		{
		
			try
			{			
				DataTable returnDataObject = new DataTable();

				OleDbCommand selectCommand = new OleDbCommand("select * from [" + iSheetName + "$]");
				selectCommand.Connection = this.m_ConnectionToExcelBook;

				this.m_AdapterForExcelBook = new OleDbDataAdapter();
								
				this.m_AdapterForExcelBook.SelectCommand = selectCommand;
				this.m_AdapterForExcelBook.Fill(returnDataObject);

				this.m_CurrentMessage = "SUCCESS - " +  returnDataObject.Rows.Count + " Records Loaded ";				

				return returnDataObject;
			
			}
			catch
			{
				
				MessageBox.Show("Không đọc được file excel!");		
				return null;
			}

		}

        /// <summary>
        /// Opens the connection to the source excel document
        /// </summary>
        /// <returns></returns>
		public bool openConnection()
		{		
			try
			{		
				this.m_ConnectionToExcelBook = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.m_SourceFileName + ";Extended Properties=Excel 8.0;");
                //this.m_ConnectionToExcelBook = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this.m_SourceFileName + ";Extended Properties=Excel 8.0;");
				
				this.m_ConnectionToExcelBook.Open();

				this.m_CurrentMessage = "SUCCESS - Connection to Source Established";		
			}
			catch
			{
                MessageBox.Show("Không đọc được file excel!");
                return false;
			}
			return true;
		}
		/// <summary>
		/// Closes the connection to the source excel document
		/// </summary>
		/// <returns></returns>
		public bool closeConnection()
		{	
			try
			{									
				this.m_ConnectionToExcelBook.Close();

				this.m_CurrentMessage = "SUCCESS - Connection to Source Closed";	
			}
            catch
            {
                MessageBox.Show("Không đọc được file excel !");
                return false;
            }
			return true;
		}
		#endregion

	}
}
