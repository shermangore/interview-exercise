using Interview_Exercise.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview_Exercise
{
    public class Repository : IRepository
    {
        string dataPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Interview-Exercise");

        /// <summary>
        /// Adds a new Country (name, code) to the CSV repository after checking that the repository exists
        /// </summary>
        /// <param name="country"></param>
        public virtual void Add(Country country)
        {
            // Verify that the country code meets the guidelines
            if (!IsCodeAllowed(country.Code))
            {
                throw new Exception("Invalid country code");
            }

            // Check for existence of file and create if it doesn't exist
            if (!File.Exists(dataPath))
            {
                File.Create(dataPath);
            }

            // Check to see if the country already exists within the file
            if (!CheckDupes(country))
            {
                // If no duplicates exist, then add the country
                File.AppendAllText(dataPath, $"[{country.Code}:{country.Name}],");
            }
        }

        /// <summary>
        /// Updates an existing country to a new name based on its 3-digit code
        /// </summary>
        /// <param name="country"></param>
        public virtual void Update(Country country)
        {
            // Check for existence of file and throw an exception if it doesn't
            if (!File.Exists(dataPath))
            {
                throw new Exception("There is no file to update");
            }

            // If file does exist, then update
            // read the file into a string
            string data = File.ReadAllText(dataPath);
            Char delimiter = ',';
            // Split the string into an array
            string[] arrString = data.Split(delimiter);
            bool isFound = false;

            // Loop through the array
            foreach(var cntry in arrString)
            {
                // If the country code is found in the string representation of the country:
                if (cntry.IndexOf(country.Code) != -1)
                {
                    // Set the isFound variable to true, so we know whether to update the file or not
                    isFound = true;
                    // Create a variable for the beginning of the country name
                    int begIdx = cntry.IndexOf(":") + 1;

                    // Update the string representation of the country name with the country name
                    cntry.Replace(cntry.Substring(begIdx, cntry.Length), $"{country.Name}]");
                }
            }

            // If the country was found:
            if (isFound)
            {
                // Convert the array back to a string
                data = string.Join(",", arrString);

                // Rewrite the file with the updated text (csv)
                File.WriteAllText(dataPath, data);
            }
        }

        /// <summary>
        /// Deletes an existing country based on its 3-digit code
        /// </summary>
        /// <param name="countryCode"></param>
        public virtual void Delete(string countryCode)
        {
            // Check for existence of file and throw an exception if it doesn't
            if (!File.Exists(dataPath))
            {
                throw new Exception("There is no file to delete");
            }

            // If file does exist, then proceed with deleting
            // read the file into a string
            string data = File.ReadAllText(dataPath);
            Char delimiter = ',';
            // Split the string into an array (yes, there are some DRY violations in this here, code)
            string[] arrString = data.Split(delimiter);
            bool isFound = false;

            // Loop through the array
            foreach (var cntry in arrString)
            {
                // If the country code is found in the string representation of the country:
                if (cntry.IndexOf(countryCode) != -1)
                {
                    // Set the isFound variable to true, so we know whether to delete the country or not
                    isFound = true;

                    // Delete the entire string representation of the country
                    cntry.Remove(0, cntry.Length);
                }
            }

            // If the country was found:
            if (isFound)
            {
                // Convert the array back to a string
                data = string.Join(",", arrString);

                // Rewrite the file with the updated text (csv)
                File.WriteAllText(dataPath, data);
            }
        }

        /// <summary>
        /// Gets a Country based on its 3-digit code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public virtual Country Get(string countryCode)
        {
            // Check for existence of file and throw an exception if it doesn't
            if (!File.Exists(dataPath))
            {
                throw new Exception("There is no file to search");
            }

            // If file does exist, then proceed with getting the country
            // read the file into a string
            string data = File.ReadAllText(dataPath);
            Char delimiter = ',';
            // Split the string into an array
            string[] arrString = data.Split(delimiter);
            bool isFound = false;
            // Create a new country variable to use to return the data
            Country result = new Country();

            // Loop through the array
            foreach (var cntry in arrString)
            {
                // If the country code is found in the string representation of the country:
                if (cntry.IndexOf(countryCode) != -1)
                {
                    // Set the isFound variable to true, so we know whether to return the country or not
                    isFound = true;

                    // Create variable for the indices where the countryCode and countryName begin and end
                    int begIdx = cntry.IndexOf("[") + 1;
                    int midIdx = cntry.IndexOf(":") + 1;
                    int endIdx = cntry.IndexOf("]");

                    // Set the values of the Country object created above
                    result.Code = cntry.Substring(begIdx, 3);
                    result.Name = cntry.Substring(midIdx, (endIdx - midIdx));
                }
            }

            // If the country was found, return the Country
            if (isFound)
            {
                return result;
            }

            // If the country wasn't found, return null
            return null;
        }

        /// <summary>
        /// Clears all entries from the csv
        /// </summary>
        public virtual void Clear()
        {
            if (!File.Exists(dataPath))
            {
                throw new Exception("There is no file to search");
            }

            File.WriteAllText(dataPath, string.Empty);
        }

        /// <summary>
        /// Checks the CSV for countries that already exist in order to eliminate duplicates
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        private bool CheckDupes(Country country)
        {
            // Check for existence of file and return false if it doesn't
            if (File.Exists(dataPath))
            {
                // If file does exist, then proceed with getting the country
                // read the file into a string
                string data = File.ReadAllText(dataPath);

                // If the countryCode is found within the file, return true
                if (data.IndexOf($"{country.Code.ToUpper()}:") > 0)
                {
                    return true;
                }
            }

            // If it wasn't found, return false
            return false;
        }

        public bool IsCodeAllowed (string countryCode)
        {
            bool result = true;

            countryCode = countryCode.ToUpper();

            // Check for values between AAA and AAZ
            if (Char.Parse(countryCode.Substring(0, 1)) == 65 && Char.Parse(countryCode.Substring(1, 1)) == 65 && (Char.Parse(countryCode.Substring(2, 1)) >= 65 && Char.Parse(countryCode.Substring(2, 1)) <= 90))
            {
                result = false;
            }

            // Check for values between QMA and QZZ
            if (Char.Parse(countryCode.Substring(0, 1)) == 81 && (Char.Parse(countryCode.Substring(1, 1)) >= 77 && Char.Parse(countryCode.Substring(1, 1)) <= 90))
            {
                result = false;
            }

            // Check for values between XAA and XZZ
            if (Char.Parse(countryCode.Substring(0, 1)) == 88 && (Char.Parse(countryCode.Substring(1, 1)) >= 65 && Char.Parse(countryCode.Substring(1, 1)) <= 90))
            {
                result = false;
            }

            // Check for values between ZZA and ZZZ
            if (Char.Parse(countryCode.Substring(0, 1)) == 90 && Char.Parse(countryCode.Substring(1, 1)) == 90)
            {
                result = false;
            }

            return result;
        }
    }
}
