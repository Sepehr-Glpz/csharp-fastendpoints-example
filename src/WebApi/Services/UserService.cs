using Bogus;
using SGSX.Examples.FastEP.WebApi.Models;
using System.Buffers;

namespace SGSX.Examples.FastEP.WebApi.Services;
public class UserService
{
    private readonly Faker<UserModel> _db = new Faker<UserModel>()
        .RuleFor(c => c.Id, faker => faker.Random.Long(1_000_000, 10_000_000))
        .RuleFor(c => c.Username, faker => faker.Internet.UserNameUnicode())
        .RuleFor(c => c.Email, faker => faker.Internet.Email());

    private IEnumerable<UserModel> Users
    {
        get
        {
            for (int index = 0; index <= 1000; index++)
            {
                yield return _db.Generate();
            }
        }
    }

    public UserModel? GetById(long id) =>
        Users.FirstOrDefault(c => c.Id == id);


    public IEnumerable<UserModel> GetList(uint page, uint limit) =>
        Users.Skip((int)((page - 1) * limit)).Take((int)limit);

    public IEnumerable<UserModel> SearchByUsername(string query) =>
        Users.Where(c => c.Username.Contains(query, StringComparison.InvariantCultureIgnoreCase))
             .OrderBy(c => c.Username);
}