# 📱 QuietSpace

QuietSpace är en mobilvänlig webbapp som hjälper dig att hitta lugna och tysta platser i din stad – perfekta för introverta, studenter eller dig som bara vill ta en paus från vardagens brus. Hitta bibliotek, parker, tysta caféer och mycket mer baserat på användarrecensioner och rekommendationer.

## 🌟 Funktioner

- 🔍 Sök och filtrera tysta platser baserat på kategori, taggar eller betyg
- 📝 Läs och skriv recensioner för olika platser
- 📍 Se platsinformation med karta och koordinater
- 📌 Spara favoritplatser som bokmärken
- 👤 Skapa ett konto och anpassa dina preferenser

---

## 🧱 Datamodell

### 🧑‍💻 User (Användare)
Representerar en användare i systemet.
- `name`: Namn på användaren
- `email`: E-postadress (unik)
- `password`: Krypterat lösenord
- `preferences`: Valfria inställningar eller intressen

**Relationer:**
- Har många `reviews`
- Har många `bookmarks`

---

### 🗺️ QuietPlace (Tyst plats)
Representerar en tyst plats, t.ex. ett bibliotek, park eller café.
- `name`: Namn på platsen
- `address`: Adress
- `lat`, `lng`: Geografiska koordinater
- `category`: Typ av plats (bibliotek, park etc.)
- `average_rating`: Genomsnittligt betyg (beräknat från recensioner)
- `tags`: Lista av taggar för filtrering (ex: lugnt, wifi, kaffe)

**Relationer:**
- Har många `reviews`
- Har många `bookmarks`

---

### ✍️ Review (Recension)
Recension av en plats, skapad av en användare.
- `rating`: Betyg (t.ex. 1–5)
- `comment`: Textkommentar
- `date`: Datum för recensionen
- `user_id`: Referens till användaren
- `quiet_place_id`: Referens till platsen

---

### 🔖 Bookmark (Bokmärke)
Visar att en användare har sparat en plats.
- `user_id`: Referens till användaren
- `quiet_place_id`: Referens till platsen
- `created_at`: Datum då bokmärket skapades

---

## 🚀 Installation & Användning

1. Klona repot:
   ```bash
   git clone https://github.com/ditt-namn/quietspace.git
   cd quietspace
