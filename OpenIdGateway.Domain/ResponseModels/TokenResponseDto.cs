﻿using System.Text.Json.Serialization;

namespace OpenIdGateway.Domain.ResponseModels
{
    public class TokenResponseDto
    {

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpireIn { get; set; }
    }
}
