namespace webdd.MyClass
{
    public class CommonUse
    {
        public string GenerateCaptchaCode()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charactersLength = characters.Length;
            var captchaCode = "";
            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                captchaCode += characters[random.Next(charactersLength)];
            }

            return captchaCode;
        }
    }
}
