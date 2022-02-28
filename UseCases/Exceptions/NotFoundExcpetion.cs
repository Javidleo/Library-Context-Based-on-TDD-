using System;

namespace UseCases.Exceptions
{
    public class NotFoundExcpetion : Exception
    {
        public NotFoundExcpetion(string message) : base(message)
        {
        }
    }
}