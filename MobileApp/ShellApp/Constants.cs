namespace ShellApp
{
    public static class Constants
    {
        public static string AuthorityUri = "https://localhost:5020"; //"https://demo.identityserver.io";
        public static string RedirectUri = "io.identitymodel.native://callback";
        public static string ApiUri = "https://demo.identityserver.io/api/";
        public static string ClientId = "interactive.public";
        public static string Scope = "IdentityServerApi openid profile email phone";
    }
}
