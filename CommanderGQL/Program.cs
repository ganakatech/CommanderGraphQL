using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using Microsoft.EntityFrameworkCore;
using GraphQL.Server.Ui.Voyager;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.GraphQL.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommanderConnection")));
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddFiltering()
    .AddSorting();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapGraphQL();

app.UseGraphQLVoyager("/graphql-voyager", new VoyagerOptions
{
    GraphQLEndPoint = "/graphql"
}); 

app.Run();
