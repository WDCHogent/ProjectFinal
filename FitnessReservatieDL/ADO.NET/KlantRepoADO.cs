using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace FitnessReservatieDL.ADO.NET
{
    public class KlantRepoADO : IKlantRepository
    {
        private string _connectiestring;

        public KlantRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectiestring);
        }

        public Klant SelecteerKlant(int? klantnummer, string mailadres)
        {
            if ((!klantnummer.HasValue) && (string.IsNullOrEmpty(mailadres)) == true) throw new KlantRepoADOException("KlantRepoADO - SelecteerKlant - 'Ongeldige input'");
            string query = "SELECT klantnummer,naam,voornaam,mailadres FROM Klant ";
            if (klantnummer.HasValue) query += "WHERE klantnummer=@klantnummer";
            else query += "WHERE mailadres=@mailadres"; 
            Klant klant = null;
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    if (klantnummer.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@klantnummer", klantnummer);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@mailadres", mailadres);
                    }
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        klant = new Klant((int)reader["klantnummer"], (string)reader["naam"], (string)reader["voornaam"], (string)reader["mailadres"]);
                    }
                    reader.Close();
                    return klant;
                }
                catch (Exception ex)
                {
                    throw new KlantRepoADOException("KlantRepoADO - SelecteerKlant", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<DTOKlantReservatieInfo> GeefKlantReservaties(int klantnummer)
        {
            if (klantnummer <= 0) throw new KlantRepoADOException("KlantRepoADO - GeefKlantReservaties - 'Ongeldige input'");
            string query = "SELECT r.reservatienummer, r.datum, i.beginuur, i.einduur, t.toestelnaam FROM Reservatie r " +
            "LEFT JOIN ReservatieInfo i ON r.reservatienummer = i.reservatienummer " +
            "LEFT JOIN toestel t ON i.toestelnummer = t.toestelnummer " +
            "LEFT JOIN Klant k ON r.klantnummer = k.klantnummer " +
            "WHERE r.klantnummer = @klantnummer ORDER BY r.datum, i.beginuur";
            SqlConnection conn = GetConnection();
            List<DTOKlantReservatieInfo> klantenreservaties = new List<DTOKlantReservatieInfo>();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@klantnummer", klantnummer);
                conn.Open();
                try
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DTOKlantReservatieInfo klantenreservatie = new DTOKlantReservatieInfo((int)reader["reservatienummer"],(DateTime)reader["datum"],(int)reader["beginuur"],(int)reader["einduur"],(string)reader["toestelNaam"]);
                        klantenreservaties.Add(klantenreservatie);
                    }
                    return klantenreservaties.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new KlantRepoADOException("KlantRepoADO - GeefKlantReservaties", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
