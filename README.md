




Installation
----------------------------------------------------------


dotnet tool install --global dotnet-ef

dotnet ef dbcontext scaffold "Server=.;Database=POSDotNetCore;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext -f


--------------------------------------------------------------


