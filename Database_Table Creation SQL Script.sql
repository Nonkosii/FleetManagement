CREATE TABLE VehicleLocations (
  Id INT PRIMARY KEY IDENTITY,
  VehicleId NVARCHAR(50) NOT NULL,
  Latitude DECIMAL(9, 4) NOT NULL,
  Longitude DECIMAL(9, 4) NOT NULL,
  Timestamp DATETIME NOT NULL
);


  INSERT INTO VehicleLocations (VehicleId, Latitude, Longitude, Timestamp)
VALUES ('V121', 39.7749, -124.4194, '2024-11-23T13:00:00Z');

