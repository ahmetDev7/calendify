import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

interface Event {
  id: number;
  title: string;
  description: string;
  date: string;
  startTime: string;
  endTime: string;
  location: string;
  adminApproval: boolean;
}

function UpdateEvent() {
  const { id } = useParams<{ id: string }>();
  const [eventData, setEventData] = useState<Event | null>(null);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  useEffect(() => {
    const fetchEventById = async () => {
      try {
        const response = await fetch(`http://localhost:5000/api/event/${id}`);
        if (!response.ok) {
          throw new Error('Failed to fetch event data.');
        }
        const data = await response.json();
        setEventData(data);
      } catch (error) {
        console.error('Error fetching event:', error);
      }
    };

    fetchEventById();
  }, [id]);

  if (!eventData) {
    return <p>Loading...</p>;
  }

    const handleChange = (
      e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
      const { name, value } = e.target;
      setEventData({ ...eventData, [name]: value });
    };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    try {
      const response = await fetch('http://localhost:5000/api/event/'+eventData.id, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        },
        body: JSON.stringify({
          title: eventData.title,
          description: eventData.description,
          date: eventData.date,
          startTime: eventData.date + 'T' + eventData.startTime + 'Z',
          endTime: eventData.date + 'T' + eventData.endTime + 'Z',
          location: eventData.location,
          adminApproval: eventData.adminApproval,
        }),
      });

      const result = await response.json();
      if (!response.ok) {
        setError(result.message || 'An error occurred. Please try again');
        return;
      }

      setSuccess('Event updated!');
    } catch (err) {
      setError('A network error occurred. Please try again.');
    }
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
      <div className="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
        <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Update event
          </h1>

          <form className="space-y-4 md:space-y-6" onSubmit={handleSubmit}>
            {error && <div className="text-red-500">{error}</div>}
            {success && <div className="text-green-500">{success}</div>}

            <div>
              <label
                htmlFor="id"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                ID
              </label>
              <input
                type="text"
                onChange={handleChange}
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                value={eventData.id}
                readOnly
              />
            </div>

            <div>
              <label
                htmlFor="title"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Title
              </label>
              <input
                type="text"
                name="title"
                id="title"
                value={eventData.title}
                onChange={handleChange}
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                placeholder="Enter title"
                required
              />
            </div>

            <div>
              <label
                htmlFor="description"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Description
              </label>
              <textarea
                name="description"
                id="description"
                value={eventData.description}
                onChange={handleChange}
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                placeholder="Enter description"
                required
              />
            </div>

            <div>
              <label
                htmlFor="date"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Date
              </label>
              <input
                type="date"
                name="date"
                id="date"
                value={eventData.date}
                onChange={handleChange}
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required
              />
            </div>

            <div>
              <label
                htmlFor="startTime"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Start Time
              </label>
              <input
                type="time"
                name="startTime"
                id="startTime"
                value={eventData.startTime}
                onChange={handleChange}
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required
              />
            </div>

            <div>
              <label
                htmlFor="endTime"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                End Time
              </label>
              <input
                type="time"
                name="endTime"
                id="endTime"
                value={eventData.endTime}
                onChange={handleChange}
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required
              />
            </div>

            <div>
              <label
                htmlFor="location"
                className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Location
              </label>
              <input
                type="text"
                name="location"
                id="location"
                value={eventData.location}
                onChange={handleChange}
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                placeholder="Enter location"
                required
              />
            </div>

            <div>
              <label className="inline-flex items-center cursor-pointer">
                <input
                  type="checkbox"
                  name="adminApproval"
                  id="adminApproval"
                  checked={eventData.adminApproval}
                  onChange={(e) =>
                    setEventData({
                      ...eventData,
                      adminApproval: e.target.checked,
                    })
                  }
                  className="sr-only peer"
                />
                <div className="relative w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full rtl:peer-checked:after:-translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:start-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-blue-600"></div>
                <span className="ms-3 text-sm font-medium text-gray-900 dark:text-gray-300">
                  Admin Approval
                </span>
              </label>
            </div>

            <button
              type="submit"
              className="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800"
            >
              Save
            </button>
          </form>
        </div>
      </div>
    </>
  );
}

export default UpdateEvent;
