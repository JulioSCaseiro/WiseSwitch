namespace WiseSwitch.Exceptions.UsersControllerExceptions
{
    public class UserUpdateException : UsersControllerException
    {
        public UserUpdateException() : base("Could not update User.") { }
    }
}
