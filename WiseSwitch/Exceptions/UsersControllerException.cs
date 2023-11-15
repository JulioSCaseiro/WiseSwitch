namespace WiseSwitch.Exceptions
{
    public class UsersControllerException : Exception
    {
        public UsersControllerException() { }
        public UsersControllerException(string message) : base(message) { }
        public UsersControllerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
