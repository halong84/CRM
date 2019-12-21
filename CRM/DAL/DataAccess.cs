using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CRM.DAL
{
    class DataAccess
    {
        //public static SqlConnection conn = new SqlConnection(@"Server=2300-ws0242\SQLEXPRESS;Database=CRM;User Id=sa;Password=123456a@;");
        //public static SqlConnection conn = new SqlConnection(@"Server=10.14.0.30\SQLEXPRESS;Database=AGRIBANKHD;User Id=sa;Password=123456a@;"); 

        //public static string conString = @"Server=localhost\SQLEXPRESS;Database=CRM;User Id=sa;Password=123456a@;";
        //public static string conString = @"Server=localhost\SQLEXPRESS;Database=CRM;User Id=sa;Password=Sql@16pht;";
        public static string conString = @"Server=10.14.0.30\SQLEXPRESS;Database=CRM;User Id=sa;Password=Sql@16pht;";
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conString);
        public static SqlConnection conn = new SqlConnection(builder.ConnectionString);
        //string user = builder.UserID;
        //string pass = builder.Password;
        //string servername = conn.DataSource;
        //string database = conn.Database;

        private static SqlCommand cmd = new SqlCommand (); 
// <summary> 
// Open the database connection. 
// </ Summary> 

public void OpenConnection() 

{ 
if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
} 

// <summary> 
// Close the connection to the database. 
// </ Summary> 

public void CloseConnection() 

{ 
 if (conn.State == System.Data.ConnectionState.Open)
 conn.Close();

} 


private static void PrepareCommand (string procName, SqlTransaction trans, SqlParameter[] parms) 

{ 

cmd.Connection = conn; 

cmd.CommandText = procName; 

if (trans != null) 

cmd.Transaction = trans; 

cmd.CommandType = CommandType.StoredProcedure; 

if (parms != null) 

{ 

foreach (SqlParameter parm in parms) 

cmd.Parameters.Add (parm); 

} 

} 


#region to execute the query and returns the first column of the first row returned by the query result set, additional columns or rows are ignored, ExecuteScalar. 

// <summary> 
// Execute the query and returns the first column of the first row returned by the query result set, additional columns or rows are ignored. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <param Name="parms"> SqlParameter array </ param> 
// <returns> ExecuteScalar </ returns> 

public object cmdExecScalarProc (string procName, SqlParameter[] parms) 

{ 

Object obj = new Object (); 

PrepareCommand (procName, null, parms); 

OpenConnection (); 

obj = cmd.ExecuteScalar (); 

CloseConnection();

cmd.Parameters.Clear (); 

return obj; 

} 


// <summary> 
// Execute the query and returns the first column of the first row returned by the query result set, additional columns or rows are ignored. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <param Name="parms"> SqlParameter array </ param> 
// The the <param name="trans"> SQL Server database processing Transact-SQL transaction </ param> 
// <returns> ExecuteScalar </ returns> 

public object cmdExecScalarProc (string procName, SqlParameter[] parms, SqlTransaction trans) 

{ 

Object obj = new Object (); 

PrepareCommand (procName, trans, parms); 

OpenConnection (); 

obj = cmd.ExecuteScalar (); 

CloseConnection (); 

cmd.Parameters.Clear (); 

return obj; 

} 

# endregion 


#region Transact-SQL statements performed on the connection, ExecuteNonQuery. 

// <summary> 
// Transact-SQL statements performed on the connection. 
// </ Summary>
public int cmdExecNonQueryProc(string procName)
{
    int i = new int();
    PrepareCommand(procName, null, null);
    OpenConnection();
    i = cmd.ExecuteNonQuery();
    CloseConnection();
    cmd.Parameters.Clear();
    return i;
} 
// <summary> 
// Transact-SQL statements performed on the connection. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <param Name="parms"> SqlParameter array </ param> 

public int cmdExecNonQueryProc (string procName, SqlParameter[] parms) 

{
int i = new int();
PrepareCommand (procName, null, parms); 
OpenConnection (); 
i = cmd.ExecuteNonQuery(); 
CloseConnection (); 
cmd.Parameters.Clear ();
return i;
} 


// <summary> 
// Transact-SQL statements performed on the connection. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <param Name="parms"> SqlParameter array </ param> 
// The the <param name="trans"> SQL Server database processing Transact-SQL transaction </ param> 

public int cmdExecNonQueryProc (string procName, SqlParameter[] parms, SqlTransaction trans) 

{ 
int i = new int();

PrepareCommand (procName, trans, parms); 

OpenConnection (); 

i  = cmd.ExecuteNonQuery(); 

CloseConnection (); 

cmd.Parameters.Clear ();

return i;

} 

# endregion 


# region Back SqlDataReader. 

// <summary> 
// Return SqlDataReader, after use, please turn off the object, at the same time automatically to call CloseConnection () to close the database connection. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <param Name="parms"> SqlParameter array </ param> 
// <returns> SqlDataReader object </ returns> 

public SqlDataReader DataReader (string ProcName) 

{ 

SqlDataReader dr = null; 

PrepareCommand (ProcName, null, null); 

OpenConnection (); 

dr = cmd.ExecuteReader (CommandBehavior.CloseConnection); 

return dr; 

} 


// <summary> 
// Return SqlDataReader, after use, please turn off the object, at the same time automatically to call CloseConnection () to close the database connection. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <param Name="parms"> SqlParameter array </ param> 
// <returns> SqlDataReader object </ returns> 

public SqlDataReader DataReader (string procName, SqlParameter[] parms) 

{ 

SqlDataReader dr = null; 

PrepareCommand (procName, null, parms); 

OpenConnection (); 

dr = cmd.ExecuteReader (CommandBehavior.CloseConnection); 

cmd.Parameters.Clear (); 

return dr; 

} 


// <summary> 
// Return SqlDataReader, after use, please turn off the object, at the same time automatically to call CloseConnection () to close the database connection. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// The the <param name="trans"> SQL Server database processing Transact-SQL transaction </ param> 
// <returns> SqlDataReader object </ returns> 

public SqlDataReader DataReader (string procName, SqlTransaction trans) 

{ 

SqlDataReader dr = null; 

PrepareCommand (procName, trans, null); 

OpenConnection (); 

dr = cmd.ExecuteReader (CommandBehavior.CloseConnection); 

return dr; 

} 


// <summary> 
// Return SqlDataReader, after use, please turn off the object, at the same time automatically to call CloseConnection () to close the database connection. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <param Name="parms"> SqlParameter array </ param> 
// The the <param name="trans"> SQL Server database processing Transact-SQL transaction </ param> 
// <returns> SqlDataReader object </ returns> 

public SqlDataReader DataReader (string procName, SqlParameter [] parms, SqlTransaction trans) 

{ 

SqlDataReader dr = null; 

PrepareCommand (procName, trans, parms); 

OpenConnection (); 

dr = cmd.ExecuteReader (CommandBehavior.CloseConnection); 

cmd.Parameters.Clear (); 

return dr; 

} 

# endregion 


# region return to the memory data in a table, DataTable. 

// <summary> 
// Returns a table in the memory data. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <returns> DataTable </ returns> 

public DataTable dt(string ProcName) 

{ 

SqlDataAdapter da = new SqlDataAdapter(ProcName, conn); 

da.SelectCommand.CommandType = CommandType.StoredProcedure; 

DataTable dt = new DataTable (); 

da.Fill (dt); 

return dt; 

} 


// <summary> 
// Returns a table in the memory data. 
// </ Summary> 
// <param Name="procName"> stored procedure </ param> 
// <param Name="parms"> SqlParameter array </ param> 
// <returns> DataTable </ returns> 

public DataTable dt (string procName, SqlParameter [] parms) 

{ 

SqlDataAdapter da = new SqlDataAdapter (procName, conn); 

da.SelectCommand.CommandType = CommandType.StoredProcedure; 

DataTable dt = new DataTable (); 

for (int i = 0; i <parms.Length; i++) 

{ 

da.SelectCommand.Parameters.Add (parms [i]); 

} 

da.Fill (dt); 

cmd.Parameters.Clear (); 

return dt; 

} 

# endregion 
    }
}
