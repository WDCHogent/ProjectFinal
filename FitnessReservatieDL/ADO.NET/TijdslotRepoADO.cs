using FitnessReservatieBL.Domeinen.Eigenschappen;
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
    public class TijdslotRepoADO : ITijdslotRepository
    {
        private string _connectiestring;

        public TijdslotRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connectie = new SqlConnection(_connectiestring);
            return connectie;
        }

        public List<Tijdslot> SelecteerTijdslot()
        {
            string query = "SELECT * FROM Tijdslot";
            List<Tijdslot> tijdsloten = new List<Tijdslot>();
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
                        tijdsloten.Add(new Tijdslot((int)reader["tijdslot"]));
                    }
                    reader.Close();
                    return tijdsloten;
                }
                catch (Exception ex)
                {
                    throw new TijdslotADOException("SelecteerEinduur", ex);
                }
                finally { connection.Close(); }
            }
        }
    }
}
