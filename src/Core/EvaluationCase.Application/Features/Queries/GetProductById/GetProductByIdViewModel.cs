using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Features.Queries.GetProductById
{
    public class GetProductByIdViewModel
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string BrandCode { get; set; }
        public decimal Price { get; set; }
        public List<string> ProductGroupCodes = new List<string>();
    }
}
