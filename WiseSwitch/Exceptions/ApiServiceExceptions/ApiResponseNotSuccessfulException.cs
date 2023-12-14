namespace WiseSwitch.Exceptions.ApiServiceExceptions
{
    public class ApiResponseNotSuccessfulException : ApiServiceException
    {
        public ApiResponseNotSuccessfulException() : base("The API response was not successful.") { }
    }
}
