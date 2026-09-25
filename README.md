# Lager – produksjons- og lagersystem for Aga Sideri

Et system for å holde oversikt over alt som skjer på gården, fra eplene henger på treet til sideren står ferdig på lager. Kjerneideen er at **informasjonen skal følge sider-batchen**: hver batch spores gjennom hele produksjonen, uansett hvor mange ganger den flyttes, filtreres eller blandes. Målet er at man for enhver flaske kan se hvordan den ble laget, og for de beste siderne kan finne ut hva som gjorde dem gode.

På sikt skal systemet bruke kunstig intelligens til å analysere historikken og finne sammenhenger mellom råvarer, produksjon og kvalitet.

## Innhold

- [Bakgrunn](#bakgrunn)
- [Brukere og tilganger](#brukere-og-tilganger)
- [Brukervennlighet](#brukervennlighet)
- [Behov: produksjon og sporing](#behov-produksjon-og-sporing)
- [Behov: lager](#behov-lager)
- [Behov: renhold og vedlikehold](#behov-renhold-og-vedlikehold)
- [Merknader](#merknader)
- [Åpne spørsmål](#åpne-spørsmål)
- [Teknologi](#teknologi)
- [Utviklingsplan](#utviklingsplan)

---

## Bakgrunn

I dag er informasjonen uorganisert og fordelt på mange ulike program. De eksisterende løsningene er ofte laget for vinprodusenter og passer ikke godt til siderproduksjon.

Å lage en sider som smaker likt hvert år er krevende, fordi smaken påvirkes av svært mange faktorer. Det er nærmest umulig for én person å holde oversikt over alle, og i dag er det vanskelig å gå tilbake i historikken for å finne ut hvorfor en batch ble dårlig, eller hva som fungerte godt for en annen.

Sideriet mangler også et godt system for renhold og vedlikehold. Når en maskin sist ble vedlikeholdt, finnes i dag bare i hukommelsen til de ansatte. Bedre oversikt her kan på sikt også si noe om kvaliteten på sideren.

---

## Brukere og tilganger

| Rolle | Hvem | Hva de gjør |
|---|---|---|
| **Produksjon** | De som jobber i sideriet | Registrerer målinger, flyttinger, tilsetninger, pastorisering og tapping |
| **Kontor** | Administrasjon og innkjøp | Følger med på beholdning, varsler og varebehov, og håndterer innsatsvarer og bestillinger |
| **Admin** | Joar | Alt det over, og kan i tillegg endre tanker, resepter og PU-krav |

---

## Brukervennlighet

Systemet må være enkelt å bruke. Det skal først og fremst fungere godt på datamaskin, men på sikt også på nettbrett og mobil. Siden mye av registreringen skjer ute i produksjonen, er det viktig med et enkelt design, store knapper og mulighet for skanning.

---

## Behov: produksjon og sporing

### Innhold i hver batch

For hver batch skal man kunne registrere:

- **Epler:** sort, mengde og hvilket felt de kom fra
- **Gjær:** hvilken gjærtype som er brukt
- **Tilsetninger:** gjærnæring, smakstilsetninger (humle, bringebær, rips osv.) og annet, med mengde og tidspunkt. For smakstilsetninger også når de ble tatt ut, siden både mengde og tid påvirker smaken
- **Målinger over tid:** sukkerinnhold (måles manuelt med Anton Paar-instrument), temperatur, og eventuelt pH, alkoholprosent, syre og sulfitt
- **Smaksnotater** og en kvalitetsvurdering

### Sporing når sideren flyttes

**Problemet i dag:** Tankene styres av et eget program for temperaturstyring, som ikke er laget for å følge sideren mellom tanker. Når sideren for eksempel filtreres over i en ny tank, forsvinner koblingen til batchen den kom fra, og med den all historikk om epler, gjær og temperatur.

**Løsningen:** Batchen har sin egen identitet som beholdes uansett hvor den befinner seg. Tanken er bare et oppbevaringssted. Hver flytting registreres som en hendelse, for eksempel *«Batch 2026-07 flyttet fra tank 3 til tank 5, filtrert»*. Slik går historikken aldri tapt.

### Blandingssider

Innhold fra flere tanker kan slås sammen til en blandingssider. Den nye batchen fungerer som et slektstre: den har flere «foreldre», og man skal kunne gå bakover fra blandingen til hver av dem, med antall liter fra hver. Historikken fra alle batchene som inngår, følger dermed med inn i blandingen.

### Sporing med RFID og strekkode

Tankene er merket med RFID. Hver tapping får et eget **lotnummer** som trykkes som strekkode på etiketten eller kartongen. Lotnummeret peker til batchen, slik at man ved å skanne strekkoden kan spore historikken helt tilbake til eplene.

En batch kan tappes flere ganger, og hver tapping får da sitt eget lotnummer. Batchkoden identifiserer sideren i tanken, mens lotnummeret identifiserer én bestemt tapping.

### Finne problematiske tanker

En batch skal kunne merkes som dårlig, med en kommentar om hva som var galt. Systemet skal kunne vise hvilke tanker som går igjen i dårlige batcher, siden det kan tyde på at noe er galt med tanken. Hver tank får også en logg over vask og vedlikehold, siden det ofte er forklaringen når en tank gir problemer.

### Eplenes opprinnelse og modenhet

Systemet skal registrere hvilket felt eplene kommer fra, for eksempel Kallhagen eller Skjæret, slik at man kan se hvilken betydning plasseringen har for resultatet. Det skal være mulig å sammenligne gjæringskurven for epler fra ulike felt.

Modenheten måles i dag ved å ta prøver av sukker og stivelse, der stivelsesinnholdet viser hvor modent eplet er. Disse målingene skal kunne legges inn, slik at man kan undersøke om litt for modne eller litt umodne epler påvirker sideren.

### Gjær og temperatur

Ulike gjærtyper trives ved ulike temperaturer. Systemet skal vise hvordan valg av gjær og temperatur har påvirket resultatet tidligere.

Når sukker og stivelse er målt på en ny avling, er ønsket at systemet skal foreslå hvilken gjær, temperatur og gjæringstid som passer best, basert på hva som har fungert og ikke fungert før.

### Jord og vanning

Det står fuktighetsmålere i bakken som styrer automatisk vanning når jorda blir for tørr. Ved å koble disse dataene til produksjonen kan man se etter sammenhenger mellom forholdene i felten og kvaliteten på sideren. Et eksempel kan være at sommeren 2020 var tørr og at sideren fra 2021 ble spesielt god, selv om mange andre faktorer også spiller inn.

### Pastorisering

I dag logges bare temperatur og tid. Joar trenger i tillegg å vite hvor mange **pastoriseringsenheter (PU)** produktet har fått, for å være sikker på at det er godt nok pastorisert.

Systemet regner ut PU automatisk fra temperatur- og tidsloggen:

```
PU = minutter × 1,393^(temperatur − 60)
```

Bidraget summeres for hvert tidsintervall. Joar har satt grenseverdien til **50 PU**, og systemet skal varsle hvis en pastorisering ligger under dette.

### Analyse med kunstig intelligens

På sikt er ønsket å bruke kunstig intelligens til å gå gjennom all historikken og finne sammenhenger som ellers ville tatt mennesker uker å finne.

KI-en skal ikke bare finne sammenhenger, men også forklare dem, altså foreslå en teori om hvorfor noe har gitt god eller dårlig sider. For eksempel: batcher med epler fra et bestemt felt blir gjennomgående bedre, og en mulig forklaring er at eplene der modnes senere.

- **Funnene** skal alltid komme fra gårdens egne data og logger.
- **Forklaringene** kan bruke generell kunnskap om gjæring og sider, men skal tydelig merkes som mulige forklaringer, ikke fakta.

Målet er å kunne svare på hva som gjør en god sider, ved å se på blant annet felt, eplesort, modenhet, lagringstid før pressing, gjær, temperatur, gjæringstid og feil som ble gjort underveis.

---

## Behov: lager

### Lagerbeholdning og lagerplasser

Systemet skal vise beholdningen av både ferdige produkter og innsatsvarer (flasker, korker, etiketter, gjær, smakstilsetninger osv.), og hvilket lager og hvilken lagerplass de ligger på. Det skal varsles når en vare er under en valgt minimumsgrense.

### Beregning av varebehov

Man skal kunne oppgi hvor mange liter av en sider man vil lage, for eksempel rosé, og få vite hvilke varer som trengs og hvor mye, hva man allerede har, hvor det ligger, og hva som må bestilles.

Dette krever en **resept per produkt**, der hver innsatsvare er oppgitt per liter ferdig sider (for eksempel gram gjær per liter, eller én flaske og én kork per 0,75 liter). Systemet ganger opp resepten, sammenligner med lagerbeholdningen og lager en bestillingsliste.

### Plantegning av tankrommet

En oversikt over tankrommet som plantegning, som kan vises på en skjerm i sideriet. Tankene vises der de faktisk står, farget etter status (tom, gjærer, klar, dårlig). Ved å klikke på en tank ser man hvilken sider som ligger der, temperatur og siste målinger.

Hvert rom registreres med bredde og lengde i meter, og hver tank med posisjon og diameter.

---

## Behov: renhold og vedlikehold

*Disse behovene har lavere prioritet enn produksjon og lager.*

### Renhold og hygiene

I dag sendes prøver til et laboratorium for å undersøke om det finnes gjærpartikler eller bakterier. Systemet skal gjøre det mulig å registrere prøver tatt av tankene etter vask, for å se om det er bakterier igjen.

Resultatene skal kunne kobles til batchene, slik at man kan svare på spørsmål som *«Har renholdet påvirket denne batchen?»* eller *«Den ene tanken gir alltid dårlige batcher, skyldes det bakterier?»*. Tanker som ikke er vasket eller testet etter planen skal markeres tydelig i rødt.

### Vedlikehold av maskiner

Flere maskiner trenger jevnlig vedlikehold, som smøring og bytte av pakninger (tre ganger i året). Systemet skal holde oversikt over vedlikeholdsoppgaver, når de sist ble utført og når de skal gjøres neste gang, og varsle når noe er forfalt.

---

## Merknader

For at analysene skal gi pålitelige svar, må det være registrert data over flere sesonger. Sammenhenger systemet finner vil være forslag til hva som kan ha betydning, ikke bevis. Det er derfor viktig at data registreres så fullstendig og konsekvent som mulig fra starten av.

---

## Åpne spørsmål

Spørsmål som må avklares med Joar:

1. Hvilke RFID-lesere brukes, og kan de lese inn i en nettleser?
2. Hva skal strekkoden sitte på: hver flaske, hver kartong eller hver pall?
3. Hvilke målinger tas utover sukker, og hvor ofte?
4. Kan temperaturdata hentes ut fra det eksisterende temperaturprogrammet, og i hvilket format?
5. Kan data fra fuktighetsmålerne hentes ut automatisk?
6. Kan laboratorieresultatene fra renholdsprøvene leveres digitalt?
7. Stemmer referansetemperaturen (60 °C) og faktoren (1,393) i PU-formelen med det Joar bruker?
8. Hvilke gjærnæringsprodukter og andre tilsetninger brukes?

---

## Teknologi

- **Backend:** ASP.NET Core MVC (.NET 10), C#
- **Database:** SQLite med Entity Framework Core og migrasjoner
- **Arkitektur:** Repository-mønster med dependency injection
- **Frontend (planlagt):** React med TypeScript, blant annet for plantegningen


## Utviklingsplan

| Versjon | Innhold | Status |
|---|---|---|
| 1 | Produkter, innsatsvarer, lagerplasser og lagerbeholdning med varsler | Under arbeid |
| 2 | Innlogging og roller | Planlagt |
| 3 | Tanker, batcher, flyttinger, målinger, pastorisering og tapping med sporing | Planlagt |
| 4 | Varebehov, plantegning og React-frontend | Planlagt |
| 5 | Renhold, vedlikehold og KI-analyse | Planlagt |