
using AppBlogAPI.Data;
using AppBlogAPI.DTOs;
using AppBlogAPI.Factory.Messages;
using AppBlogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppBlogAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IUserAuthenticationService _authService;

    public AuthController(IUserAuthenticationService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///  Realizar login no site
    /// </summary>
    /// <remarks>
    /// <b>Entre com seu usuário e sua senha</b>
    /// </remarks>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var token = await _authService.LoginAsync(model);
        if (!string.IsNullOrEmpty(token))
        {
            return Ok(token);
        }

        return Unauthorized(new Response
        {
            Status = Constants.Application.Result.ErrorPt,
            Message = "Senha ou usuário incorretos, favor verificar"
        });
    }

    /// <summary>
    /// Cadastro de usuários
    /// </summary>
    /// <remarks>
    /// <b>Atenção:</b> O Nome de usuário deve conter letras e números e não pode ter espaços.
    /// </remarks>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await _authService.RegisterAsync(model);

        if (!result.Succeeded)
        {
            var code = result.Errors.Select(x => x.Code).FirstOrDefault();
            MessageIdentityFactory messageIdentityFactory = MessageIdentityHandlerFactory.Handler(code);
            var resultMessage = messageIdentityFactory.GetMessage(model.Username);

             return BadRequest(new Response { Status = resultMessage.code, Message = resultMessage.message });
        }

        return Ok(new Response { Status = Constants.Application.Result.Success, 
            Message = "Seu cadastro foi realizado com sucesso, faça login para acessar o blog.!" });

    }


}
