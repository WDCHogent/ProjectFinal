using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace FitnessReservatieDL.ADO.NET
{
    public class ToestelTypeRepoADO : IToestelTypeRepository
    {
        private readonly string _connectiestring;

        public ToestelTypeRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectiestring);
        }

        public IReadOnlyList<ToestelType> SelecteerToestelType()
        {
            string query = "SELECT * FROM Toesteltype";
            List<ToestelType> toesteltypes = new ();
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
                        toesteltypes.Add(new ((int)reader["toesteltypeid"], (string)reader["toesteltypenaam"]));
                    }
                    reader.Close();
                    return toesteltypes;
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelTypeRepoADO - SelecteerToestelType", ex);
                }
                finally { conn.Close(); }
            }
        }

        public int GeefToestelTypeNummer(string toestelTypeNaam)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT toesteltypeid FROM Toesteltype WHERE toesteltypenaam=@toesteltypenaam";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.AddWithValue("@toesteltypenaam", toestelTypeNaam);
                    cmd.CommandText = query;

                    int toesteltypeid = 0;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        toesteltypeid = ((int)reader["toesteltypeid"]);
                    }
                    return toesteltypeid;

                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelTypeRepoADO - GeefToestelTypeNummer", ex);
                }
                finally { conn.Close(); }
            }
        }
    }
}
