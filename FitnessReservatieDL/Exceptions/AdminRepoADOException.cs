using System;

namespace FitnessReservatieDL.Exceptions
{
    public class AdminRepoADOException : Exception
    {
        public AdminRepoADOException(string message) : base(message)
        {
        }
        public AdminRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
