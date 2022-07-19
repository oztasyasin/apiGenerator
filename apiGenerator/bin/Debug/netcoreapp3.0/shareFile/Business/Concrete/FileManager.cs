using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public  class FileManager : IFileService
    {
        private IFileDal _fileDal;

        public FileManager(IFileDal fileDal)
        {
            _fileDal = fileDal;
        }

        public void Add(File file)
        {
            _fileDal.Add(file);
        }

        public void Delete(File file)
        {
            _fileDal.Delete(file);
        }

        public List<File> GetAll()
        {
           return _fileDal.GetList();
        }
	    public File Get(int id)//
	    {
	        return _filedal.Get(p => p.Id == id);
	    }
        public void Update(File file)
        {
            _fileDal.Update(file);
        }

    }
}
