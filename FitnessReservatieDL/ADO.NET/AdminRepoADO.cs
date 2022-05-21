using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace FitnessReservatieDL.ADO.NET
{
    public class AdminRepoADO : IAdminRepository
    {
        private string _connectiestring;

        public AdminRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectiestring);
        }

        public Admin SelecteerAdmin(string adminnummer)
        {
            if (string.IsNullOrEmpty(adminnummer)) throw new AdminRepoADOException("AdminRepoADO - SelecteerAdmin - 'Ongeldige input'");
            string query = "SELECT adminnummer,naam,voornaam FROM Admin " +
            "WHERE adminnummer=@adminnummer";
            Admin admin = null;
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    cmd.Parameters.AddWithValue("@adminnummer", adminnummer);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        admin = new Admin((string)reader["adminnummer"], (string)reader["naam"], (string)reader["voornaam"]);
                    }
                    reader.Close();
                    return admin;
                }
                catch (Exception ex)
                {
                    throw new AdminRepoADOException("AdminRepoADO - SelecteerAdmin", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
