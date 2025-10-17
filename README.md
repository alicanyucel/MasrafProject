# MasrafProject (Kurumsal Masraf Yonetim Platformu)

> %100 Senior seviye .NET 8, moduler, olceklenebilir mimari ornegi.

Bu proje; sirket ici masraf taleplerinin olusturulmasi, onaylanmasi, izlenmesi ve raporlanmasi icin yapilandirilabilir, genisletilebilir bir backend altyapisi sunar. Clean Architecture + Clean Code + DDD odakli katmanli yaklasim, test edilebilirlik, gozlemlenebilirlik ve operasyonel yonetilebilirlik hedeflenmistir.

---
## Icindekiler
1. One Cikan Ozellikler  
2. Clean Architecture & Clean Code  
3. Mimari  
4. Katman Sorumluluklari  
5. Teknoloji Yigini  
6. OOP, SOLID, AOP ve Cacheleme  
7. UnitOfWork, Scrutor ve Identity API  
8. Generic Repository, MediatR, CQRS, Result Pattern, SmartEnum, DbContextFactory, EnumConverter, FluentValidation  
9. FluentValidation  
10. Cekirdek Ilkeler  
11. Akis Ornegi (Login)  
12. Logging & Observability  
13. Guvenlik  
14. Rate Limiting  
15. Health Checks  
16. Hangfire (Arka Plan Isleri)  
17. Veritabani & Migrasyon  
18. Test / Coverage / Statik Analiz  
19. CI Pipeline (GitHub Actions + Sonar)  
20. Gelistirme Ortami Kurulumu  
21. Docker ile Calistirma  
22. Extensions Metotlar  
23. LoggingBehavior Pipeline  
24. SonarQube ve Coverage Workflow'lari  
25. MemoryCache (IMemoryCache) ile Cacheleme  
26. Yol Haritasi  
27. Katki Rehberi  
28. Lisans / Notlar

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

## 2. Clean Architecture & Clean Code
- Proje, Clean Architecture prensiplerine uygun olarak katmanli ve bagimsiz sekilde tasarlanmistir. Domain, Application, Infrastructure ve WebAPI katmanlari net sekilde ayrilmistir.
- Clean Code yaklasimi ile okunabilir, sade, test edilebilir ve bakimi kolay kod yazilmistir. Fonksiyonlar kisa, isimlendirmeler acik ve kod tekrarindan kacinilmistir.
- Tum business logic domain ve application katmaninda, dis bagimliliklar ise infrastructure ve WebAPI katmaninda izole edilmis ve soyutlanmistir.
- Katmanlar arasi bagimlilik sadece iceriden disariya dogrudur (Dependency Rule).

## 3. Mimari
```
Presentation  ->  Application  ->  Domain  <-  Infrastructure (implementations)
                  (Orchestration)   (Model)     (Persistence, External Adapters)
```

## 4. Katman Sorumluluklari
- `Domain`: Entity / Value Object / is kurallari. Disa bagimlilik YOK.
- `Application`: Use case’ler (Command/Query Handler), arayuzler, DTO/Result modelleri.
- `Infrastructure`: EF Core context, Identity store, repository / adapter implementasyonlari.
- `WebAPI`: DI kompozisyonu, pipeline (middleware), endpoint tanimi.
- `MasrafApi.Test`: Unit (ve ileride integration) testleri.

## 5. Teknoloji Yigini
.NET 8, C# 12, EF Core 8, Identity, MediatR, FluentValidation, Serilog, Hangfire, HealthChecks, Swagger, Sonar, OpenTelemetry,Rate Limiting,MemoryCache,XUnit.

## 6. OOP, SOLID, AOP ve Cacheleme
- Tum kod OOP prensiplerine uygun olarak yazilmistir. Entity, ValueObject, Service, Handler gibi soyutlamalar kullanilmistir.
- SOLID prensipleri (Single Responsibility, Open/Closed, Liskov, Interface Segregation, Dependency Inversion) tum katmanlarda uygulanmistir.
- AOP (Aspect Oriented Programming) icin MediatR pipeline davranislari (or: LoggingBehavior) ile cross-cutting concern'ler merkezi olarak yonetilir.
- IMemoryCache ile performansli, thread-safe ve kolayca expire edilen gecici veri saklama saglanir. Ornek: IP bazli rate limit icin request sayisi 1 dakika boyunca cache'de tutulur.
- Ileride dagitic cache (Redis, vs.) icin altyapi kolayca genisletilebilir.

