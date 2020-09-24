using System;
using System.IO;

namespace Airports
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = Menu();
            int numOfPassengers = -1;

            if (choice == 1)
            {
                Console.WriteLine(AirportDetails());
            }
            else if (choice == 2)
            {
                numOfPassengers = FlightDetails();
            }
            else if (choice == 3)
            {
                CalculateProfit();
            }
            else
            {

            }
        }

        static int Menu()
        {
            int choice = -1;
            do
            {
                Console.WriteLine("1. Enter Airport Details.");
                Console.WriteLine("2. Enter Flight Details.");
                Console.WriteLine("3. Enter Price Plan and Calculate Profits.");
                Console.WriteLine("Choose 1, 2 or 3. If you want to quit, press enter : ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid Choice... ");
                }
            } while (choice == -1);

            return choice;

        }

        static string AirportDetails()
        {
            const int numOfValsPerRow = 4;
            string ukAirport = "";
            string abroad = "";

            string[] readAirports = readFile("Airports");

            do
            {
                Console.WriteLine("Choose UK Airport: Liverpool John Lennon (type LPL) or Bourenmouth International (type BOH) : ");
                ukAirport = Console.ReadLine();
            } while (ukAirport != "LPL" && ukAirport != "BOH");

            Console.WriteLine("Enter abroad airport code: ");
            abroad = Console.ReadLine();

            return validateFileData(readAirports, abroad, numOfValsPerRow);
        }

        static int FlightDetails()
        {
            const int numOfValsPerRow = 5;
            string planeInput = "";
            int firstClassSeats = 0;
            int minimumFirstClass = 0;
            int totalSeats = 0;
            string[] planeTypes = readFile("Planes");
            do
            {
                Console.WriteLine("Choose an Aircraft Type: ");
                Console.WriteLine("Medium narrow body, Large narrow body Or Medium wide body");

                planeInput = Console.ReadLine();
            } while (validateFileData(planeTypes, planeInput, numOfValsPerRow).Length < 20);
            Console.WriteLine(validateFileData(planeTypes, planeInput, numOfValsPerRow));
            string[] chosenPlane = validateFileData(planeTypes, planeInput, numOfValsPerRow).Split(',');

            do
            {
                try
                {
                    Console.WriteLine("Enter num of first class seats: ");
                    firstClassSeats = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("That's not a number!");
                }
                
                minimumFirstClass = Convert.ToInt32(chosenPlane[numOfValsPerRow - 1]);
                totalSeats = Convert.ToInt32(chosenPlane[numOfValsPerRow - 2]);
            } while (firstClassSeats < minimumFirstClass || firstClassSeats > totalSeats/2);


            return totalSeats - 2 * firstClassSeats;

        }

        static void CalculateProfit()
        {

        }

        static string[] readFile(string filename)
        {
            string contents = "";
            string[] rows = new string[5];
            contents = File.ReadAllText(filename + ".txt");
            rows = contents.Split(',');

            return rows;
        }

        static string validateFileData(string[] fileContents, string val, int numOfValsPerRow)
        {

            bool found = false;
            int count = 0;

            while (!found && count < fileContents.Length)
            {
                if (fileContents[count] == val || fileContents[count] == "\r\n" + val)
                {
                    found = true;
                    for (int i = 1; i < numOfValsPerRow; i++)
                    {
                        val += "," + fileContents[count + 1];
                        count++;
                    }
                }
                else
                {
                    count += numOfValsPerRow;
                }
            }

            return val;
        }

    }
}
