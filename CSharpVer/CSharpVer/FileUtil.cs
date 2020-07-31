using System;
using System.IO;

namespace CSharpVer
{
    class FileUtil
    {
        private static string GetPath(string file)
        {
            var enviroment = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;

            return Path.Combine(projectDirectory, file);
        }
        public static void WriteToFile(string file, byte[] fileContent)
        {
            Console.WriteLine("Writing bytes to file...");
            File.WriteAllBytes(GetPath(file), fileContent);
            Console.WriteLine("Done writing bytes to file...");
        }

        public static byte[] ReadBytesFromFile(string file)
        {
            Console.WriteLine("Reading bytes from file...");
            byte[] data = File.ReadAllBytes(GetPath(file));
            Console.WriteLine("Done reading from file...");
            return data;
        }

        public static byte[] combineBytes(byte[] a, byte[] b)
        {
            byte[] combined = new byte[a.Length + b.Length];
            Array.Copy(a, 0, combined, 0, a.Length);
            Array.Copy(b, 0, combined, a.Length, b.Length);

            return combined;
        }
    }
}
