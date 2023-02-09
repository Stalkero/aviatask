using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace someapp.CreateAccount
{
    internal class create_account_utils
    {

        public class Airport
        {
            public string ICAO;
            public string Name;
            public float LatDec;
            public float LongDec;

        }
        public class Pilot_info
        {
            public string username { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string password { set; get; }
            public string aircraft_type { set; get; }
            public string country { set; get; }
        }

        public List<Airport> AirportsFromCountry = new List<Airport>();
        public Pilot_info pilot_Info = new Pilot_info();
        debug_params.debug_tools debug_Tools = new debug_params.debug_tools();
        

        public void getAirportsFromCountry(string country)
        {

            if (debug_Tools.debugMsg)
                MessageBox.Show("Searching, hold on");

            string fileName = "db/airports.csv";

            foreach (var line in File.ReadLines(fileName))
            {
                var columns = line.Split('\t');

                if (columns[5] == country)
                {
                    Airport airport = new Airport
                    {
                        ICAO = columns[1],
                        Name = columns[3],
                        LatDec =  float.Parse(columns[15]),
                        LongDec =  float.Parse(columns[16])
                    };
                    AirportsFromCountry.Add(airport);
                }



            }
            if (debug_Tools.debugMsg)
                MessageBox.Show("Found");
        }

        public static string EncryptText(string plainText, string password)
        {
            try
            {
                byte[] salt = Encoding.UTF8.GetBytes("salted123");
                using (AesManaged aes = new AesManaged())
                {
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);

                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Encryption failed: " + ex.Message);
                return null;
            }
        }
        public static string DecryptText(string encryptedText, string password)
        {
            try
            {
                byte[] salt = Encoding.UTF8.GetBytes("salted123");
                using (AesManaged aes = new AesManaged())
                {
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);

                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Decryption failed: " + ex.Message);
                return null;
            }
        }
    }
}
