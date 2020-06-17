using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using opg_201910_interview.Models;

namespace opg_201910_interview.Services
{
    public class UploadService
    {
        public List<string> GetClientFiles(Client client) {
            
            var clientFiles = new List<string>();

            if (client != null) {
                // Get file names
                var files = Directory.GetFiles(client.FileDirectoryPath, "*.xml")
                                    .Select(file => Path.GetFileName(file)).ToList();

                // Arrange file names
                clientFiles = client.Arrange(files);
            }

            return clientFiles;
        }
    }
}