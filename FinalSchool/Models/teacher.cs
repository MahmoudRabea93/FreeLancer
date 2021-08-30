using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalSchool.Models
{
    public class teacher
    {
        public int ID { get; set; }
        public int Serial { get; set; }
        public string teacherName { get; set; }
        public string TrainingDivision  { get; set; }
        public string Signature { get; set; }
        public bool Practical { get; set; }
        public int CourseID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}