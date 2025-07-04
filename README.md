# Simple E-Commerce Console App (OOP in C#)

A basic e-commerce system built in C# using object-oriented programming principles.

## Features

- Add customers and products to a cart
- Handle expirable and non-expirable products
- Process orders and calculate shipping fees
- Validate balance and other errors with try-catch blocks
- Static `Order` class to manage checkout and shipment
- Inheritance for product types
- Interface-based design for shippable products
- Delegate and event-based tracking

## Core Concepts Used

- **OOP Principles**: Encapsulation, Inheritance, Polymorphism, Abstraction
- **Interfaces**: `IShippable` for shipping logic
- **Inheritance**:
  - `Product` base class
  - `ExpirableProduct` for products with expiry dates
  - `ShippableProduct` for items that can be shipped
- **Static Class**: `Order` manages checkout and shipping process
- **Error Handling**: All validations are wrapped in `try..catch`
- **LINQ**: Used for filtering and working with product lists
- **Delegates and Events**: Trigger actions like order placed or shipment completed

## Main Entities

- `Customer`
- `Product`  
- `ExpirableProduct`  
- `ShippableProduct`  
- `Cart`  
- `CartItem`  
- `Order` (Static)

## Services

- `ShippingService`: Calculates shipping fees based on products implementing `IShippable`

## How It Works

1. Customer adds items to cart
2. Order checks for balance and product availability
3. Products are filtered as shippable or not
4. Shipping fees are calculated
5. Confirmation is shown or error is thrown

## Requirements

- .NET 6.0 or higher
- Console application

## Run

```bash
dotnet run
