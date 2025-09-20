using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalDataLayer;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortalBusinessLayer.Implementations
{
    public class Auth : IAuth
    {
        private DatabaseContext _db;
        public Auth(DatabaseContext db)
        {
            _db = db;
        }
        public static byte[] HashPassword(string password)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<Employee> Login(string email, string password)
        {
            byte[] passwordBytes = HashPassword(password);
            using (var connection = _db.CreateConnection())
            {
                
                var employee = await connection.QueryFirstAsync<Employee>("SELECT * FROM Employees where Email=@email and password=@password", new { email=email,password= passwordBytes });
                if (employee == null) return null;
                var role = await connection.QueryFirstAsync<string>("select RoleName from Roles join EmployeeRoleData on Roles.Id=EmployeeRoleData.RoleId where EmployeeRoleData.EmployeeId=@empid", new { empid = employee.EmployeeId });
                employee.Role = role;
                return employee;
            }
        }
    }
}
