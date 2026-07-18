namespace ControleMercadoria.Core.DTOs.Auth
{
    public record RefreshTokenResponseDTO(string AccessToken, string RefreshToken, DateTime Expiration);
}
