namespace WiseSwitch.Exceptions.ApiServiceExceptions
{
    public class CouldNotGetDataException : ApiServiceException
    {
        public CouldNotGetDataException() : base("Could not get data.") { }
    }
}
