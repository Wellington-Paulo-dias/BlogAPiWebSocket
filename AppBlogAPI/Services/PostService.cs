using AppBlogAPI.Data;
using AppBlogAPI.DTOs;
using AppBlogAPI.Extensions;
using AppBlogAPI.Helpers;
using AppBlogAPI.Models;
using AppBlogAPI.Repositories.Interfaces;
using AppBlogAPI.Services.Interfaces;
using Ardalis.GuardClauses;
using System.Linq;

namespace AppBlogAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IUserAuthenticationService _authenticationService;

        public PostService(IPostRepository repository,
            IUserAuthenticationService authenticationService)
        {
            _repository = repository;
            _authenticationService = authenticationService;
        }

        private async Task<ApplicationUser> ReturnUserByEmail(string email)
            => await _authenticationService.GetIdUserByEmailAsync(email);

        public async Task<PostResponseDTO> CreateAsync(string title, string content, string emailUser)
        {
            var aspNetUsersModel =await ReturnUserByEmail(emailUser);

            var entity = new Post(title, content, aspNetUsersModel);
            if (_repository.Add(entity))
            {
                
                entity.AspNetUsers = aspNetUsersModel;
                return PostsHelper.MapperPostUser(entity, Enumerations.EnumPostTypeCommand.Add);
            }
            else
                return new PostResponseDTO { ResponseMessage ="Houve um erro em criar este post, tente mais tarde!" };
        }

        public Post GetById(Guid postId)
        {
            Guard.Against.NullOrEmptyGuid(postId, "O Id do post não pode nulo");
            return _repository.GetById(postId);
        }

        public PostResponseDTO Edit(Guid postId, string title, string content)
        {
            var postModel = _repository.GetById(postId);
            if (postModel is not null)
            {
                postModel.update(title, content);
                if (_repository.Update(postModel))
                    return PostsHelper.MapperPostUser(postModel,Enumerations.EnumPostTypeCommand.Edit);
            }

            return new PostResponseDTO 
            {
                ResponseMessage="Não foi possível atualizar este post!"
            };
        }

        public bool Delete(Guid postId)
        {
            var postModel = new Post();
            postModel.Delete(postId);

            var entity = _repository.GetById(postModel.Id);
            if (entity is null) return false;

            return _repository.Delete(entity);
        }

        public IReadOnlyList<PostResponseDTO> GetAll() 
            => PostsHelper.ReturnListPosts(_repository.GetPosts());

    }
}