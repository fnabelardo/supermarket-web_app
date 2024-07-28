using CoreBusiness;
using Plugins.DataStore.InMemory;
using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.ProductsUseCases;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>();
builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
builder.Services.AddTransient<IViewSelectedCategoriesUseCase, ViewSelectedCategoriesUseCase>();
builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

builder.Services.AddSingleton<IProductRepository, ProductsInMemoryRepository>();
builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IViewSelectedProductsUseCase, ViewSelectedProductsUseCase>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();