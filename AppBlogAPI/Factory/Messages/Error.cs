using AppBlogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AppBlogAPI.Factory.Messages
{
    sealed class Error : MessageIdentityFactory
    {

        public override (string code, string message) GetMessage(string userName)
        {
            return (Constants.Application.Result.ErrorPt,
                $"Nome de usuário '{userName}' é inválido,  deve conter letras e números e não pode ter espaços!");

        }
    }
}
