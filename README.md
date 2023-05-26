# Drones

### Introduction 
It is a service via web api that allows the communication of clients with a fleet of cargo drones, for the transportation of medicines.

### Technologies 
This service was developed with the Microsoft ASP.NET Core framework in its version 6, the data is stored in a database in memory, using the ORM (Object Relational Mapping) Entity Framework Core in its version 6.

### Tools
Visual Studio Code
Available in: https://code.visualstudio.com/download
Git
Available in: https://git-scm.com/download/win
Postman
Available in: https://www.postman.com/downloads/

### Instructions

### Installation
Download and install the .NET 6.x.x SDK from the address https://dotnet.microsoft.com/en-us/download/dotnet/6.0

Run the git clone command in the Git terminal, followed by the address of the project on Github, to clone the project to the directory on our PC:

$ git clone https://github.com/miguelcavada/Drones_Api.git 

Open the Windows command console, from the Run option with the Windows key combination + R

In the CMD go to the address where we cloned the project from Github:

C:\Users\miguel_angel\source\repos\Drones_Api>

Run the command: dotnet restore

### Execution
Open the Windows command console, from the Run option with the Windows key combination + R.
In the CMD go to the address where we cloned the project from Github:

C:\Users\miguel_angel\source\repos\Drones_Api>

Run the command: dotnet run

### Test
### Registering a drone
In Postman we access the URL, https://localhost:7103/api/drone, specifying the type of POST request, passing the data in json format in the request body:
{
    "serialNumber": "DRONE-07",
    "model": "Heavyweight",
    "weightLimit": 500,
    "batteryCapacity": 40,
    "state": "IDLE"
}

### Answers:
If action returns a 201 Created response, then the drone was created, returning a json with the data of the new drone, with an assigned ID.
{
    "id": 6,
    "medications": [],
    "serialNumber": "DRONE-07",
    "model": "Heavyweight",
    "weightLimit": 500,
    "batteryCapacity": 40,
    "state": "IDLE"
}

### Loading a drone with medication items
In Postman we access the URL, https://localhost:7103/api/drone/1, specifying at the end the ID of the drone that will load the drugs, as well as specifying the type of request to PUT. The drugs are passed in the request body in json format:
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
### Answers:
If action returns a 201 Created response, then the drone was loaded with the drugs, returning a json with the same data.
{
    "id": 1,
    "serialNumber": "DRON-01",
    "model": "Lightweight",
    "weightLimit": 500,
    "batteryCapacity": 100,
    "state": "LOADED",
    "medications": [
        {
            "id": 6,
            "name": "ACETAMINOPHEN",
            "weight": 200,
            "code": "MEDIC-01",
            "image": null,
            "droneId": 1,
            "drone": null
        },
        {
            "id": 7,
            "name": "B-COMPLEX",
            "weight": 200,
            "code": "MEDIC-04",
            "image": null,
            "droneId": 1,
            "drone": null
        }
    ]
}

### Checking loaded medication items for a given drone
In Postman we access the URL, https://localhost:7103/api/drone/charge/1, specifying the drone ID at the end, in addition to specifying the type of GET request.
### Answers:
If action returns a 200 OK response, then it returns the drugs loaded on the drone in a json.
{
    "id": 1,
    "medications": [
        {
            "name": "VITAMIN C",
            "weight": 400,
            "code": "MEDIC-05"
        },
        {
            "name": "B-COMPLEX",
            "weight": 200,
            "code": "MEDIC-04"
        },
        {
            "name": "ANTACID TABLETS",
            "weight": 300,
            "code": "MEDIC-03"
        },
        {
            "name": "ASPIRIN",
            "weight": 100,
            "code": "MEDIC-02"
        },
        {
            "name": "ACETAMINOPHEN",
            "weight": 200,
            "code": "MEDIC-01"
        },
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
    ],
    "serialNumber": "DRON-01",
    "model": "Lightweight",
    "weightLimit": 500,
    "batteryCapacity": 100,
    "state": "LOADED"
}

### Checking available drones for loading
In Postman we access the URL, https://localhost:7103/api/drone/, specifying the type of GET request.
### Answers:
If action returns a 200 OK response, then it returns the drones with their data available to be loaded in a json.
[
    {
        "id": 2,
        "medications": [],
        "serialNumber": "DRON-02",
        "model": "Middleweight",
        "weightLimit": 500,
        "batteryCapacity": 50,
        "state": "IDLE"
    },
    {
        "id": 3,
        "medications": [],
        "serialNumber": "DRON-03",
        "model": "Cruiserweight",
        "weightLimit": 500,
        "batteryCapacity": 100,
        "state": "IDLE"
    },
    {
        "id": 4,
        "medications": [],
        "serialNumber": "DRON-04",
        "model": "Heavyweight",
        "weightLimit": 500,
        "batteryCapacity": 100,
        "state": "IDLE"
    },
    {
        "id": 5,
        "medications": [],
        "serialNumber": "DRON-05",
        "model": "Lightweight",
        "weightLimit": 500,
        "batteryCapacity": 20,
        "state": "IDLE"
    },
    {
        "id": 6,
        "medications": [],
        "serialNumber": "DRONE-07",
        "model": "Heavyweight",
        "weightLimit": 500,
        "batteryCapacity": 40,
        "state": "IDLE"
    }
]

### Check drone battery level for a given drone
In Postman we access the URL, https://localhost:7103/api/drone/batterylevel/2, specifying the drone ID at the end, in addition to specifying the type of GET request.
### Answers:
If action returns a 200 OK response, then return the drone's battery level in percent in a json.

The battery level for given drone is 50%





