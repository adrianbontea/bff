using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.InputPorts
{
    public interface IBffService
    {
        Task<Output> InteractAsync(Input input);
    }
}
