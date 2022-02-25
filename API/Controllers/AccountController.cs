namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, 
            TokenService tokenService, IEmailSender emailSender)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Mapper = mapper;
            TokenService = tokenService;
            EmailSender = emailSender;
        }

        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public IMapper Mapper { get; }
        public TokenService TokenService { get; }
        public IEmailSender EmailSender { get; }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AppUserDto>> Login(AppUserLoginDto appUserLoginDto)
        {
            var user = await UserManager.FindByEmailAsync(appUserLoginDto.Email);
            if (user == null) return Unauthorized();

            var result = await SignInManager.CheckPasswordSignInAsync(user, appUserLoginDto.Password, false);
            if (result.Succeeded)
            {
                return new AppUserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = TokenService.CreateToken(user),
                    Username = user.UserName,
                    Email= user.Email
                };
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(AppUserCreateDto appUserCreateDto)
        {
            var validator = new AppUserCreateDtoValidator();
            var validRes = validator.Validate(appUserCreateDto);
            if (!validRes.IsValid)
            {
                return BadRequest(validRes.ToString("|"));
            }

            var user =  UserManager.FindByEmailAsync(appUserCreateDto.Email);

            if (user.Result != null) return BadRequest("User already exists");
             
            

            var newuser = Mapper.Map<AppUser>(appUserCreateDto);

            var result =  await UserManager.CreateAsync(newuser, appUserCreateDto.Password);

            if (result.Succeeded)
            {
                var createdUser = await UserManager.FindByEmailAsync(appUserCreateDto.Email);
                //SendRegisteredUserEmailNotification(appUserCreateDto.Email, appUserCreateDto.Password);
                return Ok(Mapper.Map<AppUserDto>(createdUser));
            }
            return BadRequest();
        }
        private void SendRegisteredUserEmailNotification(string email, string password)
        {
            var message = new Message(new string[] { email }, $"GG8 app registration", 
                $"We are glad to inform you that you have been registered to our app, with email: {email} and password: {password}");
            EmailSender.SendEmail(message);
        }
    }
}
