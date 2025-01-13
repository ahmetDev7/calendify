import React from 'react';
import { NavLink } from 'react-router-dom';

function Navbar() {
  return (
    <>
      <nav className="bg-white border-gray-200 dark:bg-gray-900">
        <div className="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4">
          <NavLink to="/" className="flex items-center space-x-3 rtl:space-x-reverse">
            <img src="https://flowbite.com/docs/images/logo.svg" className="h-8" alt="Calendify Logo" />
            <span className="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">Calendify</span>
          </NavLink>
          <div className="flex md:order-2 space-x-3 md:space-x-0 rtl:space-x-reverse">
            <NavLink to="/login">
              <button type="button" className="mx-2 text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Login</button>
            </NavLink>
            <NavLink to="/register">
              <button type="button" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Register</button>
            </NavLink>
          </div>
          <div className="items-center justify-between hidden w-full md:flex md:w-auto md:order-1" id="navbar-cta">
            <ul className="flex flex-col font-medium p-4 md:p-0 mt-4 border border-gray-100 rounded-lg bg-gray-50 md:space-x-8 rtl:space-x-reverse md:flex-row md:mt-0 md:border-0 md:bg-white dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700">
              <li>
                <NavLink
                  to="/"
                  className={({ isActive }: { isActive: boolean }) => 
                    `block py-2 px-3 md:p-0 ${isActive ? 'text-blue-500' : 'text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700'}`
                  }
                  aria-current="page"
                >
                  Dashboard
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/event/create"
                  className={({ isActive }: { isActive: boolean }) => 
                    `block py-2 px-3 md:p-0 ${isActive ? 'text-blue-500' : 'text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700'}`
                  }
                  aria-current="page"
                >
                  Create event
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/about"
                  className={({ isActive }: { isActive: boolean }) => 
                    `block py-2 px-3 md:p-0 ${isActive ? 'text-blue-500' : 'text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700'}`
                  }
                >
                  About
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/services"
                  className={({ isActive }: { isActive: boolean }) => 
                    `block py-2 px-3 md:p-0 ${isActive ? 'text-blue-500' : 'text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700'}`
                  }
                >
                  Services
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/contact"
                  className={({ isActive }: { isActive: boolean }) => 
                    `block py-2 px-3 md:p-0 ${isActive ? 'text-blue-500' : 'text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700'}`
                  }
                >
                  Contact
                </NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </>
  );
}

export default Navbar;
