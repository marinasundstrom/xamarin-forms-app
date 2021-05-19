# Backend

## Projects

* **ShellApp.Web** - contains the actual Web app, hosting a Web API.
* **Items** - contains the behavior and implementation of the feature handling items.

**Items*** is a module representing a bounded context.

Shared "infrastructure" projects are **Domain**, **Infastructure**, **Application.Common**, and **Contracts.Common**.

## Loosely coupled monolith

The ```Items``` project can be duplicated into sub-modules in this loosely coupled monolith. These would share common infrastructure, while communicating through 

### Micro services

A further step, would be to split the modules into independent micro services, by decouple from the common infrastructure, and swapping MediatR for a out-of-process message broker.