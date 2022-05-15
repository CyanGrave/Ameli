using AmeliAPI.UserManagement.DAL.SQLite;

Console.WriteLine("MigrationBuilder");


if (!DAL.DataBaseUpdater.Update(new MigrationContext()))
    throw new Exception("Database update failed!");