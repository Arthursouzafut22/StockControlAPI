namespace ControleMercadoria.Core.DTOs.Auth
{
    public record LoginResponseDTO
    (
       bool Success,
       string Message,
       string AccessToken,
       string RefreshToken

    );
}
