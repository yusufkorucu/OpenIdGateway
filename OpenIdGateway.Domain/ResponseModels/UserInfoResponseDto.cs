using System.Text.Json.Serialization;

namespace OpenIdGateway.Domain.ResponseModels
{
    public class UserInfoResponseDto
    {
        [JsonPropertyName("sub")]
        public string Sub { get; set; }

        [JsonPropertyName("updated_at")]
        public int UpdatedAt { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("given_name")]
        public string GivenName { get; set; }

        [JsonPropertyName("family_name")]
        public string FamilyName { get; set; }

        [JsonPropertyName("personal_data_hint")]
        public string PersonalDataHint { get; set; }

        [JsonPropertyName("Key")]
        public string Key { get; set; }
    }
}
