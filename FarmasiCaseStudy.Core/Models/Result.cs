using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.Core.Models
{
    public class Result
    {
        public Result()
        {
            ResultType = true;
        }
        public bool ResultType { get; set; }
        public string Message { get; set; }
    }
    public class GetOneResult<TEntity> : Result where TEntity : class, new()
    {
        public TEntity Entity { get; set; }
    }
    public class GetManyResult<TEntity> : Result where TEntity : class, new()
    {
        public IEnumerable<TEntity> ResultList { get; set; }
    }
    public class GetOneResultObject<T> : Result
    {
        public T ObjectResult { get; set; }
    }
}
