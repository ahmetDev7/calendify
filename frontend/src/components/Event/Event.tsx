import React, { useEffect, useState } from 'react';

interface Event {
  id: string;
  title: string;
  description: string;
  date: string;
  startTime: string;
  endTime: string;
  location: string;
  adminApproval: boolean;
}

export default function Event() {
  const [events, setEvents] = useState<Event[]>([]);
  const [errorDelete, setErrorDelete] = useState('');
  const [successDelete, setSuccessDelete] = useState('');
  const userRole = localStorage.getItem('userRole');

  const fetchEvents = async () => {
    try {
      const response = await fetch('http://localhost:5000/api/event/all');
      if (!response.ok) {
        throw new Error('Failed to fetch events.');
      }
      const data = await response.json();
      setEvents(data);
    } catch (error) {
      console.error('Error fetching events:', error);
    }
  };

  useEffect(() => {
    fetchEvents();
  }, []);

  const handleDelete = (id: string) => {
    fetch(`http://localhost:5000/api/event/${id}`, {
      method: 'DELETE',
      headers: {
        Authorization: 'Bearer ' + localStorage.getItem('authToken'),
      },
    })
      .then((response) => {
        if (response.ok) {
          fetchEvents();
          setSuccessDelete('Event deleted!');
        } else {
          setErrorDelete('Failed to delete the event. Please try again.');
        }
      })
      .catch((error) => {
        console.error('Error deleting event:', error);
        setErrorDelete('An error occurred while deleting the event.');
      });
  };

  return (
    <>
      <a
        href="#"
        className="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-white"
      >
        <img
          className="w-8 h-8 mr-2"
          src="https://flowbite.s3.amazonaws.com/blocks/marketing-ui/logo.svg"
          alt="logo"
        />
        Calendify
      </a>
      <div className="w-[1280px] bg-white rounded-lg shadow dark:border md:mt-0 xl:p-0 dark:bg-gray-800 dark:border-gray-700">
        <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Events
          </h1>

          {errorDelete && <div className="text-red-500">{errorDelete}</div>}
          {successDelete && (
            <div className="text-green-500">{successDelete}</div>
          )}

          <div className="relative overflow-x-auto shadow-md sm:rounded-lg">
            <table className="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
              <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                <tr>
                  <th scope="col" className="px-6 py-3">
                    ID
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Title
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Description
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Date
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Start time
                  </th>
                  <th scope="col" className="px-6 py-3">
                    End time
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Location
                  </th>
                  <th scope="col" className="px-6 py-3">
                    Admin approval
                  </th>
                  {userRole == 'admin' && (
                    <th scope="col" className="px-6 py-3">
                      Actions
                    </th>
                  )}
                </tr>
              </thead>
              <tbody>
                {events.map((event, index) => (
                  <tr
                    key={index}
                    className="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700"
                  >
                    <th
                      scope="row"
                      className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
                    >
                      {event.id.length > 15
                        ? `${event.id.substring(0, 15)}...`
                        : event.id}
                    </th>
                    <td className="px-6 py-4">{event.title}</td>
                    <td className="px-6 py-4">{event.description}</td>
                    <td className="px-6 py-4">{event.date}</td>
                    <td className="px-6 py-4">{event.startTime}</td>
                    <td className="px-6 py-4">{event.endTime}</td>
                    <td className="px-6 py-4">{event.location}</td>
                    <td className="px-6 py-4">
                      {event.adminApproval ? 'Yes' : 'No'}
                    </td>

                    {userRole == 'admin' && (
                      <td className="px-6 py-4">
                        <div className="flex flex-col gap-2">
                          <a className="font-medium text-blue-500 hover:underline hover:cursor-pointer">
                            Edit
                          </a>
                          <a
                            className="font-medium text-rose-600  hover:underline hover:cursor-pointer"
                            onClick={() => handleDelete(event.id)}
                          >
                            Delete
                          </a>
                        </div>
                      </td>
                    )}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </>
  );
}
