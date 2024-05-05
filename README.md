# ShiftPlan

## TODO

- [ ] user authentication / login screen
- [ ] user permissions
- [ ] autoplaning algorythm (for weekday, night shift and weekend)
- [ ] print table (if possible as already existing excel template or pdf file)
- [ ] load more days button - api paging/baching records + display button
- [ ] add customization view (view week, month, year + custom amount of days (for example 15 days) + dates range)
- [ ] clean old data mechanism
- [ ] CI\CD
- [ ] user settings
- [ ] dark mode?
- [ ] mobile view only version (PWA) 📱
- [ ] UNIT TESTS! ⚗
- [ ] Telegram bot notification 🔔

## How run local env

- go in CLI to * ./Database-postgres/ *
- in *.env* file you can set database user/password and db name
- run `docker-compose up -d` to run container with database
- to run frontend go to * ./FrontendBlazor/FrontendBlazor/ * and run `dotnet watch` or `dotnet run`
- to run backend go to * ./ShiftPlan.Api * and run `dotnet watch` or `dotnet run`
