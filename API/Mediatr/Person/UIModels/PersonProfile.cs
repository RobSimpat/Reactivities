namespace API.Mediatr.Person.UIModels
{
    public class PersonProfile:Profile
    {
        public PersonProfile()
        {
            CreateMap<Db.Models.Person, PersonDto>();
        }
    }
}
