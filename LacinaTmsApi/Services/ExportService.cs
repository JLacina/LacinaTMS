using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace LacinaTmsApi.Services
{
    //TODO add proper logging latter  
    public class ExportService<T> : IExportService<T> where T : class
    {
        public MemoryStream ConvertListToBytes(List<T> records)
        {
            try
            {
                var result = CsvWriteToMemoryBytes(records);
                return new MemoryStream(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private byte[] CsvWriteToMemoryBytes(List<T> records)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(streamWriter))
                {
                    csvWriter.WriteRecords(records);
                    streamWriter.Flush();
                    return memoryStream.ToArray();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
