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
    public class ReservatieRepoADO : IReservatieRepository
    {
        private readonly string _connectiestring;

        public ReservatieRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectiestring);
        }

        public bool BestaatReservatie(Reservatie reservatie)
        {
            SqlConnection connection = GetConnection();
            string query = "SELECT count(*) FROM Reservatie WHERE klantnummer=@klantnummer AND datum=@datum";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@klantnummer", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@datum", SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@klantnummer"].Value = reservatie.Klant.Klantnummer;
                    command.Parameters["@datum"].Value = reservatie.Datum.ToString("yyyy-MM-dd");
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new ReservatieRepoADOException("ReservatieRepoADO - BestaatReservatie", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Reservatie MaakReservatie(Reservatie reservatie)
        {
            SqlConnection conn = GetConnection();
            string query = "INSERT INTO Reservatie(klantnummer,datum) " +
            "OUTPUT INSERTED.reservatienummer VALUES(@klantnummer,@datum)";
            try
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@klantnummer", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@datum", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@klantnummer"].Value = reservatie.Klant.Klantnummer;
                    cmd.Parameters["@datum"].Value = reservatie.Datum.ToString("yyyy-MM-dd");
                    cmd.CommandText = query;
                    int nieuwReservatienummer = (int)cmd.ExecuteScalar();
                    reservatie.ZetReservatienummer(nieuwReservatienummer);
                    return reservatie;
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieRepoADOException("ReservatieRepoADO - MaakReservatie", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public Reservatie GeefReservatie(Reservatie reservatie)
        {
            if (reservatie == null) throw new ReservatieRepoADOException("ReservatieRepoADOE - SelecteerReservatie - 'Ongeldige input'");
            string query = "SELECT reservatienummer FROM Reservatie WHERE klantnummer=@klantnummer AND datum=@datum";
            Reservatie geselecteerdeReservatie = null;
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    cmd.Parameters.AddWithValue("@klantnummer", reservatie.Klant.Klantnummer);
                    cmd.Parameters.AddWithValue("@datum", reservatie.Datum.ToString("yyyy-MM-dd"));
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        geselecteerdeReservatie = new ((int)reader["reservatienummer"], reservatie.Klant, reservatie.Datum);
                    }
                    reader.Close();
                    return geselecteerdeReservatie;
                }
                catch (Exception ex)
                {
                    throw new ReservatieRepoADOException("ReservatieRepoADO - GeefReservatie", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<DTOReservatieInfo> ZoekReservatie(int? reservatienummer, int? klantnummer, int? toestelnummer, DateTime? datum)
        {
            string query = "SELECT r.reservatienummer, k.klantnummer, k.voornaam, k.naam, k.mailadres, r.datum, i.beginuur, i.einduur, t.toestelnaam FROM Reservatie r " +
                "LEFT JOIN Klant k ON r.klantnummer=k.klantnummer " +
                "LEFT JOIN ReservatieInfo i ON r.reservatienummer=i.reservatienummer " +
                "LEFT JOIN Toestel t ON i.toestelnummer=t.toestelnummer ";
            if (reservatienummer > 0) query += "WHERE r.reservatienummer=@reservatienummer";
            else if (klantnummer > 0) query += "WHERE r.klantnummer=@klantnummer";
            else if (toestelnummer > 0) query += "WHERE i.toestelnummer=@toestelnummer";
            else if (datum != null) query += "WHERE r.datum=@datum";
            List<DTOReservatieInfo> reservaties = new ();
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    if (reservatienummer > 0) cmd.Parameters.AddWithValue("@reservatienummer", reservatienummer);
                    else if (klantnummer > 0) cmd.Parameters.AddWithValue("@klantnummer", klantnummer);
                    else if (toestelnummer > 0) cmd.Parameters.AddWithValue("@toestelnummer", toestelnummer);
                    else if (datum != null) cmd.Parameters.AddWithValue("@datum", datum);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DTOReservatieInfo reservatie = new ((int)reader["reservatienummer"], (int)reader["klantnummer"], (string)reader["naam"], (string)reader["voornaam"], (string)reader["mailadres"], (DateTime)reader["datum"], (int)reader["beginuur"], (int)reader["einduur"], (string)reader["toestelnaam"]);
                        reservaties.Add(reservatie);
                    }
                    reader.Close();
                    return reservaties.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new ReservatieRepoADOException("ReservatielRepoADO - ZoekReservatie", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
