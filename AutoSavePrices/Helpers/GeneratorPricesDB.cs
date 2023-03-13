using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSavePrices
{
    public static class GeneratorPricesDB
    {
        public static string sqlGeneratePrices = @"exec sp_getPriceImag 
                @idKontr, 
                @idKategory,
                @idDirect,
                @idCur,
                @inPrice,
                @idTerritory,
                @fNotWithoutGTD,
                @ListIdTopLevel,
                @Listid_tov_line,
                @Listidpresence,
                @Listidadvancement,
                @Listid_tm,
                @Listidcourse
            ";

        public static SqlParameter[] Convert(Client client)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@idKontr", client.idKontr),
                new SqlParameter("@idKategory", client.idKategory),
                new SqlParameter("@idDirect", client.idDirect),
                new SqlParameter("@idCur", client.idCur),
                new SqlParameter("@inPrice", client.inPrice),
                new SqlParameter("@idTerritory", client.idTerritory),
                new SqlParameter("@fNotWithoutGTD", client.fnotWithoutGTD),

                new SqlParameter("@ListIdTopLevel", client.ListId_TopLevel),
                new SqlParameter("@Listid_tov_line", client.Listid_tov_line),
                new SqlParameter("@Listidpresence", client.Listid_presense),
                new SqlParameter("@Listidadvancement", client.Listid_advancement),
                new SqlParameter("@Listid_tm", client.Listid_tm),
                new SqlParameter("@Listidcourse", client.Listid_course)
            }.ToArray();
        }
    }
}
