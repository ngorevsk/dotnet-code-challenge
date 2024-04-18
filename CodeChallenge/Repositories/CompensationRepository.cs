using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            compensation.Id = Guid.NewGuid().ToString();

            // find matching existing employee by id and link it
            compensation.Employee = GetEmployeeByEmployeeId(compensation.Employee.EmployeeId);

            _employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        public Employee GetEmployeeByEmployeeId(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public IReadOnlyCollection<Compensation> GetCompensationByEmployeeId(string id)
        {
            return _employeeContext.Compensations.Where(c => c.Employee.EmployeeId == id).ToList().AsReadOnly();
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
