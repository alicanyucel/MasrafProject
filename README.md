# MasrafProject (Kurumsal Masraf Yonetim Platformu)

> %100 Senior seviye .NET 8, moduler, olceklenebilir mimari ornegi.

Bu proje; sirket ici masraf taleplerinin olusturulmasi, onaylanmasi, izlenmesi ve raporlanmasi icin yapilandirilabilir, genisletilebilir bir backend altyapisi sunar. Clean Architecture + DDD odakli katmanli yaklasim, test edilebilirlik, gozlemlenebilirlik ve operasyonel yonetilebilirlik hedeflenmistir.

---
## Icindekiler
1. One Cikan Ozellikler  
2. Mimari  
3. Katman Sorumluluklari  
4. Teknoloji Yigini  
5. Cekirdek Ilkeler  
6. Akis Ornegi (Login)  
7. Logging & Observability  
8. Guvenlik  
9. Rate Limiting  
10. Health Checks  
11. Hangfire (Arka Plan Isleri)  
12. Veritabani & Migrasyon  
13. Test / Coverage / Statik Analiz  
14. CI Pipeline (GitHub Actions + Sonar)  
15. Gelistirme Ortami Kurulumu  
16. Docker ile Calistirma  
17. Extensions Metotlar  
18. LoggingBehavior Pipeline  
19. Yol Haritasi  
20. Katki Rehberi  
21. Lisans / Notlar

---
## 1. One Cikan Ozellikler
- Katmanli ve bagimlilik yonu tek yonlu (Domain merkezli)
- CQRS (MediatR) + Validasyon (FluentValidation)
- EF Core 8 + SQL Server + Identity
- Serilog (Console + MSSQL Sink) yapilandirilabilir kolon tasarimi
- Health Check JSON sozlesmesi + DB check
- Rate Limiting (IP bazli sabit pencere – Fixed Window)
- Hangfire ile arka plan is planlama ve Dashboard
- Swagger + JWT Security semasi
- Sonar entegre code quality, coverage raporlama
- Genisletilebilir test altyapisi (xUnit)

## 2. Mimari
```
Presentation  ->  Application  ->  Domain  <-  Infrastructure (implementations)
                  (Orchestration)   (Model)     (Persistence, External Adapters)
```

## 3. Katman Sorumluluklari
- `Domain`: Entity / Value Object / is kurallari. Disa bagimlilik YOK.
- `Application`: Use case’ler (Command/Query Handler), arayuzler, DTO/Result modelleri.
- `Infrastructure`: EF Core context, Identity store, repository / adapter implementasyonlari.
- `WebAPI`: DI kompozisyonu, pipeline (middleware), endpoint tanimi.
- `MasrafApi.Test`: Unit (ve ileride integration) testleri.

## 4. Teknoloji Yigini
.NET 8, C# 12, EF Core 8, Identity, MediatR, FluentValidation, Serilog, Hangfire, HealthChecks, Swagger, Sonar, xUnit.

## 5. Cekirdek Ilkeler
- SOLID & Clean Code
- Acikca tanimli bagimlilik akisi (ic cekirdek dis katmani bilmez)
- Her handler tek is kurali senaryosu
- Validasyon pipeline ile (fail fast)
- Loglarda baglam + korelasyon (gerekirse Activity/Trace genisletilebilir)

## 6. Akis Ornegi (Login)
1. Controller -> `LoginCommand`
2. `LoginCommandHandler` UserManager / SignInManager ile kimlik dogrular
3. JWT uretimi `IJwtProvider` uzerinden soyutlanmis
4. Result tipi TS.Result ile tutarli cikti

## 7. Logging & Observability
- Serilog MSSQL sink: Tablo `Logs` (otomatik olusturulur)
- Console template sade
- Gelistirilecek: OpenTelemetry + distributed tracing (planlandi)

## 8. Guvenlik
- JWT Bearer kimlik dogrulama (Swagger’da Security Scheme)
- Identity lockout mekanizmasi (yanlis parola denemeleri)
- (Plan) Refresh token / token revocation / rol tabanli politikalar

## 9. Rate Limiting
IP basina 1 dakika pencerede 100 istek (FixedWindow). Limit asiminda 429. Gerektiginde Redis ile dagitic surum (yol haritasi).

## 10. Health Checks
Endpoint: `/health`  
Ornek cikti:
```json
{
  "status": "Healthy",
  "checks": [ { "name": "self", "status": "Healthy" }, { "name": "sql", "status": "Healthy" } ],
  "totalDuration": 15.2
}
```
Genisletme: Redis / Queue / External API / Disk / Hangfire servers.

## 11. Hangfire (Arka Plan Isleri)
- Storage: SQL Server (schema otomatik)
- Dashboard: `/hangfire` (NOT: Uretimde Authorization filter ekleyin)
- Ornek:
```csharp
BackgroundJob.Enqueue(() => service.DoWork());
RecurringJob.AddOrUpdate("daily-report", () => reportGenerator.Run(), Cron.Daily);
```

## 12. Veritabani & Migrasyon
Komutlar:
```bash
dotnet ef migrations add <Ad> --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
dotnet ef database update --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
```
Connection string: `appsettings.json` -> `SqlServer`.

