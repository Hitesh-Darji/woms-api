# Form Builder CRUD API Documentation

## Overview

The Form Builder CRUD API provides comprehensive functionality for creating, managing, and manipulating dynamic forms in the WOMS (Work Order Management System). This API supports the creation of complex forms with multiple sections, various field types, validation rules, and hierarchical organization.

## Features

- **Complete CRUD Operations**: Create, Read, Update, Delete form templates
- **Hierarchical Structure**: Forms contain sections, sections contain fields
- **Rich Field Types**: Support for text, email, phone, date, select, textarea, checkbox, radio, file, and more
- **Validation Rules**: Comprehensive validation including min/max values, patterns, required fields
- **Flexible Organization**: Categorization, status management, and ordering
- **Authentication**: JWT-based authentication for all endpoints
- **CQRS Pattern**: Clean separation of commands and queries
- **AutoMapper Integration**: Automatic mapping between DTOs and entities

## API Endpoints

### Base URL
```
https://localhost:7000/api/forms
```

### Authentication
All endpoints require JWT authentication. Include the token in the Authorization header:
```
Authorization: Bearer <your_jwt_token>
```

## Endpoints

### 1. Create Form Template
**POST** `/api/forms`

Creates a new form template with sections and fields.

**Request Body:**
```json
{
  "name": "Customer Feedback Form",
  "description": "A comprehensive form for collecting customer feedback",
  "category": "Customer Service",
  "status": "draft",
  "isActive": true,
  "sections": [
    {
      "title": "Customer Information",
      "description": "Basic customer details",
      "orderIndex": 1,
      "isRequired": true,
      "isCollapsible": false,
      "isCollapsed": false,
      "fields": [
        {
          "fieldType": "text",
          "label": "Customer Name",
          "placeholder": "Enter your full name",
          "helpText": "Please provide your complete name",
          "isRequired": true,
          "isReadOnly": false,
          "isVisible": true,
          "orderIndex": 1,
          "minLength": 2,
          "maxLength": 100
        }
      ]
    }
  ]
}
```

**Response:** `201 Created`
```json
{
  "id": "guid",
  "name": "Customer Feedback Form",
  "description": "A comprehensive form for collecting customer feedback",
  "category": "Customer Service",
  "status": "draft",
  "version": 1,
  "isActive": true,
  "createdOn": "2025-01-14T10:30:00Z",
  "updatedOn": null,
  "createdBy": "guid",
  "updatedBy": null,
  "sections": [...]
}
```

### 2. Get All Form Templates
**GET** `/api/forms`

Retrieves all form templates.

**Response:** `200 OK`
```json
[
  {
    "id": "guid",
    "name": "Customer Feedback Form",
    "description": "A comprehensive form for collecting customer feedback",
    "category": "Customer Service",
    "status": "draft",
    "version": 1,
    "isActive": true,
    "createdOn": "2025-01-14T10:30:00Z",
    "updatedOn": null,
    "createdBy": "guid",
    "updatedBy": null,
    "sections": [...]
  }
]
```

### 3. Get Form Template by ID
**GET** `/api/forms/{id}`

Retrieves a specific form template with all sections and fields.

**Response:** `200 OK` or `404 Not Found`

### 4. Update Form Template
**PUT** `/api/forms/{id}`

Updates an existing form template. This operation replaces all sections and fields.

**Request Body:** Same structure as Create Form Template

**Response:** `200 OK` or `404 Not Found`

### 5. Delete Form Template
**DELETE** `/api/forms/{id}`

Deletes a form template and all associated sections and fields.

**Response:** `204 No Content` or `404 Not Found`

### 6. Get Form Templates by Category
**GET** `/api/forms/category/{category}`

Retrieves all form templates in a specific category.

**Response:** `200 OK`

### 7. Get Active Form Templates
**GET** `/api/forms/active`

Retrieves all active form templates.

**Response:** `200 OK`

## Field Types

The API supports the following field types:

| Field Type | Description | Additional Properties |
|------------|-------------|----------------------|
| `text` | Single-line text input | `minLength`, `maxLength`, `pattern` |
| `email` | Email address input | `pattern` (auto-validated) |
| `phone` | Phone number input | `pattern` |
| `number` | Numeric input | `minValue`, `maxValue`, `step` |
| `currency` | Currency input | `minValue`, `maxValue`, `step` |
| `date` | Date picker | - |
| `datetime` | Date and time picker | - |
| `textarea` | Multi-line text input | `rows`, `minLength`, `maxLength` |
| `select` | Dropdown selection | `options` (JSON array) |
| `multiselect` | Multiple selection | `options` (JSON array) |
| `checkbox` | Single checkbox | `defaultValue` |
| `radio` | Radio button group | `options` (JSON array) |
| `file` | File upload | - |
| `image` | Image upload | - |
| `signature` | Digital signature | - |
| `barcode` | Barcode/QR scanner | - |
| `location` | GPS location | - |
| `calculated` | Calculated field | - |
| `url` | URL/link field | `pattern` |
| `richtext` | Rich text editor | - |

