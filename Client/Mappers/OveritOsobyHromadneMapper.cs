using Client.Service;
using System.Linq;

namespace Client.Controllers
{
    public class OveritOsobyHromadneMapper
    {
        private OveritOsobyHromadneItem OveritOsobyHromadneItem { get; set; }
        private OveritOsobyHromadneRequest OveritOsobyHromadneRequest { get; set; }
        public OveritOsobyHromadneMapper(OveritOsobyHromadneItem item)
        {
            OveritOsobyHromadneItem = item;
        }

        public OveritOsobyHromadneRequest Map()
        {
            OveritOsobyHromadneRequest = new OveritOsobyHromadneRequest()
            {
                ICO_VCP = OveritOsobyHromadneItem.icovcp,
                CisloPozadavku = OveritOsobyHromadneItem.cisloPozadavku,
                Osoby = new OsobaKOvereniType[OveritOsobyHromadneItem.osoby.osobaKOvereni.Count]
            };

            for (int i = 0; i < OveritOsobyHromadneItem.osoby.osobaKOvereni.Count; i++)
            {
                var item = OveritOsobyHromadneItem.osoby.osobaKOvereni[i];
                OveritOsobyHromadneRequest.Osoby[i] = new OsobaKOvereniType()
                {
                    IdentifikaceZaznamu = item.identifikaceZaznamu,
                    Osoba = new OsobaType()
                    {
                        Jmeno = item.osoba.jmeno,
                        Prijmeni = item.osoba.prijmeni,
                        DatumNarozeni = item.osoba.datumNarozeni,
                        RodnePrijmeni = item.osoba.rodnePrijmeni,
                        StatniObcanstvi = item.osoba.statniObcanstvi,
                        MistoNarozeni = new MistoNarozeniType(),
                        TrvalyPobyt = new TrvalyPobytType()
                    }
                };

                if(item.osoba.mistoNarozeni?.mistoNarozeniCR != null)
                {
                    OveritOsobyHromadneRequest.Osoby[i].Osoba.MistoNarozeni.MistoNarozeniCR = new MistoNarozeniCRType
                    {
                        Obec = item.osoba.mistoNarozeni?.mistoNarozeniCR?.obec,
                        Okres = item.osoba.mistoNarozeni?.mistoNarozeniCR?.okres
                    };
                }
                else if(item.osoba.mistoNarozeni?.mistoNarozeniSvet != null)
                {
                    OveritOsobyHromadneRequest.Osoby[i].Osoba.MistoNarozeni.MistoNarozeniSvet = new MistoNarozeniSvetType
                    {
                        Misto = item.osoba.mistoNarozeni?.mistoNarozeniSvet?.misto,
                        Stat = item.osoba.mistoNarozeni?.mistoNarozeniSvet?.stat
                    };
                }
                else if(item.osoba.mistoNarozeni?.mistoNarozeniKod.HasValue ?? false)
                {
                    OveritOsobyHromadneRequest.Osoby[i].Osoba.MistoNarozeni.MistoNarozeniKod = item.osoba.mistoNarozeni?.mistoNarozeniKod ?? -1;
                    OveritOsobyHromadneRequest.Osoby[i].Osoba.MistoNarozeni.MistoNarozeniKodSpecified = item.osoba.mistoNarozeni.mistoNarozeniKod.HasValue;
                }


                if(item.osoba.trvalyPobyt?.trvalyPobytCR != null)
                {
                    OveritOsobyHromadneRequest.Osoby[i].Osoba.TrvalyPobyt.TrvalyPobytCR = new TrvalyPobytCRType
                    {
                        Obec = item.osoba.trvalyPobyt?.trvalyPobytCR?.obec,
                        CisloObvodMestaPrahy = item.osoba.trvalyPobyt?.trvalyPobytCR?.cisloObvodMestaPrahy,
                        CisloOrientacni = item.osoba.trvalyPobyt?.trvalyPobytCR?.cisloOrientacni,
                        CisloOrientacniDodatek = item.osoba.trvalyPobyt?.trvalyPobytCR?.cisloOrientacniDodatek,
                        CisloPopisneEvidencni = item.osoba.trvalyPobyt?.trvalyPobytCR?.cisloPopisneEvidencni,
                        ObecCast = item.osoba.trvalyPobyt?.trvalyPobytCR?.obecCast,
                        PSC = item.osoba.trvalyPobyt?.trvalyPobytCR != null ? item.osoba.trvalyPobyt.trvalyPobytCR.psc : -1,
                        Ulice = item.osoba.trvalyPobyt?.trvalyPobytCR?.ulice
                    };
                }
                else if (item.osoba.trvalyPobyt?.trvalyPobytSvet != null)
                {
                    OveritOsobyHromadneRequest.Osoby[i].Osoba.TrvalyPobyt.TrvalyPobytSvet = new TrvalyPobytSvetType
                    {
                        CisloOrientacni = item.osoba.trvalyPobyt?.trvalyPobytSvet?.cisloOrientacni,
                        Ulice = item.osoba.trvalyPobyt?.trvalyPobytSvet?.ulice,
                        CisloOrientacniDodatek = item.osoba.trvalyPobyt?.trvalyPobytSvet?.cisloOrientacniDodatek,
                        CisloPopisneEvidencni = item.osoba.trvalyPobyt?.trvalyPobytSvet?.cisloPopisneEvidenci,
                        Obec = item.osoba.trvalyPobyt?.trvalyPobytSvet?.obec,
                        ObecCast = item.osoba.trvalyPobyt?.trvalyPobytSvet?.obecCast,
                        PSC = item.osoba.trvalyPobyt?.trvalyPobytSvet?.psc,
                        Stat = item.osoba.trvalyPobyt?.trvalyPobytSvet?.stat
                    };
                }
                else if(item.osoba.trvalyPobyt?.trvalyPobytKod.HasValue ?? false)
                {
                    OveritOsobyHromadneRequest.Osoby[i].Osoba.TrvalyPobyt.TrvalyPobytKod = item.osoba.trvalyPobyt?.trvalyPobytKod ?? -1;
                    OveritOsobyHromadneRequest.Osoby[i].Osoba.TrvalyPobyt.TrvalyPobytKodSpecified = item.osoba.trvalyPobyt.trvalyPobytKod.HasValue;
                }
            }

            return OveritOsobyHromadneRequest;
        }
    }
}
