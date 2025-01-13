import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar.tsx'
import Register from './components/Register.tsx';
import Login from './components/Login.tsx';

function App() {
  return (
    <>
      <Router>
        <Navbar/>
        <div className="content">
          <div className="content-inner">
            <Routes>
              <Route path="/" element={<div>Home Page</div>} />
              <Route path="/event" element={<div>Event Page</div>} />
              <Route path="/login" element={<Login/>} />
              <Route path="/register" element={<Register/>} />
            </Routes>
          </div>
        </div>
      </Router>
    </>
  )
}

export default App
