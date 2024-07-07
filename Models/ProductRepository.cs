namespace MVCCourse.Models;

public static class ProductRepository
{
    private static List<Product> _products = new List<Product>()
    {
        new Product { ProductId = 1, CategoryId = 1, Name = "Iced Tea", Quantity = 100, Price = 1.99 },
        new Product { ProductId = 2, CategoryId = 1, Name = "Canadaa Dry", Quantity = 200, Price = 1.99 },
        new Product { ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Quantity = 300, Price = 1.50 },
        new Product { ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 300, Price = 1.50 }
    };

    public static void AddProduct(Product product)
    {
        if (_products is { Count: > 0 })
        {
            var maxId = _products.Max(x => x.ProductId);
            product.ProductId = maxId + 1;
        }
        else
        {
            product.ProductId = 1;
        }

        _products.Add(product);
    }

    public static List<Product> GetProducts(bool loadCategory = false)
    {
        if (!loadCategory)
        {
            return _products;
        }

        if (_products.Any())
        {
            _products.ForEach(x =>
            {
                if (x.CategoryId.HasValue)
                {
                    x.Category = CategoriesRepository.GetCategoryById(x.CategoryId.Value);
                }

            });
        }
        return _products ?? new List<Product>();
    }

    public static Product? GetProductById(int productId, bool loadCategory = false)
    {
        var product = _products.FirstOrDefault(x => x.ProductId == productId);
        if (product != null)
        {
            var prod = new Product()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            if (loadCategory && prod.CategoryId.HasValue)
            {
                prod.Category = CategoriesRepository.GetCategoryById(prod.CategoryId.Value);
            }

            return prod;
        }

        return null;
    }

    public static void UpdateProduct(int productId, Product product)
    {
        if (productId != product.ProductId) return;
        var productToUpdate = _products.FirstOrDefault(x => x.ProductId == productId );
        if (productToUpdate == null) return;
        productToUpdate.Name = product.Name;
        productToUpdate.Quantity = product.Quantity;
        productToUpdate.Price = product.Price;
        productToUpdate.CategoryId = product.CategoryId;
    }

    public static void DeleteProduct(int productId)
    {
        var product = _products.FirstOrDefault(x => x.ProductId == productId);
        if (product != null)
        {
            _products.Remove(product);
        }
    }

    public static List<Product> GetProductsByCategoryId(int categoryId)
    {
        var product = _products.Where(x => x.CategoryId == categoryId);
        if (product != null)
        {
            return product.ToList();
        }

        //return new List<Product>(); => return [];
        return [];
    }
}