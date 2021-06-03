# Migration Database
- IntegrationTest.Infra
dotnet ef migrations add IdentityCreate --context MyDbContext --project IntegrationTest.Infra

# Update Database

- IntegrationTest.Infra
dotnet ef database update --context MyDbContext --project IntegrationTest.Infra
