namespace project_api
{
    public static class MyEnvironment
    {
        public static void SetMySQLConnention(){
             System.Environment.SetEnvironmentVariable("DATABASE_URL", "server=127.0.0.1;user id=netuser;password=netpass;port=3306;database=university;");
        }
    }
}