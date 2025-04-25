using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// This class contains all the functions used by the program to perform queries and execute them against the DB.
    /// </summary>
    public class AccesADades
    {
        private string connectionString;
        private SqlConnection conn;

        /// <summary>
        /// This function establishes a connection to the DB.
        /// </summary>
        /// <param name="connectionString">Connection info is required</param>
        public AccesADades(string connectionString)
        {
            ConnectionStringSettings conf = ConfigurationManager.ConnectionStrings[connectionString];
            if (conf != null) connectionString = conf.ConnectionString;
            this.connectionString = connectionString;
            Connectar();
        }
        /// <summary>
        /// This function gets the information for the connection and creates a new connection.
        /// </summary>
        private void Connectar()
        {
            conn = new SqlConnection(connectionString);
        }
        /// <summary>
        /// This function returns StringConnectionStringBuilder to use in Crystal Reports 
        /// </summary>
        /// <returns>StringConnectionStringBuilder with connection information</returns>
        public SqlConnectionStringBuilder GetStringConnectionBuilder()
        {
            return new SqlConnectionStringBuilder(connectionString);
        }

        /// <summary>
        /// This function returns all data contained in a table
        /// </summary>
        /// <param name="nomTaula">Name of the table to be consulted</param>
        /// <returns>DataSet with table contents</returns>
        public DataSet PortarTaula(string nomTaula)
        {
            SqlDataAdapter adapter;
            DataSet dts = new DataSet();

            string query = $"SELECT * FROM {nomTaula}";
            adapter = new SqlDataAdapter(query, conn);

            conn.Open();

            adapter.Fill(dts, nomTaula);

            conn.Close();

            return dts;
        }
        /// <summary>
        /// This function will return the information resulting from executing the given query
        /// </summary>
        /// <param name="query">SQL Query in plain text</param>
        /// <returns>DataSet with the resulting data</returns>
        public DataSet PortarPerConsulta(string query)
        {
            SqlDataAdapter adapter;
            DataSet dts = new DataSet();

            adapter = new SqlDataAdapter(query, conn);

            conn.Open();
            try
            {
                adapter.Fill(dts);
            }
            catch (Exception) { };

            conn.Close();

            return dts;
        }
        /// <summary>
        /// This function overloads PortarPerConsulta(string query) in order to add the name that the returned DataTable will have
        /// </summary>
        /// <param name="query">SQL Query in plain text</param>
        /// <param name="nomTaula">Name that the returned table will have</param>
        /// <returns>DataSet with the DataTable</returns>
        public DataSet PortarPerConsulta(string query, string nomTaula)
        {
            SqlDataAdapter adapter;
            DataSet dts = new DataSet();

            adapter = new SqlDataAdapter(query, conn);

            conn.Open();
            try
            {
                adapter.Fill(dts, nomTaula);
            }
            catch (Exception) { };

            conn.Close();

            return dts;
        }
        /// <summary>
        /// This function updates the database with the modified DataSet. 
        /// </summary>
        /// <param name="query">Original DataSet SQL query in plain Text</param>
        /// <param name="dts">DataSet with changes</param>
        /// <returns>Number of rows affected or -1 if exception ocurred</returns>
        public int Actualitzar(string query, DataSet dts)
        {
            int modificats = 0;
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);

            if (dts.HasChanges())
            {
                try
                {
                    modificats = adapter.Update(dts.Tables[0]);
                }
                catch (Exception)
                {
                    modificats = -1;
                }
            }

            conn.Close();

            return modificats;
        }
        /// <summary>
        /// This function executes the query received as parameter
        /// </summary>
        /// <param name="query">SQL Query in plain text</param>
        public void Executa(string query)
        {
            conn.Open();
            SqlCommand cmd;
            cmd = new SqlCommand(query, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
        /// <summary>
        /// This function generates a SqlCommand query that returns all values from the table matching the parameters
        /// </summary>
        /// <param name="tableName">Name of the table to be queried</param>
        /// <param name="values">Parameters to be checked in the query</param>
        /// <returns>Structured SQL query</returns>
        private SqlCommand GeneraConsultaCerca(string tableName, Dictionary<string, string> values)
        {
            SqlCommand command = conn.CreateCommand();
            string query = $"SELECT * FROM {tableName} WHERE 1 = 1";
            command.CommandType = CommandType.Text;

            foreach (var entry in values)
            {
                query += $" AND {entry.Key} = '{entry.Value}'";
                command.Parameters.Add(new SqlParameter(entry.Key, entry.Value));
            }
            query += ";";
            command.CommandText = query;

            return command;
        }
        /// <summary>
        /// This function generates a SqlCommand adding parameters to a given query in plain text
        /// </summary>
        /// <param name="query">Main query in plain text. Must contain at least one condition (i.e. WHERE 1 = 1)</param>
        /// <param name="values">Parameters to be checked in the query</param>
        /// <returns>Structured SQL query</returns>
        private SqlCommand GeneraConsultaCercaQuery(string query, Dictionary<string, string> values)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.Text;

            foreach (var entry in values)
            {
                query += $" AND {entry.Key} = '{entry.Value}'";
                command.Parameters.Add(new SqlParameter(entry.Key, entry.Value));
            }
            query += ";";
            command.CommandText = query;

            return command;
        }
        /// <summary>
        /// This function will return a sql query using the name of a procedure stored in the database.
        /// </summary>
        /// <param name="procedure">Name of the procedure stored in the DB</param>
        /// <returns>Structured SQL query</returns>
        private SqlCommand GeneraConsultaStoredProcedure(string procedure)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedure;
            return command;
        }
        /// <summary>
        /// This function uses the function of GeneraConsultaConsultaCerca() to perform a query with the given parameters
        /// </summary>
        /// <param name="tableName">Name of the table to be consulted</param>
        /// <param name="values">Values to be contained in the query</param>
        /// <returns>DataSet with the resulting data</returns>
        public DataSet ExecutaCerca(string tableName, Dictionary<string, string> values)
        {
            DataSet dts = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = GeneraConsultaCerca(tableName, values);
            adapter.SelectCommand = command;

            conn.Open();
            adapter.Fill(dts);
            conn.Close();

            return dts;
        }
        /// <summary>
        /// This function uses the function of GeneraConsultaConsultaCercaQuery() to perform a query with the given parameters
        /// </summary>
        /// <param name="query">Main query in plain Text</param>
        /// <param name="values">Values to be contained in the query</param>
        /// <returns>DataSet with the resulting data</returns>
        public DataSet ExecutaCercaQuery(string query, Dictionary<string, string> values)
        {
            DataSet dts = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = GeneraConsultaCercaQuery(query, values);
            adapter.SelectCommand = command;

            conn.Open();
            adapter.Fill(dts);
            conn.Close();

            return dts;
        }
        /// <summary>
        /// This function executes a procedure that is stored in the database.
        /// </summary>
        /// <param name="procedure">Name of the procedure stored in the DB</param>
        public void ExecutaStoredProcedure(string procedure)
        {
            SqlCommand command = GeneraConsultaStoredProcedure(procedure);
            ExecutaTransaccioNonQuery(command);
        }
        /// <summary>
        /// This function executes a SQL Query and returns the number of rows affected.
        /// </summary>
        /// <param name="command">Structured SQL Query</param>
        /// <returns>Number of rows affected</returns>
        private int ExecutaTransaccioNonQuery(SqlCommand command)
        {
            int modificats = -1;

            conn.Open();
            SqlTransaction sqlTran = conn.BeginTransaction();

            command.Transaction = sqlTran;
            try
            {
                modificats = command.ExecuteNonQuery();
                sqlTran.Commit();
            }
            catch (Exception)
            {
                sqlTran.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return modificats;
        }
        /// <summary>
        /// This function executes a SQL Query that returns a number (Example: Count).
        /// </summary>
        /// <param name="command">Structured SQL Query</param>
        /// <returns>Transaction result </returns>
        private int ExecutaTransaccioEscalar(SqlCommand command)
        {
            int result = -1;
            conn.Open();
            SqlTransaction sqlTran = conn.BeginTransaction();

            command.Transaction = sqlTran;
            try
            {
                result = (int)command.ExecuteScalar();
                sqlTran.Commit();
            }
            catch (Exception)
            {
                sqlTran.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

    }
}