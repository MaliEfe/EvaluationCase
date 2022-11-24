using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Interfaces.Repositories
{
    public interface IDatabaseSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }


    }
}
