namespace CosmeticsStore.Validators.User
{
    public static class UserValidationMessages
    {
        public const string EmailRequired = "Email is required.";
        public const string EmailInvalid = "Email is not a valid email address.";
        public const string FullNameRequired = "Full name is required.";
        public const string FullNameTooLong = "Full name must not exceed 250 characters.";
        public const string PhoneInvalid = "Phone number must be digits and may start with '+' (7-15 digits).";
        public const string PasswordRequired = "Password is required.";
        public const string PasswordWeak = "Password must be at least 8 characters and include letters and numbers.";
        public const string RoleInvalid = "Roles, if provided, must be non-empty strings (max 100 chars each).";
    }
}
