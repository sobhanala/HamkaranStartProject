using System.Configuration;

namespace Shell
{
    public class AppSettings
    {
        public AppSettings()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["WarehouseDb"]?.ConnectionString;

            if (string.IsNullOrEmpty(ConnectionString))
                throw new ConfigurationErrorsException("Database connection string is not configured in app.config");
        }

        public string ConnectionString { get; }
    }
}