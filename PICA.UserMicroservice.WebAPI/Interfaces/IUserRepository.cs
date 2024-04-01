using PICA.UserMicroservice.WebAPI.Models;

namespace PICA.UserMicroservice.WebAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task CreateAsync(User user);
    }
}
