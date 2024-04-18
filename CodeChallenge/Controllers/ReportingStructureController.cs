using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingStructure")]
    public class ReportingStructure : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructure(ILogger<ReportingStructure> logger, IEmployeeService employeeService, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{id}", Name = "getTotalReportsByEmployeeId")]
        public IActionResult GetTotalReportsByEmployeeId(String id)
        {
            _logger.LogDebug($"Received reportingstructure get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();
            
            var reportingStructure = _reportingStructureService.GetTotalNumberOfReports(employee);            

            return Ok(reportingStructure);
        }

    }
}
