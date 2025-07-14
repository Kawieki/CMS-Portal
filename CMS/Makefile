build:
	dotnet build CMS.sln

test:
	dotnet test Tests/Tests.csproj

run:
	dotnet run --project Api/Api.csproj

docker-build:
	docker build -t cms-portal .

docker-run:
	docker run -p 5001:80 cms-portal 