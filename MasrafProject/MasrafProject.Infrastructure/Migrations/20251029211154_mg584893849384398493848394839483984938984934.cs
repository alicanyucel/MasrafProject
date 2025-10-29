using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasrafProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg584893849384398493848394839483984938984934 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Roles kolonu varsa, önce varsayılan kısıtını (default constraint) kaldır, sonra kolonu kaldır
            migrationBuilder.Sql(@"
IF EXISTS (
    SELECT 1 
    FROM sys.columns 
    WHERE Name = N'Roles' 
      AND Object_ID = Object_ID(N'dbo.AppUsers')
)
BEGIN
    DECLARE @dfName NVARCHAR(128);
    SELECT @dfName = dc.name
    FROM sys.default_constraints dc
    WHERE dc.parent_object_id = OBJECT_ID(N'dbo.AppUsers')
      AND COL_NAME(dc.parent_object_id, dc.parent_column_id) = 'Roles';

    IF @dfName IS NOT NULL
    BEGIN
        EXEC(N'ALTER TABLE [dbo].[AppUsers] DROP CONSTRAINT [' + @dfName + ']');
    END

    ALTER TABLE [dbo].[AppUsers] DROP COLUMN [Roles];
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Roles kolonu yoksa ekle (idempotent) ve default constraint ver
            migrationBuilder.Sql(@"
IF NOT EXISTS (
    SELECT 1 
    FROM sys.columns 
    WHERE Name = N'Roles' 
      AND Object_ID = Object_ID(N'dbo.AppUsers')
)
BEGIN
    ALTER TABLE [dbo].[AppUsers] ADD [Roles] nvarchar(max) NOT NULL CONSTRAINT DF_AppUsers_Roles DEFAULT N'';
END
");
        }
    }
}
