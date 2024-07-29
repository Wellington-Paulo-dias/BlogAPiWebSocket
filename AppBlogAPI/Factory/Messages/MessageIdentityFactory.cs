using AppBlogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AppBlogAPI.Factory.Messages
{
    public abstract class MessageIdentityFactory
    {
       
        public abstract (string code,  string message) GetMessage(string userName);
    }
}
