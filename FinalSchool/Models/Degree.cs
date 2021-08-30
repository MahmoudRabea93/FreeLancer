using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalSchool.Models
{
    public class Degree
    {
        public int ID { get; set; }
        public int QuarterlyDegree { get; set; }
        public int TheoreticalQuarterlyDegree { get; set; }
        public int PracticalQuarterlyDegree { get; set; }
        public int FinalyDegree { get; set; }
        public int TheoreticalFinalyDegree { get; set; }
        public int PracticalFinalyDegree { get; set; }
        public int CourseID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}