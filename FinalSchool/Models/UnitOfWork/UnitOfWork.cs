using FinalSchool.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalSchool.Models.UnitOfWork
{
     class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext db;

        public UnitOfWork()
        {
            db = new ApplicationDbContext();
        }

        IGenaricRepository<teacher> _Teacher;
        IGenaricRepository<teacher> IUnitOfWork.Teacher
        {
            get{
                if (_Teacher==null)
                {
                    return new EfGenaricRepository<teacher>(db);
                }
                else
                {
                    return _Teacher;
                }

            }
        }

        IGenaricRepository<Supervisors> _supervisors;
        IGenaricRepository<Supervisors> IUnitOfWork.supervisors
        {
            get
            {
                if (_supervisors == null)
                {
                    return new EfGenaricRepository<Supervisors>(db);
                }
                else
                {
                    return _supervisors;
                }

            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}