﻿using OracleHR.Models.dbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OracleHR.Repository.interfaces
{
    public interface IJobRepository
    {
        Task<List<Job>> GetJobsAsync();
    }
}
