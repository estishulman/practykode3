// import React from 'react';
// import ReactDOM from 'react-dom';
// import App from './App';

// ReactDOM.render(<App />, document.getElementById('root'));

import React from 'react';
import ReactDOM from 'react-dom/client'; // שים לב לשינוי כאן
import App from './App';

const root = ReactDOM.createRoot(document.getElementById('root')); // שימוש ב-createRoot
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
