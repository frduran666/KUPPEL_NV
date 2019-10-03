using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public sealed class DBConector
    {
        public DataTable EjecutarProcedimientoAlmacenado(string spName, System.Collections.Hashtable sqlParametersIn)
        {
            return this.ObtenerProcedimientoAlmacenado(spName, sqlParametersIn, null, null);
        }

        private DataTable ObtenerProcedimientoAlmacenado(string spName, System.Collections.Hashtable sqlParametersIn, SqlParameter singleParameter, SqlTransaction transaccion)
        {
            var sqlCommand = new SqlCommand(spName);
            var adapter = new SqlDataAdapter();
            var aData = new DataTable();
            if (sqlParametersIn != null && sqlParametersIn.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry sqlParameter in sqlParametersIn)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.Key.ToString(), sqlParameter.Value));
                }
            }
            else if (singleParameter != null)
            {
                sqlCommand.Parameters.Add(singleParameter);
            }
            try
            {
                sqlCommand.Connection = new SqlConnection(DataSource.coneccionPrimaria);
                sqlCommand.Connection.Open();
                if (transaccion != null)
                {
                    sqlCommand.Transaction = transaccion;
                }
                sqlCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(aData);
                return aData;
            }
            catch
            {
                try
                {
                    sqlCommand.Connection = new SqlConnection(DataSource.coneccionPrimaria);
                    sqlCommand.Connection.Open();
                    if (transaccion != null)
                    {
                        sqlCommand.Transaction = transaccion;
                    }
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand = sqlCommand;
                    adapter.Fill(aData);
                    return aData;
                }
                catch (SqlException e)
                {
                    throw (new CapturaExcepciones(e));
                }
                catch (Exception e)
                {
                    throw (new CapturaExcepciones(e));
                }
            }
            finally
            {
                adapter = null;
                aData = null;
                sqlCommand.Connection.Close();
                sqlCommand = null;
            }

        }
    }
}
