﻿using FitnessReservatieBL.Domeinen;
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
    public class KlantRepoADO : IKlantRepository
    {

        private string _connectiestring;

        public KlantRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectiestring);
            return connection;
        }

        public Klant SelecteerKlant(int? klantnummer, string mailadres)
        {
            if ((!klantnummer.HasValue) && (string.IsNullOrEmpty(mailadres)) == true) throw new KlantRepoADOException("KlantRepoADO - SelecteerKlant - 'Ongeldige input'");
            string query = "SELECT * FROM klant";
            if (klantnummer.HasValue) query += "WHERE klantnummer=@klantnummer";
            else query += "WHERE mailadres=@mailadres";
            SqlConnection conn = GetConnection();
            Klant klant = null;
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    if (klantnummer.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@id", klantnummer);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@mailadres", mailadres);
                    }
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        klant = new Klant((int)reader["klantnummer"], (string)reader["naam"], (string)reader["voornaam"], (string)reader["mailadres"]);
                    }
                    return klant;
                }
                catch (Exception ex)
                {
                    throw new KlantRepoADOException("KlantRepoADO - SelecteerKlant", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<KlantReservatieInfo> GeefKlantReservaties(int klantnummer)
        {
            if (klantnummer <= 0) throw new KlantRepoADOException("KlantRepoADO - GeefKlantReservaties - 'Ongeldige input'");
            string query = "SELECT klantreservaties " +
                "FROM dbo.Klant " +
                "WHERE klantnummer = @klantnummer";
            SqlConnection conn = GetConnection();
            List<KlantReservatieInfo> teams = new List<KlantReservatieInfo>();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    if (stamnummer.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@stamnummer", stamnummer);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@naam", naam);
                    }
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int stamnummerTeam = (int)reader["stamnummer"];
                        string teamNaam = (string)reader["naam"];
                        string teamBijnaam = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("teamBijnaam"))) teamBijnaam = (string)reader["teamBijnaam"];

                        TeamInfo teaminfo = new TeamInfo(stamnummerTeam, teamNaam, teamBijnaam);
                        teams.Add(teaminfo);
                    }
                    return teams.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new TeamRepoADOException("GeefTeams", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}