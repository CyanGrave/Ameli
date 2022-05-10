using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class DataBaseUpdater
    {
        public static bool Update(Context context)
        {
            try
            {
                context.Database.Migrate();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
