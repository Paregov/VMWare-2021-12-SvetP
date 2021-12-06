# VMWare-2021-12-SvetP

Technical excercise for VMWare interview.

This is a small shipping container manager. You can add shipping containers. Mark them as shipped. List the containers.

To build go into the **deployment** directory from PowerShell

```powershell
cd <git repository path>\deployment
docker-compose build
docker-compose up -d
```

This will build and deploy the services into your local Docker.

Frontend service is accessible on http://localhost:8080

Backend service is accessible on http://localhost:8081



Note: So far I am using only one supporting service, but will add a second one.

Note: Frontend not  ready. Will continue work on it.