## Summary
A simple app allowing users to build different holdings models in their portfolio. 

- Repository + Unit of Work using Entity Framework Core
- AutoMapper for mapping models into API resources.
- Swagger to have a friendly API interface.

### Overview
DDD like layering. Class libraries + ASP.NET front end. D3.js for visualization.
Base class model: Portfolio has many models that have many holdings. Holdings are stocks, options, crypyoto etc. 

#### Dependencies
ASP.NET Identity Framework