var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Books_Api>("books-api");

builder.AddProject<Projects.Users_Api>("users-api");

builder.Build().Run();
