using System;
using System.Collections.Generic;

namespace Client.Controllers
{
    public class OveritOsobyHromadneItem
    {
        public string cisloPozadavku { get; set; }
        public string icovcp { get; set; }
        public Osoby osoby { get; set; }
    }

    public class MistoNarozeniCR
    {
        public string obec { get; set; }
        public string okres { get; set; }
    }

    public class MistoNarozeni
    {
        public int? mistoNarozeniKod { get; set; }
        public MistoNarozeniCR mistoNarozeniCR { get; set; }
        public MistoNarozeniSvet mistoNarozeniSvet { get; set; }
    }

    public class MistoNarozeniSvet
    {
        public string misto { get; set; }
        public string stat { get; set; }
    }

    public class TrvalyPobytCR
    {
        public string ulice { get; set; }
        public string cisloPopisneEvidencni { get; set; }
        public string cisloOrientacni { get; set; }
        public string cisloOrientacniDodatek { get; set; }
        public string cisloObvodMestaPrahy { get; set; }
        public string obecCast { get; set; }
        public string obec { get; set; }
        public int psc { get; set; }
    }

    public class TrvalyPobyt
    {
        public int? trvalyPobytKod { get; set; }
        public TrvalyPobytCR trvalyPobytCR { get; set; }
        public TrvalyPobytSvet trvalyPobytSvet { get; set; }
    }

    public class TrvalyPobytSvet
    {
        public string cisloOrientacni { get; set; }
        public string ulice { get; set; }
        public string cisloOrientacniDodatek { get; set; }
        public string cisloPopisneEvidenci { get; set; }
        public string obec { get; set; }
        public string obecCast { get; set; }
        public string psc { get; set; }
        public string stat { get; set; }
    }

    public class Osoba
    {
        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public string rodnePrijmeni { get; set; }
        public DateTime datumNarozeni { get; set; }
        public string statniObcanstvi { get; set; }
        public MistoNarozeni mistoNarozeni { get; set; }
        public TrvalyPobyt trvalyPobyt { get; set; }
    }

    public class OsobaKOvereni
    {
        public string identifikaceZaznamu { get; set; }
        public Osoba osoba { get; set; }
    }

    public class Osoby
    {
        public List<OsobaKOvereni> osobaKOvereni { get; set; }
    }
}
