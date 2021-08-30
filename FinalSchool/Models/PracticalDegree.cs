using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalSchool.Models
{
    public class PracticalDegree
    {
        public int ID { get; set; }
        public int TheoreticalTest { get; set; }
        public int PracticalTest { get; set; }
        public int CourseID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}