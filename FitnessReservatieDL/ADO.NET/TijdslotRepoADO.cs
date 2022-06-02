using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace FitnessReservatieDL.ADO.NET
{
    public class TijdslotRepoADO : ITijdslotRepository
    {
        private readonly string _connectiestring;

        public TijdslotRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectiestring);
        }

        public IReadOnlyList<Tijdslot> SelecteerTijdslot()
        {
            string query = "SELECT * FROM Tijdslot";
            List<Tijdslot> tijdsloten = new ();
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tijdsloten.Add(new ((int)reader["tijdslot"]));
                    }
                    reader.Close();
                    return tijdsloten;
                }
                catch (Exception ex)
                {
                    throw new TijdslotADOException("TijdslotRepoADO - SelecteerTijdslot", ex);
                }
                finally { conn.Close(); }
            }
        }
    }
}
