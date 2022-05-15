using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.ADO.NET
{
    public class ReservatieInfoRepoADO : IReservatieInfoRepository
    {
        private string connectieString;

        public ReservatieInfoRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectieString);
        }

        public void MaakReservatieInfo(ReservatieInfo reservatieinfo)
        {
            SqlConnection conn = GetConnection();
            string query = "INSERT INTO ReservatieInfo(reservatienummer,beginuur,einduur,toestelnummer) " +
            "VALUES(@reservatienummer,@beginuur,@einduur,@toestelnummer)";
            try
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@reservatienummer", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@beginuur", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@einduur", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@toestelnummer", System.Data.SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@reservatienummer"].Value = reservatieinfo.Reservatienummer;
                    cmd.Parameters["@beginuur"].Value = reservatieinfo.Beginuur;
                    cmd.Parameters["@einduur"].Value = reservatieinfo.Einduur;
                    cmd.Parameters["@toestelnummer"].Value = reservatieinfo.Toestel.ToestelNummer;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieInfoRepoADOException("MaakReservatieInfo", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
