using System;
using System.Collections.Generic;

namespace opg_201910_interview.Models
{
    public class Client
    {
        public List<string> FileOrder;
        public string ClientID;
        public string ClientName;

        public List<string> Arrange(List<string> files) {
            return new List<string>{};
        }

        private DateFormatType GetDateFormatType(List<string> files) {
            return DateFormatType.NoSpaces;
        }

    }

    public enum DateFormatType {
        Hyphens,
        NoSpaces
    }
}