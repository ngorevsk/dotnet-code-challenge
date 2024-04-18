﻿using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        Employee GetById(String id);
        ReportingStructure GetTotalNumberOfReports(Employee employee);
    }
}
