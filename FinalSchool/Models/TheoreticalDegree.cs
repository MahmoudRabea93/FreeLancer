using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalSchool.Models
{
    public class TheoreticalDegree
    {
        public int ID { get; set; }
        public int Theoreticaltotal { get; set; }
        public int FinalDegree { get; set; }
        public int CourseID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}