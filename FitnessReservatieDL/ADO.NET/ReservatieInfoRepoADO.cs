using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace FitnessReservatieDL.ADO.NET
{
    public class ReservatieInfoRepoADO : IReservatieInfoRepository
    {
        private readonly string connectieString;

        public ReservatieInfoRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectieString);
        }
        public ReservatieInfo ValideerReservatieInfo(DateTime datum, int beginuur, int einduur, Toestel toestel)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT count(*) FROM ReservatieInfo i " +
            "LEFT JOIN Reservatie r ON i.reservatienummer=r.reservatienummer " +
            "WHERE r.datum=@datum AND (i.beginuur BETWEEN @beginuur AND @einduur-1 OR i.einduur BETWEEN @beginuur+1 AND @einduur) AND i.toestelnummer=@toestelnummer";

            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@datum", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@beginuur", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@einduur", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@toestelnummer", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@datum"].Value = datum.ToString("yyyy-MM-dd");
                    cmd.Parameters["@beginuur"].Value = beginuur;
                    cmd.Parameters["@einduur"].Value = einduur;
                    cmd.Parameters["@toestelnummer"].Value = toestel.ToestelNummer;
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return null;
                    else return new ReservatieInfo(beginuur, einduur, toestel);
                }
                catch (Exception ex)
                {
                    throw new ReservatieInfoRepoADOException("ReservatieInfoRepo - ValideerReservatie", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void MaakReservatieInfo(Reservatie reservatie, Toestel toestel, int beginuur, int einduur)
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
                    cmd.Parameters["@reservatienummer"].Value = reservatie.Reservatienummer;
                    cmd.Parameters["@beginuur"].Value = beginuur;
                    cmd.Parameters["@einduur"].Value = einduur;
                    cmd.Parameters["@toestelnummer"].Value = toestel.ToestelNummer;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieInfoRepoADOException("ReservatieInfoRepo - MaakReservatieInfo", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
