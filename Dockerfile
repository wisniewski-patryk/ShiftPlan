#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["ShiftPlan.Blazor/ShiftPlan.Blazor.csproj", "ShiftPlan.Blazor/"]
COPY ["ShiftPlan.Blazor.Client/ShiftPlan.Blazor.Client.csproj", "ShiftPlan.Blazor.Client/"]
RUN dotnet restore "./ShiftPlan.Blazor/ShiftPlan.Blazor.csproj"
COPY ./ShiftPlan.Blazor/ ./ShiftPlan.Blazor/
COPY ./ShiftPlan.Blazor.Client/ ./ShiftPlan.Blazor.Client/
WORKDIR "/src/ShiftPlan.Blazor"
RUN dotnet build "./ShiftPlan.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./ShiftPlan.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShiftPlan.Blazor.dll"]