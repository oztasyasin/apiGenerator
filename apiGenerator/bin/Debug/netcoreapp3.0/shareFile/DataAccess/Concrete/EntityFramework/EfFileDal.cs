using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFileDal:EfEntityRepositoryBase<File,SharefileContext>, IFileDal
    {
        
    }
}
