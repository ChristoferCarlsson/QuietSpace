document.addEventListener('DOMContentLoaded', function () {
  // Load saved places and login status
  let savedPlaces = JSON.parse(localStorage.getItem('places')) || [];
  const loggedIn = localStorage.getItem('loggedIn') === 'true';

  // Toggle login/logout links if present
  const loginLink = document.getElementById('loginLink');
  const registerLink = document.getElementById('registerLink');
  const logoutBtn = document.getElementById('logoutBtn');

  if (loginLink && registerLink && logoutBtn) {
    loginLink.style.display = loggedIn ? 'none' : 'inline-block';
    registerLink.style.display = loggedIn ? 'none' : 'inline-block';
    logoutBtn.style.display = loggedIn ? 'inline-block' : 'none';

    logoutBtn.addEventListener('click', () => {
      localStorage.removeItem('loggedIn');
      alert('Du är nu utloggad');
      location.reload();
    });
  }

  // Render all saved places
  function renderSavedPlaces() {
    const placeList = document.getElementById('placeList');
    placeList.innerHTML = '';
    savedPlaces.forEach((place, index) => {
      addPlaceToDOM(place.name, place.address, place.rating, index);
    });
  }

  // Add a single place to the DOM
  function addPlaceToDOM(name, address, rating, index) {
    const placeList = document.getElementById('placeList');
    const placeEntry = document.createElement('div');
    placeEntry.classList.add('placeEntry');
    placeEntry.innerHTML = `
      <strong>${name}</strong><br>
      ${address}<br>
      Betyg: ${rating} / 5<br>
      <button class="bookmarkBtn smallBtn">🔖 Spara</button>
      <button class="removeBtn smallBtn">❌ Ta bort</button>
      <hr>
    `;

    const bookmarkBtn = placeEntry.querySelector('.bookmarkBtn');

    bookmarkBtn.addEventListener('click', function () {
      if (!loggedIn) {
        alert("Du måste vara inloggad för att spara platser.");
        return;
      }

      placeEntry.classList.toggle('bookmarked');
      const isBookmarked = placeEntry.classList.contains('bookmarked');
      bookmarkBtn.textContent = isBookmarked ? '★ Sparad' : '🔖 Spara';

      const bookmarkedList = document.getElementById('bookmarkedList');
      if (isBookmarked) {
        const listItem = document.createElement('li');
        listItem.textContent = name;
        listItem.setAttribute('data-name', name);
        bookmarkedList.appendChild(listItem);
      } else {
        const items = bookmarkedList.querySelectorAll('li');
        items.forEach(item => {
          if (item.getAttribute('data-name') === name) {
            item.remove();
          }
        });
      }
    });

    // Always show review buttons (no login required)
    const reviewsBtn = document.createElement('button');
    reviewsBtn.textContent = '📖 Läs recensioner';
    reviewsBtn.className = 'reviewsBtn smallBtn';
    placeEntry.appendChild(reviewsBtn);
    reviewsBtn.addEventListener('click', function () {
      openReviewPopup(name);
    });

    const myReviewBtn = document.createElement('button');
    myReviewBtn.textContent = '📝 Min recension';
    myReviewBtn.className = 'myReviewBtn smallBtn';
    placeEntry.appendChild(myReviewBtn);
    myReviewBtn.addEventListener('click', function () {
      openWriteReviewPopup(name);
    });

    // Delete this place
    placeEntry.querySelector('.removeBtn').addEventListener('click', function () {
      savedPlaces.splice(index, 1);
      localStorage.setItem('places', JSON.stringify(savedPlaces));
      renderSavedPlaces();
    });

    placeList.appendChild(placeEntry);
  }

  // Handle form submission
  document.getElementById('placeForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const name = document.getElementById('name').value;
    const address = document.getElementById('address').value;
    const rating = document.getElementById('rating').value;

    const newPlace = { name, address, rating };
    savedPlaces.push(newPlace);
    localStorage.setItem('places', JSON.stringify(savedPlaces));

    addPlaceToDOM(name, address, rating, savedPlaces.length - 1);
    document.getElementById('placeForm').reset();
  });

  // Show reviews (stored or fallback)
  function openReviewPopup(placeName) {
    const reviews = JSON.parse(localStorage.getItem('reviews')) || {};
    const userReview = reviews[placeName];
    const exampleReviews = `
      <ul>
        <li>🌟 Mysig plats att jobba från.</li>
        <li>☕ Utmärkt kaffe och tyst miljö.</li>
        <li>🪑 Bekväma sittplatser, rekommenderas!</li>
      </ul>
    `;
    const popup = window.open('', '', 'width=400,height=300');
    popup.document.write(`
      <html>
        <head><title>Recension för ${placeName}</title></head>
        <body style="font-family:sans-serif; padding: 20px; background-color:#111; color:white;">
          <h2>Recension för ${placeName}</h2>
          ${userReview ? `<p>${userReview}</p>` : exampleReviews}
          <button onclick="window.close()" style="margin-top:10px;">Stäng</button>
        </body>
      </html>
    `);
  }

  // Write a review and save it
  function openWriteReviewPopup(placeName) {
    const savedReviews = JSON.parse(localStorage.getItem('reviews')) || {};
    const existingReview = savedReviews[placeName] || '';

    const popup = window.open('', '', 'width=400,height=300');
    popup.document.write(`
      <html>
        <head><title>Min recension för ${placeName}</title></head>
        <body style="font-family:sans-serif; padding: 20px; background-color:#111; color:white;">
          <h2>Skriv din recension för ${placeName}</h2>
          <textarea id="myReview" style="width:100%; height:100px;">${existingReview}</textarea><br><br>
          <button id="saveBtn">Spara</button>
          <button onclick="window.close()">Avbryt</button>
          <script>
            document.getElementById('saveBtn').addEventListener('click', () => {
              const review = document.getElementById('myReview').value;
              const reviews = JSON.parse(localStorage.getItem('reviews')) || {};
              reviews["${placeName}"] = review;
              localStorage.setItem('reviews', JSON.stringify(reviews));
              alert('Recension sparad!');
              window.close();
            });
          </script>
        </body>
      </html>
    `);
  }


  // Start rendering
  renderSavedPlaces();
});
