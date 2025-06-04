import React from "react";
import "./Test.css"; // Optional, reuse styling
import "./style.css";

function QuietPlaceCard({ item, onClick }) {
  console.log(item.latestReviewComment);
  return (
    <div className="placeEntry" onClick={onClick}>
      <h4>{item.name}</h4>
      <p>{item.address}</p>
      <p>
        Average Rating:{" "}
        {item.averageRating != null && item.averageRating > 0
          ? item.averageRating.toFixed(1)
          : "No reviews yet"}
      </p>
      {item.latestReviewComment ? (
        <>
          <p style={{ fontStyle: "italic" }}>"{item.latestReviewComment}"</p>
          <p>Rating: {item.latestReviewRating}/5</p>
          {item.latestReviewUserName && <p>— {item.latestReviewUserName}</p>}
        </>
      ) : (
        <p>No reviews with comments yet.</p>
      )}
    </div>
  );
}

export default QuietPlaceCard;
