# CMS Portal

## Opis projektu

Aplikacja CMS do zarządzania artykułami i kategoriami, napisana w .NET 8 z zgodnie z Clean Architecture. Udostępnia operacje CRUD artykułów (ze slugiem), zarządzanie kategoriami oraz pobieranie statystyk.

### Struktura projektu

- **Domain/** – logika domenowa, encje, serwisy, interfejsy repozytoriów
- **Application/** – DTO, walidacje, use case’y, wyjątki
- **Infrastructure/** – implementacje repozytoriów, konfiguracje EF Core
- **Api/** – kontrolery, konfiguracja aplikacji ASP.NET Core
- **Tests/** – testy jednostkowe (xUnit)

### Funkcje

- CRUD artykułów z automatycznym, unikalnym slugiem generowanym z tytułu.
- Walidacja danych: tytuł wymagany, treść minimum 10 znaków, unikalność nazw kategorii.
- Publikacja artykułów (Draft → Published).
- Prosty system kategorii przypisywanych do artykułów.
- Statystyki: liczba artykułów opublikowanych/draft, najczęściej używana kategoria.

### Technologie

- .NET 8
- ASP.NET Core
- Entity Framework Core
- SQL Server
- xUnit (testy)
- Docker
- Clean Architecture
- Moq

---

## Uruchomienie lokalne

1. Wymagania: .NET 8 SDK, baza SQL Server
2. Przygotuj bazę danych i ustaw connection string w `Api/appsettings.json` lub podaj go przez zmienną środowiskową.
   > **Uwaga:** Projekt używa Entity Framework Code First. Przy pierwszym uruchomieniu baza zostanie automatycznie utworzona z przykładowymi danymi.
3. Zbuduj projekt:
   ```sh
   make build
   ```
4. Uruchom testy:
   ```sh
   make test
   ```
5. Uruchom aplikację:
   ```sh
   make run
   ```
6. Dokumentacja Swagger dostępna pod: [http://localhost:5001/swagger](http://localhost:5001/swagger)

---

## Uruchomienie przez Docker

1. Zbuduj obraz:
   ```sh
   make docker-build
   ```
2. Uruchom kontener:
   ```sh
   make docker-run
   ```
3. Aplikacja dostępna pod: [http://localhost:5001/swagger](http://localhost:5001/swagger)

---

### Connection string do bazy SQL Server

Aplikacja domyślnie łączy się z bazą SQL Server pod adresem:

```
Server=host.docker.internal,1433;User Id=SA;Password=yourStrong(!)Password;Encrypt=False;TrustServerCertificate=True;
```

Można zmienić connection string edytując zmienną `CONN` w pliku `Makefile` lub przekazując go podczas uruchamiania:

```sh
make docker-run CONN="Server=ADRES_SERWERA,PORT;User Id=SA;Password=TwojeHaslo!;Encrypt=False;TrustServerCertificate=True;"
```

---

## Uruchomienie przez Docker Compose

1. Uruchom wszystkie usługi (aplikacja + baza SQL Server):
   ```sh
   docker compose up --build
   ```
2. Aplikacja dostępna pod: http://localhost:5001/swagger (dla domyślnie ustawionej zmiennej ASPNETCORE_ENVIRONMENT: Development w docker-compose.yaml)

---

## Przykładowe zapytania curl

### Artykuły

**Utwórz artykuł:**

```sh
curl -X POST http://localhost:5001/api/articles \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Nowy artykuł",
    "content": "To jest treść artykułu.",
    "author": "Jan Kowalski",
    "categoryId": null
  }'
```

**Pobierz listę artykułów:**

```sh
curl http://localhost:5001/api/articles
```

**Pobierz szczegóły artykułu:**

```sh
curl http://localhost:5001/api/articles/{articleId}
```

**Zaktualizuj artykuł:**

```sh
curl -X PUT http://localhost:5001/api/articles/{articleId} \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Zmieniony tytuł",
    "content": "Nowa treść artykułu.",
    "author": "Jan Kowalski",
    "categoryId": null
  }'
```

**Opublikuj artykuł:**

```sh
curl -X POST http://localhost:5001/api/articles/{articleId}/publish
```

**Statystyki artykułów:**

```sh
curl http://localhost:5001/api/articles/stats
```

### Kategorie

**Utwórz kategorię:**

```sh
curl -X POST http://localhost:5001/api/categories \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Nowa kategoria"
  }'
```

**Pobierz listę kategorii:**

```sh
curl http://localhost:5001/api/categories
```

---

## Testy

Testy jednostkowe uruchomisz komendą:

```sh
make test
```

---

## Autor: Łukasz Wiszniewski
