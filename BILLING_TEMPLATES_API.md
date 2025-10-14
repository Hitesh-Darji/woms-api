# Billing Templates API

This API provides functionality to manage billing templates for customers in the WOMS (Work Order Management System). Billing templates allow you to configure customer-specific billing settings, output formats, and field ordering for invoices.

## Features

- **Create Billing Templates**: Configure customer-specific billing templates with customizable field ordering
- **Retrieve Templates**: Get all templates or filter by customer ID
- **Update Templates**: Modify existing billing template configurations
- **Delete Templates**: Soft delete billing templates (mark as deleted)
- **Field Configuration**: Enable/disable specific fields and set their display order

## API Endpoints

### Base URL
```
/api/BillingTemplates
```

### Authentication
All endpoints require JWT authentication. Include the Bearer token in the Authorization header:
```
Authorization: Bearer <your_jwt_token>
```

## Endpoints

### 1. Create Billing Template
**POST** `/api/BillingTemplates`

Creates a new billing template for a customer.

**Request Body:**
```json
{
  "name": "Monthly Billing - Acme Corp",
  "customerId": "CUST001",
  "customerName": "Acme Corporation",
  "outputFormat": "PDF",
  "fileNamingConvention": "INV-{customer}-{date}-{number}",
  "deliveryMethod": "Email",
  "invoiceType": "Itemized (Per Job Line)",
  "fieldOrder": [
    {
      "fieldName": "Work Order Id",
      "displayOrder": 1,
      "isEnabled": true,
      "displayLabel": "Work Order ID",
      "fieldType": "Text"
    },
    {
      "fieldName": "Description",
      "displayOrder": 2,
      "isEnabled": true,
      "displayLabel": "Description",
      "fieldType": "Text"
    }
  ]
}
```

**Response:** `201 Created` with the created billing template

### 2. Get All Billing Templates
**GET** `/api/BillingTemplates`

Retrieves all billing templates.

**Query Parameters:**
- `customerId` (optional): Filter templates by customer ID

**Response:** `200 OK` with array of billing templates

### 3. Get Billing Template by ID
**GET** `/api/BillingTemplates/{id}`

Retrieves a specific billing template by its ID.

**Response:** `200 OK` with billing template or `404 Not Found`

### 4. Update Billing Template
**PUT** `/api/BillingTemplates/{id}`

Updates an existing billing template.

**Request Body:** Same structure as create, plus:
```json
{
  "id": "template-id",
  "isActive": true
}
```

**Response:** `200 OK` with updated billing template

### 5. Delete Billing Template
**DELETE** `/api/BillingTemplates/{id}`

Soft deletes a billing template (marks as deleted).

**Response:** `204 No Content`

## Data Models

### BillingTemplateDto
```json
{
  "id": "guid",
  "name": "string",
  "customerId": "string",
  "customerName": "string",
  "outputFormat": "string",
  "fileNamingConvention": "string",
  "deliveryMethod": "string",
  "invoiceType": "string",
  "isActive": "boolean",
  "createdOn": "datetime",
  "updatedOn": "datetime",
  "createdBy": "string",
  "updatedBy": "string",
  "fieldOrder": "BillingTemplateFieldDto[]"
}
```

### BillingTemplateFieldDto
```json
{
  "fieldName": "string",
  "displayOrder": "number",
  "isEnabled": "boolean",
  "displayLabel": "string",
  "fieldType": "string",
  "fieldSettings": "string"
}
```

## Field Configuration

The `fieldOrder` array allows you to configure which fields are included in the billing template and their display order. Available field names include:

- `Work Order Id`
- `Description`
- `Quantity`
- `Rate`
- `Total`
- `Location`
- `Category`
- `Work Type`

Each field can be:
- **Enabled/Disabled**: Control whether the field appears in the billing output
- **Reordered**: Set the `displayOrder` to control the sequence
- **Customized**: Provide custom `displayLabel` and `fieldType`

## Output Formats

Supported output formats:
- `PDF`
- `CSV`
- `Excel`
- `XML`
- `EDI`

## Delivery Methods

Supported delivery methods:
- `Email`
- `Download`
- `SFTP`
- `API`

## Invoice Types

Supported invoice types:
- `Itemized (Per Job Line)`
- `Summary`
- `Service`
- `Product`
- `Time & Materials`
- `Fixed Price`

## File Naming Convention

The `fileNamingConvention` supports placeholders:
- `{customer}` - Customer name
- `{date}` - Invoice date
- `{number}` - Invoice number

Example: `INV-{customer}-{date}-{number}`

## Error Handling

The API returns appropriate HTTP status codes:

- `200 OK` - Successful operation
- `201 Created` - Resource created successfully
- `204 No Content` - Successful deletion
- `400 Bad Request` - Invalid request data
- `401 Unauthorized` - Missing or invalid authentication
- `404 Not Found` - Resource not found

Error responses include descriptive messages in the response body.

## Validation Rules

- Template name is required and must be unique per customer
- Customer ID and name are required
- Output format must be one of the supported formats
- Delivery method must be one of the supported methods
- Invoice type must be one of the supported types
- At least one field must be specified in fieldOrder
- Field names must be valid and non-empty

## Database Schema

The billing templates are stored in the `BillingTemplate` table with the following key fields:

- `Id` (Primary Key)
- `Name` - Template name
- `CustomerId` - Customer identifier
- `CustomerName` - Customer name
- `OutputFormat` - Output format (PDF, CSV, etc.)
- `DeliveryMethod` - Delivery method (Email, SFTP, etc.)
- `InvoiceType` - Invoice type (Itemized, Summary)
- `FieldOrder` - JSON string containing field configuration
- `FileNamingConvention` - File naming pattern
- `IsActive` - Whether template is active
- `IsDeleted` - Soft delete flag
- Audit fields (CreatedOn, UpdatedOn, CreatedBy, UpdatedBy, DeletedBy, DeletedOn)

## Usage Examples

### Creating a Simple Template
```bash
curl -X POST "https://localhost:7001/api/BillingTemplates" \
  -H "Authorization: Bearer your_token" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Basic Template",
    "customerId": "CUST001",
    "customerName": "Test Customer",
    "outputFormat": "PDF",
    "deliveryMethod": "Email",
    "invoiceType": "Itemized (Per Job Line)",
    "fieldOrder": [
      {
        "fieldName": "Work Order Id",
        "displayOrder": 1,
        "isEnabled": true
      }
    ]
  }'
```

### Getting Templates for a Customer
```bash
curl -X GET "https://localhost:7001/api/BillingTemplates?customerId=CUST001" \
  -H "Authorization: Bearer your_token"
```

This API provides a complete solution for managing billing templates in the WOMS system, allowing for flexible configuration of customer-specific billing requirements.
