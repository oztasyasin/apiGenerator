using ShareFile.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFileService
    {
        List<File> GetAll();
        File Get(int id);
	    void Add(File file);
        void Update(File file);
        void Delete(File file);

    }
}
