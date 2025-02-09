
// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Models;
// using TodoApi;

// var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// var builder = WebApplication.CreateBuilder(args);

// // הוספת שירותים
// builder.Services.AddAuthorization(); 
// builder.Services.AddControllers(); 

// // הוספת שירותי CORS
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//                       policy =>
//                       {
//                           policy.AllowAnyOrigin() // מאפשר כל מקור
//                                 .AllowAnyHeader() // מאפשר כל כותרת
//                                 .AllowAnyMethod(); // מאפשר כל שיטה (GET, POST, PUT, DELETE)
//                       });
// });

// // הוספת Swagger
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// // הוספת DbContext
// builder.Services.AddDbContext<ToDoDbContext>(options =>
//     options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"), new MySqlServerVersion(new Version(8, 0, 41))));

// var app = builder.Build();

// // הפעלת CORS
// app.UseCors(MyAllowSpecificOrigins);

// // הפעלת Swagger
// app.UseSwagger();
// app.UseSwaggerUI(c =>
// {
//     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//     c.RoutePrefix = string.Empty; 
// });

// // הפעלת Authorization

// app.UseAuthorization();

// app.MapControllers();

// // הגדרת ה-endpoints
// app.MapGet("/items", async (ToDoDbContext db) =>
// {
//     return await db.Items.ToListAsync();
// });

// app.MapGet("/items/{id}", async (int id, ToDoDbContext db) =>
// {
//     return await db.Items.FindAsync(id) is Item item ? Results.Ok(item) : Results.NotFound();
// });

// app.MapPost("/items", async (ToDoDbContext db, Item newItem) =>
// {
//     await db.Items.AddAsync(newItem);
//     await db.SaveChangesAsync();
//     return Results.Created($"/items/{newItem.IdItems}", newItem);
// });

// app.MapPut("/items/{id}", async (int id, bool IsComplete, ToDoDbContext db) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item is null) return Results.NotFound();

//     item.IsComplete = IsComplete;
//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });

// app.MapDelete("/items/{id}", async (int id, ToDoDbContext db) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item is null) return Results.NotFound();

//     db.Items.Remove(item);
//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });

// app.MapGet("/",()=>"AhuthServer API is running!");
// app.Run();


using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// הוספת שירותים
builder.Services.AddAuthorization(); 
builder.Services.AddControllers(); 

// הוספת שירותי CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://todolistreact-master-f7ya.onrender.com") // מאפשר מקור ספציפי
                                .AllowAnyHeader() // מאפשר כל כותרת
                                .AllowAnyMethod(); // מאפשר כל שיטה (GET, POST, PUT, DELETE)
                      });
});

// הוספת Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// הוספת DbContext
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"), new MySqlServerVersion(new Version(8, 0, 41))));

var app = builder.Build();

// הפעלת CORS
app.UseCors(MyAllowSpecificOrigins);

// הפעלת Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; 
});

// הפעלת Authorization
app.UseAuthorization();

app.MapControllers();

// הגדרת ה-endpoints
app.MapGet("/items", async (ToDoDbContext db) =>
{
    return await db.Items.ToListAsync();
});

app.MapGet("/items/{id}", async (int id, ToDoDbContext db) =>
{
    return await db.Items.FindAsync(id) is Item item ? Results.Ok(item) : Results.NotFound();
});

app.MapPost("/items", async (ToDoDbContext db, Item newItem) =>
{
    await db.Items.AddAsync(newItem);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{newItem.IdItems}", newItem);
});

app.MapPut("/items/{id}", async (int id, bool IsComplete, ToDoDbContext db) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    item.IsComplete = IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/items/{id}", async (int id, ToDoDbContext db) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/",()=>"AhuthServer API is running!");
app.Run();
