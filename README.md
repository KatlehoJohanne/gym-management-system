# Gym Management System

A C# Windows Forms desktop application built to manage member information, class schedules, and training programs for a local gym.

This was a group project for Programming 262 (Belgium Campus iTversity).

## Features
- **User Authentication** — Secure login system for gym staff, with account lockout after three failed login attempts and admin unlock functionality.
- **Member Management** — Full CRUD operations (Create, Read, Update, Delete) for member records, including personal details, membership dates, and assigned training programs.
- **Class & Training Program Management** — CRUD operations for managing class/program details, including instructor, schedule, capacity, and duration.
- **Search Functionality** — Search members by ID or name, and search for all members assigned to a specific instructor.
- **Exception Handling** — Custom exceptions implemented to handle specific error scenarios gracefully.
- **Data Persistence** — Member and class data stored in a SQL Server database via ADO.NET; staff login credentials stored in a text file.

## Tech Stack
- **Language:** C#
- **UI Framework:** Windows Forms
- **Database:** SQL Server
- **Data Access:** ADO.NET
- **IDE:** Microsoft Visual Studio

## Project Structure
- `Classes/` — Core application classes (models and business logic)
- `Database/` — Database connection and query handling
- `Forms/` — Windows Forms UI components
- `Program.cs` — Application entry point

## How to Run
1. Clone or download this repository.
2. Open `GymManagement.sln` in Visual Studio.
3. Set up the SQL Server database (see `Database/` for schema/scripts).
4. Build and run the project.
5. Log in with staff credentials to access the main application.

## Project Context
Developed as part of a 3-member group project for the Programming 262 module, focusing on Windows Forms GUI development, ADO.NET database connectivity, file handling, and exception handling.
