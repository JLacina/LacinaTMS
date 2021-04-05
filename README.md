# Task Management System (TMS) Coding challenge Ilay (Jacek Lacina)
First release of this Microservice would consist of API - LacinaTmsApi developed using:  
* netcoreapp3.1, 
* Microsoft.EntityFrameworkCore.Design  (3.1.6)
* Microsoft.EntityFrameworkCore.SqlServer (3.1.6)
* Microsoft.EntityFrameworkCore.Tools (3.1.6)
* CXDevToolkitCommon (1.3.2)  for CSV
* Newtonsoft.Json (13.0.1)

LacinaTmsApi: WebAPI implementing CRUD operations, using SQL Server DB (Microsoft SQL Server MSSQL14.SQLEXPRESS), and Postman collections (11 requests)
with small unit test project (Work in proggres), will have swagger documentation soon. 

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* Visual Studio 2019 https://www.visualstudio.com/downloads/
* SQL Server® 2014 Express https://www.microsoft.com/en-ie/download/details.aspx?id=42299
* SQL Server Management Studio (SSMS) https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017
* .NET Core SDK 3.1 https://dotnet.microsoft.com/download/dotnet
* Postman for Windows https://www.getpostman.com/downloads/

### Setup
Follow these steps to get your development environment set up:  

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```
  4. Once the front end has started, within the `../LacinaTmsApi` directory, launch the back end by running:
     ```
	 dotnet run
	 ```
 5. App will run at  https://localhost:44386/api/tasks
  


## Technologies
* ASP.NET Core 3.1
* Entity Framework Core 3.1


## Details  

 1. Change connection string in ../LacinaTmsApi/appsettings.json; 
   ```
  "DefaultConnection": "Data Source=DESKTOP-0KVNRQD\SQLEXPRESS; Database=LacinaTms;Trusted_Connection=True; MultipleActiveResultSets=True;Integrated Security=True"
   ```

 2. In case of SQL Server not starting run this as admin: 
   ```
  c:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\Binn> sqlservr.exe -s SQLEXPRESS
   ```

  And run add migration using Package Management Console:
   ```
   PM> Add-Migration Initial
   PM> Update-Database 
   ```

3.   Find Postman collection that include 11 requests:
      * 0.01 Get all Main Tasks             https://localhost:44386/api/tasks 
      * 0.02 Get MainTask by ID             https://localhost:44386/api/tasks/1
      * 0.03 POST new MainTasks             https://localhost:44386/api/tasks 
      * 0.04 Delete MainTasks using ID      https://localhost:44386/api/tasks/3
      * 0.05 PUT existing Main Tasks        https://localhost:44386/api/tasks/1
      * 0.06 POST new SubTasks              https://localhost:44386/api/tasks/1/subtasks
      * 0.07 Get SubTasks by ID             https://localhost:44386/api/tasks/1/subtasks/1 
      * 0.08 Get all SubTasks for a Parent  https://localhost:44386/api/tasks/1/subtasks
      * 0.09 Delete SubTasks using ID       https://localhost:44386/api/tasks/1/subtasks/3
      * 0.10 PUT existing SubTasks          https://localhost:44386/api/tasks/1/subtasks/2
      * 0.11 Get Report Export (use in Google Chrome)  https://localhost:44386/api/tasks/report/export?StartDate=2021-04-04T10:34

4.   For Database Diagram please see:
   ```
    ../LacinaTMS/Docs/DB_Diagram_LacinaTms.png
  ```
  
5.   CSV report please see:
   ```
    ../LacinaTMS/Docs/taskManagementReport (1).csv
  ```

Thank you.


