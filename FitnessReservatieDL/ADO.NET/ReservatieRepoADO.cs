using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.ADO.NET
{
    public class ReservatieRepoADO : IReservatieRepository
    {
        private string _connectiestring;

        public ReservatieRepoADO(string connectiestring)
        {
            this._connectiestring = connectiestring;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectiestring);
            return connection;
        }
        public IReadOnlyList<ReservatieInfo> SelecteerReservatie(int? klantnummer, int? toestelnummer)
        {
            throw new NotImplementedException();
        }

        public bool BestaatReservatie(Reservatie reservatie)
        {
            throw new NotImplementedException();
        }

    }
}
