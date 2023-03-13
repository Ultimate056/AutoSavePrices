using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSavePrices
{
    public static class DBExecutor
    {
        private const string stringConnection = "Data Source = DBSRV\\DBSRV; Initial Catalog = TEST; Integrated Security = True";
        public static DataTable getDataTable(Client client)
        {
            try
            {
                SqlConnection connection = new SqlConnection(stringConnection);
                connection.Open();
                DataSet Ds = new DataSet();
                // генерит 17-20 секунд
                SqlDataAdapter da = new SqlDataAdapter(GeneratorPricesDB.sqlGeneratePrices, connection);
                AddParameters(da.SelectCommand, GeneratorPricesDB.Convert(client));
                da.Fill(Ds);

                da.Dispose();
                connection.Close();
                return Ds.Tables[0];
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog("Выполнение SQL запроса", 1, ex.Message);
                return null;
            }
        }

        private static void AddParameters(SqlCommand sqlCommand, SqlParameter[] Parameters)
        {
            if (Parameters != null && Parameters.Count() > 0)
            {
                foreach (var par in Parameters)
                    sqlCommand.Parameters.Add(par);
            }
        }
    }
}
