import React, { useEffect, useState } from 'react';

function Dashboard() {
  const [userInfo, setUserInfo] = useState<{
    firstName: string;
    lastName: string;
    email: string;
    role: string;
  } | null>(null);

  useEffect(() => {
    const token = localStorage.getItem('authToken');

    if (token) {
      try {
        // Token decoderen zonder jwt-decode
        const payload = token.split('.')[1]; // Pak het payload-gedeelte
        const decodedPayload = atob(payload); // Base64 decoderen
        const userData = JSON.parse(decodedPayload); // String naar object

        setUserInfo({
          firstName: userData.given_name || "Unknown", // 'given_name' uit token
          lastName: userData.family_name || "Unknown",    // 'surname' uit token
          email: userData.unique_name || "Unknown",  // 'unique_name' voor e-mail
          role: userData.role
        });
        localStorage.setItem("userRole", userData.role);
      } catch (error) {
        console.error("Fout bij het decoderen van token:", error);
      }
    }
  }, []);

  if (!userInfo) {
    return (
      <div>
        <p>Log in om toegang te krijgen tot het dashboard.</p>
      </div>
    );
  }

  return (
    <>
      <a href="#" className="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-white">
        <img className="w-8 h-8 mr-2" src="https://flowbite.s3.amazonaws.com/blocks/marketing-ui/logo.svg" alt="logo" />
        Calendify
      </a>
      <div className="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-3xl xl:p-0 dark:bg-gray-800 dark:border-gray-700">
        <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Welcome back, {userInfo.firstName} {userInfo.lastName}!
          </h1>
          <p className="text-cyan-600 dark:text-cyan-400">E-mail: {userInfo.email}</p>
        </div>
      </div>
    </>
  );
}

export default Dashboard;
