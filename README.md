# Project Users Management
One Paragraph of project description goes here

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.


# Azure SQL Edge with Docker in a .NET 8 Project

This repository demonstrates how to set up and use Azure SQL Edge with Docker in a .NET 8 project. It includes a `docker-compose.yml` file for running the SQL Server container and initializing the database with required tables and data.

## Features
- Azure SQL Edge container setup using `docker-compose`.
- Automatic initialization of the database with SQL scripts (`create_tables.sql` and `fill_data.sql`).
- Simple and reusable setup for development environments.

## Prerequisites
- Docker and Docker Compose installed.
- .NET 8 SDK installed.
- SQL scripts (`create_tables.sql` and `fill_data.sql`) for initializing the database.

## Getting Started

### Clone the Repository
```bash
git clone <repository-url>
cd <repository-directory>
```

### File Structure

.
├── docker-compose.yml       # Docker Compose configuration
├── sql-scripts/             # SQL initialization scripts directory
│   ├── create_tables.sql    # Script to create tables
│   ├── fill_data.sql        # Script to populate initial data
│   └── .db_initialized      # Marker file for database initialization (created automatically)
├── API/                     # .NET 8 source code directory
│   └── ...
├── ...

### Start the Azure SQL Edge Container
### Run the following command to start the Azure SQL Edge container:
```bash
docker-compose up
```

### What Happens During Startup?
The Azure SQL Edge container starts and waits for the SQL Server service to become available.
The container checks if the database has already been initialized.
If not initialized, it:
Creates the UsersDatabase.
Executes the create_tables.sql script to set up the database schema.
Executes the fill_data.sql script to populate initial data.
Creates a .db_initialized file to mark the setup as complete.
If already initialized, the setup process is skipped.

### Access the Database

Host: localhost
Port: 1433
Username: sa
Password: Ak.12345Qw

You can connect to the database using any SQL client or programmatically in your .NET application.

### Docker Commands for Managing the Container
### Stop the container:

```bash
docker-compose down
```

### View container logs:
```bash
docker logs azure-sql-edge
```

### Restart the container:
```bash
docker-compose down && docker-compose up
```

### Reset the Database Initialization
### To reinitialize the database:

1. Remove the .db_initialized file
```bash
rm ./sql-scripts/.db_initialized
```
2. Restart the container:
```bash
docker-compose down && docker-compose up
```

### Integrating with .NET 8
### Use the following connection string in your .NET 8 project:

```bash
"Server=localhost,1433;Database=UsersDatabase;User Id=sa;Password=Ak.12345Qw;"
```