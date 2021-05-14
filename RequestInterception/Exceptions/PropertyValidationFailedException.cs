using System;

namespace RequestInterception.Exceptions
{
    internal class PropertyValidationFailedException : Exception
    {
        public PropertyValidationFailedException() : base("Please read API documentation") { }
    }
}