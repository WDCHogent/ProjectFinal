﻿using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieDL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace FitnessReservatieDL.ADO.NET
{
    public class ToestelRepoADO : IToestelRepository
    {
        private string connectieString;

        public ToestelRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectieString);
        }
        public IReadOnlyList<Toestel> GeefVrijToestelVoorGeselecteerdTijdslot(DateTime datum, string toesteltype, int beginuur, int einduur)
        {
            //if (klantnummer <= 0) throw new KlantRepoADOException("KlantRepoADO - GeefKlantReservaties - 'Ongeldige input'");
            string query = "SELECT t.toestelnummer,t.toestelnaam,t.status,tt.toesteltypeid,tt.toesteltypenaam FROM Toestel t " +
            "LEFT JOIN Toesteltype tt ON t.toesteltype = tt.toesteltypeid " +
            "WHERE t.[status]= 'operatief' AND tt.toesteltypenaam = @toesteltype AND t.toestelnummer " +
            "NOT IN (SELECT i.toestelnummer FROM ReservatieInfo i " +
            "LEFT JOIN Reservatie r ON i.reservatienummer = r.reservatienummer " +
            "WHERE r.datum LIKE @datum AND (i.beginuur BETWEEN @beginuur AND @einduur - 1 OR i.einduur BETWEEN @beginuur + 1 AND @einduur))";
            SqlConnection conn = GetConnection();
            List<Toestel> beschikbareToestellen = new List<Toestel>();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@toesteltype", toesteltype);
                cmd.Parameters.AddWithValue("@datum", datum.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@beginuur", beginuur);
                cmd.Parameters.AddWithValue("@einduur", einduur);
                conn.Open();
                try
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Status status = (Status)Enum.Parse(typeof(Status), (string)reader["status"]);
                        Toestel beschikbaarToestel = new Toestel((int)reader["toestelnummer"], (string)reader["toestelnaam"], status, new ToestelType((int)reader["toesteltypeid"], (string)reader["toesteltypenaam"]));
                        beschikbareToestellen.Add(beschikbaarToestel);
                    }
                    return beschikbareToestellen.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelRepoADO - GeefVrijToestelVoorGeselecteerdTijdslot", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<DTOToestelInfo> ZoekToestellen(Status? status, int toestelnummer, string toestelnaam, string toesteltype)
        {
            if (status.GetType() != typeof(Status)) throw new ToestelRepoADOException("ToestelRepoADO - ZoekToestellen - 'Ongeldige input'");
            string query = "SELECT t.toestelnummer, t.toestelnaam, t.status, tt.toesteltypenaam FROM Toestel t " +
                "LEFT JOIN Toesteltype tt ON t.toesteltype=tt.toesteltypeid ";
            if (status != null) query += "WHERE status=@status";
            else if (toestelnummer > 0) query += "WHERE toestelnummer=@toestelnummer";
            else if (!string.IsNullOrWhiteSpace(toestelnaam)) query += "WHERE toestelnaam LIKE '%' + @toestelnaam + '%'";
            else if (!string.IsNullOrWhiteSpace(toesteltype)) query += "WHERE toesteltypenaam=@toesteltypenaam";
            List<DTOToestelInfo> toestellen = new List<DTOToestelInfo>();
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    if (status != null) cmd.Parameters.AddWithValue("@status", status.ToString());
                    else if (toestelnummer > 0) cmd.Parameters.AddWithValue("@toestelnummer", toestelnummer);
                    else if (!string.IsNullOrWhiteSpace(toestelnaam)) cmd.Parameters.AddWithValue("@toestelnaam", toestelnaam);
                    else if (!string.IsNullOrWhiteSpace(toesteltype)) cmd.Parameters.AddWithValue("@toesteltypenaam", toesteltype);
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DTOToestelInfo toestel = new DTOToestelInfo((int)reader["toestelnummer"], (string)reader["toestelnaam"], (string)reader["status"], (string)reader["toesteltypenaam"]);
                        toestellen.Add(toestel);
                    }
                    reader.Close();
                    return toestellen.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelRepoADO - GeefToestellenMetStatus", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool BestaatToestel(Toestel toestel)
        {
            SqlConnection conn = GetConnection();
            string query1 = "SELECT toesteltypeid FROM Toesteltype WHERE toesteltypenaam=@toesteltypenaam";
            string query2 = "SELECT count(*) FROM Toestel WHERE toestelnaam=@toestelnaam AND toesteltype=@toesteltype";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    int toestelTypeID = 0;
                    cmd.Parameters.AddWithValue("@toesteltypenaam", toestel.ToestelType.ToestelNaam);
                    cmd.CommandText = query1;

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        toestelTypeID = ((int)reader["toesteltypeid"]);
                    }
                    reader.Close();

                    cmd.CommandText = query2;
                    cmd.Parameters.AddWithValue("@toestelnaam", toestel.ToestelNaam);
                    cmd.Parameters.AddWithValue("@toesteltype", toestelTypeID);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelRepoADO - BestaatToestel", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public string SchrijfToestelInDB(Toestel toestel)
        {
            SqlConnection conn = GetConnection();
            string query1 = "SELECT toesteltypeid FROM Toesteltype WHERE toesteltypenaam=@toesteltypenaam";
            string query2 = "INSERT INTO Toestel(toestelnaam,status,toesteltype) VALUES (@toestelnaam,@status,@toesteltype)";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    int toestelTypeID = 0;

                    cmd.Parameters.AddWithValue("@toesteltypenaam", toestel.ToestelType.ToestelNaam);
                    cmd.CommandText = query1;

                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        toestelTypeID = ((int)reader["toesteltypeid"]);
                    }
                    reader.Close();

                    if (toestelTypeID > 0)
                    {
                        cmd.Parameters.AddWithValue("@toestelnaam", toestel.ToestelNaam);
                        cmd.Parameters.AddWithValue("@status", "operatief");
                        cmd.Parameters.AddWithValue("@toesteltype", toestelTypeID);
                        cmd.CommandText = query2;
                        cmd.ExecuteNonQuery();

                        return $"{toestel.ToestelNaam} werd succesvol toegevoegd aan uw toestellen.";
                    }
                    else
                    {
                        return $"{toestel.ToestelNaam} werd NIET toegevoegd aan uw toestellen.";
                    }

                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelRepoADO - SchrijfToestelInDB", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public string UpdateToestelStatus(DTOToestelInfo toestelInfo, string toestelStatus)
        {
            if (toestelInfo.Status == "verwijderd") throw new ToestelRepoADOException("ToestelRepoADO - UpdateToestelStatus - 'Toestel Bestaat niet meer'");
            string query1 = "SELECT count(*) FROM Reservatie r LEFT JOIN ReservatieInfo i ON r.reservatienummer=i.reservatienummer WHERE r.datum>=@datum AND i.toestelnummer=@toestelnummer";
            string query2 = "UPDATE Toestel SET status=@status WHERE toestelnummer=@toestelnummer AND status != 'verwijderd'";
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.CommandText = query1;
                    cmd.Parameters.AddWithValue("@datum", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@toestelnummer", toestelInfo.Toestelnummer);
                    int entries = (int)cmd.ExecuteScalar();

                    if (toestelStatus == "verwijderd")
                    {
                        if (entries > 0)
                        {
                            return $"{toestelInfo.Toestelnaam} heeft nog reservaties.";
                        }
                        else
                        {
                            cmd.CommandText = query2;
                            cmd.Parameters.AddWithValue("@status", toestelStatus);
                            cmd.ExecuteNonQuery();
                            return $"{toestelInfo.Toestelnaam} werd succesvol {toestelStatus}.";
                        }
                    }
                    if (toestelStatus == "onderhoud")
                    {
                        cmd.CommandText = query2;
                        cmd.Parameters.AddWithValue("@status", toestelStatus);
                        cmd.ExecuteNonQuery();
                        if (entries > 0)
                        {
                            return $"{toestelInfo.Toestelnaam} werd succesvol in {toestelStatus} geplaatst, \r dit toestel heeft echter nog {entries} reservaties. \r\r Gelieve de getroffen klanten op de hoogte te brengen of hun reservaties manueel te herboeken.";
                        }
                        else
                        {
                            return $"{toestelInfo.Toestelnaam} werd succesvol in {toestelStatus} geplaatst.";
                        }
                    }
                    else
                    {
                        cmd.CommandText = query2;
                        cmd.Parameters.AddWithValue("@status", toestelStatus);
                        cmd.ExecuteNonQuery();
                        return $"{toestelInfo.Toestelnaam} werd succesvol {toestelStatus} gezet.";
                    }
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelRepoADO - UpdateToestelStatus", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
