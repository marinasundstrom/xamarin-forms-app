# Items

This directory contains all the features in the bounded context of handling items.

Obviously, this is a very simplified context. It could handle more than just items, depending on the case.

## Projects
* **Items** - contains the actual behavior of the application, handlers for command and queries. Regard it as its own module. The naming could be better.
* **Items.Contracts** - contains the contracts. that is commands, queries, and domain events.
* **Items.WebApi** - contains the Web API, i.e. controllers.
