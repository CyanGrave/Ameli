using AmeliAPI.UserManagement.DAL;

Console.WriteLine("MigrationBuilder");


if (!DAL.DataBaseUpdater.Update(new UserManagementContext()))
    throw new Exception("Database update failed!");