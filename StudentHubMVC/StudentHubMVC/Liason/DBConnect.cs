using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentHubMVC.Liason
{
    public class DBConnect
    {
        MySqlConnection MyConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["bntstudenthub_db"].ConnectionString);


        // ---------------------------------- STATEMENT EXEC - INSERT / UPDDATE / DELETE ------------------------------------*/
        public void Exec_Statement(string statement)
        {
            MySqlCommand MyCmd = new MySqlCommand(statement, MyConn);

            if (MyConn.State == ConnectionState.Closed)
                MyConn.Open();

            int Response = MyCmd.ExecuteNonQuery();

            if (MyConn.State == ConnectionState.Open)
                MyConn.Close();
        }


        // ------------------------------------------------------------- GET DATA ------------------------------------*/
        public DataTable GetData(string statement)
        {
            MySqlDataAdapter MyDa = new MySqlDataAdapter(statement, MyConn);

            DataTable dt_Response = new DataTable();
            MyDa.Fill(dt_Response);

            return (dt_Response);
        }


        /* --------------------------------------------------- DYNAMIC PROCEDURE ------------------------------------*/

        public List<KeyValuePair<string, string>> ProcedureDynamic(string name, List<KeyValuePair<string, string>> parameters, string[] outparameters)
        {
            var list_return_out = new List<KeyValuePair<string, string>>();

            try
            {
                MySqlCommand Cmd_DyanamicProc = new MySqlCommand(name, MyConn);
                Cmd_DyanamicProc.CommandType = CommandType.StoredProcedure;

                foreach (var keyvalue in parameters)
                {
                    //cmd_procedure_dynamic.Parameters.Add(keyvalue.Key, SqlDbType.VarChar);
                    //cmd_procedure_dynamic.Parameters[keyvalue.Key].Value = keyvalue.Value;
                    Cmd_DyanamicProc.Parameters.AddWithValue(keyvalue.Key, keyvalue.Value);
                }

                foreach (string outparameter in outparameters)
                {
                    Cmd_DyanamicProc.Parameters.Add(outparameter, MySqlDbType.LongText);
                    Cmd_DyanamicProc.Parameters[outparameter].Direction = ParameterDirection.Output;
                }

                if (MyConn.State != System.Data.ConnectionState.Open)
                    MyConn.Open();

                Cmd_DyanamicProc.CommandTimeout = 500;
                    Cmd_DyanamicProc.ExecuteNonQuery();

                if (MyConn.State != System.Data.ConnectionState.Closed)
                    MyConn.Close();

                foreach (MySqlParameter param in Cmd_DyanamicProc.Parameters)
                {
                    if (param.Direction == ParameterDirection.Output)
                    {
                        list_return_out.Add(new KeyValuePair<string, string>(param.ParameterName, param.Value.ToString()));
                    }
                }
            }

            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                exceptions_final(trace, ex.Message.ToString());
            }

            return list_return_out;
        }


        /* ----------------------------------------------------- DATA ADAPTER PARAMETERS PROCEDURES ----------------------------- */
        // TWO PARAMETERS
        /// <summary>
        /// Returns Datatable.
        /// Gets data of the last SELECT statement executed in a PROCEDURE with 2 Parameters.
        /// </summary>
        /// <returns>Datatable data from SELECT</returns>
        public DataTable procedure_exec_2_data_adapter(string name, string param_1, string value_1, string param_2, string value_2)
        {
            DataTable dt_data = new DataTable();

            // data adapter
            MySqlDataAdapter da = new MySqlDataAdapter();

            // sql command
            MySqlCommand cmd_procedure = new MySqlCommand(name, MyConn);
            cmd_procedure.CommandType = CommandType.StoredProcedure;
            cmd_procedure.Parameters.Add(param_1, MySqlDbType.VarChar);
            cmd_procedure.Parameters[param_1].Value = value_1;
            cmd_procedure.Parameters.Add(param_2, MySqlDbType.VarChar);
            cmd_procedure.Parameters[param_2].Value = value_2;

            //cmd_procedure.Parameters.Add("@out", MySqlDbType.String, 100);
            //cmd_procedure.Parameters["@out"].Direction = ParameterDirection.Output;

            // adding sql cmd into adapter
            da.SelectCommand = cmd_procedure;

            cmd_procedure.CommandTimeout = 500;
            da.Fill(dt_data);

            return dt_data;
        }


        // TWO PARAMETERS - ONE OUTPUT
        /// <summary>
        /// Returns Datatable.
        /// Gets data of the last SELECT statement executed in a PROCEDURE with 2 Parameters.
        /// </summary>
        /// <returns>Datatable data from SELECT</returns>
        public DataTable ProcExec_2_Out(string name, string param_1, string value_1, string param_2, string value_2)
        {
            DataTable dt_data = new DataTable();

            // data adapter
            MySqlDataAdapter da = new MySqlDataAdapter();

            // sql command
            MySqlCommand cmd_procedure = new MySqlCommand(name, MyConn);
            cmd_procedure.CommandType = CommandType.StoredProcedure;
            cmd_procedure.Parameters.Add(param_1, MySqlDbType.VarChar);
            cmd_procedure.Parameters[param_1].Value = value_1;
            cmd_procedure.Parameters.Add(param_2, MySqlDbType.VarChar);
            cmd_procedure.Parameters[param_2].Value = value_2;

            cmd_procedure.Parameters.Add("@result", MySqlDbType.String, 100);
            cmd_procedure.Parameters["@result"].Direction = ParameterDirection.Output;

            // adding sql cmd into adapter
            da.SelectCommand = cmd_procedure;

            cmd_procedure.CommandTimeout = 500;
            da.Fill(dt_data);

            return dt_data;
        }


        /* --------------------------------------------------- DATA ADAPTER - OPEN ROWSET -------------------------------- */

        /// <summary>
        /// Return OPENROWSET Datatable
        /// </summary>
        /// <param name="file_name">FileName including FilePath</param>
        /// <param name="top">Interger-No of TOP Rows- 0 Indicates All Rows</param>
        /// <param name="hdr">YES or NO values - For Column Names</param>
        /// <returns></returns>
        public DataTable adapter_data_openrowset(string file_name, int top, string hdr)
        {
            string query = "SELECT ##TOP## * FROM OPENROWSET('Microsoft.Ace.Oledb.12.0', 'Excel 12.0;Database=" + file_name + ";HDR=" + hdr + "', 'SELECT * FROM [Sheet1$]')";
            //string query = "SELECT ##TOP## * FROM OPENROWSET('Microsoft.Ace.Oledb.12.0', 'Excel 12.0;Database=D:\\file-upload\\DailyTotalOrderPOUpdates_Template.xls;HDR=" + hdr + "', 'SELECT * FROM [Sheet1$]')";

            if (top == 0)
                query = query.Replace("##TOP## ", "");

            else if (top > 0)
                query = query.Replace("##TOP##", "TOP " + top.ToString());

            DataTable dt = GetData(query);

            return (dt);
        }


        /* ----------------------------------------------------- DATA ADAPTER DATASET PARAMETERS PROCEDURES ----------------------------- */
        // TWO PARAMETERS
        /// <summary>
        /// Returns DataSet.
        /// Gets data of the last SELECT statement executed in a PROCEDURE with 2 Parameters.
        /// </summary>
        /// <returns>DataSet data from SELECT</returns>
        public DataSet procedure_exec_2_data_adapter_dataset(string name, string param_1, string value_1, string param_2, string value_2)
        {
            DataSet ds_data = new DataSet();

            // data adapter
            MySqlDataAdapter da = new MySqlDataAdapter();

            // sql command
            MySqlCommand cmd_procedure = new MySqlCommand(name, MyConn);
            cmd_procedure.CommandType = CommandType.StoredProcedure;
            cmd_procedure.Parameters.Add(param_1, MySqlDbType.VarChar);
            cmd_procedure.Parameters[param_1].Value = value_1;
            cmd_procedure.Parameters.Add(param_2, MySqlDbType.VarChar);
            cmd_procedure.Parameters[param_2].Value = value_2;

            //cmd_procedure.Parameters.Add("@out", MySqlDbType.String, 100);
            //cmd_procedure.Parameters["@out"].Direction = ParameterDirection.Output;

            // adding sql cmd into adapter
            da.SelectCommand = cmd_procedure;

            cmd_procedure.CommandTimeout = 500;
            da.Fill(ds_data);

            return ds_data;
        }

        // ---------------------------------------------------------------------- GET SCALAR DATA - ONE VALUE FROM DB */

        public object scalar_data(string query)
        {
            object return_scalar_data = "";

            MySqlCommand cmd_all = new MySqlCommand();
            cmd_all.CommandText = query;
            cmd_all.Connection = MyConn;

            if (MyConn.State != System.Data.ConnectionState.Open)
                MyConn.Open();

            cmd_all.CommandTimeout = 2000;
            return_scalar_data = cmd_all.ExecuteScalar();

            if (MyConn.State != System.Data.ConnectionState.Closed)
                MyConn.Close();

            return return_scalar_data;
        }


        // ---------------------------------------------------------------------- GET SCALAR DATA - USING SQL COMMAND */

        public object scalar_data_sqlcommand(MySqlCommand cmd)
        {
            object return_scalar_data = "";

            cmd.Connection = MyConn;

            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                cmd.Connection.Open();

            cmd.CommandTimeout = 2000;
            return_scalar_data = cmd.ExecuteScalar();

            if (cmd.Connection.State != System.Data.ConnectionState.Closed)
                cmd.Connection.Close();

            return return_scalar_data;
        }


        public object non_query_sqlcommand(MySqlCommand cmd)
        {
            object return_scalar_data = "";

            cmd.Connection = MyConn;

            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                cmd.Connection.Open();

            cmd.CommandTimeout = 2000;
            return_scalar_data = cmd.ExecuteNonQuery();

            if (cmd.Connection.State != System.Data.ConnectionState.Closed)
                cmd.Connection.Close();

            return return_scalar_data;
        }


        // ---------------------------------------------------------------------- TO CHECK FOR AN ITEM - IF ALREADY IN THE DB OR NOT */

        //public bool already_exists(string query)
        //{
        //    object return_check = "";

        //    cmd_all.CommandText = query;
        //    cmd_all.Connection = conn_all;

        //    if (conn_all.State != System.Data.ConnectionState.Open)
        //        conn_all.Open();

        //    cmd_all.CommandTimeout = 500;
        //    return_check = cmd_all.ExecuteScalar();

        //    if (conn_all.State != System.Data.ConnectionState.Closed)
        //        conn_all.Close();

        //    if (return_check.ToString() == "0")
        //        return false;
        //    else
        //        return true;
        //}



        /* ----------------------------------------------------- LIST TO DATATABLE ----------------------------- */
        public DataTable ConvertListToDataTable(List<string> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }


        // ------------------------------------------------------- DATA TABLE TO JSON -------------------- //
        public String ConvertDataTableTojSonString(DataTable dataTable)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                   new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                tableRows.Add(row);
            }
            return serializer.Serialize(tableRows);
        }


        // ---------------------------------------------------------------------- READING DATA FROM MSACCESS */
        public DataTable GetMSAccessData(string path, string fileName, string tableName, string fieldName, string value, bool isNameSearch = false)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.Combine(path, fileName);
            string sqlString = "";

            if(isNameSearch)
            {
                sqlString = string.Format("SELECT TOP {3} * FROM [{0}] WHERE [{1}] = \"{2}\" OR [{1}] LIKE \"{2}%\" OR [{1}] LIKE \"%{2}%\";", tableName, fieldName, value, Constants_ConfigVars.ConfigMaximumResultsCount.ToString());
            }
            else
            {
                sqlString = string.Format("SELECT * FROM [{0}] WHERE [{1}] = \"{2}\";", tableName, fieldName, value);
            }

            DataTable dt = new DataTable();

            using (var con = new OleDbConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(sqlString, con))
                    {
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    new Help().ExcpetionsHandling(ex);
                    dt = null;
                }
                finally
                {
                    con.Close();
                }
            }

            return dt;
        }


        // ---------------------------------------------------------------------- READING DATA FROM EXCEL .xlsx */
        public DataTable GetExcelData(string path, string fileName, string sheetName, string fieldName, string value)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.Combine(path, fileName) + ";Extended Properties=\"Excel 12.0 Xml; HDR=YES;IMEX=1\"";
            string sqlString = string.Format("SELECT TOP 10 * FROM [{0}$] WHERE [" + fieldName + "] = \"{1}\";", sheetName, value);

            DataTable dt = new DataTable();

            using (var con = new OleDbConnection(connectionString))
            {
                try
                {
                    using (var cmd = new OleDbCommand(sqlString, con))
                    {
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    new Help().ExcpetionsHandling(ex);
                    dt = null;
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }

            return dt;
        }



        // ---------------------------------------------------------------------- EXCEPTIONS HANDLING BLOCK */

        public void exceptions_final(System.Diagnostics.StackTrace trace, string ex_message)
        {
            string for_trace = "";

            for (int i = 0; i < trace.FrameCount; i++)
            {
                if (trace.GetFrame(i).ToString().Contains(".cs"))
                {
                    for_trace += trace.GetFrame(i).ToString();
                }
            }

            exception_insert(ex_message.Replace("'", ""), for_trace.Replace("'", ""), trace.GetFrame(0).GetFileColumnNumber().ToString().Replace("'", ""));

        }


        // ---------------------------------------------------------------------- EXCEPTIONS INSERTING BLOCK */

        private void exception_insert(string exception_name, string exception_source, string exception_method)
        {
            if (exception_name != "Thread was being aborted.")
            {
                if (MyConn.State == System.Data.ConnectionState.Closed)
                    MyConn.Open();

                MySqlCommand cmd_exception_insert = new MySqlCommand("INSERT INTO shub_exceptions (ex_name, ex_location, ex_other) VALUES ('" + exception_name + "','" + exception_source + "','" + exception_method + "')", MyConn);

                cmd_exception_insert.ExecuteNonQuery();

                if (MyConn.State == System.Data.ConnectionState.Open)
                    MyConn.Close();

            }
        }

    }
}