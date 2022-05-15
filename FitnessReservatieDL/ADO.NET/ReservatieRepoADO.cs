using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.ADO.NET
{
    public class ReservatieRepoADO : IReservatieRepository
    {
        private string _connectiestring;

        public ReservatieRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectiestring);
            return connection;
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
                    throw new ReservatieRepoADOException("BestaatReservatie", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<DTOReservatieInfo> SelecteerReservatie(int? klantnummer, int? toestelnummer)
        {
            throw new NotImplementedException();
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
                throw new ReservatieRepoADOException("MaakReservatie", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public Reservatie GeefReservatie(Reservatie reservatie)
        {
            if (reservatie==null) throw new ReservatieRepoADOException("ReservatieRepoADOE - SelecteerReservatie - 'Ongeldige input'");
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
                        geselecteerdeReservatie = new Reservatie((int)reader["reservatienummer"], reservatie.Klant, reservatie.Datum);
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
    }
}
