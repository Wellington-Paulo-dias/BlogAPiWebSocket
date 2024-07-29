using AppBlogAPI.Models;

namespace AppBlogAPI.Repositories.Interfaces
{
    public interface IPostRepository: DataAcess<Post>
    {
        IEnumerable<Post> GetPosts();
    }
}
