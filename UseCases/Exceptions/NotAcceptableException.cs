using System;

namespace UseCases.Exceptions
{
    public class NotAcceptableException : Exception
    {
        public NotAcceptableException(string message) : base(message)
        {

        }
    }
}