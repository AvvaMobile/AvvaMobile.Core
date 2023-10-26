using System.Text;

namespace AvvaMobile.Core;

public class KeyGenerator
{
    public static string CreateRandomPassword(int length = 14)
    {

        var validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
        var random = new Random();

        var chars = new char[length];
        for (var i = 0; i < length; i++)
        {
            chars[i] = validChars[random.Next(0, validChars.Length)];
        }
        return new string(chars);
    }

    public static int CreateRandomNumber(int length = 6)
    {
        Random random = new Random();

        StringBuilder password = new StringBuilder();

        // Önceki rastgele rakamları sıfırla.
        int previousDigit = -1;

        // Belirtilen uzunlukta rastgele rakamlar ekle, ardışık ve tersine ardışık sayıları engelle.
        for (int i = 0; i < length; i++)
        {
            int randomDigit;

            // Ardışık ve tersine ardışık sayıları önlemek için kontrol
            do
            {
                randomDigit = random.Next(10);
            } while (randomDigit == previousDigit || IsConsecutiveOrReverse(previousDigit, randomDigit));

            // Şu anki rakamı önceki rakam olarak kaydet.
            previousDigit = randomDigit;

            password.Append(randomDigit);
        }

        return Convert.ToInt32(password.ToString());
    }
    private static bool IsConsecutiveOrReverse(int previousDigit, int currentDigit)
    {
        if (previousDigit == -1)
            return false; // İlk rakam

        if (Math.Abs(previousDigit - currentDigit) == 1)
            return true; // Ardışık sayıları veya tersine ardışık sayıları kontrol et

        return false;
    }
    public static string CreateAlphanumericCode(int length = 14)
    {

        var validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();

        var chars = new char[length];
        for (var i = 0; i < length; i++)
        {
            chars[i] = validChars[random.Next(0, validChars.Length)];
        }
        return new string(chars);
    }
}