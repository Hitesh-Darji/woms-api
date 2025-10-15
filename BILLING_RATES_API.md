# Billing Rates API Documentation

This API provides comprehensive CRUD operations for managing billing rate tables in the WOMS (Work Order Management System). Rate tables define pricing structures and effective date ranges for different types of services.

## Features

- **Create Rate Tables**: Configure pricing structures with customizable rate types and effective date ranges
- **Retrieve Rate Tables**: Get all rate tables or filter by active status and date ranges
- **Update Rate Tables**: Modify existing rate table configurations
- **Delete Rate Tables**: Soft delete rate tables (mark as deleted)
- **Rate Type Support**: Support for various rate types including Flat Fee, Tiered, Conditional, Hourly, and Unit-based pricing

## API Endpoints

### Base URL
```
/api/BillingRates
```

### Authentication
All endpoints require JWT authentication. Include the Bearer token in the Authorization header:
```
Authorization: Bearer <your_jwt_token>
```

## Endpoints

### 1. Create Rate Table
**POST** `/api/BillingRates`

Creates a new billing rate table.

**Request Body:**
```json
{
  "name": "Standard Hourly Rates",
  "description": "Standard hourly rates for general maintenance work",
  "rateType": "Flat Fee",
  "baseRate": 75.00,
  "effectiveStartDate": "2025-10-14T00:00:00Z",
  "effectiveEndDate": "2026-12-31T23:59:59Z",
  "isActive": true
}
```

**Response:** `201 Created` with the created rate table

### 2. Get All Rate Tables
**GET** `/api/BillingRates`

Retrieves all billing rate tables.

**Query Parameters:**
- `isActive` (optional): Filter by active status (true/false)
- `startDate` (optional): Filter by effective start date
- `endDate` (optional): Filter by effective end date

**Response:** `200 OK` with array of rate tables

### 3. Get Rate Table by ID
**GET** `/api/BillingRates/{id}`

Retrieves a specific rate table by its ID.

**Response:** `200 OK` with rate table or `404 Not Found`

### 4. Update Rate Table
**PUT** `/api/BillingRates/{id}`

Updates an existing rate table.

**Request Body:** Same structure as create

**Response:** `200 OK` with updated rate table

### 5. Delete Rate Table
**DELETE** `/api/BillingRates/{id}`

Soft deletes a rate table (marks as deleted).

**Response:** `204 No Content`

## Data Models

### BillingRateDto
```json
{
  "id": "guid",
  "name": "string",
  "description": "string",
  "rateType": "string",
  "baseRate": "decimal",
  "effectiveStartDate": "datetime",
  "effectiveEndDate": "datetime",
  "isActive": "boolean",
  "createdOn": "datetime",
  "updatedOn": "datetime",
  "createdBy": "guid",
  "updatedBy": "guid"
}
```

### CreateBillingRateDto
```json
{
  "name": "string",
  "description": "string",
  "rateType": "string",
  "baseRate": "decimal",
  "effectiveStartDate": "datetime",
  "effectiveEndDate": "datetime",
  "isActive": "boolean"
}
```

### UpdateBillingRateDto
```json
{
  "name": "string",
  "description": "string",
  "rateType": "string",
  "baseRate": "decimal",
  "effectiveStartDate": "datetime",
  "effectiveEndDate": "datetime",
  "isActive": "boolean"
}
```

## Rate Types

Supported rate types:
- `Flat Fee` - Fixed price regardless of time or quantity
- `Hourly` - Price per hour of work
- `Tiered` - Different rates based on quantity or time thresholds
- `Unit` - Price per unit (e.g., per square foot, per item)
- `Conditional` - Rates that vary based on specific conditions

## Validation Rules

### Create/Update Validation
- **Name**: Required, maximum 255 characters, must be unique
- **Rate Type**: Required, maximum 20 characters
- **Effective Start Date**: Required
- **Effective End Date**: Required, must be after start date
- **Base Rate**: Optional, must be >= 0 when provided

### Business Rules
- Rate table names must be unique across the system
- Effective end date must be after effective start date
- Soft delete preserves data integrity for historical records
- All operations require valid JWT authentication

## Error Responses

### 400 Bad Request
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Name": ["Rate table name is required."],
    "EffectiveEndDate": ["Effective end date must be after effective start date."]
  }
}
```

### 401 Unauthorized
```json
{
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
  "title": "Unauthorized",
  "status": 401
}
```

### 404 Not Found
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

## Example Usage

### Creating Different Rate Types

**Flat Fee Rate:**
```json
{
  "name": "Standard Service Call",
  "description": "Fixed rate for standard service calls",
  "rateType": "Flat Fee",
  "baseRate": 150.00,
  "effectiveStartDate": "2025-10-14T00:00:00Z",
  "effectiveEndDate": "2026-12-31T23:59:59Z",
  "isActive": true
}
```

**Hourly Rate:**
```json
{
  "name": "Labor Rates",
  "description": "Hourly rates for skilled technicians",
  "rateType": "Hourly",
  "baseRate": 85.00,
  "effectiveStartDate": "2025-10-14T00:00:00Z",
  "effectiveEndDate": "2026-12-31T23:59:59Z",
  "isActive": true
}
```

**Tiered Rate:**
```json
{
  "name": "Volume Discount Rates",
  "description": "Tiered pricing for bulk services",
  "rateType": "Tiered",
  "baseRate": 100.00,
  "effectiveStartDate": "2025-10-14T00:00:00Z",
  "effectiveEndDate": "2026-12-31T23:59:59Z",
  "isActive": true
}
```

## Integration Notes

- Rate tables are used by the billing system to calculate charges for work orders
- Effective date ranges allow for rate changes over time
- Soft delete ensures historical billing data remains intact
- Rate tables can be linked to specific customers, work types, or service categories
- The system supports complex pricing structures through the tiered and conditional rate types

