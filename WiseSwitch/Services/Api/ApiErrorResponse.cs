namespace WiseSwitch.Services.Api
{
    public class ApiErrorResponse : ApiResponse
    {
        public static ApiErrorResponse ApiCallFailed =>
            new ApiErrorResponse
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                Content = "API call failed.",
            };
    }
}
