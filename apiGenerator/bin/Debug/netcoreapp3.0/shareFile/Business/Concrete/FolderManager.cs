using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public  class FolderManager : IFolderService
    {
        private IFolderDal _folderDal;

        public FolderManager(IFolderDal folderDal)
        {
            _folderDal = folderDal;
        }

        public void Add(Folder folder)
        {
            _folderDal.Add(folder);
        }

        public void Delete(Folder folder)
        {
            _folderDal.Delete(folder);
        }

        public List<Folder> GetAll()
        {
           return _folderDal.GetList();
        }
	    public Folder Get(int id)//
	    {
	        return _folderdal.Get(p => p.Id == id);
	    }
        public void Update(Folder folder)
        {
            _folderDal.Update(folder);
        }

    }
}
