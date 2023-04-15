using AutoMapper;
using BookStore.Api.Profiles;
using BookStore.Api.Services;
using BookStore.Repository.Persistence;
using BookStore.Repository.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("BookStoreConnectionString"))
);

builder.Services.AddTransient<IBookstoreContext, BookstoreContext>();

builder.Services.AddAutoMapper(typeof(Program));
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService,BookService>();

builder.Services.AddOutputCache(options =>
{
    //books controller policy
    options.AddPolicy("CachingPolicyBooks", builder => builder.Tag("CachingPolicyBooks"));
    options.AddPolicy("CachingPolicyBooksById", builder =>
    {
        builder.Tag("CachingPolicyBooks");
        builder.SetVaryByRouteValue("id");
    });
    //authors controller policy
    options.AddPolicy("CachingPolicyAuthors", builder => builder.Tag("CachingPolicyAuthors"));
    options.AddPolicy("CachingPolicyAuthorsById", builder =>
    {
        builder.Tag("CachingPolicyAuthors");
        builder.SetVaryByRouteValue("id");
    });


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseOutputCache();

app.Run();
