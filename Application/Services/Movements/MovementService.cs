using ControleMercadoria.Core.DTOs.Movements;
using ControleMercadoria.Core.Enums;
using ControleMercadoria.Core.Models.Movements;
using ControleMercadoria.Core.Models.Users;
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
                .Where(x => x.UserId == userId && x.Type == MovementType.ENTRADA);

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

        public async Task<MovementsResponseDTO> CreateExitMovement(long userId, CreateExitMovementDTO dto)
        {
            var product = await _productRepository.FindById(dto.ProductId);

            if (product == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            if (product.UserId != userId)
                throw new UnauthorizedAccessException("Você não tem permissão para criar uma movimentação.");

            if (product.StockQuantity == 0)
                throw new InvalidOperationException("Produto sem estoque disponível.");

            if (dto.Amount > product.StockQuantity)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {product.StockQuantity} unidades.");


            product.StockQuantity -= dto.Amount;

            await _productRepository.Update(product.Id, product);

            var movement = new Movement
            {
                ProductId = product.Id,
                UserId = userId,
                Type = MovementType.SAIDA,
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
                MovementType.SAIDA,
                createdMovement.Amount,
                createdMovement.UnitValue,
                createdMovement.TotalValue,
                createdMovement.Observation,
                createdMovement.CreatedAt
            );
        }

        public async Task<IEnumerable<MovementsResponseDTO>> GetExitMovements(long userId)
        {
            var movements = (await _repository.GetAllMovementsWithProduct())
                .Where(x => x.UserId == userId && x.Type == MovementType.SAIDA);

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

        public async Task<IEnumerable<MovementsResponseDTO>> FindByIdMovements(long id, long userId)
        {
            var movements = (await _repository.GetAllMovementsWithProduct())
                .Where(x => x.Id == id && x.UserId == userId);

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

        public async Task UpdateMovement(long id, long userId, long productId, UpdateMovementsDTO dto)
        {
            var movement = await _repository.FindById(id);
            var product = await _productRepository.FindById(productId);

            if (movement == null)
                throw new KeyNotFoundException(
                    "Movimento não encontrado.");

            if(product == null)
                throw new KeyNotFoundException(
                    "Produto não encontrado.");

            if (movement.UserId != userId)
                throw new UnauthorizedAccessException(
                    "Você não tem permissão para editar este movimento.");


            if(movement.Type == MovementType.ENTRADA)
            {
                var previousQuantityEntry = product!.StockQuantity - movement.Amount;
                var newInputQuantity = previousQuantityEntry += dto.Amount;
                product.StockQuantity = newInputQuantity; 
            }


            if (movement.Type == MovementType.SAIDA)
            {
                var previousOutflowQuantity = product!.StockQuantity + movement.Amount;

                if (dto.Amount > previousOutflowQuantity)
                    throw new InvalidOperationException
                        ("Quantidade informada maior que a quantidade do estoque atual.");

                var newOutputQuantity = previousOutflowQuantity -= dto.Amount; 
                product.StockQuantity = newOutputQuantity;
            }

            movement.Amount = dto.Amount;
            movement.UnitValue = dto.UnitValue;
            movement.Observation = dto.Observation;

            await _productRepository.Update(product.Id, product);
            await _repository.Update(movement.Id, movement);
           
        }

        public async Task<IEnumerable<MovementsResponseDTO>> GetAll(long userId)
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
