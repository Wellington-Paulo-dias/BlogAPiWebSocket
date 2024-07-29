namespace AppBlogAPI.DTOs
{
    public class PostResponseDTO
    {
        public string ResponseMessage { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Createdby { get; set; }
        public string AuthorEmail { get; set; }
    }
}