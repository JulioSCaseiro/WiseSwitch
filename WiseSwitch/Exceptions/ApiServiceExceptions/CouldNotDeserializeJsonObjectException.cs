namespace WiseSwitch.Exceptions.ApiServiceExceptions
{
    public class CouldNotDeserializeJsonObjectException : ApiServiceException
    {
        public CouldNotDeserializeJsonObjectException() : base("Could not deserialize JSON object.") { }
    }
}
