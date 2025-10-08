# MasrafProject

Kurumsal masraf yönetimi için geliþtirilen .NET 8 tabanlý modüler API.

## Mimari Özet
- Katmanlar:
  - `MasrafProject.Domain`: Saf domain modelleri ve iþ kurallarý.
  - `MasrafProject.Application`: CQRS (MediatR), Validasyon (FluentValidation), DTO / Command / Query.
  - `MasrafProject.Infrastructure`: EF Core 8, Identity, veri eriþimi, dýþ servis adaptörleri.
  - `MasrafProject.WebAPI`: API uçlarý, DI kompozisyonu, middleware zinciri.
  - `MasrafApi.Test`: xUnit testleri (unit + ileride integration senaryolarý).

## Teknolojiler
- .NET 8, C# 12
- EF Core 8 + SQL Server
- ASP.NET Core Identity
- MediatR (CQRS pattern)
- FluentValidation
- Serilog + MSSQL & Console sink (yapýlandýrýlabilir column options)
- Health Checks (`/health`) + SQL Server check
- Rate Limiting (Fixed Window, IP bazlý)
- Hangfire (background jobs) + Dashboard (`/hangfire`)
- Swagger (`/swagger`) + JWT Security þemasý
- Sonar & Code Coverage (GitHub Actions CI workflow)

## Çalýþtýrma
```bash
dotnet restore
dotnet build
dotnet ef database update --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
dotnet run --project MasrafProject/MasrafProject.WebAPI
```
Varsayýlan uçlar:
- Swagger: https://localhost:<port>/swagger
- Health: https://localhost:<port>/health
- Hangfire Dashboard: https://localhost:<port>/hangfire

## Konfigürasyon
`MasrafProject.WebAPI/appsettings.json` içinde:
- ConnectionStrings: `SqlServer`
- Serilog seviyeleri
- JWT ayarlarý (varsa) — Not: README güncellenirken gizli deðerleri paylaþmayýn.

## Loglama
Serilog MSSQL sink tablo adý: `Logs` (otomatik oluþturulur). Ek kolon yapýlandýrmasý `Program.cs` içindeki `ColumnOptions` bölümünde.

## Rate Limiting
Global sabit pencere (FixedWindow): Dakikada 100 istek / IP. 429 dönerse bir dakika bekleyin.

## Health Checks
`/health` JSON çýktýsý:
```json
{
  "status": "Healthy",
  "checks": [
    { "name": "self", "status": "Healthy" },
    { "name": "sql",  "status": "Healthy" }
  ],
  "totalDuration": 12.34
}
```
Ýleride Redis, Queue vb. eklendikçe `AddHealthChecks()` geniþletilebilir.

## Hangfire
Dashboard þimdilik açýk (yetkilendirme yok). Üretimde bir authorization filter ekleyin.
Örnek iþ planlama:
```csharp
BackgroundJob.Enqueue(() => service.DoWork());
RecurringJob.AddOrUpdate("daily-report", () => reportGenerator.Run(), Cron.Daily);
```

## Testler & Coverage
xUnit projesi: `MasrafApi.Test`.
Örnek test dosyasý: `UnitTest1.cs`.
Coverage CI’da `XPlat Code Coverage` collector ile üretilir ve Sonar tarafýndan tüketilir.
Lokal coverage:
```bash
dotnet test --collect:"XPlat Code Coverage" --results-directory TestResults
```
Rapor dönüþtürme (isteðe baðlý): ReportGenerator gibi bir araç eklenebilir.

## CI / SonarQube (SonarCloud)
Workflow: `.github/workflows/ci.yml`.
Gerekli Secrets:
- `SONAR_TOKEN`
- `SONAR_HOST_URL` (SonarCloud ise `https://sonarcloud.io` veya boþ)
Deðiþtirilecek parametreler:
- Project Key: `/k:"<your-project-key>"`
- Organization: `/o:"<your-organization>"`

## Kod Kalitesi Kurallarý (Öneri)
- Public API yüzeyini minimal tutun.
- Domain modellerinde yan etkisiz (pure) metotlar tercih edin.
- Command ve Query handler'larýnda tek sorumluluk.
- Validation her zaman `IRequest` öncesi.
- Logging: Kritik domain karar noktalarýnda `Information`, hatalarda `Error`.

## Geniþletme Yol Haritasý
- JWT Refresh token flow
- Audit log tablosu
- Çoklu tenant / þirket desteði
- Redis cache & distributed rate limit
- Integration test suite (WebApplicationFactory)
- OpenTelemetry + tracing

## Katký Rehberi
1. Fork & branch (feature/<konu>)
2. Unit test ekleyin veya güncelleyin.
3. PR açýn; CI geçmeli + coverage gerilememeli.

## Lisans
Bu kýsma seçtiðiniz lisansý ekleyin (MIT, Apache 2.0, vb.)

---
Senior notu: Domain baðýmlýlýklarýný dýþ katmanlara sýzdýrmayýn; DI kompozisyonu sadece WebAPI katmanýnda tanýmlý kalsýn. Yeni altyapý entegrasyonlarý için Infrastructure'da adapter + interface (Application) yaklaþýmý sürdürülmeli.
