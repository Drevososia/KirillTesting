namespace KirillTesting.Helpers
{
    public static class ConverterForImport
    {
        public static int ConvertToInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;
            else
                return Convert.ToInt32(value);
        }

        public static DateTime? ConvertToNullubleDateTime(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            else
                return Convert.ToDateTime(value);
        }
    }
}
