using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortalBusinessLayer.Interfaces
{
    public interface IAuth
    {
        public Task<Employee> Login(string email,string password);
    }
}
