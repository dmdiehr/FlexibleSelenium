using System;


namespace FlexibleSelenium.StaticDriver
{
    public class AwaitElementException : Exception
    {

        public AwaitElementException() { }

        public AwaitElementException(string message) : base(message) { }

        public AwaitElementException(string message, Exception innerException) : base(message, innerException) { }

    }
}
