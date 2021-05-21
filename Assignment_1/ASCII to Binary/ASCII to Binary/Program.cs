using ASCII_to_Binary.Models;
using System;
using System.Text;
using System.IO;
using System.Linq;

namespace ASCII_to_Binary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type yout full name : ");
            string Name = Console.ReadLine();

            BinaryConverter binaryConverter = new BinaryConverter();
            string binaryValue = binaryConverter.ConvertTo(Name);
            Console.WriteLine($"\n{Name} as Binary: {binaryValue}");

            Console.WriteLine("\nCopy and Paste your Binary full name here : ");
            string binvalue = Console.ReadLine();
            Console.WriteLine($"\n{binvalue} from Binary to ASCII: {binaryConverter.ConvertBinaryToString(binaryValue)}");

            HexadecimalConverter hexadecimalConverter = new HexadecimalConverter();
            string hexadecimalValue = hexadecimalConverter.ConvertTo(Name);
            Console.WriteLine($"\n{Name} as Hexadecimal: {hexadecimalValue}");
            Console.WriteLine($"\n{Name} from Hexadecimal to ASCII: {hexadecimalConverter.ConveryFromHexToASCII(hexadecimalValue)}");

            string nameBase64Encoded = StringToBase64(Name);
            Console.WriteLine($"\n{Name} as Base64: {nameBase64Encoded}");

            
            string nameBase64Decoded = Base64ToString(nameBase64Encoded);
            Console.WriteLine($"\n{Name} from Base64 to ASCII: {nameBase64Decoded}");

            int[] cipher = new[] { 1, 1, 2, 3, 5, 8, 13 }; //Fibonacci Sequence
            string cipherasString = String.Join(",", cipher.Select(x => x.ToString())); //FOr display

            int encryptionDepth = 20;

            Encrypter encrypter = new Encrypter(Name, cipher, encryptionDepth);

            //Deep Encrytion
            string nameDeepEncryptWithCipher = Encrypter.DeepEncryptWithCipher(Name, cipher, encryptionDepth);
            Console.WriteLine($"\nDeep Encrypted {encryptionDepth} times using the cipher {{{cipherasString}}} {nameDeepEncryptWithCipher}");

            string nameDeepDecryptWithCipher = Encrypter.DeepDecryptWithCipher(nameDeepEncryptWithCipher, cipher, encryptionDepth);
            Console.WriteLine($"\nDeep Decrypted {encryptionDepth} times using the cipher {{{cipherasString}}} {nameDeepDecryptWithCipher}");

        }

        public static string StringToBase64(string data)
        {
            byte[] bytearray = Encoding.ASCII.GetBytes(data);

            string result = Convert.ToBase64String(bytearray);

            return result;
        }

        public static string Base64ToString(string base64String)
        {
            byte[] bytearray = Convert.FromBase64String(base64String);

            using (var ms = new MemoryStream(bytearray))
            {
                using (StreamReader reader = new StreamReader(ms))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
        }
    }
}

