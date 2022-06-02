using FitnessReservatieBL.Domeinen;
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
        private readonly string _connectiestring;

        public ToestelRepoADO(string connectieString)
        {
            this._connectiestring = connectieString;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectiestring);
        }
        public List<Toestel> GeefVrijToestelVoorGeselecteerdTijdslot(DateTime datum, string toesteltypenaam, int beginuur, int einduur)
        {
            string query = "SELECT t.toestelnummer,t.toestelnaam,t.status,tt.toesteltypeid,tt.toesteltypenaam FROM Toestel t " +
            "LEFT JOIN Toesteltype tt ON t.toesteltype = tt.toesteltypeid " +
            "WHERE t.[status]= 'operatief' AND tt.toesteltypenaam = @toesteltypenaam AND t.toestelnummer " +
            "NOT IN (SELECT i.toestelnummer FROM ReservatieInfo i " +
            "LEFT JOIN Reservatie r ON i.reservatienummer = r.reservatienummer " +
            "WHERE r.datum LIKE @datum AND (i.beginuur BETWEEN @beginuur AND @einduur - 1 OR i.einduur BETWEEN @beginuur + 1 AND @einduur))";
            SqlConnection conn = GetConnection();
            List<Toestel> beschikbareToestellen = new ();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@toesteltypenaam", toesteltypenaam);
                cmd.Parameters.AddWithValue("@datum", datum.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@beginuur", beginuur);
                cmd.Parameters.AddWithValue("@einduur", einduur);
                conn.Open();
                try
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Toestel beschikbaarToestel = new ((int)reader["toestelnummer"], (string)reader["toestelnaam"], (Status)Enum.Parse(typeof(Status), (string)reader["status"]), new((int)reader["toesteltypeid"], (string)reader["toesteltypenaam"]));
                        beschikbareToestellen.Add(beschikbaarToestel);
                    }
                    return beschikbareToestellen;
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
            string query = "SELECT t.toestelnummer, t.toestelnaam, t.status, tt.toesteltypenaam FROM Toestel t " +
                "LEFT JOIN Toesteltype tt ON t.toesteltype=tt.toesteltypeid ";
            if (status != null) query += "WHERE status=@status";
            else if (toestelnummer > 0) query += "WHERE toestelnummer=@toestelnummer";
            else if (!string.IsNullOrWhiteSpace(toestelnaam)) query += "WHERE toestelnaam LIKE '%' + @toestelnaam + '%'";
            else if (!string.IsNullOrWhiteSpace(toesteltype)) query += "WHERE toesteltypenaam=@toesteltypenaam";
            List<DTOToestelInfo> toestellen = new ();
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
                        DTOToestelInfo toestel = new ((int)reader["toestelnummer"], (string)reader["toestelnaam"], (Status)Enum.Parse(typeof(Status), (string)reader["status"]), (string)reader["toesteltypenaam"]);
                        toestellen.Add(toestel);
                    }
                    reader.Close();
                    return toestellen.AsReadOnly();
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("ToestelRepoADO - ZoekToestellen", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool BestaatToestel(Toestel toestel, int toestelTypeNummer)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT count(*) FROM Toestel WHERE toestelnaam=@toestelnaam AND toesteltype=@toesteltype";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@toestelnaam", toestel.ToestelNaam);
                    cmd.Parameters.AddWithValue("@toesteltype", toestelTypeNummer);
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
        public string SchrijfToestelInDB(Toestel toestel, int toestelTypeNummer)
        {
            SqlConnection conn = GetConnection();
            string query = "INSERT INTO Toestel(toestelnaam,status,toesteltype) VALUES (@toestelnaam,@status,@toesteltype)";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    if (toestelTypeNummer > 0)
                    {
                        cmd.Parameters.AddWithValue("@toestelnaam", toestel.ToestelNaam);
                        cmd.Parameters.AddWithValue("@status", "operatief");
                        cmd.Parameters.AddWithValue("@toesteltype", toestelTypeNummer);
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        return $"{toestel.ToestelNaam} werd succesvol toegevoegd aan uw toestellen.";
                    }
                    else return $"{toestel.ToestelNaam} werd NIET toegevoegd aan uw toestellen.";
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
                            return $"{toestelInfo.Toestelnaam} heeft nog reservaties,\rhierdoor kon {toestelInfo.Toestelnaam} niet {toestelStatus} worden.";
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
