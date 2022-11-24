using EvaluationCase.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public List<string> ProductGroupCodes { get; set; }

    }
}
