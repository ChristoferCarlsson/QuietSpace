import React from "react";
import "./Test.css"; // Optional, reuse styling
import "./style.css";

import axios from "axios";

function QuietPlaceCard({ item, onClick, onDelete }) {
  const handleDelete = async (e) => {
    e.stopPropagation(); // prevent triggering onClick
    const token = localStorage.getItem("token");

    if (!token) {
      alert("You must be logged in to delete a QuietPlace.");
      return;
    }

    if (!window.confirm("Are you sure you want to delete this QuietPlace?"))
      return;

    try {
      await axios.delete(`https://localhost:7220/api/QuietPlace/${item.id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (onDelete) {
        onDelete(); // Refresh the list from parent
      }
    } catch (error) {
      console.error("Error deleting QuietPlace:", error);
      alert("Failed to delete QuietPlace.");
    }
  };

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
      {item.latestReviewComment && (
        <>
          <p>
            <em>"{item.latestReviewComment}"</em>
          </p>
          <p>– {item.latestReviewRating}/5</p>
        </>
      )}
      <button className="removeBtn" onClick={handleDelete}>
        X
      </button>
    </div>
  );
}

export default QuietPlaceCard;
