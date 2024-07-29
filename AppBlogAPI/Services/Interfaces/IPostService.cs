using AppBlogAPI.DTOs;
using AppBlogAPI.Models;

namespace AppBlogAPI.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostResponseDTO> CreateAsync(string title, string content, string emailUser);
        PostResponseDTO Edit(Guid postId, string title, string content);
        bool Delete(Guid postId);
        IReadOnlyList<PostResponseDTO> GetAll();
        Post GetById(Guid postId);

    }
}
