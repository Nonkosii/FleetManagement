// Initialize DataTable
const dataTable = $('#dataTable').DataTable({
    "order": []
});

// Initialize Leaflet map
const map = L.map('map').setView([0, 0], 2);
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 15,
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

let markers = [];

// DataTable and map
function updateData(locations) {
    const hasActiveFilter = dataTable.search() !== '';

    dataTable.clear();
    locations.forEach(location => {
        dataTable.row.add([
            location.vehicleId,
            location.latitude,
            location.longitude,
            new Date(location.timestamp).toLocaleString()
        ]);
    });

    // Notify user
    if (hasActiveFilter) {
        alert("Locations have been refreshed. Please reapply your filter.");
        dataTable.search('').draw();
    } else {
        dataTable.draw();
    }

    // Map markers
    updateMapMarkers(locations);

    console.log("Refreshed successfully.");
}

// Map markers
function updateMapMarkers(locations) {
    // Clear exiting markers
    markers.forEach(marker => map.removeLayer(marker));
    markers = [];

    // Marker for each location
    locations.forEach(vehicle => {
        const marker = L.marker([vehicle.latitude, vehicle.longitude]).addTo(map);
        marker.bindPopup(`<b>Vehicle ID:</b> ${vehicle.vehicleId}<br><b>Location:</b> ${vehicle.latitude}, ${vehicle.longitude}`);
        markers.push(marker);
    });

    // Adjust map view to fit all markers
    if (markers.length > 0) {
        const group = new L.featureGroup(markers);
        map.fitBounds(group.getBounds(), { padding: [50, 50] });
    } else {
        map.setView([0, 0], 2);
    }
}

// Fetch locations from server
function fetchVehicleLocations() {
    $.getJSON('/api/vehicles/locations', function (data) {
        updateData(data);
    });
}

// Initial load
fetchVehicleLocations();

// Refresh locations
setInterval(fetchVehicleLocations, 30000);

// Listen to search events in DataTable
dataTable.on('search.dt', function () {
    // Filtered data
    const filteredData = dataTable.rows({ filter: 'applied' }).data().toArray();
    const filteredLocations = filteredData.map(row => ({
        vehicleId: row[0],
        latitude: parseFloat(row[1]),
        longitude: parseFloat(row[2]),
        timestamp: row[3]
    }));

    // Filtered locations
    updateMapMarkers(filteredLocations);
});
