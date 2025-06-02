namespace MySelf.MSACommerce.VerificationServer.Tools
{
    public static class StringExtensions
    {
        public static string Generate(this string source, int length = 6)
        {
            var random = new Random();
            var code = "";
            for (var i = 0; i < length; i++)
            {
                code += random.Next(0, 10);  // 生成 0-9 的随机数字
            }
            return code;
        }
    }
}
