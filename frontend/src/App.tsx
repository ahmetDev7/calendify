import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar.tsx';
import Register from './components/Register.tsx';
import Login from './components/Login.tsx';
import PrivateRoute from './components/PrivateRoute.tsx'; // Import the PrivateRoute component
import Dashboard from './components/Dashboard.tsx';

function App() {
  return (
    <>
      <Router>
        <Navbar />
        <div className="content">
          <div className="content-inner">
            <Routes>
              <Route element={<PrivateRoute />}>
                <Route path="/" element={<Dashboard/>} />
              </Route>
              <Route element={<PrivateRoute />}>
                <Route path="/event" element={<div>Event Page</div>} />
              </Route>
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
            </Routes>
          </div>
        </div>
      </Router>
    </>
  );
}

export default App;
