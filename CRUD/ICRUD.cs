using System.Collections.Generic;
using WindowsFormsApp1.Tables;

namespace WindowsFormsApp1.CRUD
{
    internal interface ICRUD<T> where T : IBaseID
    {
        IEnumerable<T> GetAll();

        void Update(T element);

        void Insert(T element);

        void Delete(int id);

        int CheckForID(int id);

        bool DuplicateData(T element);

    }
}