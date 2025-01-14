import React, { useEffect, useState } from 'react';

interface Attendance {
  id: string;
  date: string;  
}

function formatDateTime(dateString: string): string {
  const date = new Date(dateString);

  const options: Intl.DateTimeFormatOptions = {
    weekday: 'long',
    year: 'numeric',
    month: 'short',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    hour12: false,
  };

  return new Intl.DateTimeFormat('en-US', options).format(date);
}

export default function CreateAttendance() {
  const [attendances, setAttendances] = useState<Attendance[]>([]);
  const [formData, setFormData] = useState({ date: '' });
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const fetchAttendees = async () => {
    try {
      const response = await fetch('http://localhost:5000/api/attendance/planned', {
        method: 'GET',
        headers: {
          Authorization: 'Bearer ' + localStorage.getItem('authToken'),
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error('Failed to fetch attendance data.');
      }

      const data = await response.json();
      setAttendances(data);
    } catch (error) {
      console.error('Error fetching attendance data:', error);
    }
  };

  useEffect(() => {
    fetchAttendees();
  }, []);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    // Reset messages
    setError('');
    setSuccess('');

    try {
      const response = await fetch('http://localhost:5000/api/attendance', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        },
        body: JSON.stringify({
          date: formData.date + 'Z',
        }),
      });

      const result = await response.json();

      if (!response.ok) {
        setError(result.message || 'An error occurred. Please try again');
        return;
      }

      setSuccess('Attendance planned!');
      // Reset form to initial state
      setFormData({
        date: '',
      });
      fetchAttendees();
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

      <div className="flex gap-10 ">
        <div className="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
          <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
            <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
              Plan office attendance
            </h1>

            <form className="space-y-4 md:space-y-6" onSubmit={handleSubmit}>
              {error && <div className="text-red-500">{error}</div>}
              {success && <div className="text-green-500">{success}</div>}

              <div>
                <label
                  htmlFor="date"
                  className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >
                  Date
                </label>
                <input
                  type="datetime-local"
                  name="date"
                  id="date"
                  value={formData.date}
                  onChange={handleChange}
                  className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  required
                />
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

        <div className="h-[800px] w-[700px] border-solid border-2 border-sky-500 rounded px-8 py-4 overflow-auto">
          <ol className="relative border-s border-gray-200 dark:border-gray-700">
            {attendances.map((attendance, index) => (
              <li className="mb-10 ms-6" key={index}>
                <span className="absolute flex items-center justify-center w-6 h-6 bg-blue-100 rounded-full -start-3 ring-8 ring-white dark:ring-gray-900 dark:bg-blue-900">
                  <svg
                    className="w-2.5 h-2.5 text-blue-800 dark:text-blue-300"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="currentColor"
                    viewBox="0 0 20 20"
                  >
                    <path d="M20 4a2 2 0 0 0-2-2h-2V1a1 1 0 0 0-2 0v1h-3V1a1 1 0 0 0-2 0v1H6V1a1 1 0 0 0-2 0v1H2a2 2 0 0 0-2 2v2h20V4ZM0 18a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V8H0v10Zm5-8h10a1 1 0 0 1 0 2H5a1 1 0 0 1 0-2Z" />
                  </svg>
                </span>
                <h3 className="mb-1 text-lg font-semibold text-gray-900 dark:text-white">
                  Office attendance
                </h3>

                <time className="block mb-2 text-sm font-normal leading-none text-gray-400 dark:text-gray-500">
                  {formatDateTime(attendance.date)}
                </time>
              </li>
            ))}
          </ol>
        </div>
      </div>
    </>
  );
}
