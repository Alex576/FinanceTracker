using Security.Data.DBModels;

namespace Security.Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? OptionJson { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? AccessToken { get; set; }

        public UserModel(string name)
        {
            Name = name;
        }

        public UserModel(User dbUser)
        {
            Id = dbUser.Id;
            Name = dbUser.Name;
            OptionJson = dbUser.OptionsJson;
            LastLogin = dbUser.LastLogin;
        }

    }
}
