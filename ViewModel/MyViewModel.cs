using System.Collections.ObjectModel;

namespace MyMauiApp
{
    public class MyViewModel : ObservableObject
    {
        private readonly ProductService _productService;

        public ObservableCollection<CategoryDTO> Categories { get; } = new ObservableCollection<CategoryDTO>();

        public MyViewModel(ProductService productService)
        {
            _productService = productService;
            LoadCategories();
        }

        private async void LoadCategories()
        {
            var categories = await _productService.GetCategoriesAsync();

            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
    }
}