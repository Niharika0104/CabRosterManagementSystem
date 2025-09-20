using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZelisCabPortalBusinessLayer.Interfaces;
using ZelisCabPortalDataLayer;
using ZelisCabPortalDataLayer.Models;

namespace ZelisCabPortalBusinessLayer.Implementations
{
    public class EmployeeImplementation : IEmployee
    {
        private DatabaseContext _db;
        public EmployeeImplementation(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                using (var connection = _db.CreateConnection())
                {

                    var employee = await connection.QueryFirstAsync<Employee>("SELECT * FROM Employees where EmployeeId=@id", new {id=id});
                    
                    return employee;
                }
            }
            catch (Exception)
            {

            }
            return new Employee();
        }
    }
}
