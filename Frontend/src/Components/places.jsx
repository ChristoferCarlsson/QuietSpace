import React, { useState } from "react";
import axios from "axios";

function Places({ selectedData, onReviewAdded }) {
  const [reviewData, setReviewData] = useState({ rating: "", comment: "" });
  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setReviewData((prev) => ({ ...prev, [name]: value }));
  };

  const handleReviewSubmit = async (e) => {
    e.preventDefault();
    const token = localStorage.getItem("token");

    if (!token) {
      setMessage("You must be logged in to submit a review.");
      return;
    }

    try {
      await axios.post(
        "https://localhost:7220/api/Review",
        {
          placeId: selectedData.id,
          rating: parseInt(reviewData.rating),
          comment: reviewData.comment,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      setMessage("Review submitted successfully!");
      setReviewData({ rating: "", comment: "" });

      // 👇 Refresh the QuietPlaces data in parent
      if (onReviewAdded) {
        onReviewAdded();
      }
    } catch (error) {
      console.error("Error submitting review: here it is", error);
      const errorMsg =
        error.response?.data?.message || error.response?.data || error.message;
      setMessage("Review failed: " + errorMsg);
    }
  };

  return (
    <div className="place-popup">
      <h3>{selectedData.name}</h3>
      <p>{selectedData.address}</p>
      <p>
        Average Rating:{" "}
        {selectedData.averageRating != null
          ? selectedData.averageRating.toFixed(1)
          : "No reviews yet"}
      </p>

      <form onSubmit={handleReviewSubmit}>
        <h4>Leave a Review</h4>
        <input
          type="number"
          name="rating"
          min="1"
          max="5"
          value={reviewData.rating}
          onChange={handleChange}
          placeholder="Rating (1–5)"
          required
        />
        <textarea
          name="comment"
          value={reviewData.comment}
          onChange={handleChange}
          placeholder="Write your thoughts..."
          required
        />
        <button type="submit">Submit Review</button>
      </form>

      {message && <p style={{ color: "green" }}>{message}</p>}
    </div>
  );
}

export default Places;
