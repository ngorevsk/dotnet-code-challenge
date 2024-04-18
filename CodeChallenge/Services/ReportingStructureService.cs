using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public ReportingStructure GetTotalNumberOfReports(Employee employee)
        {
            var reportingStructure = new ReportingStructure();

            var employeeLoaded = GetById(employee.EmployeeId); // ensure DR field is loaded
            reportingStructure.Employee = employeeLoaded;

            // recursively look up nested employees, keeping track of total under given employee
            int total = 0;
            var stack = new Stack<Employee>();
            stack.Push(employeeLoaded);
            var employeeDictionary = new Dictionary<string, Employee>(); // (employeeId, object)
            employeeDictionary.Add(employeeLoaded.EmployeeId, employeeLoaded);
            while (stack.Count > 0)
            {
                var stackEmployee = stack.Pop();
                stackEmployee = GetById(stackEmployee.EmployeeId); // ensure DR field is loaded

                for (int i = 0; i < stackEmployee.DirectReports.Count; i++)
                {
                    var underling = stackEmployee.DirectReports[i];
                    if (!employeeDictionary.ContainsKey(underling.EmployeeId)) // ensure each employee is counted only once
                    {
                        stack.Push(underling);
                        employeeDictionary.Add(underling.EmployeeId, underling);

                        total++; // count each underling
                    }
                }
            }
            reportingStructure.NumberOfReports = total;

            return reportingStructure;
        }
    }
}
