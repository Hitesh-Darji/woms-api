-- Add missing ApplicationUser columns
ALTER TABLE AspNetUsers ADD FullName nvarchar(200) NULL;
ALTER TABLE AspNetUsers ADD Phone nvarchar(20) NULL;
ALTER TABLE AspNetUsers ADD Role nvarchar(max) NOT NULL DEFAULT 'Technician';
ALTER TABLE AspNetUsers ADD Skills nvarchar(max) NULL;
ALTER TABLE AspNetUsers ADD Status nvarchar(max) NULL;
