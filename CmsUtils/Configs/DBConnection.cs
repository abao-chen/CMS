namespace CmsUtils
{
    public class DBConnection
    {
        public DBConnection(bool encrypt)
        {
            Encrypt = encrypt;
        }

        public static bool Encrypt { get; set; }

        public static string connectionString
        {
            get
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["NFineDbContext"]
                    .ConnectionString;
                if (Encrypt)
                    return DESEncrypt.Decrypt(connection);
                return connection;
            }
        }
    }
}