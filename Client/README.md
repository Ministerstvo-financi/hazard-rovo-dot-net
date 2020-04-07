# Referenční klient v technologii .NET
Jedná se o konzolovou .NET aplikaci (.NET Framework 4.8), která umožňuje jednotlivě zavolat všechny služby pro provozovatele:
- Test - TestRequest
- OveritOsobu - OveritOsobuRequest
- ZmenitUdajeOsoby - ZmenitUdajeOsobyRequest
- OveritOsobyHromadne - OveritOsobyHromadneRequest
- ZiskatVysledkyOveritOsobyHromadne – ZiskatVysledkyOveritOsobyHromadneRequest

Aplikace naváže spojení s HTTPs serverem, podepíše a odešle zprávu a zkontroluje podpis přijaté zprávy.<br/>
Číslo požadavku se generuje pro každý požadavek zvlášť a ICO_OVP se plní z konfigurace.

## Kompilace
```
Kompilaci provádět v nástroji VisualStudio (vyvíjeno na verzi 2019) - stačí verze Community Edition. Nutné mít nainstalovaný .NET Framework ve verzi 4.8.

## Knihovny
```
Klient využívá pouze jedné knihovny třetích stran: Newtonsoft.Json verze 12.0.3 - pod licencí MIT

```
## Spuštění

```
HazardRefClient.exe --operace=Test
```

## Logování komunikace

```
V defaultním nastavení se vám do c:\Temp\TracingAndLogging-client.svclog ukládají kompletní logy komunikace klienta a serveru. 
Umístění i název souboru lze změnit v konfiguraci (soubor App.config).

## Parametry
```
--operace=Test|OveritOsobu|ZmenitUdajeOsoby|OveritOsobyHromadne|ZiskatVysledkyOveritOsobyHromadne
--outputJSONRequest=cesta k souboru pro uložení dat volání - deafult - System.out
--outputJSONResponse=cesta k souboru pro uložení vrácených dat - deafult - System.out
```
## Příklad
```
overeni.json:
{
  "duvod": "Registrace",
  "osoba": {
    "jmeno": "RADKA",
    "prijmeni": "RANDÁČKOVÁ",
        "datumNarozeni": "1980-12-13T00:00:00.000+0100",
    "statniObcanstvi": "CZ",
    "mistoNarozeni": {
      "mistoNarozeniCR": {
        "obec": "Dýšina",
        "okres": "Plzeň-město"
      }
    }
  }
}

output.json:
{
  "cisloPozadavku": "973c2689-0b3f-4843-b7cb-5e9683ccec8b",
  "identifikace": "2019-12-17T17:24:48.402|RADKA|RANDÁČKOVÁ|1980-12-13+01:00",
  "hid": "b4e13dce-b2fc-4779-b953-3e9534a7cbb7",
  "plnoleta": "ANO",
  "nalezenaROB": "NENALEZENA",
  "nalezenaRVO": "NE"
}
```