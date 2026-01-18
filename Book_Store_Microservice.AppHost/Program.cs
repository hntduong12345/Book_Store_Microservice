var builder = DistributedApplication.CreateBuilder(args);

//Define Postgres Server
//var postgres = builder.AddPostgres("postgres").WithPgAdmin();

//Define Postgres Database
var userDb = builder.AddConnectionString("bookstoreuser");

//Setup reference to APIs
builder.AddProject<Projects.Books_Api>("books-api");
builder.AddProject<Projects.Users_Api>("users-api").WithReference(userDb);

builder.Build().Run();
