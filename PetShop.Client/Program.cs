using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Data.Repositories;
using PetShop.Data.Repositories.AnimalRepository;
using PetShop.Data.Repositories.CategoryRepository;
using PetShop.Data.Repositories.PictureRepository;
using PetShop.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PetShopDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PetShopConnection")));
builder.Services.AddTransient<ICommentRepository<Comment>, CommentRepository<Comment>>();
builder.Services.AddTransient<IAnimalRepository<Animal>,AnimalRepository<Animal>>();
builder.Services.AddTransient<ICategoryRepository<Category>, CategoryRepository<Category>>();
builder.Services.AddTransient<IPictureRepository<Picture>, PictureRepository<Picture>>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
