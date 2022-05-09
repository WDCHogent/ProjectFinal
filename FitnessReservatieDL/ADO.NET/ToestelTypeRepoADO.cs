using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.DTO;
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

        public IReadOnlyList<ToestelTypeInfo> SelecteerToestelOpToestelType()
        {
            string query = "SELECT toesteltypenaam FROM Toesteltype";
            List<ToestelTypeInfo> toesteltypes = new List<ToestelTypeInfo>();
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
                        toesteltypes.Add(new ToestelTypeInfo((string)reader["toesteltypenaam"]));
                    }
                    reader.Close();
                    return toesteltypes;
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("SelecteerToestelOpToestelType", ex);
                }
                finally { connection.Close(); }
            }
        }
    }
}
