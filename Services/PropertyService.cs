using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PropertyManagerApp.Models;

namespace PropertyManagerApp.Services
{
    public class PropertyService
    {
        private readonly HttpClient _httpClient;

        // For Android emulator: use your wifi ipv4 address instead of localhost
        private const string BaseUrl = "https://127.0.0.1:7239/api/Properties";

        public PropertyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ── READ ALL ──────────────────────────────────────────────────
        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Property>>(BaseUrl);
            return result ?? new List<Property>();
        }

        // ── READ ONE (DETAILS) ────────────────────────────────────────
        public async Task<Property?> GetPropertyAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Property>($"{BaseUrl}/{id}");
        }

        // ── CREATE ────────────────────────────────────────────────────
        public async Task<bool> CreatePropertyAsync(Property property)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, property);
            return response.IsSuccessStatusCode;
        }

        // ── UPDATE ────────────────────────────────────────────────────
        public async Task<bool> UpdatePropertyAsync(Property property)
        {
            var response = await _httpClient
                .PutAsJsonAsync($"{BaseUrl}/{property.PropertyID}", property);
            return response.IsSuccessStatusCode;
        }

        // ── DELETE ────────────────────────────────────────────────────
        public async Task<bool> DeletePropertyAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
