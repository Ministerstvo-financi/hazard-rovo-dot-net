using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Client.Controllers
{
    public class FileSerializer
    {
        private Logger logger { get; set; }

        public FileSerializer()
        {
            logger = new Logger();
        }

        public void SaveAsJson(object myObject, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    file.Write(convertObjectToJsonString(myObject));
                    file.Flush();
                }
            }
            else
                Console.WriteLine(convertObjectToJsonString(myObject));
        }

        public void SaveAsXml(object myObject, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    XmlSerializer writer = new XmlSerializer(myObject.GetType());
                    writer.Serialize(fileStream, myObject);
                    fileStream.Flush();
                }
            }
            else
                logger.PrintObjectLikeXml(myObject);
        }

        private string convertObjectToJsonString(object myObject)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(myObject, serializerSettings);
        }
    }
}
