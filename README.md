# MasrafProject (Kurumsal Masraf Yonetim Platformu)

> %100 Senior seviye .NET 8, moduler, olceklenebilir mimari ornegi.

Bu proje; sirket ici masraf taleplerinin olusturulmasi, onaylanmasi, izlenmesi ve raporlanmasi icin yapilandirilabilir, genisletilebilir bir backend altyapisi sunar. Clean Architecture + DDD odakli katmanli yaklasim, test edilebilirlik, gozlemlenebilirlik ve operasyonel yonetilebilirlik hedeflenmistir.

---
## Icindekiler
1. One Cikan Ozellikler  
2. Mimari  
3. Katman Sorumluluklari  
4. Teknoloji Yigini  
5. OOP, SOLID, AOP ve Cacheleme  
6. Cekirdek Ilkeler  
7. Akis Ornegi (Login)  
8. Logging & Observability  
9. Guvenlik  
10. Rate Limiting  
11. Health Checks  
12. Hangfire (Arka Plan Isleri)  
13. Veritabani & Migrasyon  
14. Test / Coverage / Statik Analiz  
15. CI Pipeline (GitHub Actions + Sonar)  
16. Gelistirme Ortami Kurulumu  
17. Docker ile Calistirma  
18. Extensions Metotlar  
19. LoggingBehavior Pipeline  
20. SonarQube ve Coverage Workflow'lari  
21. MemoryCache (IMemoryCache) ile Cacheleme  
22. Yol Haritasi  
23. Katki Rehberi  
24. Lisans / Notlar

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

## 5. OOP, SOLID, AOP ve Cacheleme
- Tum kod OOP prensiplerine uygun olarak yazilmistir. Entity, ValueObject, Service, Handler gibi soyutlamalar kullanilmistir.
- SOLID prensipleri (Single Responsibility, Open/Closed, Liskov, Interface Segregation, Dependency Inversion) tum katmanlarda uygulanmistir.
- AOP (Aspect Oriented Programming) icin MediatR pipeline davranislari (or: LoggingBehavior) ile cross-cutting concern'ler merkezi olarak yonetilir.
- IMemoryCache ile performansli, thread-safe ve kolayca expire edilen gecici veri saklama saglanir. Ornek: IP bazli rate limit icin request sayisi 1 dakika boyunca cache'de tutulur.
- Ileride dagitic cache (Redis, vs.) icin altyapi kolayca genisletilebilir.

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

## 19. SonarQube ve Coverage Workflow'lari
- `.github/workflows/sonar.yml` : Sadece SonarQube/SonarCloud analizini ve coverage raporunu otomatik calistirir. SonarCloud icin gerekli secret'lar: `SONAR_TOKEN`, `SONAR_HOST_URL`.
- `.github/workflows/coverage.yml` : Sadece test coverage raporu uretir ve artifact olarak kaydeder. Sonar entegrasyonu olmadan coverage takibi icin kullanilabilir.
- Her iki workflow da push ve pull requestlerde otomatik calisir.
- Sonar yml icinde project key ve organization ayarlarini kendi SonarCloud hesabina gore degistirmelisin.
- Coverage raporlari `coverage.cobertura.xml` ve `coverage.opencover.xml` olarak upload edilir.

## 20. Yol Haritasi
- [ ] JWT Refresh Token & Revocation listesi
- [ ] Redis cache + dagitic rate limit
- [ ] Integration test (WebApplicationFactory)
- [ ] OpenTelemetry (Tracing + Metrics + Logs birlestirme)
- [ ] Coklu tenant yapisi (TenantId stratejisi)
- [ ] Audit trail & degisiklik kaydi
- [ ] Role / Permission matrix (fine-grained)

## 21. Katki Rehberi
1. Branch: `feature/<ozellik-adi>`
2. Kod stili: Varsayilan .editorconfig (eklenecek ise) + anlamli commit mesajlari
3. Unit test ekleyin / guncelleyin (coverage dusmesin)
4. PR acin, CI basarili olmali, Quality Gate gecmeli
5. Kod inceleme geri bildirimlerini uygulayin

## 22. Lisans / Notlar
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
