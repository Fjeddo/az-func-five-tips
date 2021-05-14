namespace RequestInterception.Exceptions
{
    internal class PropertyMissingException : PropertyValidationFailedException
    {
        public string PropertyName { get; }

        public PropertyMissingException(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}