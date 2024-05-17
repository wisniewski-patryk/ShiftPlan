# ShiftPlan

## How run local env

- go in CLI to * ./Database-postgres/ *
- in *.env* file you can set database user/password and db name
- run `docker-compose up -d` to run container with database
- to run frontend go to * ./FrontendBlazor/FrontendBlazor/ * and run `dotnet watch` or `dotnet run`
- to run backend go to * ./ShiftPlan.Api * and run `dotnet watch` or `dotnet run`
