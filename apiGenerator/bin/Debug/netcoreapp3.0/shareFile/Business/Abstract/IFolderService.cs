using ShareFile.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFolderService
    {
        List<Folder> GetAll();
        Folder Get(int id);
	    void Add(Folder folder);
        void Update(Folder folder);
        void Delete(Folder folder);

    }
}
