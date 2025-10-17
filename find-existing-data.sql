-- SQL Queries to find existing InventoryItem and Location data
-- Run these queries in your database to find valid IDs

-- 1. Find all Inventory Items (for itemId)
SELECT 
    Id,
    PartNumber,
    Description,
    Category,
    Manufacturer,
    UnitOfMeasure,
    UnitCost,
    IsActive,
    CreatedOn
FROM InventoryItem 
WHERE IsActive = 1 AND IsDeleted = 0
ORDER BY CreatedOn DESC;

-- 2. Find all Locations (for fromLocationId and toLocationId)
SELECT 
    Id,
    Name,
    Type,
    Address,
    Manager,
    IsActive,
    CreatedOn
FROM Location 
WHERE IsActive = 1 AND IsDeleted = 0
ORDER BY CreatedOn DESC;

-- 3. Find all Work Orders (for workOrderId)
SELECT 
    Id,
    WorkOrderNumber,
    Customer,
    Status,
    Priority,
    CreatedOn
FROM WorkOrder 
WHERE IsDeleted = 0
ORDER BY CreatedOn DESC;

-- 4. Sample data insertion (if no data exists)
-- Insert sample InventoryItem
INSERT INTO InventoryItem (
    Id, PartNumber, Description, Category, Manufacturer, 
    UnitOfMeasure, UnitCost, IsActive, CreatedOn, CreatedBy
) VALUES (
    NEWID(), 'PART-001', 'Sample Inventory Item', 'Electronics', 'Sample Manufacturer',
    'Each', 25.50, 1, GETDATE(), '00000000-0000-0000-0000-000000000000'
);

-- Insert sample Location
INSERT INTO Location (
    Id, Name, Type, Address, Manager, IsActive, CreatedOn, CreatedBy
) VALUES (
    NEWID(), 'Main Warehouse', 'Warehouse', '123 Main St, City, State', 'John Manager',
    1, GETDATE(), '00000000-0000-0000-0000-000000000000'
);
