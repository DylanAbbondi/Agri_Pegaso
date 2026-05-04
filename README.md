## Prerequisites
Before running the application, make sure you have the following installed:
* **Docker** (with Docker Compose V2)
* **Git**

## Getting Started

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd <project-folder>
   ```

2. **Run the application:**
   Execute the following command to start the containers in detached mode:
   ```bash
   docker compose -f docker-compose.yml -f docker-compose.override.yml up -d
   ```

3. **Access the dashboard:**
   Once the setup is complete, the application will be available at:
   [https://localhost:8072/dashboard](https://localhost:8072/dashboard)

## Stopping the application

To stop and remove the containers, run:
```bash
docker compose down
```

*Note: If you want to reset the environment and delete all data (including volumes), use:*
`docker compose down -v`