## 13. Test / Coverage / Statik Analiz
- Test Cercevesi: xUnit
- Coverage: `dotnet test --collect:"XPlat Code Coverage"`
- Sonar: OpenCover / Cobertura raporlarini isler (`**/coverage.opencover.xml`)
- Oneri: Kritik domain kurallarina mutlak test, handler’larda edge case senaryolari.

## 14. CI Pipeline (GitHub Actions + Sonar)
Workflow dosyasi: `.github/workflows/ci.yml`
Secrets:
- `SONAR_TOKEN`
- `SONAR_HOST_URL` (SonarCloud icin opsiyonel – `https://sonarcloud.io`)
Degistirilecek argumanlar: Project Key (`/k:`) + Organization (`/o:`)
Kalite Esigi: Sonar Quality Gate (or: min % coverage, yeni kod hatasiz).

## 15. Gelistirme Ortami Kurulumu
```bash
dotnet restore
dotnet build
dotnet ef database update --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
dotnet run --project MasrafProject/MasrafProject.WebAPI
```
Varsayilan uclar:
- Swagger: https://localhost:<port>/swagger
- Health: https://localhost:<port>/health
- Hangfire: https://localhost:<port>/hangfire

## 16. Docker ile Calistirma

Proje Docker ve docker-compose ile kolayca ayaða kalkar. Aþaðýdaki adýmlarý izleyin:

1. Docker ve docker-compose yüklü olduðundan emin olun.
2. Ortam deðiþkenlerini (gerekirse) .env dosyasýna veya docker-compose.yml içine düzenleyin.
3. Terminalde proje kök dizininde aþaðýdaki komutu çalýþtýrýn:

```bash
docker-compose up -d --build
```

- Web API: http://localhost:8080
- Swagger: http://localhost:8080/swagger
- Health: http://localhost:8080/health
- Hangfire: http://localhost:8080/hangfire
- SQL Server: localhost:1433 (kullanýcý: sa, þifre: Your_password123)

Ýlk çalýþtýrmada SQL Server container'ý hazýrlanýrken birkaç dakika bekleyin. 
Connection string ve portlarý ihtiyaca göre docker-compose.yml'den deðiþtirebilirsiniz.

Durdurmak için:
```bash
docker-compose down
```

Loglarý izlemek için:
```bash
docker-compose logs -f
```

## 17. Extensions Metotlar
- `MasrafProject.Application.Extensions.RepositoryExtensions.cs` ve diger extension dosyalari ile repository, validation ve utility fonksiyonlarin tekrar kullanilabilirligi artirildi.
- Extension metotlar ile kodun okunabilirligi ve test edilebilirligi artar.
- Ornek: `IQueryable` uzantisi ile filtreleme, validasyon icin SmartEnum uzantilari, vs.

## 18. LoggingBehavior Pipeline
- `MasrafProject.Application.Behaviors.LoggingBehavior.cs` ile MediatR pipeline'a tum request/response loglama davranisi eklendi.
- Tum CQRS komut ve sorgulari icin otomatik loglama saglanir.
- Loglama davranisi Serilog ile entegre calisir, log seviyeleri ayarlanabilir.
- Bu sayede tum is akislari merkezi olarak izlenebilir ve hata ayiklama kolaylasir.

## 19. Yol Haritasi
- [ ] JWT Refresh Token & Revocation listesi
- [ ] Redis cache + dagitic rate limit
- [ ] Integration test (WebApplicationFactory)
- [ ] OpenTelemetry (Tracing + Metrics + Logs birlestirme)
- [ ] Coklu tenant yapisi (TenantId stratejisi)
- [ ] Audit trail & degisiklik kaydi
- [ ] Role / Permission matrix (fine-grained)

## 20. Katki Rehberi
1. Branch: `feature/<ozellik-adi>`
2. Kod stili: Varsayilan .editorconfig (eklenecek ise) + anlamli commit mesajlari
3. Unit test ekleyin / guncelleyin (coverage dusmesin)
4. PR acin, CI basarili olmali, Quality Gate gecmeli
5. Kod inceleme geri bildirimlerini uygulayin

## 21. Lisans / Notlar
Lisans secimi yapilmadi – MIT / Apache 2.0 onerilir.
Gizli bilgiler (connection string parolalari, API key) versiyon kontrolune eklenmemeli.

---
### Senior Notlari
- Domain katmani bagimsiz tutulmali; dis sistem arayuzleri Application katmaninda soyutlanmali.
- Transaction sinirlari (Unit of Work) handler seviyesinde tutarlilik saglamali.
- Komutlar (state mutasyonu) ile sorgular (okuma) ayrimi korunmali.
- Gelecekte event-driven genisleme (Domain Event / Integration Event) icin alt yapi hazirlanabilir.
- Performans analizi gerekirse (profiling) EF query planlarini ve N+1 riskini gozlemleyin.

> Bu README kurumsal seviyede belgeleme sablonu olacak sekilde hazirlanmistir. Gerektikce ek moduller (Notification, Reporting, File Storage) ayri bounded context olarak eklenebilir.
