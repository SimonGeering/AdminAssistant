# Overview

This document is intended to act as an aid-memoir for the reasoning behind design decisions made during the development of the system.  

The decisions noted may or may not be related and maybe at different levels of scope and granularity to one another. The key point is that the thought process and reasoning are being captured somewhere.

## List of decisions

### Implement unit tests for the DAL in addition to integration and acceptance tests. 

- While the code covered by these unit tests would be included in both the Server Side integration tests and the functional acceptance tests, both those sets of tests require external infrastructure. 

- The unit tests, by definition, have no external dependencies and so can be run as part of the CI/CD pipeline, increasing test scenario coverage at that crucial time. 

- The unit tests cover a smaller area of code, so while functional acceptance tests will tell us that the application is not fit for purpose, it will be the unit tests that will help to precisely pinpoint why and where. 

### Centralising all interfaces within AdminAssistant assembly

- This helps reduce dependency coupling when changing a single unit of functionality, since the only things that need to change are:
  - The assembly containing the unit of code being modified or added.
  - The test assembly containing the Unit Test for that code.
  - Core AdminAssistant assembly containing the interfaces for the dependencies of that code, and any POCOs used in that interface.  

  As a result, the compiler will spend less time compiling after each change and so shorten the TDD feedback loop. 
