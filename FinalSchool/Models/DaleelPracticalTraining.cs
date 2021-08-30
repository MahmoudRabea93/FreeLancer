using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalSchool.Models
{
    public class DaleelPracticalTraining
    {
        public int ID { get; set; }
        public string Department { get; set; }
        [Required(ErrorMessage = " حقل التخصص مطلوب !")]
        public string Specialty { get; set; }
        public string YearOfTraining { get; set; }
        [Required(ErrorMessage = " حقل أسم / رمز المقرر مطلوب !")]
        public string  CourseName { get; set; }    
        public string TraningDepartmentName { get; set; }
        public string TheOverallGoalIsFromTraning { get; set; }
        public string CreditHours { get; set; }
        public string TheoreticalHours { get; set; }
        public string PracticalHours { get; set; }
        public string Term { get; set; }
        public bool Practical { get; set; }
        public int Type { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}