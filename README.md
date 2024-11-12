Fleet Management Web Application

This project is a web application built using ASP.NET Core WEB API and Entity Framework Core for fleet management. It tracks vehicle geolocations and presents them on a dashboard. 
The application includes both an API and a web interface for rendering and saving vehicle location data.
Features:

    Track vehicle geolocations: Display vehicle positions on an interactive map.
    Web Interface: View the latest vehicle locations in a table format and map.
    API Endpoints: Save and retrieve vehicle location data via the API.
    Unit Testing: xUnit.

Technologies

    ASP.NET Core WEB API
    Entity Framework Core (SQL Server)
    Leaflet.js (for interactive maps)
    DataTables (for tabular data display)
    Bootstrap (for UI design)

Setup Instructions
Prerequisites

    .NET SDK: Make sure you have the .NET SDK 8.0 or higher installed.
        Download .NET

    SQL Server: You need a SQL Server instance running. You can use SQL Server Express or any other edition of SQL Server.
        Download SQL Server

    OpenStreetMap for location display

1. Running the Application

    Clone the repository:

git clone https://github.com/Nonkosii/FleetManagement.git
cd FleetManagement

2. Install dependencies(Visual Studio):

Run the following command to restore NuGet packages:

dotnet restore

3. Configure Database Connection:

    Open appsettings.json and make sure your DefaultConnection points to a valid SQL Server database.


Example:

"ConnectionStrings": {
    "DefaultConnection": "Server=your-server-name;Database=Fleet_Management;Trusted_Connection=True;TrustServerCertificate=True;"
}

4. Run SQL Script attached to this project(SSMS)

Run the Application:

Run the application using:

    dotnet run

    The web app will be accessible at:
        HTTP: http://localhost:5086
        HTTPS: https://localhost:7211

5. API Endpoints

    POST /api/vehicles/location: Save vehicle location data (VehicleId, Latitude, Longitude).
    GET /api/vehicles/locations: Retrieve the latest vehicle locations.

Testing the API with Postman

    POST /api/vehicles/location:
        URL: http://localhost:5086/api/vehicles/location
        Method: POST
        Body (raw JSON):

{
  "vehicleId": "V123",
  "latitude": 34.0522,
  "longitude": -118.2437
}

Note: Timestamp will be date time now since vehicles location change time to time

Response:

    {
      "Message": "Location of the vehicle saved successfully"
    }

GET /api/vehicles/locations:

    URL: http://localhost:5086/api/vehicles/location
    Method: GET
    Response:

        [
          {
            "vehicleId": "V123",
            "latitude": 34.0522,
            "longitude": -118.2437,
            "timestamp": "2024-11-07T12:00:00Z"
          }
        ]

6. Unit Testing

    Run Unit Tests:

    The project uses xUnit for unit testing. To run the tests, use the following:

    Run the tests via Visual Studio:

    Open the Test Explorer in Visual Studio.

    Click on Run All Tests to run the tests.

Additional:
    
    To run test, package manager console install NuGet\Install-Package Microsoft.EntityFrameworkCore.InMemory -Version 8.0.10

7. const map = new google.maps.Map(document.getElementById('map'), {
    center: { lat: 0, lng: 0 },
    zoom: 2
});

9. Filter vehicle search on Data table search input by Vehicle ID and the specific vehicle location will be focused or only show on the map, this exclude other vehicle ID
   on the table and map.

10. Every 30 seconds all vehicle locations are updated on the dashboard. If filters were applied, an alert will pop about you need to re-apply filters again.

8. Preloader: The page shows a preloader (loading spinner) until the content is ready to be displayed.