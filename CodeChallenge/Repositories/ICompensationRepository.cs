using CodeChallenge.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Employee GetEmployeeByEmployeeId(string id);
        IReadOnlyCollection<Compensation> GetCompensationByEmployeeId(string id);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}