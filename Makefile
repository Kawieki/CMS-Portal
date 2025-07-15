build:
	dotnet build CMS/CMS.sln

test:
	dotnet test CMS/Tests/Tests.csproj

run:
	dotnet run --project CMS/Api/Api.csproj

docker-build:
	docker build -t cms-portal .

docker-run:
	docker run -p 5001:80 cms-portal