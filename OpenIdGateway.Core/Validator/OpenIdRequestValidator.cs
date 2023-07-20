using FluentValidation;
using OpenIdGateway.Domain.RequestModels;

namespace OpenIdGateway.Core.Validator
{
    public class OpenIdRequestValidator: AbstractValidator<OpenIdConnectRequestDto>
    {
        public OpenIdRequestValidator()
        {
            RuleFor(x => x.OpenIdCode).NotEmpty().NotNull();
            RuleFor(x => x.OpenIdRequestDto).NotEmpty().NotNull();
            RuleFor(x => x.OpenIdRequestDto.RedirectUri).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.OpenIdRequestDto.RedirectUri));
            RuleFor(x => x.OpenIdRequestDto.UserInfoUrl).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.OpenIdRequestDto.UserInfoUrl));
            RuleFor(x => x.OpenIdRequestDto.TokenUrl).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.OpenIdRequestDto.TokenUrl));
        }
    }
}
