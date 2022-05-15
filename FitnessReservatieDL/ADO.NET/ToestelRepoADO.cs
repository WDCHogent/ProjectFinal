using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace FitnessReservatieDL.ADO.NET
{
    public class ToestelRepoADO : IToestelRepository
    {
        private string connectieString;

        public ToestelRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectieString);
        }
        public IReadOnlyList<Toestel> GeefVrijToestelVoorGeselecteerdTijdslot(DateTime datum, string toesteltype, int beginuur, int einduur)
        {
            //if (klantnummer <= 0) throw new KlantRepoADOException("KlantRepoADO - GeefKlantReservaties - 'Ongeldige input'");
            string query = "SELECT t.toestelnummer,t.toestelnaam,t.status,tt.toesteltypeid,tt.toesteltypenaam FROM Toestel t " +
            "LEFT JOIN Toesteltype tt ON t.toesteltype = tt.toesteltypeid " +
            "WHERE t.[status]= 'operatief' AND tt.toesteltypenaam = @toesteltype AND t.toestelnummer " +
            "NOT IN (SELECT i.toestelnummer FROM ReservatieInfo i " +
            "LEFT JOIN Reservatie r ON i.reservatienummer = r.reservatienummer " +
            "WHERE r.datum LIKE @datum AND (i.beginuur BETWEEN @beginuur AND @einduur - 1 OR i.einduur BETWEEN @beginuur + 1 AND @einduur))";
            SqlConnection conn = GetConnection();
            List<Toestel> beschikbareToestellen = new List<Toestel>();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@toesteltype", toesteltype);
                cmd.Parameters.AddWithValue("@datum", datum.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@beginuur", beginuur);
                cmd.Parameters.AddWithValue("@einduur", einduur);
                conn.Open();
                try
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Status status = (Status)Enum.Parse(typeof(Status), (string)reader["status"]);
                        Toestel beschikbaarToestel = new Toestel((int)reader["toestelnummer"], (string)reader["toestelnaam"], status, new ToestelType((int)reader["toesteltypeid"], (string)reader["toesteltypenaam"]));
                        beschikbareToestellen.Add(beschikbaarToestel);
                    }
                    return beschikbareToestellen.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelRepoADO - GeefVrijToestelVoorGeselecteerdTijdslot", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
