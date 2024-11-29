using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Repository.IRepository
{
    public interface IArticuloRepository: IRepository<Articulo>
    {
        void Update(Articulo articulo);
        IQueryable<Articulo> AsQueryable();
    }
}
