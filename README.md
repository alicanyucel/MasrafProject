# MasrafProject (Kurumsal Masraf Yönetim Platformu)

> %100 Kurumsal / Senior seviye .NET 8, modüler, ölçeklenebilir mimari örneði.

Bu proje; þirket içi masraf taleplerinin oluþturulmasý, onaylanmasý, izlenmesi ve raporlanmasý için yapýlandýrýlabilir, geniþletilebilir bir arka uç (backend) altyapýsý sunar. Clean Architecture + DDD odaklý katmanlý yaklaþým, test edilebilirlik, gözlemlenebilirlik ve operasyonel yönetilebilirlik hedeflenmiþtir.

---
## Ýçindekiler
1. Öne Çýkan Özellikler  
2. Mimari  
3. Katman Sorumluluklarý  
4. Teknoloji Yýðýný  
5. Çekirdek Ýlkeler  
6. Akýþ Örneði (Login)  
7. Logging & Observability  
8. Güvenlik  
9. Rate Limiting  
10. Health Checks  
11. Hangfire (Arka Plan Ýþleri)  
12. Veritabaný & Migrasyon  
13. Test / Coverage / Statik Analiz  
14. CI Pipeline (GitHub Actions + Sonar)  
15. Geliþtirme Ortamý Kurulumu  
16. Yol Haritasý  
17. Katký Rehberi  
18. Lisans / Notlar

---
## 1. Öne Çýkan Özellikler
- Katmanlý ve baðýmlýlýk yönü tek yönlü (Domain merkezli)
- CQRS (MediatR) + Validasyon (FluentValidation)
- EF Core 8 + SQL Server + Identity
- Serilog (Console + MSSQL Sink) yapýlandýrýlabilir kolon tasarýmý
- Health Check JSON sözleþmesi + DB check
- Rate Limiting (IP bazlý sabit pencere – Fixed Window)
- Hangfire ile arka plan iþ planlama ve Dashboard
- Swagger + JWT Security þemasý
- Sonar entegre code quality, coverage raporlama
- Geniþletilebilir test altyapýsý (xUnit)

## 2. Mimari
```
Presentation  ->  Application  ->  Domain  <-  Infrastructure (implementations)
                  (Orchestration)   (Model)     (Persistence, External Adapters)
```

## 3. Katman Sorumluluklarý
- `Domain`: Entity / Value Object / iþ kurallarý. Dýþa baðýmlýlýk YOK.
- `Application`: Use case’ler (Command/Query Handler), arayüzler, DTO/Result modelleri.
- `Infrastructure`: EF Core context, Identity store, repository / adapter implementasyonlarý.
- `WebAPI`: DI kompozisyonu, pipeline (middleware), endpoint tanýmý.
- `MasrafApi.Test`: Unit (ve ileride integration) testleri.

## 4. Teknoloji Yýðýný
.NET 8, C# 12, EF Core 8, Identity, MediatR, FluentValidation, Serilog, Hangfire, HealthChecks, Swagger, Sonar, xUnit.

## 5. Çekirdek Ýlkeler
- SOLID & Clean Code
- Açýkça tanýmlý baðýmlýlýk akýþý (iç çekirdek dýþ katmaný bilmez)
- Her handler tek iþ kuralý senaryosu
- Validasyon pipeline ile (fail fast)
- Loglarda baðlam + korelasyon (gerekirse Activity/Trace geniþletilebilir)

## 6. Akýþ Örneði (Login)
1. Controller ? `LoginCommand`
2. `LoginCommandHandler` UserManager / SignInManager ile kimlik doðrular
3. JWT üretimi `IJwtProvider` üzerinden soyutlanmýþ
4. Result tipi TS.Result ile tutarlý çýktý

## 7. Logging & Observability
- Serilog MSSQL sink: Tablo `Logs` (otomatik oluþturulur)
- Console template sadeleþtirilmiþ
- Geliþtirilecek: OpenTelemetry + distributed tracing (planlandý)

## 8. Güvenlik
- JWT Bearer kimlik doðrulama (Swagger’da Security Scheme)
- Identity lockout mekanizmasý (yanlýþ parola denemeleri)
- (Plan) Refresh token / token revocation / rol tabanlý politikalar

## 9. Rate Limiting
IP baþýna 1 dakika pencerede 100 istek (FixedWindow). Limit aþýmýnda 429. Gerektiðinde Redis ile daðýtýk sürüm (yol haritasý).

