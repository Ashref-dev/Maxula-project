# Maxula Project - HR Management Website

Welcome to Maxula Project, an HR management website designed to streamline human resource processes for organizations! 🌐👥

![Maxula HR](Maxula_preview.gif)

## About

Maxula Project is a comprehensive HR management platform built using Blazor Server, Entity Framework, and PostgreSQL. With a focus on modern web technologies, it integrates Tailwind CSS for sleek and responsive design, and C# for robust backend functionality. Maxula Project aims to enhance HR operations by providing tools for efficient management of employee information, attendance, payroll, and performance.

## Features

- **Employee Management:** Easily add, update, and track employee information including personal details, job roles, etc
- **Attendance Tracking:** Monitor and record employee attendance, leaves, and work hours.
- **Analytics & charts:** Access key company data in the form of charts.
- **Reports and Analytics:** Generate detailed reports about employee attendance.
- **Secure Authentication:** Ensure secure access with role-based authentication and authorization.

## How to Use

Getting started with Maxula Project is straightforward:

1. Clone the repository to your local machine using `git clone https://github.com/yourusername/maxula-project.git`
2. Navigate to the project directory: `cd maxula-project`
3. Open the project in your preferred IDE (e.g., Visual Studio).
4. Set up the PostgreSQL database and update the connection string in the appsettings.json file.
5. Run the application using the IDE's run/debug feature.
6. Access the website locally via `https://localhost:7024/` or the specified port.
7. Begin to manage your HR operations efficiently !

## Contribution

We welcome contributions to enhance Maxula Project and make it a valuable resource for HR management. Here's how you can get involved:

- Fork the repository and implement your desired changes.
- Submit a pull request with a detailed explanation of the enhancements you've made.
- Engage in discussions with other contributors to collaborate and exchange ideas for further improvements.

Let's work together to create a robust and efficient HR management platform. Join us in revolutionizing HR processes!

### Technologies Used

- [Blazor Server](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor): A framework for building interactive web UIs using C#.
- [Entity Framework](https://docs.microsoft.com/en-us/ef/): An ORM framework for data access in .NET applications.
- [PostgreSQL](https://www.postgresql.org/): A powerful, open-source object-relational database system.
- [Tailwind CSS](https://tailwindcss.com/): A utility-first CSS framework for rapid UI development.
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/): A modern, object-oriented programming language developed by Microsoft.

We appreciate your support and contributions. Let's make HR management better together! 🚀👥

### Random anecdotes

Blazor uses a specific model class for every form validation,
so if the login uses a different model and has unfilled fields in the model for the general account structure, it'll not submit successfully until all the model fields are filled even if just artificially.

Databse is on Neon pg, Blazor Server app is on Azure Cloud.

#### command to run tailwind compilation when developing:
npx tailwindcss -i wwwroot/app.css -o wwwroot/app.min.css --watch

#### command to run blazor on VS Code
dotnet watch run

Feel free to reach out for any questions or support. Happy managing!