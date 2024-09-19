# ASP.NET Web API Project Structure

## 1. Login System

### 1.1 Admin Login System

**POST** `/api/login`

- **Description**: Authenticates an admin user and creates a session.
- **Request Body**: `username`, `password`
- **Response**: Success or failure message, session registration

**GET** `/api/login/status`

- **Description**: Checks if a session is active and returns the logged-in admin's name.
- **Response**: Boolean value for session status, admin's name

### Implementation Details

- **Service**: `LoginService`
  - Handles authentication and session management.
- **Controller**: `LoginController`
  - Manages login and session status endpoints.

## 2. Event Management

### 2.2 CRUD API for Event Entity

**GET** `/api/events`

- **Description**: Retrieves a list of events including reviews and attendees. Public endpoint.

**POST** `/api/events`

- **Description**: Creates a new event. Requires admin authentication.
- **Request Body**: `Title`, `Description`, `Date`, `StartTime`, `EndTime`, `Location`

**PUT** `/api/events/{id}`

- **Description**: Updates an existing event. Requires admin authentication.
- **Request Body**: `Title`, `Description`, `Date`, `StartTime`, `EndTime`, `Location`

**DELETE** `/api/events/{id}`

- **Description**: Deletes an event. Requires admin authentication.

### Implementation Details

- **Service**: `EventService`
  - Manages CRUD operations for events.
- **Controller**: `EventController`
  - Manages CRUD operations and public event listing.

## 3. User Roles and Registration

### 3.1 Role Management

**POST** `/api/register`

- **Description**: Registers a new user. Handles user and admin roles.
- **Request Body**: `Username`, `Password`, `Role`

**POST** `/api/login`

- **Description**: Authenticates users, distinguishing between admin and regular users.

### Implementation Details

- **Service**: `RoleService`
  - Handles role-based authentication and user registration.
- **Controller**: `RegistrationController`
  - Manages user registration and role assignment.

## 4. Event Attendance

### 4.1 Attendance Management

**POST** `/api/attendances`

- **Description**: Allows a logged-in user to attend an event.
- **Request Body**: `UserId`, `EventId`
- **Response**: Event details or error message if attendance fails.

**GET** `/api/attendances`

- **Description**: Retrieves a list of events the user has attended. Requires user authentication.

**DELETE** `/api/attendances/{id}`

- **Description**: Removes a user's attendance for a specific event. Requires user authentication.

### Implementation Details

- **Service**: `AttendanceService`
  - Manages event attendance logic.
- **Controller**: `AttendanceController`
  - Manages attendance creation, listing, and deletion.

## 5. Office Attendance

### 5.1 Office Attendance Management

**POST** `/api/office-attendance`

- **Description**: Allows a logged-in user to modify their office attendance.
- **Request Body**: `UserId`, `Date`, `TimeSlot`
- **Response**: Success or error message if the slot is occupied.

### Implementation Details

- **Service**: `OfficeAttendanceService`
  - Manages office attendance logic.
- **Controller**: `OfficeAttendanceController`
  - Manages office attendance modifications.

## Best Practices

- **Separation of Concerns**: Use services to handle business logic and controllers to manage HTTP requests.
- **Error Handling**: Ensure proper error messages and status codes are returned for failed operations.
- **Authentication**: Implement role-based authentication and authorization.
- **Validation**: Validate request data to prevent invalid or harmful input.

---

This structure outlines the key endpoints and services for your ASP.NET Web API project. Ensure each service and controller is properly implemented and tested according to these guidelines.
