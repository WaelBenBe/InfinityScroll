using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityScrollSimple.Services
{
    public interface ITodoService
    {
        Task<List<Todo>> GetDotos();
    }
}
