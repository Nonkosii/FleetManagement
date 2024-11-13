WITH LatestLocation AS (
    SELECT 
        [Id],
        [VehicleId],
        [Latitude],
        [Longitude],
        [Timestamp],
        ROW_NUMBER() OVER (PARTITION BY [VehicleId] ORDER BY [Timestamp] DESC) AS RowNumVehicleId
    FROM 
        [Fleet_Management].[dbo].[VehicleLocations]
)
SELECT [Id], [VehicleId], [Latitude],
    [Longitude], [Timestamp]
FROM LatestLocation
WHERE RowNumVehicleId = 1
ORDER BY [Timestamp] desc



