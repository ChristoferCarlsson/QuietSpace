import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.jsx";
import Test from "./Components/Test.jsx";
import FrontPage from "./Pages/FrontPage.jsx";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <FrontPage />
  </StrictMode>
);
