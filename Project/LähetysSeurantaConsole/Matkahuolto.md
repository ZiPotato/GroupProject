# YLEISTÄ 
Tässä dokumentissa kuvataan Matkahuollon seurantatietohaun rajapinta. Rajapinnan 
avulla Matkahuollon pakettipalveluiden asiakkaat voivat hakea lähetystensä tiedot 
Matkahuollon palvelusta. Asiakas voi tallettaa tiedot omaan järjestelmäänsä tai 
näyttää ne omalla seurantasivullaan omille asiakkailleen. 
Rajapintaan tehtävät kyselyt tapahtuvat ns. REST-kutsuina http-metodilla GET. 
Palvelun käyttöönotto vaatii, että Matkahuollon myyjä avaa asiakkaan 
asiakasnumerolle käyttöoikeuden palveluun. Tunnus ja salasana asetetaan http
kyselyn basic authentication –tietoihin. 
Tuotantokyselyt lähetetään osoitteeseen  
https://extservices.matkahuolto.fi/mpaketti/public/tracking 
Testausta varten on osoite  
https://extservicestest.matkahuolto.fi/mpaketti/public/tracking 

## SANOMAKUVAUKSET 
### Kysely 
GET mpaketti/public/tracking?ids=<id1,id2,…>&from=<date>&to=<date> 
Parametrit 
1. ids = pilkulla eroteltu lista lähetys- tai pakettinumeroita. Listassa voi olla max. 10 tunnusta. 
2. from = aikaleima, mitä uudemmat tapahtumat haetaan. Arvon tietotyyppi on dateTime, esimerkiksi 2018-01-11T11:47:30. 
3. to = aikaleima, mitä vanhemmat tapahtumat haetaan. Arvon tietotyyppi on dateTime, esimerkiksi 2018-01-11T13:47:30. 

Joko id tai vähintään toinen aikaleimaparametreista on pakollinen. Mikäli mitään 
näistä ei anneta, palvelu palauttaa vastauksena http-status 400 Bad request. 
Vastaussanoman muodon oletustyyppi on XML. Mikäli vastaussanoman tyypiksi 
halutaan JSON, se tapahtuu http:n Accept –headerin avulla (Accept: application/json). 
