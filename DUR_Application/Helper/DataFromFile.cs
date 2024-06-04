using DUR_Application.Entities;
using DUR_Application.Services.Services_Magazine.AddSpareParts;
using System.Text;

namespace DUR_Application.Helper
{
    public class DataFromFile
    {

        public static readonly string FilePath = @"C:\Users\jjmar\Desktop\Repository\NET_libraries\NET_Libraries\WriteToFileProgram\Test.txt";
        public static async Task<string[]> ReadFile(string path)
        {
            if(!File.Exists(path)) 
            {
                throw new FileNotFoundException("Not found File");
            }
            byte[] buffer; 
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                buffer = new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, (int)fs.Length);
            }

            var result = Encoding.ASCII.GetString(buffer).Split(new string[] { Environment.NewLine}, StringSplitOptions.None);

            return result;  
        } 


        public static IEnumerable<AddSparePartsDto> WriteToList(string [] array, int MagazineId)
        {
            var result = new List<AddSparePartsDto>();
            foreach (var item in array)
            {
                string[] elements = item.Split(", ", StringSplitOptions.None);
                result.Add(new AddSparePartsDto()
                {
                    Name = elements[0],
                    Description = elements[1],
                    Type = elements[2],
                    Price = decimal.Parse(elements[3]),
                    Count = int.Parse(elements[4]),
                    MagazineId = MagazineId
                });

            }

            return result;
        }
    }
}
