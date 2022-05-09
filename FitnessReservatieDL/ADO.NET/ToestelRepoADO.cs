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
    }
}
