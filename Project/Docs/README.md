# GroupProject

### Minimum usable product idea 1 sisältää:
- Mahdollisuus hakea toimitukset suoraan kuljetusyrityksiltä.

### Aloitus ideat:
- Tutkinta jos kyseinen on mahdollista suorittaa URI informaation muokkaamisella
- Tutkinta onko mahdollista saada suora Widget haettua kuljetusyrityksiltä

### Muuta tehtävää ennenkuin voi aloittaa kirjoittamaan koodia
- Suunnitella pohja millaisen käyttöliittymän haluaa ekaksi sivuksi asiakkaalle
- Päättää miten haluamme että tieto kerätään käyttäjältä. (Onko se että käyttäjä asettaa lähetystunnuksen manuaalisesti vai hakeeko se suoraan sähköpostista
- SQLlite?
- Serverside blazor hybrid?
- MVP Malli suunnittelu
- Ensimmainen versio voi mahdollisesti olla vain konsoli versio
- Suunnittelu mitä kaikkea halutaan laittaa backendiin palvelumielessä
- Pitää varmistaa, että ei kerätä turhaa informaatiota liikaa. Eli jos haetaan tieto sähköpostista varmistetaan viestin pvm jotta ei lähetä etsimään liian vanhoja paketteja.

### Toiminta
#### miten hakea pakettien tiedot
Verrannollisesti "pakettiseuranta.fi" Avoimesti käyttää ideaa "https://{sivustojostapakettihaetaan}/seuranta{sivustonperusteellaolevaaloite}{KäännettyURIkomponentti}"  
Kyseisen sivun tracking.js tiedostosta voi käydä hakemsassa kyseisen logiikan ja kokeilla saada selville tarkalleen mitä tapahtuu. Sivustoon ei ole tehty sen suurempia defensiivisiä nimeämisiä tai muuta piilottelua joten se on vain kunhan lukaa.  

#### Kuinka usein haluamme päivittää tiedon per käyttäjä
Todellisuudessa olisi fiksua varmaan kerätä tunnin välein, joka voidaan tietona päivittää tietokantaan.

#### Tietokanta
Mitä tietoa haluamme kerätä tietokantaan?

Lähettäjä | Saaja | Sijainti | Tila ? Onko nämä riittävät vai onko nämä liikaa? Tarvitsemmeko tietokantaa kyseiseen?


## To-DO

- Exception luokka.
- CustomerHandling.
- Yritysten simulointi ilman API avainta.
- Blazor UI

### Clean architechture

[Arkkitehtuurista](https://github.com/gmagana/clean-architecture-example-csharp?tab=readme-ov-file)
