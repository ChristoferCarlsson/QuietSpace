# 📱 QuietSpace

QuietSpace är en mobilvänlig app som hjälper dig hitta lugna och tysta platser i din stad – perfekta för introverta, studenter och alla som behöver en paus från stadens brus. Hitta bibliotek, parker, tysta caféer och mycket mer.

---

## 🌟 Funktioner

- 🔍 Sök efter tysta platser baserat på kategori eller taggar
- 📝 Läs och skriv recensioner
- 📌 Spara dina favoritplatser som bokmärken
- 👤 Skapa en användarprofil och hantera dina inställningar
- 🗺️ Se alla platser direkt på en karta

---

## 🧱 Datamodeller & Funktionalitet

### 👤 User (Användare)

Representerar en användare i systemet.

#### 🔧 Funktioner

- **Registrera konto & logga in**  
  Skapa konto med namn, e-post och lösenord.

- **Spara preferenser**  
  Möjlighet att ange personliga inställningar (t.ex. favorittaggar).

- **Skriva recensioner**  
  Lämna recensioner på olika tysta platser.

- **Spara bokmärken**  
  Spara platser för snabb åtkomst senare.


---

### 🗺️ QuietPlace (Tyst plats)

Representerar en tyst plats i staden, t.ex. bibliotek, park eller café.

#### 🔧 Funktioner

- **Skapa och spara en ny plats**  
  Registrera namn, adress, koordinater, kategori och taggar.

- **Filtrera & sök bland platser**  
  Filtrering via kategori eller taggar.

- **Visa platsinformation**  
  All platsdata kan visas på karta eller detaljsida.

- **Beräkna genomsnittligt betyg**  
  Dynamisk uppdatering baserat på recensioner.

- **Visa recensioner & bokmärken**  
  Lista alla kopplade recensioner och hur många som har bokmärkt platsen.


---

### ✍️ Review (Recension)

En användares bedömning av en tyst plats.

#### 🔧 Funktioner

- **Lämna betyg & kommentar**  
  Betygsätt platsen (t.ex. 1–5) och skriv en kort kommentar.

- **Tidsstämpling av recensioner**  
  Varje recension sparas med datum för att visa aktuell feedback.

- **Koppling till användare och plats**  
  Recensionen länkas både till den användare som skrev den och platsen den gäller.

---

### 🔖 Bookmark (Bokmärke)

En sparad plats från en användare.

#### 🔧 Funktioner

- **Spara en plats som favorit**  
  Lägg till en QuietPlace till din bokmärkeslista.

- **Visa sparade platser**  
  Lista alla bokmärkta platser på din profilsida.

- **Tidsstämpling**  
  Varje bokmärke sparas med datum då det lades till.

---
#### 🛠️ Teknikstack

- Frontend: 

- Backend: 

- Databas: SQL 

- Kartor: Leaflet.js eller Google Maps API

- Autentisering: JWT / OAuth

#### 📄 Licens

- **MIT License**
