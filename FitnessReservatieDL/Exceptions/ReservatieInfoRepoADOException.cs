using System;

namespace FitnessReservatieDL.Exceptions
{
    public class ReservatieInfoRepoADOException : Exception
    {
        public ReservatieInfoRepoADOException(string message) : base(message)
        {
        }

        public ReservatieInfoRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
