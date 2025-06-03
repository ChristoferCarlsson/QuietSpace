import React from "react";
import "./Test.css";

function Places({ selectedData }) {
  return (
    <div className="popup">
      <h4>{selectedData.name}</h4>
      <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed elit velit,
        sagittis eu neque ac, venenatis eleifend risus. Proin consectetur
        blandit tortor. Nam varius id tortor mollis ullamcorper. Phasellus a
        fringilla diam. Proin in rutrum nisl. Mauris sem neque, blandit in diam
        eget, hendrerit ullamcorper lectus. Cras non odio eu mi sodales varius
        sed vitae turpis. Donec a condimentum mauris. Suspendisse arcu nulla,
        tempor id feugiat ac, ultricies ac mauris. Vestibulum ante ipsum primis
        in faucibus orci luctus et ultrices posuere cubilia curae;
      </p>
    </div>
  );
}

export default Places;
