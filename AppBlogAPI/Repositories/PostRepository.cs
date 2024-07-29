using AppBlogAPI.Data;
using AppBlogAPI.Models;
using AppBlogAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppBlogAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Post Entity)
        {
            _context.Add(Entity);
            var res = _context.SaveChanges() > 0;
            return res;
        }

        public bool Update(Post Entity)
        {
            _context.Update(Entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(Post Entity)
        {
            _context.Remove(Entity);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Post> GetPosts()
            => _context.Posts.Include(x => x.AspNetUsers).ToList();

        public Post GetById(Guid Id)
            => _context.Posts
            .Include(u => u.AspNetUsers)
            .FirstOrDefault(x => x.Id == Id)!;
    }
}