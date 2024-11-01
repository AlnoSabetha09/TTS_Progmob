using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyMauiApp
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://actualbackendapp.azurewebsites.net/swagger/v1/swagger.json");
            if (response.IsSuccessStatusCode)
            {
                var swaggerJson = await response.Content.ReadAsStringAsync();
                var swaggerData = JsonSerializer.Deserialize<SwaggerData>(swaggerJson);

                // Mengambil data kategori dari SwaggerData
                // Sesuaikan dengan struktur SwaggerData
                var categories = swaggerData.paths["/api/v1/Categories"]["get"].responses["200"].content["application/json"].schema.items.properties;

                var categoryList = new List<CategoryDTO>();
                foreach (var category in categories)
                {
                    categoryList.Add(new CategoryDTO
                    {
                        categoryId = int.Parse(category.Key),
                        name = category.Value.description,
                        description = category.Value.description
                    });
                }

                return categoryList;
            }
            else
            {
                throw new Exception("Error retrieving categories from API.");
            }
        }
    }
}