import React, { useEffect, useState } from "react";
import axios from "axios";
import "./Places.css"; // Optional: for modal styles

function Places({ selectedData, onReviewAdded }) {
  const [reviewData, setReviewData] = useState({ rating: "", comment: "" });
  const [message, setMessage] = useState("");
  const [showReviewPopup, setShowReviewPopup] = useState(false);

  useEffect(() => {
    if (selectedData) {
      setShowReviewPopup(true); // Auto-open modal when a place is selected
    }
  }, [selectedData]);

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
      setShowReviewPopup(false); // Close after submit

      if (onReviewAdded) {
        onReviewAdded();
      }
    } catch (error) {
      console.error("Error submitting review:", error);
      const errorMsg =
        error.response?.data?.message || error.response?.data || error.message;
      setMessage("Review failed: " + errorMsg);
    }
  };

  return (
    <div>
      {showReviewPopup && (
        <div className="modal-overlay">
          <div className="modal-content">
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
              <button
                type="button"
                onClick={() => setShowReviewPopup(false)}
                className="cancel-button"
              >
                Cancel
              </button>
            </form>
          </div>
        </div>
      )}

      {message && <p style={{ color: "green" }}>{message}</p>}
    </div>
  );
}

export default Places;
