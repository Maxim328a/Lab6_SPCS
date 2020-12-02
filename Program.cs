using System;

namespace LB_5_SPCS
{
    class Program
    {
        static int[] decimals;
        static string[] binaries;
        static string[] addresses;

        static void Main(string[] args)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;

            FillDecimalsArray();
            Console.WriteLine("From decimals to binaries:\n");
            foreach (int number in decimals)
            {
                Console.WriteLine(number.ToString().PadLeft(4) + " =   " + FromDecimalToBinary(number).PadLeft(8, '0'));
            }
            FillBinariesArray();
            Console.WriteLine("\nFrom binaries to decimals:\n");
            foreach (string str in binaries)
            {
                Console.WriteLine(str.PadLeft(10) + " = " + FromBinaryToDecimal(str));
            }
            FillAddressesArray();
            GetNetAddress();
            Console.WriteLine("\nNet Addresses\n");
            for (int i = 0; i < 12; i+=3) 
            {
                Console.WriteLine(("IP-адрес " + (i % 6 == 0 ? "отправителя" : "получателя")).PadRight(24) + addresses[i]);
                Console.WriteLine("Маска подсети ".PadRight(24) + addresses[i + 1]);
                Console.WriteLine("Адрес сети ".PadRight(24) + addresses[i + 2] + "     " + BinaryAddressToDecimal(addresses[i+2]) + "\n");
            }
            Console.ReadKey();
        }

        static void FillDecimalsArray() 
        {
            decimals = new int[] { 101, 204, 19, 68, 98, 166, 33, 212, 
            156, 200, 255, 88, 56, 73, 47, 13, 20, 90, 223, 63, 195, 180, 
            95, 111, 114, 76, 200, 91, 132, 54};
        }

        static void FillBinariesArray() 
        {
            binaries = new string[] {"00010001", "00110101", "10101010", "11101011",
            "01010101", "11000101", "11110000", "10110010", "11111111", "11001110",
            "00110011", "00101011", "00011111", "11011100", "01111101", "01001101",
            "01011111", "10111001", "11100010", "11010101"};
        }

        static void FillAddressesArray()
        {
            addresses = new string[] {"10011001 10101010 00100101 10100011",
            "11111111 11111111 00000000 00000000", "", "11011001 10101010 10101010 11101001",
            "11111111 11111111 00000000 00000000", "", "00000101 10010010 00010000 10100000",
            "11111111 11111111 00000000 00000000", "", "11011001 10101010 10101010 11101001",
            "11111111 11111111 00000000 00000000", ""};
        }

        static int FromBinaryToDecimal(string str) 
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            int number = 0;
            for (int i = 0; i < charArray.Length; i++) 
            {
                number += (int)Math.Pow(2, i) * (int)Char.GetNumericValue(charArray[i]);
            }
            return number;
        }

        static string FromDecimalToBinary(int number) 
        {
            string result = "";
            do
            {
                result = result.Insert(0, (number % 2).ToString());
                number /= 2;
            } while (number >= 2);
            result = result.Insert(0, number.ToString());
            return result;
        }

        static void GetNetAddress() 
        {
            for (int i = 0; i < 12; i += 3) 
            {
                string netAddress = "";
                for (int a = 0; a < addresses[i].Length; a++) 
                {
                    if (addresses[i].Substring(a, 1).Equals("1") && addresses[i + 1].Substring(a, 1).Equals("1")) netAddress += "1";
                    else if (addresses[i].Substring(a, 1).Equals(" ") && addresses[i + 1].Substring(a, 1).Equals(" ")) netAddress += " ";
                    else netAddress += "0";
                }
                addresses[i + 2] = netAddress;
            }
        }

        static string BinaryAddressToDecimal(string address)
        {
            string result = "";
            for (int i = 0; i < 4; i++) 
            {
                result += FromBinaryToDecimal(address.Substring(i * 8 + i, 8));
                if (i != 3) result += ".";
            }
            return result;
        }
    }
}
