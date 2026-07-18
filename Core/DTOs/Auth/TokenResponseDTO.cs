namespace ControleMercadoria.Core.DTOs.Auth
{
    public record TokenResponseDTO(string Token, DateTime Expiration, string RefreshToken, DateTime RefreshTokenExpiration);
}
