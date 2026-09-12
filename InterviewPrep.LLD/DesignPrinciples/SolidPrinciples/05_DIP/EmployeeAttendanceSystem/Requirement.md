Enterprise Project
Employee Attendance & Notification System
Folder Structure
EmployeeAttendanceSystem
│
├── Models
│      Employee.cs
│      AttendanceRecord.cs
│      NotificationMessage.cs
│      AttendanceResult.cs
│
├── Interfaces
│      IAttendanceService.cs
│      INotificationService.cs
│
├── Services
│      AttendanceService.cs
│      EmailNotificationService.cs
│      SmsNotificationService.cs
│      TeamsNotificationService.cs
│
├── Exceptions
│      AttendanceException.cs
│
└── Program.cs
Business Workflow
Employee Marks Attendance
            │
            ▼
AttendanceService
            │
Attendance Validation
            │
Attendance Saved
            │
            ▼
INotificationService
            │
     ┌──────┼────────┐
     ▼      ▼        ▼
 Email     SMS     Teams

Notice something important.

AttendanceService never talks to

Email
SMS
Teams

It only knows

INotificationService

This is DIP.