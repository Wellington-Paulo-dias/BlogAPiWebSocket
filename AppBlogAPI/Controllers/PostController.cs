using AppBlogAPI.DTOs;
using AppBlogAPI.Services;
using AppBlogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly WebSocketNotificationService _notificationService;

        public PostController(IPostService postService, WebSocketNotificationService notificationService)
        {
            _postService = postService;
            _notificationService = notificationService;
        }

      
       
        /// <summary>
        /// Listagem de todos os posts do site
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = _postService.GetAll();
            return Ok(posts);
        }

        /// <summary>
        /// Criar um post
        /// </summary>
        /// <remarks>
        /// <b>Atenção:</b> Para realizar a criação de um post, você precisa primeiro fazer uma cadastro e estar logado no site.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] PostRequestDTO request)
        {
            try
            {
                var email = User.Claims.Select(x => x.Value).ToList().ElementAt(1);

                var post = await _postService.CreateAsync(request.Title, request.Content, email);

                await _notificationService.SendMessageAsync($@"
--------------Nova mensagem recebida----------------------------------------------------------------

Um novo post acaba de ser criado:
Usuário: {User.Identity!.Name}
Título: {request.Title}
Data: {DateTime.Now}

Clique AQUI e saiba mais.

----------------------------------------------------------------------------------------------------

");

                return Ok(post);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        /// <summary>
        /// Editar um post
        /// </summary>
        /// <remarks>
        /// <b>Atenção:</b> Para realizar a edição deste post é necessário fazer login no site.
        /// </remarks>
        /// <param name="postId">Id do post a ser editado</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{postId}")]
        [Authorize]
        public IActionResult EditPost(Guid postId, [FromBody] PostRequestDTO request)
        {
            var post = _postService.Edit(postId, request.Title, request.Content);
            if (post is null)
            {
                return NotFound("Não foi possível editar este post.");
            }

            return Ok(post);
        }

        /// <summary>
        /// Deletar um post
        /// </summary>
        /// <remarks>
        /// <b>Atenção:</b> Para deletar um post é necessário fazer login no site.
        /// </remarks>
        /// <param name="postId">Id do post a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("{postId}")]
        [Authorize]
        public IActionResult DeletePost(Guid postId)
        {
            if (_postService.Delete(postId))
                return Ok("Post deletado com sucesso");

            return BadRequest("Houve um erro em deletar este post, tente mais tarde!");
        }
    }
}
