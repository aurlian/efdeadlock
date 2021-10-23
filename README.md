# efdeadlock

To create project from scratch follow the commands below:

dotnet new console -o EFGetStarted
cd EFGetStarted

dotnet add package Microsoft.EntityFrameworkCore --version 3.1.14
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.14

dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
