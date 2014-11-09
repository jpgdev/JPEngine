using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters;

namespace JPEngine.Utils
{
    public static class JsonHelper
    {       
        public static string SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple });
        }

        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(
                json,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
        }
        
        
        public static T LoadFromFile<T>(string path)
        {
            //TODO: Validate the path before creating the file

            StreamReader file = new StreamReader(path);
            string json = file.ReadToEnd();

            return DeserializeObject<T>(json);
        }

        public static void SaveToFile<T>(T obj, string path)
        {
            string json = SerializeObject<T>(obj);
            
            //TODO: Validate the path before creating the file

            StreamWriter file = new StreamWriter(path);
            file.WriteLine(json);
            file.Close();
        }







    }
}
