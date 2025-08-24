namespace gymLog.API.Model.enums
{
    public enum ErrorCode
    {
        None,
        NotFound,
        Unauthorized,
        Forbidden,
        ValidationFailed,
        Conflict,
        ServerError,

        //User related errors
        UserNotFound,
        WrongPassword,
        EmailAlreadyUsed
    }

}
