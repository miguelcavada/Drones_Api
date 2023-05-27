# Drones
Develop a service via REST API that allows clients to communicate with the drones.

### Technologies 
- This service was developed with the ASP.NET Core framework.
- Data is stored in an in-memory database, using the ORM Entity Framework Core.

# Instructions
# Build
Download and install .NET Core version 6 SDK

### Clone project from GitHub
Run git clone command in Git terminal:

> $ git clone https://github.com/miguelcavada/Drones_Api.git

### Restore packages of project
In the CMD go to the address where we cloned the project from Github:

> Run the command: dotnet restore

# Run
In the CMD go to the address where we cloned the project from Github:

> Run the command: dotnet run

# Test

### *Registering a drone*

POST /api/drone

Passing the data in json format in the request body:

{
    "serialNumber": "DRONE-07",
    "model": "Heavyweight",
    "weightLimit": 500,
    "batteryCapacity": 40,
    "state": "IDLE"
}

Responses: 201 Created

### *Loading a drone with medication items*

PUT /api/drone/1

The drugs are passed in the request body in json format:
[
    {
        "name": "ACETAMINOPHEN",
        "weight": 200,
        "code": "MEDIC-01"
    },
    {
        "name": "B-COMPLEX",
        "weight": 200,
        "code": "MEDIC-04"
    }
]

Responses: 201 Created

### *Checking loaded medication items for a given drone*

GET api/drone/charge/1

Responses: 200 OK

{
    "id": 1,
    "medications": [
        {
            "name": "VITAMIN C",
            "weight": 400,
            "code": "MEDIC-05"
        }
	]
	"serialNumber": "DRON-01",
    "model": "Lightweight",
    "weightLimit": 500,
    "batteryCapacity": 100,
    "state": "LOADED"
}

### *Checking available drones for loading*

GET /api/drone/

Responses: 200 OK

[
    {
        "id": 2,
        "medications": [],
        "serialNumber": "DRON-02",
        "model": "Middleweight",
        "weightLimit": 500,
        "batteryCapacity": 50,
        "state": "IDLE"
    }
]

### *Check drone battery level for a given drone*

GET /api/drone/batterylevel/2

Responses: 200 OK

The battery level for given drone is 50%

