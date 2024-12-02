CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- Create an index on Username
CREATE INDEX IX_Users_Username ON Users (Username);

-- Create an index on IsActive
CREATE INDEX IX_Users_IsActive ON Users (IsActive);