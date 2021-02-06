using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Laba_1_2_Bilkevych
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Paths to .txt
            string pathFile_1 = @"D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\First\Acid.txt";
            string pathFile_2 = @"D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Second\Lina Kostenko.txt";
            string pathFile_3 = @"D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Third\Phishing.txt";

            //Paths to archives
            string pathArchive_1 = @"D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\First\Acid.txt.bz2";
            string pathArchive_2 = @"D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Second\Lina Kostenko.txt.bz2";
            string pathArchive_3 = @"D:\Study University Borys Bilkevych 3 course 2 semestr\Computer System\Laboratory works\Laboratory work 1\Part 1\Third\Phishing.txt.bz2";

            //Outputs for file
            OutputTheResultsForFile(pathFile_1);
            OutputTheResultsForFile(pathFile_2);
            OutputTheResultsForFile(pathFile_3);

            //Outputs for archives
            OutputTheResultsForArcives(pathFile_1, pathArchive_1, "Acid.txt.bz2");
            OutputTheResultsForArcives(pathFile_2, pathArchive_2, "Lina Kostenko.txt.bz2");
            OutputTheResultsForArcives(pathFile_2, pathArchive_3, "Phishing.txt.bz2");

            Console.ReadKey();
        }
        static void OutputTheResultsForFile(string path)
        {
            string text = ReadFile(path);
            List<char[]> listOfByte = new List<char[]>();
            listOfByte = EncodeToASCII(text);
            int numberOfCase;
            listOfByte = EncodeToArrayInSixBit(listOfByte, out numberOfCase);
            Console.WriteLine("_____________________________________________________________________________________________________");
            EncodeToBase64(listOfByte, numberOfCase);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");
            var plainTextBytes = Encoding.Default.GetBytes(text);
            Console.WriteLine(Convert.ToBase64String(plainTextBytes) + "\n\n");
        }
        static void OutputTheResultsForArcives(string pathfile, string patharch, string str)
        {
            byte[] list = new byte[] { };
            string text = ReadFile(pathfile);
            List<char[]> listOfByte = new List<char[]>();
            listOfByte = EncodeToASCII(text);
            int numberOfCase;
            listOfByte = ReadArchive(patharch, ref list);
            listOfByte = EncodeToArrayInSixBit(listOfByte, out numberOfCase);
            Console.WriteLine("_____________________________________________________________________________________________________");
            Console.WriteLine("\n\nArchive " + str + "\n\n");
            EncodeToBase64(listOfByte, numberOfCase);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");
            Console.WriteLine(Convert.ToBase64String(list.ToArray()) + "\n\n");
        }
        static List<char[]> ReadArchive(string path, ref byte[] array)
        {
            List<char[]> listOfByte = new List<char[]>();
            array = File.ReadAllBytes(path);
            foreach (byte a in array)
            {
                listOfByte.Add(ConvertToBit(a));
            }
            return listOfByte;
        }

        static void EncodeToBase64(List<char[]> listOfByteBase64, int numberOfCase)
        {
            //Converrt to Base64
            Dictionary<int, char> base64 = new Dictionary<int, char>
            {
                {0, 'A'},
                {1, 'B'}, {2, 'C'}, {3, 'D'}, {4, 'E'}, {5, 'F'},
                {6, 'G'}, {7, 'H'}, {8, 'I'}, {9, 'J'}, {10, 'K'},
                {11, 'L'}, {12, 'M'}, {13, 'N'}, {14, 'O'}, {15, 'P'},
                {16, 'Q'}, {17, 'R'}, {18, 'S'}, {19, 'T'}, {20, 'U'},
                {21, 'V'}, {22, 'W'}, {23, 'X'}, {24, 'Y'}, {25, 'Z'},
                {26, 'a'}, {27, 'b'}, {28, 'c'}, {29, 'd'}, {30, 'e'},
                {31, 'f'}, {32, 'g'}, {33, 'h'}, {34, 'i'}, {35, 'j'},
                {36, 'k'}, {37, 'l'}, {38, 'm'}, {39, 'n'}, {40, 'o'},
                {41, 'p'}, {42, 'q'}, {43, 'r'}, {44, 's'}, {45, 't'},
                {46, 'u'}, {47, 'v'}, {48, 'w'}, {49, 'x'}, {50, 'y'},
                {51, 'z'}, {52, '0'}, {53, '1'}, {54, '2'}, {55, '3'},
                {56, '4'}, {57, '5'}, {58, '6'}, {59, '7'}, {60, '8'},
                {61, '9'}, {62, '+'}, {63, '/'},
            };

            string stringBase64 = "";
            string buffer = "";
            for (int i = 0; i < listOfByteBase64.Count; i++)
            {
                buffer = "";
                foreach (char number in listOfByteBase64[i])
                {
                    buffer += number;
                }
                stringBase64 += base64[Convert.ToInt32(buffer, 2)];
            }
            if (numberOfCase == 1)
                stringBase64 += "==";
            else
                 if (numberOfCase == 2)
                stringBase64 += "=";

            Console.WriteLine(stringBase64);
        }

        static List<char[]> EncodeToArrayInSixBit(List<char[]> listOfByte, out int numberOfCase)
        {
            //Convert array char 8 bit to array char 6 bit
            char[] arrayBuffer = new char[6];
            List<char[]> listOfByteBase64 = new List<char[]>();

            int j = 0;
            numberOfCase = 0;
            int g;
            int countOfIterator = 0;
            foreach (char[] array in listOfByte)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    arrayBuffer[j] = array[i];

                    if (j == 5)
                    {
                        if ((g = (array.Length - i + 1)) % 3 == 0)
                        {
                            numberOfCase = 0;
                        }
                        else
                        {
                            if (g % 3 == 1)
                            {
                                numberOfCase = 1;
                            }
                            else
                            {
                                if (g % 3 == 2)
                                {
                                    numberOfCase = 2;
                                }
                            }
                        }

                        listOfByteBase64.Add(arrayBuffer);
                        arrayBuffer = new char[6];
                        j = 0;
                        if (listOfByte.Count - 1 == countOfIterator)
                        {
                            //i++;
                            int counterOfZeros = 0;
                            for (i += 1; i < array.Length; i++)
                            {
                                counterOfZeros++;
                                arrayBuffer[j] = array[i];
                                j++;
                            }
                            for (; j < arrayBuffer.Length; j++)
                            {
                                arrayBuffer[j] = '0';
                            }
                            listOfByteBase64.Add(arrayBuffer);
                            break;
                        }
                        //j = 0;
                        continue;
                    }
                    j++;
                }
                countOfIterator++;
            }
            return listOfByteBase64;
        }

        static char[] ConvertToBit(int a)
        {
            char[] boolArrayOfEightBit = new char[8];
            string boolArray = Convert.ToString(a, 2);

            int i = 0;
            if (boolArray.Length != 8)
            {
                for (; i < 8 - boolArray.Length; i++)
                {
                    boolArrayOfEightBit[i] = '0';
                }
            }

            int j = 0;
            for (; i < boolArrayOfEightBit.Length; i++)
            {
                boolArrayOfEightBit[i] = boolArray[j];
                j++;
            }

            return boolArrayOfEightBit;
        }

        //Fead from file
        static string ReadFile(string pathFile)
        {
            string text = "";
            string line;
            if (File.Exists(pathFile))
            {
                using (StreamReader sw = new StreamReader(pathFile))
                {
                    while ((line = sw.ReadLine()) != null)
                    {
                        text += line + "\n";
                    }
                }
            }
            else
                throw new Exception("Файлу не існує!");
            Console.WriteLine(text);
            Console.WriteLine();
            return text;
        }

        static List<char[]> EncodeToASCII(string text)
        {
            Dictionary<char, int> ascii = new Dictionary<char, int>
            {
                        {'\n', 10 },
                        {' ', 32}, {'!', 33}, { '\"', 34},{ '#', 35},
                        { '$', 36}, {'%', 37}, {'&', 38}, {'\'', 39}, {'(', 40},
                        {')', 41}, {'*', 42}, {'+', 43},  {'-', 45},
                        {'.', 46}, {'/', 47}, {'0', 48}, {'1', 49}, {'2', 50},
                        {'3', 51}, {'4', 52}, {'5', 53}, {'6', 54}, {'7', 55},
                        {'8', 56}, {'9', 57}, {':', 58}, {';', 59}, {'<', 60},
                        {'=', 61}, {'>', 62}, {'?', 63}, {'@', 64}, {'A', 65},
                        {'B', 66}, {'C', 67}, {'D', 68}, {'E', 69}, {'F', 70},
                        {'G', 71}, {'H', 72}, {'I', 73}, {'J', 74}, {'K', 75},
                        {'L', 76}, {'M', 77}, {'N', 78}, {'O', 79}, {'P', 80},
                        {'Q', 81}, {'R', 82}, {'S', 83}, {'T', 84}, {'U', 85},
                        {'V', 86}, {'W', 87}, {'X', 88}, {'Y', 89}, {'Z', 90},
                        {'[', 91}, {'\\', 92}, {']', 93}, {'^', 94}, {'_', 95},
                        {'`', 96}, {'a', 97}, {'b', 98}, {'c', 99}, {'d', 100},
                        {'e', 101}, {'f', 102}, {'g', 103}, {'h', 104}, {'i', 105},
                        {'j', 106}, {'k', 107}, {'l', 108}, {'m', 109}, {'n', 110},
                        {'o', 111}, {'p', 112}, {'q', 113}, {'r', 114}, {'s', 115},
                        {'t', 116}, {'u', 117}, {'v', 118}, {'w', 119}, {'x', 120},
                        {'y', 121}, {'z', 122}, {'{', 123}, {'|', 124}, {'}', 125},
                        {'~', 126},

                        { 'Ђ', 128}, { 'Ѓ', 129}, { ',', 130},
                        { 'ѓ', 131},
                        { '„', 132}, {'…', 133}, {'†', 134}, {'‡', 135},
                        {'€', 136}, {'‰', 137}, {'Љ', 138}, {'‹', 139}, {'Њ', 140},
                        {'‘', 145},
                        {'’', 146}, {'“', 147}, {'”', 148}, {'•', 149}, {'–', 150},
                        {'—', 151},             {'™', 153}, {'љ', 154},
                        {'њ', 156}, {'ќ', 157}, {'ћ', 158}, {'џ', 159},
                        {'Ў', 161}, {'ў', 162}, {'Ћ', 163}, {'¤', 164}, {'Ґ', 165},
                        {'¦', 166}, {'§', 167}, {'Ё', 168}, {'©', 169}, {'Є', 170},
                        {'«', 171}, {'¬', 172},             {'®', 174}, {'Ї', 175},
                        {'°', 176}, {'±', 177}, {'І', 178}, {'і', 179}, {'ґ', 180},
                        {'µ', 181}, {'¶', 182}, {'·', 183}, {'ё', 184}, {'№', 185},
                        {'є', 186}, {'»', 187},
                        {'ї', 191}, {'А', 192}, {'Б', 193}, {'В', 194}, {'Г', 195},
                        {'Д', 196}, {'Е', 197}, {'Ж', 198}, {'З', 199}, {'И', 200},
                        {'Й', 201}, {'К', 202}, {'Л', 203}, {'М', 204}, {'Н', 205},
                        {'О', 206}, {'П', 207}, {'Р', 208}, {'С', 209}, {'Т', 210},
                        {'У', 211}, {'Ф', 212}, {'Х', 213}, {'Ц', 214}, {'Ч', 215},
                        {'Ш', 216}, {'Щ', 217}, {'Ъ', 218}, {'Ы', 219}, {'Ь', 220},
                        {'Э', 221}, {'Ю', 222}, {'Я', 223}, {'а', 224}, {'б', 225},
                        {'в', 226}, {'г', 227}, {'д', 228}, {'е', 229}, {'ж', 230},
                        {'з', 231}, {'и', 232}, {'й', 233}, {'к', 234}, {'л', 235},
                        {'м', 236}, {'н', 237}, {'о', 238}, {'п', 239}, {'р', 240},
                        {'с', 241}, {'т', 242}, {'у', 243}, {'ф', 244}, {'х', 245},
                        {'ц', 246}, {'ч', 247}, {'ш', 248}, {'щ', 249}, {'ъ', 250},
                        {'ы', 251}, {'ь', 252}, {'э', 253}, {'ю', 254}, {'я', 255},
            };

            //Convert to ASCII and get the bits
            List<char[]> listOfByte = new List<char[]>();
            foreach (char letter in text)
            {
                listOfByte.Add(ConvertToBit(ascii[letter]));
            }
            return listOfByte;
        }
    }
}