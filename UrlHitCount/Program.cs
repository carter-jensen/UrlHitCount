using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UrlHitCount {
    class Program {
        private static IDictionary<string, Dictionary<string, int>> _dictionary = new Dictionary<string, Dictionary<string, int>>();

        static void Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("A File Path must be entered as an argument");
                Console.ReadKey();
                return;
            }
            // Get the Input File from arguments
            string filePath = args[0];
            // Console.WriteLine($"File Path: {filePath}");

            // Check if the file exists
            if (!File.Exists(filePath)) {
                Console.WriteLine($"File at {filePath} does not exist.");
                return;
            }
            ReadData(filePath);
            DisplayData();

            Console.ReadKey();
        }

        public static void ReadData(string filePath) {
            // Open the file
            // Read the file line by line
            foreach (var line in File.ReadLines(filePath)) {
                var splitLine = line.Split('|');
                // Check the input date from unix epoch and translate to datetime
                var date = ConvertDate(splitLine[0].Trim());
                var url = splitLine[1].Trim();

                if (_dictionary.ContainsKey(date)) {
                    // dictionary contains date, check for url
                    if (_dictionary[date].ContainsKey(url)) {
                        // increment the inner dictionary value
                        _dictionary[date][url] += 1;
                    } else {
                        // Add to inner dictionary if not present
                        _dictionary[date].Add(url, 1);
                    }
                } else {
                    // dictionary does not contain date, so add it
                    var innerDictionary = new Dictionary<string, int>(){ { url, 1 } };
                    _dictionary.Add(date, innerDictionary);
                }
            }
        } // Read Data

        public static string ConvertDate(string epochDate) {
            long seconds = Convert.ToInt64(epochDate);
            DateTimeOffset offset = DateTimeOffset.FromUnixTimeSeconds(seconds);
            DateTime newDate = offset.UtcDateTime;
            string date = newDate.ToString("MM/dd/yyyy 'GMT'");
            return date;

        } // ConvertDate

        public static void DisplayData() {
            // order the lists by date first
            var orderDates = _dictionary.Keys.ToList();
            orderDates.Sort();
            // When finished reading all the data, output the Date
            Console.WriteLine("Output: ");
            foreach (var dates in orderDates) {
                Console.WriteLine(dates);
                // now order the urls by hit count
                var orderUrls = _dictionary[dates].ToList();
                foreach (var url in orderUrls.OrderByDescending(key => key.Value)) {
                    Console.WriteLine($"{url.Key} {url.Value}");
                }
            }           
        } // Display Data
    } // Program
} // UrlHitCount
