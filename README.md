# OrderPlacer

> ⚠️ **This project is still under development.**

## Overview

OrderPlacer is a sample .NET Aspire project designed to experiment with and showcase Aspire capabilities in a microservice architecture. This portfolio project demonstrates modern .NET development practices, distributed systems patterns, and cloud-native technologies.

## Architecture & Tech Stack

### Components

- **API Gateway**: YARP
  - Load balancing
  - Rate limiting
- **Microservices**:
  - **Orders API**: Order placing web API with two endpoints:
    - `GET` endpoint for retrieving order information
    - `POST` endpoint for creating (placing) orders
    - Implemented with FastEndpoints / VerticalSlice architecture. 
  - **Fulfillment Service**: Order fulfillment processing service
    - Consumes order creation events from RabbitMQ
    - Communicates with the Fulfillment External API to process orders
    - Updates order status
  - **Fulfillment External API**: A simulated external service to showcase resilience policies (timeout, circuit breaker, retry)
- **Infrastructure**:
  - **Redis**: Caching layer for the GET endpoint
  - **PostgreSQL**: Primary database for data persistence
  - **RabbitMQ**: Message broker for inter-service communication
- **Platform**: .NET Aspire for orchestration and observability

## Planned Features & TODOs

- Integrate OpenTelemetry for distributed tracing
- etc.