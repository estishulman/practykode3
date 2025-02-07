// // using Microsoft.EntityFrameworkCore;
// // using TodoApi;

// // var builder = WebApplication.CreateBuilder(args);

// // // הוספת שירותי CORS
// // builder.Services.AddCors(options =>
// // {
// //     options.AddPolicy("AllowAllOrigins",
// //         builder => builder.AllowAnyOrigin()
// //                           .AllowAnyMethod()
// //                           .AllowAnyHeader());
// // });


// // builder.Services.AddEndpointsApiExplorer();
// // builder.Services.AddSwaggerGen();

// // // הוספת הקשר למסד הנתונים
// // builder.Services.AddSingleton<ToDoDbContext>();

// // var app = builder.Build();

// // // הפעלת מדיניות CORS
// // app.UseCors("AllowAllOrigins");

// // app.UseSwagger();
// // app.UseSwaggerUI(c =>
// // {
// //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
// //     c.RoutePrefix = string.Empty; // זה יגרום ל-Swagger UI להיות זמין בכתובת הבית
// // });

// // app.MapGet("/items", async (ToDoDbContext db) =>
// // {
// //     return await db.Items.ToListAsync();
// //     //await context.Response.WriteAsJsonAsync(new { Message = "All todo items" });
// // });

// // app.MapGet("/items/{id}", async (int id, ToDoDbContext db) =>
// //     await db.Items.FindAsync(id) is Item item ? Results.Ok(item) : Results.NotFound());

// // app.MapPost("/items/", async (Item item, ToDoDbContext db) =>
// // {
// //     db.Add(item);
// //     await db.SaveChangesAsync();
// //     return Results.Created($"/items/{item.IdItems}", item);
// // });

// // app.MapPut("/items/{id}", async (int id, bool IsComplete, ToDoDbContext db) =>
// // {
// //     var item = await db.Items.FindAsync(id);
// //     if (item is null) return Results.NotFound();

// //     // עדכון המאפיינים של item לפי updatedItem
// //     item.IsComplete = IsComplete;

// //     await db.SaveChangesAsync();
// //     return Results.NoContent();
// // });

// // app.MapDelete("/items/{id}", async (int id, ToDoDbContext db) =>
// // {
// //     var i = await db.Items.FindAsync(id);
// //     if (i is null)
// //         return Results.NotFound();
// //     db.Items.Remove(i);
// //     await db.SaveChangesAsync();
// //     return Results.NoContent();
// // });

// // app.Run();




// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Models;
// using TodoApi;


// var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddAuthorization(); 

// builder.Services.AddControllers(); 

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//                       policy =>
//                       {
//                           policy.AllowAnyOrigin() 
//                                 .AllowAnyHeader()
//                                 .AllowAnyMethod();
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

// app.MapGet("/items", async (ToDoDbContext db) =>
// {
//     return await db.Items.ToListAsync();
// });

// app.MapGet("/items/{id}", async (int id, ToDoDbContext db) =>
// {
//     return await db.Items.FindAsync(id) is Item item ? Results.Ok(item) : Results.NotFound();
// });

// // app.MapPost("/items", async (string name, ToDoDbContext db) =>
// // {
// //     const item = { 
// //       Name: name,           
// //       IsComplete: false,  
// //   };
// //     db.Items.Add(item);
// //     await db.SaveChangesAsync();
// //     return Results.Created($"/items/{item.IdItems}", item);
// // });

// app.MapPost("/items",async (ToDoDbContext db,Item newItem) =>{
//     await db.Items.AddAsync(newItem);
//     await db.SaveChangesAsync();
//     return Results.Created($"/items/{newItem.IdItems}", newItem);
// });


// // app.MapPut("/items/{id}", async (int id, Item updatedItem, ToDoDbContext db) =>
// // {
// //     var item = await db.Items.FindAsync(id);
// //     if (item is null) return Results.NotFound();

// //     item.Name = updatedItem.Name;
// //     item.IsComplete = updatedItem.IsComplete;

// //     await db.SaveChangesAsync();
// //     return Results.NoContent();
// // });

//  app.MapPut("/items/{id}", async (int id, bool IsComplete, ToDoDbContext db) =>
// {
  
//     var item = await db.Items.FindAsync(id);
//     if (item is null) return Results.NotFound();

//     // עדכון המאפיינים של item לפי updatedItem
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
                          policy.AllowAnyOrigin() // מאפשר כל מקור
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

app.Run();
