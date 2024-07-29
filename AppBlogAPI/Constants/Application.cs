namespace AppBlogAPI.Constants
{
    public static class Application
    {
        public static class Password
        {
            public const int SaltSize = 16;
            public const int HashSize = 32;
            public const int Iterations = 10000;
        }

        public static class User
        {
            public const string UserExists = "UserExists";
          
        }

        public static class Result
        {
            public const string Error = "Error";
            public const string Success = "Success";
            public const string InvalidUserName = "InvalidUserName";
            public const string InvalidUserNamePt = "Usuário inválido";
            public const string ErrorPt = "Erro";
        }
    }
}
