using Ardalis.GuardClauses;

namespace AppBlogAPI.Models
{
    public class User : Entity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public User()
        { }

        public User(string username, string password, string email)
        {
            ValidateDomain(username, password, email);
            Username = username;
            Password = password;
            Email = email;
        }

        public User(string username, string password)
        {
            ValidateDomain(username, password);
            Username = username;
            Password = password;
        }
        public void Login(string username)
        {
            ValidateUserName(username);
            Username = username;
        }

        private void ValidateDomain(string userName, string password)
        {
            ValidateUserName(userName);
            ValidaPassword(password);
        }

        private void ValidateDomain(string userName, string password, string email)
        {
            ValidateUserName(userName);
            ValidadeEmail(email);
            ValidaPassword(password);
        }

        private void ValidateUserName(string userName)
        {
            Guard.Against.NullOrEmpty(userName, nameof(Username), "Campo de usuário não pode ser vazio!");
            Guard.Against.LengthOutOfRange(userName, 3, 20, "O Nome de usuário deve ter entre 3 e 20 caracteres");
        }

        private void ValidadeEmail(string email)
        {
            Guard.Against.NullOrEmpty(email, nameof(email), "Campo de e-mail não pode ser vazio!");
            Guard.Against.InvalidFormat(email, nameof(email), @"^[^@\s]+@[^@\s]+\.[^@\s]+$", "Email incorreto, favor verificar!");
        }

        public void ValidaPassword(string password)
        {
            Guard.Against.NullOrEmpty(password, nameof(password), "O campo senha não pode ser vazio!");
        }
    }
}
