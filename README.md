# JAQOps

JAQOps is een schaalbaar SaaS-platform dat field service bedrijven zoals installatie- en onderhoudsbedrijven helpt hun bedrijfsprocessen te digitaliseren en stroomlijnen door klantenbeheer, techniekerplanning, voorraadbeheer en facturatie in één krachtig systeem te combineren.

---

## 🚀 Features

JAQOps biedt een uitgebreid pakket aan functionaliteiten die typische uitdagingen van field service bedrijven aanpakken:

- **Multi-tenant architectuur**: meerdere bedrijven (tenants) kunnen het platform onafhankelijk gebruiken, met data-isolatie en eigen branding.
- **CRM module**: klantenbeheer, contactpersonen, historiek en offertes.
- **Planning & Dispatch**: real-time planning van techniekers op basis van vaardigheden en beschikbaarheid, inclusief mobiele PWA-ondersteuning.
- **Voorraadbeheer**: inzicht in materiaal, verbruik en minimumvoorraad, gekoppeld aan werkbonnen.
- **Facturatie**: automatische factuur- en werkbongeneratie, PDF-export en e-mailnotificaties.
- **Role-based Access Control (RBAC)**: rollen zoals Admin, Planner en Technieker met toegangscontrole.
- **Realtime status updates**: met SignalR voor live communicatie tussen backend en frontend.
- **Offline ondersteuning**: techniekers kunnen jobs ook zonder internet volgen en updaten via de PWA.
- **Rapportage & analytics**: omzet, jobkosten, productiviteit en exportmogelijkheden naar CSV/Excel.
- **Docker-gebaseerde deployment**: eenvoudig lokaal en in de cloud te draaien.
- **Asynchrone taakverwerking**: bijvoorbeeld automatische factuurherinneringen via Hangfire.

---

## 🛠️ Tech stack

**Backend**  
- ASP.NET Core 8 Web API (C#)  
- Entity Framework Core  
- PostgreSQL database  
- SignalR (Realtime communicatie)  
- Hangfire (Background jobs & taakverwerking)  
- JWT-authenticatie met multi-tenant ondersteuning  
- Docker containerisatie

**Frontend**  
- React 18 + TypeScript  
- Zustand (lichte state management)  
- TailwindCSS (styling)  
- React Router v6 (routing)  
- PWA ondersteuning (service workers voor offline gebruik)  

**Deployment**  
- Gratis hostingmogelijkheden: Fly.io, Railway, Render  
- CI/CD pipelines via GitHub Actions (optioneel)

---

## 🏗️ Architectuur overzicht

- **Multi-tenant backend** die data scheidt per klant met JWT claims.  
- **RESTful API** die alle kernfunctionaliteiten bedient.  
- **Realtime updates** via SignalR hub voor statuswijzigingen in planning en jobs.  
- **Frontend PWA** met role-based dashboards voor Admin, Planner en Technieker.  
- **Background worker** (Hangfire) die taken zoals factuurherinneringen, rapporten en e-mails afhandelt.  

---

## 🔧 Installatie & Deployment

### Lokale installatie (development)

1. Clone de repo:  
   ```bash
   git clone https://github.com/yourusername/jaqops.git
   cd jaqops
