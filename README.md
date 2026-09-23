# Sider Lager

## Hovedprinsipp: alt skal følge sideren

Systemet skal ikke bare være et lager. Det viktigste er at all informasjon følger sideren gjennom hele produksjonen, uansett hvor mange ganger den flyttes, filtreres eller blandes. Målet er at man for enhver flaske kan se hvordan den ble laget, og for de beste siderne kan finne ut hva som gjorde dem gode.


## Dagens problem
Vanskelig å gå tilbake i historikk for å finne ut når det gikk galt for en bach, eller hva som fungerte godt for en annen bach. 


## Ønsket funksjoner

**Innhold i bach**
For hver batch skal man kunne registrere hvilke epler som er brukt (sort, mengde og gjerne hvilket felt), hvilken gjær som er brukt, hvilke næringsstoffer som er tilsatt og når, temperaturforløpet under gjæringen, sukkerinnhold over tid, og smaksnotater.

**Sukkerinnhold**
Sukkerinnholdet måles manuelt med et Anton Paar-instrument og skal kobles til batchen som ligger i tanken.

**Oversikt over tanker**
I dag brukes et eget program til temperaturstyring av tankene, men det er ikke laget for å følge sideren når den flyttes fra én tank til en annen. Når sideren for eksempel filtreres over i en ny tank, forsvinner koblingen til hvilken batch den kom fra, og dermed også historikken om gjær, epler og temperatur.
Batchen har sin egen id, flytting registreres som en hendelse («batch 2026-07 flyttet fra tank 3 til tank 5, filtrert»). 

**Pastorisering av most**
I dag logges bare temperatur og tid. Joar trenger i tillegg å vite hvor mange pastoriseringsenheter (PU) sideren har fått, slik at han kan være sikker på at den faktisk er godt nok pastorisert. Systemet kan da regne ut PU automatisk fra temperatur- og tidsloggen og varsle hvis en batch ligger under en valgt grenseverdi. 
PU = minutter × 1,393^(temperatur − 60)

- Referansetemperatur, faktor og hvilken minimumsverdi de vil kreve må avklares med Joar, siden dette kan variere.

**Blandingssider**
Innholdet fra flere tanker kan flyttes sammen og brukes i en blandingssider. Historikken fra alle batchene som inngår må da følge med over i den nye blandingen.

En blanding fungerer som et slektstre, den nye batchen har flere «foreldre», og man skal kunne gå bakover fra blandingen til hver av dem, med andel eller antall liter fra hver. 

**Andre målinger ???**
Hvilke andre målinger de tar, for eksempel pH, alkoholprosent, syre eller sulfitt, og hvor ofte. Da kan målingene lagres i én felles målingstabell med type, verdi, tidspunkt og hvem som målte, i stedet for et eget felt for hver.

**Sporing med FID og strekkode**
Tankene er merket med RFID. Man skal kunne skanne en strekkode og spore historikken tilbake. Hvis flere dårlige sidere har vært innom samme tank, kan det tyde på at noe er galt med tanken, så systemet må kunne vise dette.

- Det bør avklares hva strekkoden sitter på: hver flaske, hver kartong eller hver pall.
Det vanligste er at hver tapping får et batch-nummer som trykkes på etiketten eller kartongen, og at batch-nummeret peker til batchen. Man bør også kunne merke en batch som «dårlig» med en kommentar, slik at systemet kan finne tanker som går igjen i dårlige batcher. Tanken kan i tillegg få sin egen logg over vask og vedlikehold, som ofte er forklaringen når en tank gir problemer.

**Lager**
Man må vite hvor sideren og varene er lagret, altså hvilket lager eller hvilken lagerplass.

**Beregning av varebehov**
Man skal kunne oppgi hvor mange liter av en sider man vil lage, for eksempel rosé, og få vite hvilke varer som trengs og hvor mye, hva man allerede har, hvor det ligger, og hva som må bestilles.

- Dette krever en resept per produkt, der hvert råstoff og hver innsatsvare er oppgitt per liter ferdig sider (for eksempel gram gjær per liter, en flaske og en kork per 0,75 liter). Systemet ganger opp resepten, sammenligner med lagerbeholdningen og lager en bestillingsliste. Det er som å skalere en kakeoppskrift fra én kake til tjue og så sjekke kjøleskapet før du går på butikken.

**Plantegning**
Hvert rom har bredde og lengde i meter, og hver tank har en posisjon (X og Y) og en diameter i meter. Da kan React tegne rommet som en SVG, omtrent som setekartet når man bestiller kinobilletter, med tankene som sirkler farget etter status (tom, gjærer, klar, dårlig). Et klikk på en tank viser batchen og siste målinger. Dette passer perfekt i React-delen, og posisjonene kan du fylle inn allerede nå ved å måle opp tankrommet.



## Kravspesifikasjoner

**mobil-app**
Siden mye av registreringen skjer ute i produksjonen, bør systemet fungere godt på mobil eller nettbrett, med store knapper og mulighet for skanning.

**Nettside til å ha på skjerm i sideriet**
En oversikt, en plantegnign, der man kan se hvor de ulike tankene er. Der det er lett å klikke på for å finne ut hvilken sider som er der, temperatur og annen informasjon. 


## Målgruppe

**Produksjon**
Produksjon registrerer målinger, flyttinger, tilsetninger, pastorisering og tapping, og bør få en enkel mobilvennlig visning med store knapper.

**Kontor**
Kontor ser beholdning, varsler og varebehov, og håndterer innsatsvarer og bestillinger. 

**Admin**
Admin (Joar) kan i tillegg endre ting som tanker, resepter og PU-krav.






## spørsmål

- Hvilke RFID-lesere bruker de, og kan de lese inn i en nettleser? 
- Hvilke målinger tas utover sukker, og hvor ofte?