## Validation Rules

### Form Template Validation
- **Name**: Required, max 255 characters, must be unique
- **Description**: Optional, max 1000 characters
- **Category**: Required, max 100 characters
- **Status**: Optional, max 20 characters

### Section Validation
- **Title**: Required, max 255 characters
- **Description**: Optional, max 1000 characters
- **OrderIndex**: Required, must be >= 0

### Field Validation
- **FieldType**: Required, max 20 characters
- **Label**: Required, max 255 characters
- **Placeholder**: Optional, max 255 characters
- **HelpText**: Optional, max 1000 characters
- **OrderIndex**: Required, must be >= 0
- **ValidationRules**: Optional, max 2000 characters (JSON)
- **Options**: Optional, max 2000 characters (JSON array)
- **DefaultValue**: Optional, max 1000 characters
- **Pattern**: Optional, max 255 characters (regex)
- **MinValue/MaxValue**: Must be valid decimal numbers
- **MinLength/MaxLength**: Must be valid integers
- **MinValue < MaxValue**: When both are provided
- **MinLength < MaxLength**: When both are provided

## Error Responses

### 400 Bad Request
```json
{
  "message": "Validation failed",
  "errors": [
    {
      "propertyName": "Name",
      "errorMessage": "Form template name is required."
    },
    {
      "propertyName": "Sections[0].Title", 
      "errorMessage": "Section title is required."
    },
    {
      "propertyName": "Sections[0].Fields[0].MinValue",
      "errorMessage": "Minimum value must be less than maximum value."
    }
  ]
}
```

