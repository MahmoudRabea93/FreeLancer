using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalSchool.Models
{
    public class Evaluation
    {
        public int ID { get; set; }
        [Required(ErrorMessage = " حقل عدد الخانات المدمجة مطلوب !")]
        public int CellCount { get; set; }
        public string DegreeRating { get; set; }
        public string RatingType { get; set; }
        public bool Practical { get; set; }
        public int CourseID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}