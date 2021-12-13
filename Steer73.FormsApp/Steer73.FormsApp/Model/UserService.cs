using System.Collections.Generic;
using System.Threading.Tasks;

namespace Steer73.FormsApp.Model
{
    public class UserService : IUserService
    {
        private static readonly IEnumerable<User> Users = new[]
        {
            new User { FirstName = "Milosz", LastName = "Skalecki" },
            new User { FirstName = "Jon", LastName = "Bennett" },
            new User { FirstName = "Alex", LastName = "Welding" },
            new User { FirstName = "Nick", LastName = "Waites" },
            new User { FirstName = "Akinpelumi", LastName = "Akinlade" },
        };

        public async Task<IEnumerable<User>> GetUsers()
        {
            await Task.Delay(1000);

            return await Task.FromResult(Users);
        }
    }

    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
    }
}