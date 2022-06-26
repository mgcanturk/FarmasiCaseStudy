# FarmasiCaseStudy
#### Docker Compose establishment with all microservices on docker;
* Containerization of microservice
* Containerization of database
* Containerization of cache

## Run The Project
You will need the following tools:

* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 6 or later](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Installing
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)
1. Clone the repository
2. Once Docker for Windows is installed
3. At the root directory which include **docker-compose.yml** files, run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up
```
4. Wait for docker compose all microservices. That’s it! (some microservices need extra time to work so please wait if not worked in first shut)

5. You can **launch microservice** as below urls:
6. * **FarmasiCaseStudy API -> http://localhost:5000/swagger/index.html**

![image](https://user-images.githubusercontent.com/43003253/175831265-99b652d7-0a12-4289-911b-b1b81127b9a5.png)

## Authors
* **Mehmet Gurcan Canturk** - *Initial work* - [mgcanturk](https://github.com/mgcanturk)