## 10. Health Checks
Endpoint: `/health`  
Örnek çýktý:
```json
{
  "status": "Healthy",
  "checks": [ { "name": "self", "status": "Healthy" }, { "name": "sql", "status": "Healthy" } ],
  "totalDuration": 15.2
}
```
Geniþletme: Redis / Queue / External API / Disk / Hangfire servers.

## 11. Hangfire (Arka Plan Ýþleri)
- Storage: SQL Server (schema otomatik)
- Dashboard: `/hangfire` (NOT: Üretimde Authorization filter ekleyin)
- Örnek:
```csharp
BackgroundJob.Enqueue(() => service.DoWork());
RecurringJob.AddOrUpdate("daily-report", () => reportGenerator.Run(), Cron.Daily);
```

## 12. Veritabaný & Migrasyon
Komutlar:
```bash
dotnet ef migrations add <Ad> --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
dotnet ef database update --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
```
Connection string: `appsettings.json` ? `SqlServer`.

## 13. Test / Coverage / Statik Analiz
- Test Çerçevesi: xUnit
- Coverage: `dotnet test --collect:"XPlat Code Coverage"`
- Sonar: OpenCover / Cobertura raporlarýný iþler (`**/coverage.opencover.xml`)
- Öneri: Kritik domain kurallarýna mutlak test, handler’larda edge case senaryolarý.

## 14. CI Pipeline (GitHub Actions + Sonar)
Workflow dosyasý: `.github/workflows/ci.yml`
Secrets:
- `SONAR_TOKEN`
- `SONAR_HOST_URL` (SonarCloud için opsiyonel – `https://sonarcloud.io`)
Deðiþtirilecek argümanlar: Project Key (`/k:`) + Organization (`/o:`)
Kalite Eþiði: Sonar Quality Gate (ör: min % coverage, yeni kod hatasýz).

## 15. Geliþtirme Ortamý Kurulumu
```bash
dotnet restore
dotnet build
dotnet ef database update --project MasrafProject/MasrafProject.Infrastructure --startup-project MasrafProject/MasrafProject.WebAPI
dotnet run --project MasrafProject/MasrafProject.WebAPI
```
Varsayýlan uçlar:
- Swagger: https://localhost:<port>/swagger
- Health: https://localhost:<port>/health
- Hangfire: https://localhost:<port>/hangfire

## 16. Yol Haritasý
- [ ] JWT Refresh Token & Revocation listesi
- [ ] Redis cache + daðýtýk rate limit
- [ ] Integration test (WebApplicationFactory)
- [ ] OpenTelemetry (Tracing + Metrics + Logs birleþtirme)
- [ ] Çoklu tenant yapýsý (TenantId stratejisi)
- [ ] Audit trail & deðiþiklik kaydý
- [ ] Role / Permission matrix (fine-grained)

## 17. Katký Rehberi
1. Branch: `feature/<özellik-adý>`
2. Kod stili: Varsayýlan .editorconfig (eklenecek ise) + anlamlý commit mesajlarý
3. Unit test ekleyin / güncelleyin (coverage düþmesin)
4. PR açýn, CI baþarýlý olmalý, Quality Gate geçmeli
5. Kod inceleme geri bildirimlerini uygulayýn

## 18. Lisans / Notlar
Lisans seçimi yapýlmadý – MIT / Apache 2.0 önerilir.
Gizli bilgiler (connection string parolalarý, API key) versiyon kontrolüne eklenmemeli.

---
### Senior Notlarý
- Domain katmaný baðýmsýz tutulmalý; dýþ sistem arayüzleri Application katmanýnda soyutlanmalý.
- Transaction sýnýrlarý (Unit of Work) handler seviyesinde tutarlýlýk saðlamalý.
- Komutlar (state mutasyonu) ile sorgular (okuma) ayrýmý korunmalý.
- Gelecekte event-driven geniþleme (Domain Event / Integration Event) için alt yapý hazýrlanabilir.
- Performans analizi gerekirse (profiling) EF query planlarýný ve N+1 riskini gözlemleyin.

> Bu README kurumsal seviyede belgeleme þablonu olacak þekilde hazýrlanmýþtýr. Gerektikçe ek modüller (Notification, Reporting, File Storage) ayrý bounded context olarak eklenebilir.
