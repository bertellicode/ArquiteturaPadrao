using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ArquiteturaPadrao.Infra.CrossCutting.Identity.Models;
using ArquiteturaPadrao.Infra.CrossCutting.Identity.Models.AccountViewModels;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Security.Services.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ITokenConfiguration _tokenConfiguration;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ILoggerFactory loggerFactory,
                                ITokenConfiguration tokenConfiguration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _tokenConfiguration = tokenConfiguration;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario criado com sucesso!");

                await InsertClaims(user);

                var claims = await ConfigureClaims(new LoginViewModel { Email = model.Email, Senha = model.Senha });

                var response = _tokenConfiguration.GenerateToken(claims);

                return Ok(response);
            }

            return BadRequest(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado com sucesso");
                var claims = await ConfigureClaims(model);
                var response = _tokenConfiguration.GenerateToken(claims);
                return Ok(response);
            }

            return BadRequest(model);
        }

        #region Insert Claims

        private async Task InsertClaims(ApplicationUser user)
        {
            var insertClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Test"),
            };

            await _userManager.AddClaimsAsync(user, insertClaims);
        }

        #endregion

        #region Configure Claims

        private async Task<ClaimsIdentity> ConfigureClaims(LoginViewModel login)
        {
            var user = await GetUser(login);
            var claims = await GetJWTClains(user);
            var claimsIdentity = await GetClaims(user, claims);

            return claimsIdentity;
        }

        private async Task<ApplicationUser> GetUser(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            return user;
        }

        private async Task<List<Claim>> GetJWTClains(ApplicationUser user)
        {
            return new List<Claim>
            {
                new Claim("primeiroNome", user.NormalizedUserName),
                new Claim("apelido", user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
            };
        }

        private async Task<ClaimsIdentity> GetClaims(ApplicationUser user, List<Claim> userClaims)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            userClaims.AddRange(claims);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            return identityClaims;
        }

        #endregion

    }
}
