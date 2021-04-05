using System.Collections.Generic;
using System.IO;

namespace LacinaTmsApi.Services
{
    public interface IExportService<T>
    { 
        MemoryStream ConvertListToBytes(List<T> records);
    }
}
