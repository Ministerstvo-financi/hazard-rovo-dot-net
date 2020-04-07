using Client.Service;
using System;
using System.Configuration;
using System.ServiceModel;

namespace Client.Controllers
{
    public class AisgService
    {
        public readonly AisgPortClient AisgPortClient;
        private readonly FileDeserializer fileDeserializer;
        private readonly FileSerializer fileSerializer;
        private readonly ParamsItem paramsItem;

        public AisgService(ParamsItem paramsItem)
        {
            fileDeserializer = new FileDeserializer(paramsItem);
            fileSerializer = new FileSerializer();
            AisgPortClient = new AisgPortClient("AisgPortImplPort");
            this.paramsItem = paramsItem;
        }

        public void OveritOsobu()
        {
            if(AisgPortClient != null)
            {
                try
                {
                    var request = fileDeserializer.GetRequestOveritOsobu();
                    request.CisloPozadavku = Guid.NewGuid().ToString();
                    request.ICO_VCP = ConfigurationManager.AppSettings.Get("icovcp").ToString();

                    fileSerializer.SaveAsJson(request, paramsItem.OutputJSONRequestFilename);

                    var response = AisgPortClient.OveritOsobu(request);
                    fileSerializer.SaveAsJson(response, paramsItem.OutputJSONResponseFilename);
                }
                catch (FaultException<ErrorMessageType> errMsg)
                {
                    Console.WriteLine($"{errMsg.Detail.Kod}: {errMsg.Detail.Popis}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void OveritOsobyHromadne()
        {
            if (AisgPortClient != null)
            {
                try
                {
                    var request = fileDeserializer.GetRequestOveritOsobyHromadne();
                    request.CisloPozadavku = Guid.NewGuid().ToString();
                    request.ICO_VCP = ConfigurationManager.AppSettings.Get("icovcp").ToString();

                    fileSerializer.SaveAsJson(request, paramsItem.OutputJSONRequestFilename);

                    var response = AisgPortClient.OveritOsobyHromadne(request);
                    fileSerializer.SaveAsJson(response, paramsItem.OutputJSONResponseFilename);
                }
                catch (FaultException<ErrorMessageType> errMsg)
                {
                    Console.WriteLine($"{errMsg.Detail.Kod}: {errMsg.Detail.Popis}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ZiskatVysledkyOveritOsobyHromadne()
        {
            if (AisgPortClient != null)
            {
                try
                {
                    var request = fileDeserializer.GetRequestVysledekHromadnehoOvereni();
                    request.CisloPozadavku = Guid.NewGuid().ToString();
                    request.ICO_VCP = ConfigurationManager.AppSettings.Get("icovcp").ToString();

                    fileSerializer.SaveAsJson(request, paramsItem.OutputJSONRequestFilename);

                    var response = AisgPortClient.ZiskatVysledkyOveritOsobyHromadne(request);
                    fileSerializer.SaveAsJson(response, paramsItem.OutputJSONResponseFilename);
                }
                catch (FaultException<ErrorMessageType> errMsg)
                {
                    Console.WriteLine($"{errMsg.Detail.Kod}: {errMsg.Detail.Popis}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ZmenitUdajeOsoby()
        {
            if (AisgPortClient != null)
            {
                try
                {
                    var request = fileDeserializer.GetRequestZmenitUdajeOsoby();
                    request.CisloPozadavku = Guid.NewGuid().ToString();
                    request.ICO_VCP = ConfigurationManager.AppSettings.Get("icovcp").ToString();

                    fileSerializer.SaveAsJson(request, paramsItem.OutputJSONRequestFilename);

                    var response = AisgPortClient.ZmenitUdajeOsoby(request);
                    fileSerializer.SaveAsJson(response, paramsItem.OutputJSONResponseFilename);
                }
                catch (FaultException<ErrorMessageType> errMsg)
                {
                    Console.WriteLine($"{errMsg.Detail.Kod}: {errMsg.Detail.Popis}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Test()
        {
            if (AisgPortClient != null)
            {
                try
                {
                    BaseRequestType type = new BaseRequestType
                    {
                        CisloPozadavku = Guid.NewGuid().ToString(),
                        ICO_VCP = ConfigurationManager.AppSettings.Get("icovcp").ToString()
                    };

                    fileSerializer.SaveAsJson(type, paramsItem.OutputJSONRequestFilename);

                    var response = AisgPortClient.Test(type);
                    fileSerializer.SaveAsJson(response, paramsItem.OutputJSONResponseFilename);
                }
                catch (FaultException<ErrorMessageType> errMsg)
                {
                    Console.WriteLine($"{errMsg.Detail.Kod}: {errMsg.Detail.Popis}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
