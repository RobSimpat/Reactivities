namespace API.Mediatr.AppUser.UIModels
{
    public class AppUserCreateDtoValidator : AbstractValidator<AppUserCreateDto>
    {
        public AppUserCreateDtoValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty();
            RuleFor(x=>x.Password).NotEmpty();
            RuleFor(x=>x.Email).NotEmpty();
        }
    }
}