## 7. UnitOfWork, Scrutor ve Identity API
- Tum transaction yonetimi ve repository islemleri UnitOfWork pattern'i ile soyutlanmistir. Bu sayede birden fazla repository ile calisirken tutarli transaction garantisi saglanir.
- Scrutor ile otomatik dependency injection (service scanning) kullanilmistir. Application ve Infrastructure katmanlarinda interface-implementation otomatik olarak DI container'a eklenir.
- Kimlik dogrulama ve kullanici yonetimi icin Microsoft.AspNetCore.Identity API kullanilmistir. UserManager, SignInManager, RoleManager gibi servisler ile modern kimlik altyapisi saglanir.
- API uzerinden JWT tabanli authentication ve role-based authorization desteklenir.

## 8. Generic Repository, MediatR, CQRS, Result Pattern, SmartEnum, DbContextFactory, EnumConverter, FluentValidation
- Tum veri erisim islemleri icin generic repository pattern'i kullanilmistir. Bu sayede tekrar eden CRUD kodlari minimize edilmis, test edilebilirlik ve bakim kolayligi artmistir.
- MediatR ile CQRS (Command Query Responsibility Segregation) pattern'i uygulanmistir. Tum is akislari command ve query handler'lar ile ayrik sekilde yonetilir.
- Sonuclarin tutarli sekilde donulmesi icin Result pattern (TS.Result) kullanilmistir. Basari, hata ve mesajlar tek tipte doner.
- SmartEnum ile enum'larin validasyonu ve genisletilmesi kolaylastirilmistir. Extension metotlar ile enum degerleri uzerinde is kurali yazmak kolaydir.
- DbContextFactory ile EF Core context'lerinin thread-safe ve performansli sekilde olusturulmasi saglanmistir. Background job ve test senaryolari icin uygundur.
- EnumConverter altyapisi ile enum'larin JSON serialization/deserialization islemleri kolayca yapilir. API'de enum'larin string olarak donmesi ve alinmasi desteklenir.
- **FluentValidation** ile tum command/query modellerinde otomatik ve merkezi validasyon uygulanir. Validation kurallari Application katmaninda tanimlanir ve pipeline ile otomatik tetiklenir.

## 9. Akis Ornegi (Login)
1. Controller -> `LoginCommand`
2. `LoginCommandHandler` UserManager / SignInManager ile kimlik dogrular
3. JWT uretimi `IJwtProvider` uzerinden soyutlanmis
4. Result tipi TS.Result ile tutarli cikti

## 10. Logging & Observability
- Serilog MSSQL sink: Tablo `Logs` (otomatik olusturulur)
- Console template sade
- OpenTelemetry tracing/metrics entegre: `AddCustomOpenTelemetry` (Infrastructure.Extensions.OpenTelemetryExtensions) ile ASP.NET Core, HttpClient ve SqlClient enstrumantasyonu aktif. Varsayilan exporter Console; OTLP icin endpoint ayarlanabilir (ornegin env: `OTEL_EXPORTER_OTLP_ENDPOINT=http://localhost:4317`).

## 11. Guvenlik
- JWT Bearer kimlik dogrulama (Swagger’da Security Scheme)
- Identity lockout mekanizmasi (yanlis parola denemeleri)
- (Plan) Refresh token / token revocation / rol tabanli politikalar

## 12. Rate Limiting
IP basina 1 dakika pencerede 100 istek (FixedWindow). Limit asiminda 429. Gerektiginde Redis ile dagitic surum (yol haritasi).

## 13. Health Checks
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

## 14. Hangfire (Arka Plan Isleri)
- Storage: SQL Server (schema otomatik)
- Dashboard: `/hangfire` (NOT: Uretimde Authorization filter ekleyin)
- Ornek:
```csharp
BackgroundJob.Enqueue(() => service.DoWork());
RecurringJob.AddOrUpdate("daily-report", () => reportGenerator.Run(), Cron.Daily);
```

## 15. Veritabani & Migrasyon
Komutlar:
```bash
dotnet ef migrations add <Ad> --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
dotnet ef database update --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
```
Connection string: `appsettings.json` -> `SqlServer`.

## 16. Test / Coverage / Statik Analiz
- Test Cercevesi: xUnit
- Coverage: `dotnet test --collect:"XPlat Code Coverage"`
- Sonar: OpenCover / Cobertura raporlarini isler (`**/coverage.opencover.xml`)
- Oneri: Kritik domain kurallarina mutlak test, handler’larda edge case senaryolari.

