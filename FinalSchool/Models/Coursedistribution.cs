using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalSchool.Models
{
    public class Coursedistribution
    {
        public int ID { get; set; }
        public string Week { get; set; }
      
        public string Weekdate { get; set; }
        public string Content { get; set; }
        public bool Practical { get; set; }
        public int CourseID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}