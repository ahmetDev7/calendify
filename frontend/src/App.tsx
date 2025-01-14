import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar.tsx';
import Register from './components/Register.tsx';
import Login from './components/Login.tsx';
import CreateEvent from './components/Event/CreateEvent.tsx';
import UpdateEvent from './components/Event/UpdateEvent.tsx';
import Event from './components/Event/Event.tsx';
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
              <Route path="/events" element={<Event/>} />
              <Route element={<PrivateRoute />}>                
                <Route path="/event/create" element={<CreateEvent/>} />
                <Route path="/event/update/:id" element={<UpdateEvent />} />
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
