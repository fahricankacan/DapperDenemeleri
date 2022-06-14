using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDenemeleri.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IDbTransaction DbTransaction { get; set; }
    }
}
