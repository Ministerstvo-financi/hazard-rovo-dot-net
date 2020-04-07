using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Client.Controllers
{
    public class Logger 
    {
        public void PrintObjectLikeJson(object myObject)
        {
            string json = JsonConvert.SerializeObject(myObject);
            Console.WriteLine(json + "\r\n");
        }

        public void PrintObjectLikeXml(object myObject)
        {
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(myObject.GetType());
                serializer.Serialize(stringwriter, myObject);
                Console.WriteLine(stringwriter.ToString() + "\r\n");
            }
        }
    }
}
