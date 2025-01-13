import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar.tsx'
import Register from './components/Register.tsx';

function App() {
  return (
    <>
      <Router>
        <Navbar/>
        <div className="content">
          <div className="content-inner">
            <Routes>
              <Route path="/" element={<div>Home Page</div>} />
              <Route path="/about" element={<div>About Page</div>} />
              <Route path="/services" element={<div>Services Page</div>} />
              <Route path="/contact" element={<div>Contact Page</div>} />
              <Route path="/login" element={<div>Login Page</div>} />
              <Route path="/register" element={<Register/>} />
            </Routes>
          </div>
        </div>
      </Router>
    </>
  )
}

export default App
