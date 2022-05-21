using System;

namespace FitnessReservatieDL.Exceptions
{
    public class ReservatieRepoADOException : Exception
    {
        public ReservatieRepoADOException(string message) : base(message)
        {
        }
        public ReservatieRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
