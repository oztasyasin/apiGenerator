using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core;

namespace Entities.Concrete
{
    public class File : IEntity
    {
        public int İd { get; set; }
		public string Name { get; set; }
		
    }
}