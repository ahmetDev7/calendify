import { NavLink } from 'react-router-dom';
import React, { useState } from 'react';

function Register() {
  // State to hold form values
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    repeatPassword: '',
  });
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  // Handle input changes
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  // Handle form submission
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Basic form validation for password match
    if (formData.password !== formData.repeatPassword) {
      setError("Passwords don't match");
      return;
    }

    try {
        const response = await fetch('http://localhost:5000/api/register', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: formData.email,
            password: formData.password,
            recurringDays: 0,
          }),
        });
      
        let result;
        const contentType = response.headers.get('Content-Type');
      
        if (contentType && contentType.includes('application/json')) {
          result = await response.json(); // Probeer JSON te parseren als het beschikbaar is
        } else {
          result = await response.text(); // Anders lees de respons als platte tekst
        }
      
        if (!response.ok) {
          if (response.status === 400) {
            setSuccess("");
            setError(result || 'User already exists.'); // Gebruik de platte tekst of een fallback
          } else {
            setSuccess("");
            setError('An unexpected error occurred.');
          }
          return;
        }
      
        // Succesvolle respons
        console.log('Registratie gelukt:', result);
        setError("");
        setSuccess("User has been registered!");
      } catch (err) {
        console.error('Netwerkfout:', err);
        setError('A network error occurred. Please try again.');
        setSuccess("");
      }
    }      

  return (
    <>
      <a href="#" className="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-white">
        <img className="w-8 h-8 mr-2" src="https://flowbite.s3.amazonaws.com/blocks/marketing-ui/logo.svg" alt="logo"/>
        Calendify    
      </a>
      <div className="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
        <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Register
          </h1>
          <form className="space-y-4 md:space-y-6" onSubmit={handleSubmit}>
            {error && <div className="text-red-500">{error}</div>}
            {success && <div className="text-green-500">{success}</div>}
            
            {/* First Name Field */}
            <div>
              <label htmlFor="firstName" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Your firstname</label>
              <input
                type="text"
                name="firstName"
                id="firstName"
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                placeholder="First name"
                required
                value={formData.firstName}
                onChange={handleChange}
              />
            </div>

            {/* Last Name Field */}
            <div>
              <label htmlFor="lastName" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Your lastname</label>
              <input
                type="text"
                name="lastName"
                id="lastName"
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                placeholder="Last name"
                required
                value={formData.lastName}
                onChange={handleChange}
              />
            </div>

            {/* Email Field */}
            <div>
              <label htmlFor="email" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Your email</label>
              <input
                type="email"
                name="email"
                id="email"
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                placeholder="youremail@example.com"
                required
                value={formData.email}
                onChange={handleChange}
              />
            </div>

            {/* Password Field */}
            <div>
              <label htmlFor="password" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Password</label>
              <input
                type="password"
                name="password"
                id="password"
                placeholder="••••••••"
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required
                value={formData.password}
                onChange={handleChange}
              />
            </div>

            {/* Repeat Password Field */}
            <div>
              <label htmlFor="repeatpassword" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Repeat password</label>
              <input
                type="password"
                name="repeatPassword"
                id="repeatpassword"
                placeholder="••••••••"
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required
                value={formData.repeatPassword}
                onChange={handleChange}
              />
            </div>

            <button
              type="submit"
              className="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800"
            >
              Register
            </button>

            <p className="text-sm font-light text-gray-500 dark:text-gray-400">
              Already have an account? <NavLink to="/login" className="font-medium text-primary-600 hover:underline dark:text-primary-500">Login</NavLink>
            </p>
          </form>
        </div>
      </div>
    </>
  );
}

export default Register;
