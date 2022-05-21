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
        private string _connectiestring;

        public ToestelTypeRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connectie = new SqlConnection(_connectiestring);
            return connectie;
        }

        public IReadOnlyList<ToestelType> SelecteerToestelType()
        {
            string query = "SELECT * FROM Toesteltype";
            List<ToestelType> toesteltypes = new List<ToestelType>();
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();
                try
                {
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        toesteltypes.Add(new ToestelType((int)reader["toesteltypeid"], (string)reader["toesteltypenaam"]));
                    }
                    reader.Close();
                    return toesteltypes;
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("SelecteerToestelType", ex);
                }
                finally { connection.Close(); }
            }
        }
    }
}
