import React, { useEffect, useState } from "react";
import axios from "axios";
import Places from "./places";

import "./Test.css";

function Test() {
  const [data, setData] = useState([]);
  const [showPlaces, setShowPlaces] = useState(false);

  const [formData, setFormData] = useState({
    name: "",
    address: "",
    latitude: 0,
    longitude: 0,
    category: "",
    averageRating: 0,
    tags: "",
  });

  useEffect(() => {
    axios
      .get("https://localhost:7220/api/QuietPlace")
      .then((response) => {
        setData(response.data);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const createAPICall = (e) => {
    e.preventDefault();

    const payload = {
      ...formData,
      latitude: parseFloat(formData.latitude),
      longitude: parseFloat(formData.longitude),
      averageRating: parseFloat(formData.averageRating),
    };

    axios
      .post("https://localhost:7220/api/QuietPlace", payload)
      .then((response) => {
        console.log("Post successful:", response.data);
        setData((prevData) => [...prevData, response.data]);
      })
      .catch((error) => {
        console.error("Error posting data:", error);
      });
  };

  return (
    <div id="body">
      <div className="mainContainer">
        {showPlaces ? <Places /> : ""}
        {data.map((item, index) => (
          <div
            onClick={() => setShowPlaces(!showPlaces)}
            className="subContainer"
            key={index}
          >
            <h4>{item.name}</h4>
            <p>{item.address}</p>
          </div>
        ))}

        <form onSubmit={createAPICall}>
          <h4>Add New Location</h4>

          <input
            type="text"
            name="name"
            value={formData.name}
            onChange={handleInputChange}
            placeholder="Name"
          />
          <input
            type="text"
            name="address"
            value={formData.address}
            onChange={handleInputChange}
            placeholder="Address"
          />
          <input
            type="number"
            name="latitude"
            value={formData.latitude}
            onChange={handleInputChange}
            placeholder="Latitude"
          />
          <input
            type="number"
            name="longitude"
            value={formData.longitude}
            onChange={handleInputChange}
            placeholder="Longitude"
          />
          <input
            type="text"
            name="category"
            value={formData.category}
            onChange={handleInputChange}
            placeholder="Category"
          />
          <input
            type="number"
            name="averageRating"
            value={formData.averageRating}
            onChange={handleInputChange}
            placeholder="Average Rating"
          />
          <input
            type="text"
            name="tags"
            value={formData.tags}
            onChange={handleInputChange}
            placeholder="Tags"
          />

          <button type="submit">Create new location</button>
        </form>
      </div>
    </div>
  );
}

export default Test;
