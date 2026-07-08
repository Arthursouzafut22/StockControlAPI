using BCrypt.Net;
using ControleMercadoria.Core.DTOs.Users;
using ControleMercadoria.Infrastructure.Repository.Users;
using System.Security.Claims;
using UserEntity = ControleMercadoria.Core.Models.Users.User;

namespace ControleMercadoria.Application.Services.Users
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
                Nome = dto.Name,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _repository.Create(user);
            return new UserResponseDTO(user.Id, user.Nome, user.Email);
        }

        public async Task<UserResponseDTO> Update(long id, long userId, UpdateUserDTO dto)
        {
            var existUser = await _repository.FindById(id);
            var existEmail = await _repository.EmailExists(dto.Email);

            if (userId != id)
                throw new UnauthorizedAccessException
                    ("Você não tem permissão para editar esse recurso.");

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {userId} não encontrado.");

            if (existEmail)
                throw new InvalidOperationException
                    ("E-mail já cadastrado, informe outro e-mail para atualização.");


            existUser.Nome = dto.Nome;
            existUser.Email = dto.Email;

            await _repository.Update(userId, existUser);
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

        public async Task Delete(long id, long userIdToken)
        {
            if (id != userIdToken)
                throw new UnauthorizedAccessException("Você não tem permissão para deletar este recurso.");

            var existUser = await _repository.FindById(id);

            if (existUser == null)
                throw new KeyNotFoundException($"Usuário com id {id} não encontrado.");

            await _repository.Delete(id);
        }
    }
}
