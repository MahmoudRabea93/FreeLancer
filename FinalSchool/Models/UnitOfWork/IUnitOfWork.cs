using FinalSchool.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSchool.Models.UnitOfWork
{
    interface IUnitOfWork
    {
        IGenaricRepository<teacher> Teacher { get; }
        IGenaricRepository<Supervisors> supervisors { get; }
        void Save();    //Commite
    }
}
