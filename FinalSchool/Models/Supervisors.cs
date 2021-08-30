using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalSchool.Models
{
    public class Supervisors
    {
        public int ID { get; set; }
        public string SupervisorsName { get; set; }
        public string RepresentativeName { get; set; }
        public string CoordinatorName { get; set; }
        public string SupervisorsSignature { get; set; }
        public string RepresentativSignature { get; set; }
        public string CoordinatorSignature { get; set; }
        public int CourseID { get; set; }
        public bool IsDeleted { get; set; }
        
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}