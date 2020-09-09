public class SecurityHelp
{
    public static string XSS(string dt)
    {
        if (dt != null)
        {
            string gvCopy = dt.ToLowerInvariant();
            string[,] arr = new string[,]
            {
                { "'", "" },{ "%27", "" },{ "<", "" },{ ">", "" },{ "&lt;", "" },{ "&gt;", "" },  { "script", "" }, { "cookie", "" }, { "document", "" }, { "src", "" }, { "alert", "" }, { "embed", "" }, { "object", "" }, { "applet", "" }, { "geturl", "" }, { "applet", "" }, { ";", "" }
            };
            int abc = -1;
            for (int i = 0; i < arr.Length / 2; i++)
            {
                abc = gvCopy.IndexOf(arr[i, 0]);
                if (abc > -1)
                {
                bastan:
                    dt = dt.Substring(0, abc) + arr[i, 1] + dt.Substring(abc + arr[i, 0].Length, dt.Length - abc - arr[i, 0].Length);
                    gvCopy = gvCopy.Substring(0, abc) + arr[i, 1] + gvCopy.Substring(abc + arr[i, 0].Length, gvCopy.Length - abc - arr[i, 0].Length);
                    abc = gvCopy.IndexOf(arr[i, 0]);
                    if (abc > -1) goto bastan;
                }
            }
        }
        return dt;
    }
}