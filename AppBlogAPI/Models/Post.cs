using AppBlogAPI.Data;
using AppBlogAPI.Extensions;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using ZstdSharp;

namespace AppBlogAPI.Models
{
    public class Post : Entity
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string AspNetUsersId { get; private set; }
        public ApplicationUser AspNetUsers { get; set; }

        public Post(string title, string content, ApplicationUser aspNetUsers)
        {
            ValidateDomain(title, content, aspNetUsers);
            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now;
            AspNetUsersId = aspNetUsers.Id;
            AspNetUsers = aspNetUsers;
        }

        public Post()
        { }

        public void update(string title, string content)
        {
            ValidateInformations(title, content);
            Title = title;
            Content = content;
        }

        public void Delete(Guid postId)
        {
            ValidateDomain(postId);
            Id = postId;
        }

        private void ValidateDomain(Guid postId)
            => Guard.Against.NullOrEmptyGuid(postId, "O Id do post não pode nulo");

        private void ValidateDomain(string title, string content, ApplicationUser aspNetUsers)
        {
            if (aspNetUsers is null) throw new ArgumentException("Falha em busca o usuário.");
            ValidateInformations(title, content);          
        }
        private void ValidateInformations(string title, string content)
        {
            Guard.Against.NullOrEmpty(title, nameof(title), "Campo de usuário não pode ser vazio!");
            Guard.Against.LengthOutOfRange(title, 3, 50, "O Nome de título deve ter entre 0 e 50 caracteres");
            Guard.Against.NullOrEmpty(content, nameof(content), "Campo de usuário não pode ser vazio!");
        }
    }
}