import React, { useState } from "react";
import axios from "axios";
import "./style.css";

function AuthForm() {
  const [isRegistering, setIsRegistering] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem("token"));
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    password: "",
  });
  const [message, setMessage] = useState("");

  const toggleMode = () => {
    setIsRegistering((prev) => !prev);
    setMessage("");
    setFormData({ name: "", email: "", password: "" });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    setIsLoggedIn(false);
    setMessage("Logged out successfully.");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");

    const endpoint = isRegistering
      ? "https://localhost:7220/api/Auth/register"
      : "https://localhost:7220/api/Auth/login";

    const payload = isRegistering
      ? {
          name: formData.name,
          email: formData.email,
          password: formData.password,
        }
      : {
          email: formData.email,
          password: formData.password,
        };

    try {
      const response = await axios.post(endpoint, payload);

      if (!isRegistering) {
        const token = response.data.token;
        localStorage.setItem("token", token);
        setMessage("Login successful!");
        setIsLoggedIn(true);
      } else {
        setMessage("Registration successful! You can now log in.");
      }
    } catch (error) {
      console.error("Auth error:", error);
      const errorMsg =
        error.response?.data?.message || error.response?.data || error.message;
      setMessage("Something went wrong: " + errorMsg);
    }
  };

  return (
    <div className="auth-form">
      <h1>Quiet Space</h1>

      {isLoggedIn ? (
        <>
          <h2>You're logged in</h2>
          <button onClick={handleLogout}>Logout</button>
          <p style={{ color: "green" }}>{message}</p>
        </>
      ) : (
        <>
          <h2>{isRegistering ? "Register" : "Login"}</h2>
          <form onSubmit={handleSubmit}>
            {isRegistering && (
              <input
                type="text"
                name="name"
                value={formData.name}
                onChange={handleChange}
                placeholder="Name"
                required
              />
            )}

            <input
              type="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              placeholder="Email"
              required
            />

            <input
              type="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
              placeholder="Password"
              required
            />

            <button type="submit">
              {isRegistering ? "Register" : "Login"}
            </button>
          </form>

          <p style={{ color: "green" }}>{message}</p>
          <button onClick={toggleMode}>
            {isRegistering
              ? "Already have an account? Login"
              : "Need an account? Register"}
          </button>
        </>
      )}
    </div>
  );
}

export default AuthForm;
