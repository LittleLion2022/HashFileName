using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace HashFilename
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string currentFolder = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(currentFolder);
            foreach (string file in files)
            {
                if (IsImageFile(file))
                {
                    string newFileName = GetFileHashValue(file);
                    ChangeFileName(file, newFileName);
                }
            }
        }
        static bool IsImageFile(string fileName)
        {
            string[] imageExtensions = { ".webp", ".jpeg", ".jpg", ".png" };
            string extension = Path.GetExtension(fileName).ToLower();
            return imageExtensions.Contains(extension);
        }

        static string GetFileHashValue(string fileName)
        {
            SHA1 hash = SHA1.Create();
            var fs = File.OpenRead(fileName);
            byte[] result = hash.ComputeHash(fs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in result)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            fs.Close();
            return sb.ToString();
        }

        static void ChangeFileName(string filePath, string newPath)
        {
            string extention = Path.GetExtension(filePath);
            File.Move(filePath, newPath + extention);
        }
    }
}