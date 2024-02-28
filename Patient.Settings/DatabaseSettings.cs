namespace Patient.Settings
{
    public class DatabaseSettings
    {
        public string ConnectionString
        {
            get; set;
        } = string.Empty;

        public int MaxRetryCount
        {
            get; set;
        }

        public int CommandTimeout
        {
            get; set;
        } = 30;

        public bool EnableDetailedErrors
        {
            get; set;
        }

        public bool EnableSensitiveDataLogging
        {
            get; set;
        }
    }
}
