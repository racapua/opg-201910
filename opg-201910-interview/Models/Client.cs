using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace opg_201910_interview.Models
{
    public class Client
    {
        public List<string> FileOrder { get; set; }
        public string ClientId { get; set; }
        public string FileDirectoryPath { get; set; }

        public List<string> Arrange(List<string> files) {
            
            var arrangedFiles = new List<string>();

            // Determine DateFormat
            var dateFormat = GetDateFormatType(files);

            // Iterate through the file order
            foreach (var orderFile in FileOrder) {
                // Search the name in the order from the list of files
                // Arrange them in ascending order
                var filesOfName = from file in files 
                                where file.StartsWith(orderFile)
                                && IsCompliant(file, dateFormat)
                                orderby file
                                select file ;
                
                // Add to the arranged files
                arrangedFiles.AddRange(filesOfName);
            }

            return arrangedFiles;
        }

        private DateFormatType GetDateFormatType(List<string> files) {
            // Counters on date types
            int hyphens = 0;
            int noSpaces = 0;

            // Iterate on files
            foreach (var file in files) {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var tokens = fileName.Split("-", 2);

                // Skip if there are no hyphens
                if (tokens.Length < 2) continue;

                // Skip if the date is missing/empty 
                if (tokens[1].Trim() == "") continue;

                var dateString = tokens[1];

                if (dateString.Contains("-"))
                    hyphens++;
                else 
                    noSpaces++;
            }

            // If the count is equal, prefer hyphens
            if (hyphens >= noSpaces)
                return DateFormatType.Hyphens;
            else if (hyphens < noSpaces)
                return DateFormatType.NoSpaces;
            else
                return DateFormatType.Hyphens;
        }

        private bool IsCompliant(string fileName, DateFormatType format) {
            if (fileName == null || fileName == "") return false;

            var fileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
            var fileNameExt = Path.GetExtension(fileName);

            // Check if file is .xml
            if (fileNameExt != ".xml") return false;

            // Check if there are no hyphens
            if (!(fileNameNoExt.Contains("-"))) return false;

            // Split the file name for further parsing
            var tokens = fileNameNoExt.Split("-", 2);

            // Skip if the date is missing/empty 
            if (tokens[1].Trim() == "") return false;

            var dateString = tokens[1];
            DateTime date;

            if (format == DateFormatType.Hyphens) {
                return DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, DateTimeStyles.None, out date);
            } else if (format == DateFormatType.NoSpaces) {
                return DateTime.TryParseExact(dateString, "yyyyMMdd", null, DateTimeStyles.None, out date);
            } else {
                return false;
            }
        }
    }

    public enum DateFormatType {
        Hyphens,
        NoSpaces
    }
}