import React, { useEffect, useState } from "react";
import axios from "axios";
import Places from "./places";
import QuietPlaceCard from "./QuietPlaceCard";
import "./Test.css";

function Test() {
  const [data, setData] = useState([]);
  const [showPlaces, setShowPlaces] = useState(false);
  const [selectedData, setSelectedData] = useState(null);

  const [formData, setFormData] = useState({
    name: "",
    address: "",
  });

  const refreshPlaces = () => {
    axios
      .get("https://localhost:7220/api/QuietPlace")
      .then((response) => {
        setData(response.data);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  };

  useEffect(() => {
    refreshPlaces();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handlePopup = (item) => {
    setSelectedData(item);
    setShowPlaces(true);
  };

  const createAPICall = (e) => {
    e.preventDefault();
    const token = localStorage.getItem("token");

    if (!token) {
      alert("You must be logged in to create a QuietPlace.");
      return;
    }

    axios
      .post("https://localhost:7220/api/QuietPlace", formData, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        refreshPlaces();
        setFormData({ name: "", address: "" });
      })
      .catch((error) => {
        console.error("Error posting data:", error);
        alert("Error posting data: " + (error.response?.data || error.message));
      });
  };

  const handleDelete = (id) => {
    const token = localStorage.getItem("token");

    if (!token) {
      alert("You must be logged in to delete a QuietPlace.");
      return;
    }

    axios
      .delete(`https://localhost:7220/api/QuietPlace/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then(() => {
        refreshPlaces(); // 👈 updates immediately
        if (selectedData?.id === id) {
          setShowPlaces(false);
          setSelectedData(null);
        }
      })
      .catch((error) => {
        console.error("Error deleting QuietPlace:", error);
        alert("Error deleting: " + (error.response?.data || error.message));
      });
  };

  return (
    <div id="body">
      <div className="mainContainer">
        {showPlaces && selectedData && (
          <Places selectedData={selectedData} onReviewAdded={refreshPlaces} />
        )}

        {data.map((item, index) => (
          <QuietPlaceCard
            key={index}
            item={item}
            onClick={() => handlePopup(item)}
            onDelete={() => handleDelete(item.id)}
          />
        ))}

        <form onSubmit={createAPICall}>
          <h4>Add New Location</h4>
          <input
            type="text"
            name="name"
            value={formData.name}
            onChange={handleInputChange}
            placeholder="Name"
            required
          />
          <input
            type="text"
            name="address"
            value={formData.address}
            onChange={handleInputChange}
            placeholder="Address"
            required
          />
          <button type="submit">Create new location</button>
        </form>
      </div>
    </div>
  );
}

export default Test;
