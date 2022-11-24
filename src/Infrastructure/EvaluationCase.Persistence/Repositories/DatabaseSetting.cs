using EvaluationCase.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Persistence.Repositories
{
    public class DatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }
}
