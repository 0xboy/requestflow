# Talep Yönetim Sistemi - Interview Assignment

Kurum içi talep oluşturma, onaylama ve takip sistemi. Rol bazlı ASP.NET MVC uygulaması.

---

## 📋 Proje Özeti

| Özellik | Değer |
|---------|-------|
| **Proje Adı** | Talep Yönetim Sistemi |
| **Teslim Süresi** | 5 Gün |
| **Mimari Yaklaşım** | 3-Tier (Presentation / Business / Data) |
| **Rol** | Yazılım Mimarı (Tasarım) / Yazılım Mühendisi (Kodlama) |

---

## 🛠 Teknik Stack

| Bileşen | Teknoloji |
|---------|-----------|
| Framework | ASP.NET MVC (.NET Core 6+) |
| ORM | Entity Framework Core (Code First) |
| Veritabanı | MS SQL Server |
| View Engine | Razor Views |
| CSS Framework | Bootstrap 5 |
| Authentication | ASP.NET Core Identity / Cookie-based |

---

## 👥 Roller ve Yetkiler

| Rol | Yetkiler |
|-----|----------|
| **Kullanıcı** | Talep oluşturur, düzenler, sadece kendi taleplerini görür |
| **Yönetici** | Tüm talepleri görür, onaylar/reddeder |
| **Admin** (opsiyonel) | Kullanıcı & rol yönetimi |

---

## 🗺 Geliştirme Roadmap

### Faz 0: Hazırlık (Gün 0)
- [ ] Proje yapısı oluşturma (Solution, projeler)
- [ ] Git repository kurulumu, `.gitignore` ayarları
- [ ] Sabitler ve enum tanımları (magic string yok)
- [ ] Temel klasör yapısı (Controllers, Services, Repositories, Models)

### Faz 1: Altyapı & Kimlik Doğrulama (Gün 1)
- [ ] ASP.NET Core MVC projesi oluşturma
- [ ] Entity Framework Core + SQL Server bağlantısı
- [ ] ASP.NET Core Identity entegrasyonu
- [ ] Rol tanımları (User, Manager, Admin)
- [ ] Login / Logout
- [ ] Rol bazlı yetkilendirme (Authorize attribute)
- [ ] Yetkisiz erişim sayfası (403 / Unauthorized)
- [ ] Session/Cookie tabanlı authentication

### Faz 2: Veri Modeli & Talep Modülü (Gün 2)
- [ ] Entity modelleri (Code First):
  - `User`, `Demand` (Talep), `DemandStatusHistory`, `DemandType`, `Priority`
- [ ] Migration oluşturma ve veritabanı
- [ ] Talep alanları:
  - Talep No (otomatik), Başlık, Açıklama
  - Talep Türü (Dropdown), Öncelik (Düşük/Orta/Yüksek)
  - Oluşturan Kullanıcı, Oluşturma Tarihi
  - Durum (Taslak, Onay Bekliyor, Onaylandı, Reddedildi)
- [ ] Repository pattern / Unit of Work (opsiyonel)
- [ ] Talep servis katmanı
- [ ] İş kuralları:
  - Kullanıcı sadece kendi taleplerini görür
  - Yönetici tüm talepleri görür
  - Onaylanan talep güncellenemez

### Faz 3: Talep CRUD & Onay Akışı (Gün 3)
- [ ] Talep oluşturma formu
- [ ] Talep düzenleme (durum kontrolü ile)
- [ ] Talep listeleme sayfası
- [ ] Talep detay sayfası
- [ ] Onay / Reddet modal veya sayfası
- [ ] Red durumunda açıklama zorunluluğu
- [ ] Talep durum geçmişi (DemandStatusHistory) kaydı

### Faz 4: Listeleme, Filtreleme & Dashboard (Gün 4)
- [ ] Talep listesi filtreleri:
  - Tarihe göre filtre
  - Duruma göre filtre
  - Başlıkta arama
- [ ] Sayfalama (Paging)
- [ ] Yönetici Dashboard:
  - Toplam talep sayısı
  - Bekleyen onay sayısı
  - Son 5 talep
- [ ] Kullanıcı Dashboard:
  - Kendi taleplerinin durumu
  - Son eklenen talepler

### Faz 5: Admin Modülü & Son Rötuşlar (Gün 5)
- [ ] Admin: Kullanıcı yönetimi (opsiyonel)
- [ ] Admin: Rol yönetimi (opsiyonel)
- [ ] UI/UX iyileştirmeleri (Bootstrap)
- [ ] Hata yönetimi ve validasyonlar
- [ ] README güncellemesi (kurulum adımları)
- [ ] Seed data (test kullanıcıları, talep türleri)
- [ ] Final test ve commit

---

## 📐 Mimari Yaklaşım

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                     │
│  (Controllers, Razor Views, ViewModels)                   │
└─────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────┐
│                    Business Layer                         │
│  (Services, DTOs, İş Kuralları)                           │
└─────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────┐
│                      Data Layer                            │
│  (DbContext, Entities, Repositories)                      │
└─────────────────────────────────────────────────────────┘
```

---

## 📄 Zorunlu Arayüzler

| # | Ekran | Açıklama |
|---|-------|----------|
| 1 | Login | Kullanıcı girişi |
| 2 | Ana Dashboard | Rol bazlı özet bilgiler |
| 3 | Talep Oluşturma Formu | Yeni talep ekleme |
| 4 | Talep Listeleme | Filtreleme, sayfalama |
| 5 | Talep Detay | Tek talep görüntüleme |
| 6 | Onay / Reddet | Modal veya ayrı sayfa |
| 7 | Yetkisiz Erişim | 403 sayfası |

---

## 📁 Önerilen Klasör Yapısı

```
assignment/
├── src/
│   └── TalepYonetim/
│       ├── Controllers/
│       ├── Models/
│       ├── Views/
│       ├── Services/
│       ├── Data/
│       │   ├── Entities/
│       │   ├── DbContext/
│       │   └── Migrations/
│       ├── Constants/
│       └── wwwroot/
├── README.md
└── .gitignore
```

---

## ✅ Teslim Kriterleri

- [ ] Çalışan proje (localde ayağa kalkmalı)
- [ ] README: Kurulum adımları, mimari açıklama
- [ ] Temiz kod: Magic string yok, sabitler, enum kullanımı
- [ ] Anlamlı commit mesajları (Git)
- [ ] GitHub linki teslim

---

## 📌 Sabitler & Enum (Örnek)

```csharp
// DemandStatus.cs
public enum DemandStatus { Taslak, OnayBekliyor, Onaylandi, Reddedildi }

// Priority.cs  
public enum Priority { Dusuk, Orta, Yuksek }

// RoleNames.cs
public static class RoleNames { User, Manager, Admin }
```

---

*Bu roadmap, yazılım mimarı tarafından belirlenen tasarıma göre yazılım mühendisi tarafından adım adım uygulanacaktır.*
