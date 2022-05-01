using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;

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

        public bool BestaatToestelType(string toestelNaam)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT count(*) FROM dbo.ToestelType WHERE toestelNaam=@toestelNaam";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@toestelNaam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@toestelNaam"].Value = toestelNaam;

                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new ToestelTypeRepoADOException("ToestelTypeRepoADOBestaatToestelType", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public ToestelType SelecteerToestelType(string toestelNaam)
        {
            throw new NotImplementedException();
        }
    }
}
