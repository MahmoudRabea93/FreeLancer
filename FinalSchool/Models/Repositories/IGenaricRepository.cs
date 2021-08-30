using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSchool.Models.Repositories
{
    interface IGenaricRepository <T> where T : class
    {
        IEnumerable<T> List();

        T Find(int? id);

        T Display(int? id);

        void Add(T EntityToAdd);

        void Update(int id, T EntityToUpdate);

        void Delete(int id,T EntityToDelete);
    }
}
