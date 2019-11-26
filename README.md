# Runpath
Coding test for RunPath

## Test Driven Design
The project was developed with TDD to ensure the best possible design using SOLID principles in the time suggested without over-developing with unnecessary abstractions or future features.
### MockHttp
This package simplifies the unit tests for the JsonApiClient.

## Resharper
I use resharper to enforce a coding style across my projects.

### Regions
I appreciate that regions are not popular with all developers - my code formatting style places these regions in this way automatically. This means I am not in the habit of putting regions everywhere and not using them is a simple matter of using a new code style with resharper rather than a change in mindset.

## XML Docs
I do sometimes add XML doc comments to code which is largely for public APIs or because I think they will help the next developer but never as a rule.

## Web API
The web API project exists to expose the internal API via HTTP so it is a simple enough project with a simple controller that acts as an endpoint with which to get a list of albums.

### Swagger
For convenience I have configured swagger into the ASP.NET pipeline so the API can be tested easily in an ad-hoc manner.
