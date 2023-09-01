
namespace WebServiceAutomation.Helper.Authentication
{
    public class Base64StringConverter
    {
        public static string GetBase64String(string username, string password)
        {
            string auth = username + ":" + password;
            byte[] inArray = System.Text.Encoding.UTF8.GetBytes(auth);
            return System.Convert.ToBase64String(inArray);
        }
    }
}
