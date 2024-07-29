using AppBlogAPI.DTOs;
using AppBlogAPI.Enumerations;
using AppBlogAPI.Models;

namespace AppBlogAPI.Helpers
{
    public class PostsHelper
    {
        public static string ReturnMessage(string status)
        {
            switch (status)
            {
                case Constants.Application.User.UserExists:
                    break;
                default:
                    break;
            }
            return "";
        }

        public static IReadOnlyList<PostResponseDTO> ReturnListPosts(IEnumerable<Post> list)
        {
            List<PostResponseDTO> postLits = [];
            postLits.AddRange(list.Select(post => new PostResponseDTO
            {
                ResponseMessage="Ok",
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                AuthorEmail = post.AspNetUsers.Email!,
                Createdby = post.AspNetUsers.UserName!
            }));

            return postLits;
        }

        public static PostResponseDTO MapperPostUser(Post postModel, EnumPostTypeCommand eTypeCommand)
        {
            return new PostResponseDTO
            {
                Id = postModel.Id,
                Title = postModel.Title,
                Content = postModel.Content,
                CreatedAt = postModel.CreatedAt,
                AuthorEmail = postModel.AspNetUsers.Email!,
                Createdby = postModel.AspNetUsers.UserName!,
                ResponseMessage = ReturnMessageByType(eTypeCommand)
            };
        }

        private static string ReturnMessageByType(EnumPostTypeCommand eTypeCommand)
        {
            switch (eTypeCommand)
            {
                case EnumPostTypeCommand.Add:
                    return "Post Adicionado com sucesso!";
                case EnumPostTypeCommand.Edit:
                    return "Post Atualizado com sucesso!";
                case EnumPostTypeCommand.Delete:
                    return "Post Deletado com sucesso!";
                default:
                    return string.Empty;
            }
        }
    }
}
