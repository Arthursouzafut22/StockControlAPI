using ControleMercadoria.DTOs.User;
using ControleMercadoria.DTOs.Users;
using ControleMercadoria.Models.Users;
using ControleMercadoria.Repositoy.User;
using UserEntity = ControleMercadoria.Models.Users.User;
using BCrypt.Net;

namespace ControleMercadoria.Services.User
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDto> Create(CreateUserDTO dto)
        {
            var existEmail = await _repository.EmailExists(dto.Email);

            if (existEmail)
            {
                throw new InvalidOperationException("E-mail já cadastrado.");
            }

            var user = new UserEntity
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            await _repository.Create(user);
            return new UserResponseDto(user.Id, user.Nome, user.Email);
        }

        public async Task<UserResponseDto> Update(long id, UpdateUserDto dto)
        {
            var existUser = await _repository.FindById(id);
            var existEmail = await _repository.EmailExists(dto.Email);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

            if (existEmail)
                throw new InvalidOperationException("E-mail já cadastrado.");
            

            existUser.Nome = dto.Nome;
            existUser.Email = dto.Email;

             await _repository.Update(id, existUser);
            return new UserResponseDto(existUser.Id, existUser.Nome, existUser.Email);
        }

        public async Task<UserResponseDto> FindById(long id)
        {
            var existUser = await _repository.FindById(id);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

            return new UserResponseDto(existUser.Id, existUser.Nome, existUser.Email);
        }

        //public async Task<IEnumerable<UserEntity>> GetAll()
        //{
        //    return await _repository.GetAll();
        //}

        //public async Task Delete(int id)
        //{
        //    var existUser = await _repository.FindById(id);

        //    if (existUser == null)
        //        throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

        //    await _repository.Delete(id);
        //}

        //public async Task<UserEntity> FindByEmail(string email)
        //{
        //    var findEmail = await _repository.FindByEmail(email);

        //    if (findEmail == null)
        //        throw new KeyNotFoundException($"Usuário com id {email} não encontrado.");

        //    return findEmail;
        //}
        //public async Task<bool> EmailExists(string email)
        //{
        //    return await _repository.EmailExists(email);
        //}
    }
}
