// QuietPlaceCard.jsx
import React from "react";

function QuietPlaceCard({ place, onClick }) {
  return (
    <div className="subContainer" onClick={() => onClick(place)}>
      <h4>{place.name}</h4>
      <p>{place.address}</p>
      <p>
        Average Rating:{" "}
        {place.averageRating != null && place.averageRating > 0
          ? place.averageRating.toFixed(1)
          : "No reviews yet"}
      </p>

      {place.latestReviewComment ? (
        <div className="latest-review">
          <p>
            <em>"{place.latestReviewComment}"</em>
          </p>
          <p>
            — {place.latestReviewerName ?? "Anonymous"}, Rating:{" "}
            {place.latestReviewRating}
          </p>
        </div>
      ) : (
        <p>No reviews with comments yet.</p>
      )}
    </div>
  );
}

export default QuietPlaceCard;
