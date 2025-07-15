#connection string do bazy SQL Server
CONN = Server=host.docker.internal,1433;User Id=SA;Password=yourStrong(!)Password;Encrypt=False;TrustServerCertificate=True;

# Buduje cały projekt .NET
build:
	dotnet build CMS/CMS.sln

# Uruchamia testy jednostkowe
test:
	dotnet test CMS/Tests/Tests.csproj

# Uruchamia aplikację lokalnie (bez Dockera)
run:
	dotnet run --project CMS/Api/Api.csproj

# Buduje obraz Dockera z tagiem cms-portal
docker-build:
	docker build -t cms-portal .

# Uruchamia kontener Dockera na porcie 5001
docker-run:
	docker run \
	  -e ConnectionStrings__DefaultConnection="$(CONN)" \
	  -e ASPNETCORE_URLS=http://+:80 \
	  -p 5001:80 cms-portal
