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
    public class ToestelRepoADO : IToestelRepository
    {
        private string connectieString;

        public ToestelRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        private SqlConnection getConnection()
        {
            return new SqlConnection(connectieString);
        }

        //public Toestel GeefVrijToestel(int klantnummer, string datum, string beginuur, string einduur, string toesteltype)
        //{
        //    if (klantnummer == 0 || DateTime.TryParse(datum, out DateTime reservatiedatum) || int.TryParse(beginuur.TrimEnd(beginuur[beginuur.Length-1]), out int starttijdslot) || int.TryParse(einduur.TrimEnd(einduur[einduur.Length - 1]), out int eindtijdslot) || toesteltype==null) throw new ToestelRepoADOException("ToestelRepoADO - GeefVrijToestel - 'Ongeldige input'");
        //    string query = "SELECT klantnummer,naam,voornaam,mailadres FROM Klant ";
        //    if (klantnummer.HasValue) query += "WHERE klantnummer=@klantnummer";
        //    else query += "WHERE mailadres=@mailadres";
        //    Klant klant = null;
        //    SqlConnection conn = GetConnection();
        //    using (SqlCommand cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandText = query;
        //        conn.Open();
        //        try
        //        {
        //            if (klantnummer.HasValue)
        //            {
        //                cmd.Parameters.AddWithValue("@klantnummer", klantnummer);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@mailadres", mailadres);
        //            }
        //            IDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                klant = new Klant((int)reader["klantnummer"], (string)reader["naam"], (string)reader["voornaam"], (string)reader["mailadres"]);
        //            }
        //            reader.Close();
        //            return klant;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new KlantRepoADOException("KlantRepoADO - SelecteerKlant", ex);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}
    }
}
