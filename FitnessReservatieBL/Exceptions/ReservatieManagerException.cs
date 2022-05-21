using System;

namespace FitnessReservatieBL.Exceptions
{
    public class ReservatieManagerException : Exception
    {
        public ReservatieManagerException(string message) : base(message)
        {
        }
        public ReservatieManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
