# Employee Management System

## Overview

Hello there! 👋 Welcome to my Employee Management System project. This C# .NET 6 WPF application is designed to streamline the management of employee details using the GoRest API. It features a user-friendly interface for performing CRUD operations on employee data, integrating seamlessly with the GoRest API.

## Features

### Application Features

1. **Create Employee:** Easily add a new employee with all the necessary details.
![image](https://github.com/sarkeranik/EMS/assets/65606710/2e278ea5-82f6-4b28-99b3-a48e93e2ddea)

2. **Update Employee:** Effortlessly modify existing employee information as needed. (Double-click on any row of the Data table, Modify the data, and Click Update.) 
![image](https://github.com/sarkeranik/EMS/assets/65606710/35956fb6-8540-4a71-8979-236696e11121)

3. **Delete Employee:** Quickly remove an employee from the system. (Double-click on any row of the Data table, and Click Delete.) 
![image](https://github.com/sarkeranik/EMS/assets/65606710/b465b798-cc5a-4789-83d0-394c8df6b76a)

4. **View Employee:** Get a detailed view of an individual employee's information. (Double-click on any row of the Data table.) 
![image](https://github.com/sarkeranik/EMS/assets/65606710/80278553-503d-4db4-ab2e-34709d041875)

5. **Search Employee by Name:** Use a handy search feature to find employees based on their names. (Input your desired employee name on the text box and hit the keyboard Enter.)
![image](https://github.com/sarkeranik/EMS/assets/65606710/f070dc1b-e619-40d9-89ab-33835be9c166)

6. **Pagination:** Implementing pagination for a smooth and organized display of employees.
![image](https://github.com/sarkeranik/EMS/assets/65606710/6c853ab0-7b15-4d90-87a3-04c8481be34b)

7. **Export to CSV:** Export the employee list to a CSV file for convenient data handling. (Click on the Export Button to save the table data as a CSV file. A file named Employee.CSV will be created in your download folder.)
![image](https://github.com/sarkeranik/EMS/assets/65606710/2de4c07e-ddea-44fd-90d9-35af6bdf052d)

### Application Architecture/Design Patterns Features

1. **Dependency Injection:** Leveraging the power of .NET's Dependency Injection for efficient and modular code.

2. **MVVM Architecture:** Following the Model-View-ViewModel pattern for a clean and maintainable code structure.

## RESTful Service Integration

This application seamlessly integrates with the GoRest API (https://gorest.co.in/public/v2/) using the provided API token. I've taken care to ensure data consistency and concurrency in all interactions with the API.

### Prerequisites

Ensure you have the following installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Visual Studio](https://visualstudio.microsoft.com/)

## Usage

1. **Clone the repository:** Start by cloning the repository to your local machine.

   ```bash
   git clone https://github.com/sarkeranik/EMS.git
   ```

2. **Open the Soltuion:** Fire up Visual Studio or your preferred C# development environment.
3. **Build and run the solution:** Just hit that run button and you're good to go!
4. **Manage employees:** Utilize the desktop application to efficiently manage employee details.

## Dependencies

No need to worry about additional dependencies; the project is configured to compile and run seamlessly with .NET 6.

## Contributing

Feel free to contribute to the project! If you have any suggestions or improvements, fork the repository and create a pull request.

## Bonus Points

In developing this project, I've made sure to:

- Implement best practices and design patterns.
- Optimize resource usage for top-notch performance.
- Address data concurrency considerations when interacting with the GoRest API.

## Acknowledgments

A big shoutout to the GoRest API for providing the backbone for this project. Thanks for reading and happy coding! 🚀
