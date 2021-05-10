# Xamarin Forms Shell App

Mobile App with Web API backend. Based on the Shell app project template.

## Contents

### Mobile App

* Model View View-Model (MVVM)
* Inversion of Control (IoC), and Dependency injection

### Backend

* Web API, with Open API definition (Swagger)
* Azure SQL Edge (ARM64) for storage - EF Core for ORM

## Development

### Requirements

* .NET 5 SDK
* Xamarin workload (VS Studio 2019, VS for Mac)
* Docker Desktop
* Microsoft Tye - CLI tools
* Visual Studio 2019, VS 2019 for Mac, or VS Code

### Tye

This project uses Tye to run projects and services. The definition is in the ```tye.yaml``` file.

### Database setup

This project is being developed on an M1 MacBook, since the MS SQL Server image does not support the ARM64 architecture, we use Azure SQL Edge instead.

If you run on x86-64, you can just uncomment the lines for SQL Server in the ```tye.yaml``` file.

## Run the project

Having installed the Tye tools:

1. Launch Tye from the command line in the solution folder:

```
tye run
```

2. Launch the Simulator and run the mobile app.

## To Do
* Improve this sample project
* Add authentication

## Other

### Incremental loading

Add this to the CollectionView to enable incremental loading in the control:

```xaml
RemainingItemsThreshold="5"
RemainingItemsThresholdReachedCommand="{Binding LoadMoreCommand}"
```