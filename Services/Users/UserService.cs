using BCrypt.Net;
using ControleMercadoria.DTOs.User;
using ControleMercadoria.DTOs.Users;
using ControleMercadoria.Models.Users;
using ControleMercadoria.Repositoy.User;
using System.Security.Claims;
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

        public async Task<UserResponseDTO> Create(CreateUserDTO dto)
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
            return new UserResponseDTO(user.Id, user.Nome, user.Email);
        }

        public async Task<UserResponseDTO> Update(long userIdToken, UpdateUserDTO dto)
        {
            var existUser = await _repository.FindById(userIdToken);
            var existEmail = await _repository.EmailExists(dto.Email);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {userIdToken} não encontrado.");

            if (existEmail)
                throw new InvalidOperationException("E-mail já cadastrado, informe outro e-mail para atualização.");


            existUser.Nome = dto.Nome;
            existUser.Email = dto.Email;

            await _repository.Update(userIdToken, existUser);
            return new UserResponseDTO(existUser.Id, existUser.Nome, existUser.Email);
        }

        public async Task<UserResponseDTO> FindById(long id, long userIdToken)
        {

            if (id != userIdToken)
                throw new UnauthorizedAccessException("Você não tem permissão para acessar este recurso.");

            var existUser = await _repository.FindById(id);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

            return new UserResponseDTO(existUser.Id, existUser.Nome, existUser.Email);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAll()
        {
            var users = await _repository.GetAll();
            return users.Select(item => new UserResponseDTO(item.Id, item.Nome, item.Email));
        }

        public async Task Delete(long id, long userIdToken)
        {
            if (id != userIdToken)
                throw new UnauthorizedAccessException("Você não tem permissão para acessar este recurso.");

            var existUser = await _repository.FindById(id);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

            await _repository.Delete(id);
        }
    }
}
