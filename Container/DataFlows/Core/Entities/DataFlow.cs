using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class DataFlow
    {
        public int Id { get; set; }

        public DateTime SubmissionDate { get; set; }

        public DataType DataType { get; set; }

        public string DestinationHost { get; set; }
    }
}