namespace AuthenticationService.Shared.Validator
{
    public enum UserValidatorStatus
    {
        Success,
        UsernameAlreadyExists,
        InvalidPassword,
        InvalidRole
    }
}
