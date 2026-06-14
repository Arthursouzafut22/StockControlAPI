using ControleMercadoria.Core.DTOs.Movements;
using ControleMercadoria.Core.Enums;
using ControleMercadoria.Core.Models.Movements;
using ControleMercadoria.Infrastructure.Repository.Movements;
using ControleMercadoria.Infrastructure.Repository.Products;

namespace ControleMercadoria.Application.Services.Movements
{
    public class MovementService : IMovementService
    {
        private readonly IMovementsRepository _repository;
        private readonly IProductRepository _productRepository;

        public MovementService(
            IMovementsRepository repository,
            IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<MovementsResponseDTO> CreateEntryMovement(long userId, CreateEntryMovementDTO dto)
        {
            var product = await _productRepository.FindById(dto.ProductId);

            if (product == null)
                throw new KeyNotFoundException("Produto não encontrado");

            if (product.UserId != userId)
                throw new UnauthorizedAccessException("Você não tem permissão para criar uma movimentação.");

            product.StockQuantity += dto.Amount;

            await _productRepository.Update(product.Id, product);

            var movement = new Movement
            {
                ProductId = product.Id,
                UserId = userId,
                Type = MovementType.ENTRADA,
                Amount = dto.Amount,
                UnitValue = dto.UnitValue,
                Observation = dto.Observation
            };

            var createdMovement = await _repository.Create(movement);

            return new MovementsResponseDTO(
                createdMovement.Id,
                createdMovement.ProductId,
                product.Name,
                createdMovement.UserId,
                MovementType.ENTRADA,
                createdMovement.Amount,
                createdMovement.UnitValue,
                createdMovement.TotalValue,
                createdMovement.Observation,
                createdMovement.CreatedAt
            );
        }

        public async Task<IEnumerable<MovementsResponseDTO>> GetEntryMovements(long userId)
        {
            var movements = (await _repository.GetAllMovementsWithProduct())
                .Where(x => x.UserId == userId);

            return movements.Select(x =>
                new MovementsResponseDTO(
                    x.Id,
                    x.ProductId,
                    x.Product.Name,
                    x.UserId,
                    x.Type,
                    x.Amount,
                    x.UnitValue,
                    x.TotalValue,
                    x.Observation,
                    x.CreatedAt
                )
            );
        }
    }
}
