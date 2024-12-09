namespace SchoolProject.Domain.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";
        public const string WithId = "{id}";

        public static class UserRouting
        {
            // Api/V1/User
            private const string Prefix = Rule + "User/";
            public const string List = Prefix + "List";
            public const string GetById = Prefix + WithId;
            public const string Create = Prefix;
            public const string Update = Prefix;
            public const string Delete = Prefix + WithId;
            public const string Paginated = Prefix + "Paginated";
        }
        public static class AuthenticationRouting
        {
            // Api/V1/Authentication
            private const string Prefix = Rule + "Authentication/";
            public const string SignIn = Prefix + "SignIn";
        }
    }
}
