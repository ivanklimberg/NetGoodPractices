﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SessionStorageExample.Models
{
    public class LoginRequestModel
    {
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
