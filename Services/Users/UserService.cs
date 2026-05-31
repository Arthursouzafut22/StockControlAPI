using ControleMercadoria.Repositoy.User;
using UserEntity = ControleMercadoria.Models.Users.User;

namespace ControleMercadoria.Services.User
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserEntity> Create(UserEntity user)
        {
            var existEmail = await _repository.EmailExists(user.Email);

            if (existEmail)
            {
                throw new InvalidOperationException("E-mail já cadastrado.");
            }

            return await _repository.Create(user);
        }

        public async Task<UserEntity> Update(int id, UserEntity user)
        {
            var existUser = await _repository.FindById(id);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

            user.Id = id;
            return await _repository.Update(id, user);
        }

        public async Task<UserEntity> FindById(int id)
        {
            var existUser = await _repository.FindById(id);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

            return existUser;
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Delete(int id)
        {
            var existUser = await _repository.FindById(id);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

            await _repository.Delete(id);
        }

        public async Task<UserEntity> FindByEmail(string email)
        {
            var findEmail = await _repository.FindByEmail(email);

            if (findEmail == null)
                throw new KeyNotFoundException($"Usuário com id {email} não encontrado.");

            return findEmail;
        }
        public async Task<bool> EmailExists(string email)
        {
            return await _repository.EmailExists(email);
        }
    }
}
