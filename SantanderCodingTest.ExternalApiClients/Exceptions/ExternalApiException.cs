﻿namespace SantanderCodingTest.ExternalApiClients.Exceptions
{
    public class ExternalApiException : Exception
    {
        public ExternalApiException()
        {
        }

        public ExternalApiException(string message)
            : base(message)
        {
        }

        public ExternalApiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