## 17. CI Pipeline (GitHub Actions + Sonar)
Workflow dosyasi: `.github/workflows/ci.yml`
Secrets:
- `SONAR_TOKEN`
- `SONAR_HOST_URL` (SonarCloud icin opsiyonel – `https://sonarcloud.io`)
Degistirilecek argumanlar: Project Key (`/k:`) + Organization (`/o:`)
Kalite Esigi: Sonar Quality Gate (or: min % coverage, yeni kod hatasiz).

## 18. Gelistirme Ortami Kurulumu
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

## 19. Docker ile Calistirma

Proje Docker ve docker-compose ile kolayca ayağa kalkar. Aşağıdaki adımları izleyin:

1. Docker ve docker-compose yüklü olduğundan emin olun.
2. Ortam değişkenlerini (gerekirse) .env dosyasına veya docker-compose.yml içine düzenleyin.
3. Terminalde proje kök dizininde aşağıdaki komutu çalıştırın:

```bash
docker-compose up -d --build
```

- Web API: http://localhost:8080
- Swagger: http://localhost:8080/swagger
- Health: http://localhost:8080/health
- Hangfire: http://localhost:8080/hangfire
- SQL Server: localhost:1433 (kullanıcı: sa, şifre: Your_password123)

İlk çalıştırmada SQL Server container'ı hazırlanırken birkaç dakika bekleyin. 
Connection string ve portları ihtiyaca göre docker-compose.yml'den değiştirebilirsiniz.

Durdurmak için:
```bash
docker-compose down
```

Logları izlemek için:
```bash
docker-compose logs -f
```

## 20. Extensions Metotlar
- `MasrafProject.Application.Extensions.RepositoryExtensions.cs` ve diger extension dosyalari ile repository, validation ve utility fonksiyonlarin tekrar kullanilabilirligi artirildi.
- Extension metotlar ile kodun okunabilirligi ve test edilebilirligi artar.
- Ornek: `IQueryable` uzantisi ile filtreleme, validasyon icin SmartEnum uzantilari, vs.

## 21. LoggingBehavior Pipeline
- `MasrafProject.Application.Behaviors.LoggingBehavior.cs` ile MediatR pipeline'a tum request/response loglama davranisi eklendi.
- Tum CQRS komut ve sorgulari icin otomatik loglama saglanir.
- Loglama davranisi Serilog ile entegre calisir, log seviyeleri ayarlanabilir.
- Bu sayede tum is akislari merkezi olarak izlenebilir ve hata ayiklama kolaylasir.

## 22. SonarQube ve Coverage Workflow'lari
- `.github/workflows/sonar.yml` : Sadece SonarQube/SonarCloud analizini ve coverage raporunu otomatik calistirir. SonarCloud icin gerekli secret'lar: `SONAR_TOKEN`, `SONAR_HOST_URL`.
- `.github/workflows/coverage.yml` : Sadece test coverage raporu uretir ve artifact olarak kaydeder. Sonar entegrasyonu olmadan coverage takibi icin kullanilabilir.
- Her iki workflow da push ve pull requestlerde otomatik calisir.
- Sonar yml icinde project key ve organization ayarlarini kendi SonarCloud hesabina gore degistirmelisin.
- Coverage raporlari `coverage.cobertura.xml` ve `coverage.opencover.xml` olarak upload edilir.

## 23. Yol Haritasi
- [ ] JWT Refresh Token & Revocation listesi
- [ ] Redis cache + dagitic rate limit
- [ ] Integration test (WebApplicationFactory)
- [ ] OpenTelemetry (Tracing + Metrics + Logs birlestirme)
- [ ] Coklu tenant yapisi (TenantId stratejisi)
- [ ] Audit trail & degisiklik kaydi
- [ ] Role / Permission matrix (fine-grained)

## 24. Katki Rehberi
1. Branch: `feature/<ozellik-adi>`
2. Kod stili: Varsayilan .editorconfig (eklenecek ise) + anlamli commit mesajlari
3. Unit test ekleyin / guncelleyin (coverage dusmesin)
4. PR acin, CI basarili olmali, Quality Gate gecmeli
5. Kod inceleme geri bildirimlerini uygulayin

## 25. Lisans / Notlar
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
