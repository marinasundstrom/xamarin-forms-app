# Xamarin Forms Shell App

Mobile App with Web API backend. Based on the Shell app project template.

Using [Tye](https://github.com/dotnet/tye) for easy development and deployment.

## The Mobile App

In the app you can:

* View items and their pictures in a view that is infinitely scrollable.
* Add an item with at picture that is getting uploaded to Azure Blob storage. (Azurite emulator).
* Delete items (Soft delete)

The app is based on the Shell app template, with the addition of uploading images, and changes to interfaces in code.

## Contents

### Mobile App

* Model View View-Model (MVVM)
* Inversion of Control (IoC), and Dependency injection.

### Backend

* Web API, with Open API definition (Swagger)
* SignalR for sending real-time notifications to client
* Structured according to Command, Query, Request Segretation (CQRS) using Mediator pattern.
* Azure SQL Edge (ARM64*) for storage - EF Core for ORM
* Azurite Storage emulator for Blob storage

Following the Clean Architecture pattern and CQRS, it enables the creation of a loosely-coupled monolith.

**SQL Server is optional for x86-64*

## Screenshots

| About         | Browse        | Item Details  |
| ------------- | --------------| --------------| 
| <img src="/Screenshots/AboutView.png" width="200px;" /> | <img src="/Screenshots/BrowseView.png" width="200px;" /> | <img src="/Screenshots/ItemDetailView.png" width="200px;" /> | 


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

### Create an account

In order to log into the app you need an account.

In your browser, go to the *Swagger UI* at https://localhost:5020/swagger/.

View ```Identity``` > ```POST```. Click *"Try it out"*, enter an email address, any will do. Then, enter a password of your choice. Send the request, and your account will be created.

Use these credentials to log into the app.

## To Do
* Improve this sample project
* Add authentication