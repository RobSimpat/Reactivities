using API.Mediatr.AppUser.UIModels;

namespace API.Mediatr.AppUser
{
    public class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUserDto, Db.Models.AppUser>();
            CreateMap<Db.Models.AppUser, AppUserDto>();
            CreateMap<AppUserCreateDto, Db.Models.AppUser>();
        }
        
    }
}
