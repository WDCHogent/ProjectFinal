using System;

namespace FitnessReservatieDL.Exceptions
{
    public class ToestelRepoADOException : Exception
    {
        public ToestelRepoADOException(string message) : base(message)
        {
        }
        public ToestelRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