### 401 Unauthorized
```json
{
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
  "title": "Unauthorized",
  "status": 401,
  "detail": "User ID not found in token"
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

## Architecture

The Form Builder API follows Clean Architecture principles with the following layers:

### Domain Layer (`WOMS.Domain`)
- **Entities**: `FormTemplate`, `FormSection`, `FormField`
- **Repositories**: `IFormTemplateRepository`, `IFormSectionRepository`, `IFormFieldRepository`
- **Base Entity**: Common properties for all entities

### Application Layer (`WOMS.Application`)
- **Commands**: `CreateFormTemplateCommand`, `UpdateFormTemplateCommand`, `DeleteFormTemplateCommand`
- **Queries**: `GetAllFormTemplatesQuery`, `GetFormTemplateByIdQuery`
- **DTOs**: `FormTemplateDto`, `CreateFormTemplateDto`, `UpdateFormTemplateDto`
- **Handlers**: Command and Query handlers with business logic
- **Validators**: FluentValidation validators for all commands
- **Profiles**: AutoMapper profiles for entity-DTO mapping

### Infrastructure Layer (`WOMS.Infrastructure`)
- **Repositories**: Concrete implementations of domain repositories
- **DbContext**: Entity Framework DbContext with form entities
- **Services**: Additional infrastructure services

### API Layer (`WOMS.Api`)
- **Controllers**: `FormsController` with RESTful endpoints
- **Middleware**: Exception handling and authentication
- **Configuration**: Dependency injection setup

## Database Schema

### FormTemplate Table
```sql
CREATE TABLE FormTemplate (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Category NVARCHAR(100) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'draft',
    Version INT NOT NULL DEFAULT 1,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedOn DATETIME2 NOT NULL,
    UpdatedOn DATETIME2,
    CreatedBy UNIQUEIDENTIFIER,
    UpdatedBy UNIQUEIDENTIFIER,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedBy UNIQUEIDENTIFIER,
    DeletedOn DATETIME2
);
```

### FormSection Table
```sql
CREATE TABLE FormSection (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    FormTemplateId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    OrderIndex INT NOT NULL,
    IsRequired BIT NOT NULL DEFAULT 0,
    IsCollapsible BIT NOT NULL DEFAULT 0,
    IsCollapsed BIT NOT NULL DEFAULT 0,
    CreatedOn DATETIME2 NOT NULL,
    UpdatedOn DATETIME2,
    CreatedBy UNIQUEIDENTIFIER,
    UpdatedBy UNIQUEIDENTIFIER,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedBy UNIQUEIDENTIFIER,
    DeletedOn DATETIME2,
    FOREIGN KEY (FormTemplateId) REFERENCES FormTemplate(Id)
);
```

### FormField Table
```sql
CREATE TABLE FormField (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    FormSectionId UNIQUEIDENTIFIER NOT NULL,
    FieldType NVARCHAR(20) NOT NULL,
    Label NVARCHAR(255) NOT NULL,
    Placeholder NVARCHAR(255),
    HelpText NVARCHAR(MAX),
    IsRequired BIT NOT NULL DEFAULT 0,
    IsReadOnly BIT NOT NULL DEFAULT 0,
    IsVisible BIT NOT NULL DEFAULT 1,
    OrderIndex INT NOT NULL,
    ValidationRules NVARCHAR(MAX),
    Options NVARCHAR(MAX),
    DefaultValue NVARCHAR(MAX),
    MinValue DECIMAL(15,2),
    MaxValue DECIMAL(15,2),
    MinLength INT,
    MaxLength INT,
    Pattern NVARCHAR(255),
    Step DECIMAL(15,2),
    Rows INT,
    Columns INT,
    CreatedOn DATETIME2 NOT NULL,
    UpdatedOn DATETIME2,
    CreatedBy UNIQUEIDENTIFIER,
    UpdatedBy UNIQUEIDENTIFIER,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedBy UNIQUEIDENTIFIER,
    DeletedOn DATETIME2,
    FOREIGN KEY (FormSectionId) REFERENCES FormSection(Id)
);
```

## Testing

Use the provided `forms-api.http` file to test all endpoints. The file includes:

1. **Create Form Template** - Complete example with multiple sections and field types
2. **Get All Form Templates** - Retrieve all templates
3. **Get Form Template by ID** - Retrieve specific template
4. **Update Form Template** - Update existing template
5. **Delete Form Template** - Remove template
6. **Get by Category** - Filter by category
7. **Get Active Templates** - Filter active templates
8. **Validation Tests** - Test error scenarios
9. **Authorization Tests** - Test authentication requirements

## Usage Examples

### Creating a Simple Contact Form
```json
{
  "name": "Contact Us Form",
  "description": "Simple contact form for customer inquiries",
  "category": "Support",
  "status": "active",
  "sections": [
    {
      "title": "Contact Information",
      "orderIndex": 1,
      "isRequired": true,
      "fields": [
        {
          "fieldType": "text",
          "label": "Name",
          "isRequired": true,
          "orderIndex": 1
        },
        {
          "fieldType": "email",
          "label": "Email",
          "isRequired": true,
          "orderIndex": 2
        },
        {
          "fieldType": "textarea",
          "label": "Message",
          "isRequired": true,
          "orderIndex": 3,
          "rows": 5
        }
      ]
    }
  ]
}
```

### Creating a Survey Form with Multiple Field Types
```json
{
  "name": "Customer Satisfaction Survey",
  "description": "Comprehensive customer satisfaction survey",
  "category": "Research",
  "status": "active",
  "sections": [
    {
      "title": "Demographics",
      "orderIndex": 1,
      "fields": [
        {
          "fieldType": "select",
          "label": "Age Range",
          "options": "[\"18-24\", \"25-34\", \"35-44\", \"45-54\", \"55+\"]",
          "isRequired": true,
          "orderIndex": 1
        },
        {
          "fieldType": "radio",
          "label": "Gender",
          "options": "[\"Male\", \"Female\", \"Other\", \"Prefer not to say\"]",
          "isRequired": false,
          "orderIndex": 2
        }
      ]
    },
    {
      "title": "Satisfaction Rating",
      "orderIndex": 2,
      "fields": [
        {
          "fieldType": "select",
          "label": "Overall Satisfaction",
          "options": "[\"Very Satisfied\", \"Satisfied\", \"Neutral\", \"Dissatisfied\", \"Very Dissatisfied\"]",
          "isRequired": true,
          "orderIndex": 1
        },
        {
          "fieldType": "checkbox",
          "label": "Would Recommend",
          "isRequired": false,
          "orderIndex": 2
        }
      ]
    }
  ]
}
```

## Security Considerations

1. **Authentication**: All endpoints require valid JWT tokens
2. **Authorization**: User context is extracted from JWT claims
3. **Input Validation**: Comprehensive validation using FluentValidation
4. **SQL Injection**: Protected by Entity Framework parameterized queries
5. **Soft Delete**: Entities are soft-deleted rather than hard-deleted
6. **Audit Trail**: All entities track creation and modification details

## Performance Considerations

1. **Eager Loading**: Related entities are loaded efficiently using Include()
2. **Pagination**: Consider implementing pagination for large result sets
3. **Caching**: Consider caching frequently accessed form templates
4. **Indexing**: Ensure proper database indexes on frequently queried columns

## Future Enhancements

1. **Form Submissions**: API for submitting form data
2. **Form Analytics**: Analytics and reporting on form usage
3. **Form Versioning**: Advanced versioning with rollback capabilities
4. **Conditional Logic**: Dynamic field visibility based on other field values
5. **Form Templates**: Pre-built form templates for common use cases
6. **Bulk Operations**: Bulk create/update/delete operations
7. **Export/Import**: JSON export/import functionality
8. **Form Sharing**: Public form sharing with unique URLs
