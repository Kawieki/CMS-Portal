build:
	dotnet build cms/CMS.sln

test:
	dotnet test cms/Tests/Tests.csproj

run:
	dotnet run --project cms/Api/Api.csproj

docker-build:
	docker build -t cms-portal .

docker-run:
	docker run -p 5001:80 cms-portal