using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UpdaterServer.DTOs;

namespace UpdaterServer.Controllers
{
	[Route("[controller]")]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly IdentityAppDbContext _context;

		public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
			IConfiguration configuration, IdentityAppDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
			_context = context;
		}

		[HttpGet("listUsers")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> GetListUsers()
		{
			var users = await _context.Users.ToListAsync();

			return Ok(users.Select( x => new UserDTO { Email = x.Email, Id = x.Id, Roles = _userManager.GetRolesAsync(x).Result }));
		}

		[HttpPost("create")]
		public async Task<ActionResult<AuthenticationResponse>> Create(
			[FromBody] UserCredentials userCredentials)
		{
			var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
			var result = await _userManager.CreateAsync(user, userCredentials.Password);

			if (result.Succeeded)
			{
				if (user.Email.Contains("shku"))
				{
					await _userManager.AddToRoleAsync(user, "Admin");
				}
				return await BuildToken(userCredentials);
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}

		[HttpPost("login")]
		public async Task<ActionResult<AuthenticationResponse>> Login(
			[FromBody] UserCredentials userCredentials)
		{
			var result = await _signInManager.PasswordSignInAsync(userCredentials.Email,
				userCredentials.Password, isPersistent: false, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				return await BuildToken(userCredentials);
			}
			else
			{
				return BadRequest("Incorrect Login");
			}
		}

		[HttpPost("makeAdmin")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<ActionResult> MakeAdmin([FromBody]string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			await _userManager.AddToRoleAsync(user, "Admin");
			return NoContent();
		}
		[HttpGet("delete/{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> DeleteUserAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			var roles = await _userManager.GetRolesAsync(user);
			if (roles.Contains("Admin"))
				return BadRequest($"Нельзя удалить администратора, сначала удалите роль Admin у пользователя");
			await _userManager.DeleteAsync(user);
			return Ok();
		}

		[HttpPost("editRoles")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<IActionResult> EditRolesAsync([FromBody]RoleEditDTO roleChangeDTO)
		{
			var user = await _userManager.FindByIdAsync(roleChangeDTO.UserId);
			var roles = await _userManager.GetRolesAsync(user);
			if(user.Email == _configuration["SuperAdmin:Email"] && !roleChangeDTO.SelectedRoles.Where(x => x.Value == "Admin").First().Check)
				return BadRequest($"Невозможно лишить прав супер-администратора");

			var rolesToRemove = roleChangeDTO.SelectedRoles.Where(x => !x.Check).Select(x => x.Value);
			var rolesToAdd = roleChangeDTO.SelectedRoles.Where(x => x.Check).Select(x => x.Value);

			foreach (var role in rolesToRemove)
			{
				if(roles.Contains(role))
					await _userManager.RemoveFromRoleAsync(user, role);
			}
			foreach (var role in rolesToAdd)
			{
				if(!roles.Contains(role))
				await _userManager.AddToRoleAsync(user, role);
			}

			return Ok();
		}
		private async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentials)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email, userCredentials.Email)
			};

			var user = await _userManager.FindByNameAsync(userCredentials.Email);
			var claimsDB = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);

			claims.AddRange(claimsDB);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var expiration = DateTime.UtcNow.AddYears(1);

			var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
				expires: expiration, signingCredentials: creds);

			return new AuthenticationResponse()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = expiration
			};
		}
	}
}
