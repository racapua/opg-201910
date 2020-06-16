using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using opg_201910_interview.Models;

namespace opg_201910_interview.Tests
{
    [TestClass]
    public class ClientTests
    {
        private readonly Client _client;

        public ClientTests()
        {
            _client = new Client();
        }

        [TestMethod]
        public void Client_ShouldArrangeByName()
        {
            _client.FileOrder = new List<string>{"apple", "squirrel", "acorn", "pine"};

            // File names for testing
            var files = new List<string>{"apple-2019-10-13.xml", "acorn-2020-01-09.xml", 
            "pine-2019-09-17.xml", "apple-2020-06-12.xml", "squirrel-2020-02-20.xml"};

            // Expected file order
            var expectedFiles = new List<string>{"apple-2019-10-13.xml", "apple-2020-06-12.xml",
            "squirrel-2020-02-20.xml", "acorn-2020-01-09.xml", "pine-2019-09-17.xml"};

            var newFiles = _client.Arrange(files);

            Assert.IsTrue(newFiles.SequenceEqual(expectedFiles));
        }

        [TestMethod]
        public void Client_ShouldArrangeByDate()
        {
            _client.FileOrder = new List<string>{"apple"};

            // File names for testing
            var files = new List<string>{"apple-2019-10-13.xml", "apple-2020-01-09.xml", 
            "apple-2019-09-17.xml", "apple-2020-06-12.xml", "apple-2020-02-20.xml"};

            // Expected file order
            var expectedFiles = new List<string>{"apple-2019-09-17.xml", "apple-2019-10-13.xml", 
            "apple-2020-01-09.xml", "apple-2020-02-20.xml", "apple-2020-06-12.xml"};

            var newFiles = _client.Arrange(files);

            Assert.IsTrue(newFiles.SequenceEqual(expectedFiles));
        }

        [TestMethod]
        public void Client_ShouldRemoveInvalid()
        {
            _client.FileOrder = new List<string>{"apple"};

            // File names for testing
            var files = new List<string>{"apple-2019-10-13.xml", "apple-2020.xml", 
            "apple-2019-09-17.xlsx", "2020-06-12.xml", "mango-2020-02-20.xml"};

            // Expected file order
            var expectedFiles = new List<string>{"apple-2019-10-13.xml"};

            var newFiles = _client.Arrange(files);

            Assert.IsTrue(newFiles.SequenceEqual(expectedFiles));
        }

        [TestMethod]
        public void Client_ShouldRemoveDifferentDateFormat() 
        {
            _client.FileOrder = new List<string>{"apple"};

            // File names for testing
            var files = new List<string>{"apple-2019-10-13.xml", "apple-20200109.xml", 
            "apple-20190917.xml", "apple-2020-06-12.xml", "apple-2020-02-20.xml"};

            // Expected file order
            var expectedFiles = new List<string>{"apple-2019-10-13.xml", "apple-2020-02-20.xml", 
            "apple-2020-06-12.xml"};

            var newFiles = _client.Arrange(files);

            Assert.IsTrue(newFiles.SequenceEqual(expectedFiles));
        }

        [TestMethod]
        public void Client_ShouldUseHyphensByDefault() 
        {
            _client.FileOrder = new List<string>{"apple"};

            // File names for testing
            var files = new List<string>{"apple-2019-10-13.xml", "apple-20200109.xml"};

            // Expected file order
            var expectedFiles = new List<string>{"apple-2019-10-13.xml"};

            var newFiles = _client.Arrange(files);

            Assert.IsTrue(newFiles.SequenceEqual(expectedFiles));
        }
    }
}
