using Common;
using System.Collections.Generic;

namespace Services.Services
{
    public class ContainerResult<TOutput>
    {
        public TOutput Output { get; set; }
        public List<Error> ErrorList { get; set; } = new List<Error>();
    }
}
