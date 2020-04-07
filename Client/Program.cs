using Client.Controllers;
using Client.Security;
using NDesk.Options;
using System;
using System.ServiceModel.Description;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //nastavení jazyku errorových hlášek na angličtinu
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            string serviceName = null;
            string inputFilename = null;
            string outputJSONRequestFilename = null;
            string outputJSONResponseFilename = null;

            //načtení hodnot z příkazové řádky
            OptionSet options = new OptionSet
            {
                { "operace=", operace => serviceName = operace },
                { "input=", input => inputFilename = input },
                { "outputJSONRequest=", outputJSONRequest => outputJSONRequestFilename = outputJSONRequest },
                { "outputJSONResponse=", outputJSONResponse => outputJSONResponseFilename = outputJSONResponse },
            };

            //zpracovnání
            options.Parse(args);

            ParamsItem paramsItem = new ParamsItem()
            {
                ServiceName = serviceName,
                InputFilename = inputFilename,
                OutputJSONRequestFilename = outputJSONRequestFilename,
                OutputJSONResponseFilename = outputJSONResponseFilename,
            };

            if (paramsItem.ServiceName == null)
            {
                Console.WriteLine("Nezadán parameter service");
                PrintHelp();
                return;
            }

            CallService(paramsItem);

            //Console.ReadKey();
        }

        /// <summary>
        /// Zavolá service dle parametru paramsItem.ServiceName
        /// <paramref name="paramsItem">Parametry z příkazové řádky</paramref>
        /// </summary>
        private static void CallService(ParamsItem paramsItem)
        {
            AisgService aisgApiClient = new AisgService(paramsItem);

            InitEndpoint(aisgApiClient.AisgPortClient.Endpoint);

            //test|overeni|zmenaUdaju|hromadneOvereni|vysledekHromadnehoOvereni

            switch (paramsItem.ServiceName.ToLower())
            {
                case ("overitosobu"):
                    aisgApiClient.OveritOsobu();
                    break;
                case ("overitosobyhromadne"):
                    aisgApiClient.OveritOsobyHromadne();
                    break;
                case ("ziskatvysledkyoveritosobyhromadne"):
                    aisgApiClient.ZiskatVysledkyOveritOsobyHromadne();
                    break;
                case ("zmenitudajeosoby"):
                    aisgApiClient.ZmenitUdajeOsoby();
                    break;
                case ("test"):
                    aisgApiClient.Test();
                    break;
                default:
                    Console.WriteLine("Špatně zadaná služba");
                    PrintHelp();
                    break;
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Parametry:");
            Console.WriteLine("\t--operace=Test|OveritOsobu|ZmenitUdajeOsoby|OveritOsobyHromadne|ZiskatVysledkyOveritOsobyHromadne");
            Console.WriteLine("\t--input=cesta k souboru - JSON vstupni parametry operace");
            Console.WriteLine("\t--outputJSONRequest=cesta k souboru pro uložení dat volání - default: Konzole");
            Console.WriteLine("\t--outputJSONResponse=cesta k souboru pro uložení vrácených dat - default: Konzole");
            Console.WriteLine("\n\n");
        }

        private static void InitEndpoint(ServiceEndpoint endpoint)
        {
            var credentials = new MyClientCredentials();
            // nastaveni certifikatu pro podepisovani a komunikaci
            CertificateManager.For(credentials).SetUpCertificates();

            //  prirazeni nasich client credentials s potrebnymi certifikaty do endpoint behaviors
            endpoint.Behaviors.Remove(typeof(ClientCredentials));
            endpoint.Behaviors.Add(credentials);
        }
    }
}
