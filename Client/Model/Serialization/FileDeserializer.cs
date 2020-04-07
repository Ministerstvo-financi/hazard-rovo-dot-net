using Newtonsoft.Json;
using System;
using System.IO;

namespace Client.Controllers
{
    /**
    * trida slouzi k nacitani vstupnich souboru a ukladani vystupnich souboru
    */
    public class FileDeserializer
    {
        private readonly ParamsItem paramsItem;
        // slouzi k nacitani a ukladani dat do z json souboru 
        private readonly JsonSerializer jsonSerializer;


        public FileDeserializer(ParamsItem paramsItem)
        {
            this.paramsItem = paramsItem;
            jsonSerializer = new JsonSerializer();
        }

        public Service.OveritOsobuRequest GetRequestOveritOsobu()
        {
            Service.OveritOsobuRequest overitOsobu = null;
            string filename = paramsItem.InputFilename;

            if (filename == null)
                filename = Properties.Resources.OveritOsobuJson;

            try
            {
                using (StreamReader file = File.OpenText(filename))
                {
                    overitOsobu = (Service.OveritOsobuRequest)jsonSerializer.Deserialize(file, typeof(Service.OveritOsobuRequest));
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }

            return overitOsobu;
        }

        public Service.OveritOsobyHromadneRequest GetRequestOveritOsobyHromadne()
        {
            OveritOsobyHromadneItem overitOsobuHromadneItem = null;
            Service.OveritOsobyHromadneRequest overitOsobuHromadneRequest = null;
            string filename = paramsItem.InputFilename;

            if (filename == null)
                filename = Properties.Resources.OveritOsobyHromadneJson;
            
            try
            {
                using (StreamReader file = File.OpenText(filename))
                {
                    overitOsobuHromadneItem = (OveritOsobyHromadneItem)jsonSerializer.Deserialize(file, typeof(OveritOsobyHromadneItem));
                    OveritOsobyHromadneMapper mapper = new OveritOsobyHromadneMapper(overitOsobuHromadneItem);
                    overitOsobuHromadneRequest = mapper.Map();
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }

            return overitOsobuHromadneRequest;
        }

        public Service.ZiskatVysledkyOveritOsobyHromadneRequest GetRequestVysledekHromadnehoOvereni()
        {
            Service.ZiskatVysledkyOveritOsobyHromadneRequest ziskatVysledkyOveritOsobyHromadne = null;
            string filename = paramsItem.InputFilename;

            if (filename == null)
                filename = Properties.Resources.ZiskatVysledkyOveritOsobyHromadneJson;

            try
            {
                using (StreamReader file = File.OpenText(filename))
                {
                    ziskatVysledkyOveritOsobyHromadne = (Service.ZiskatVysledkyOveritOsobyHromadneRequest)jsonSerializer.Deserialize(file, typeof(Service.ZiskatVysledkyOveritOsobyHromadneRequest));
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }

            return ziskatVysledkyOveritOsobyHromadne;
        }

        public Service.ZmenitUdajeOsobyRequest GetRequestZmenitUdajeOsoby()
        {
            Service.ZmenitUdajeOsobyRequest zmenitUdajeOsoby = null;
            string filename = paramsItem.InputFilename;

            if (filename == null)
                filename = Properties.Resources.ZmenitUdajeOsobyJson;

            try
            {
                using (StreamReader file = File.OpenText(filename))
                {
                    zmenitUdajeOsoby = (Service.ZmenitUdajeOsobyRequest)jsonSerializer.Deserialize(file, typeof(Service.ZmenitUdajeOsobyRequest));
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }

            return zmenitUdajeOsoby;
        }

        public Service.BaseRequestType GetRequestTest()
        {
            Service.BaseRequestType testType = null;
            string filename = paramsItem.InputFilename;

            if (filename == null)
                filename = Properties.Resources.OveritOsobuJson;

            try
            {
                using (StreamReader file = File.OpenText(filename))
                {
                    testType = (Service.BaseRequestType)jsonSerializer.Deserialize(file, typeof(Service.BaseRequestType));
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }

            return testType;
        }
    }
}
