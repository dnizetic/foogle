Da detaljnije opišem logiku baze i samog progmrama i da dogovorimo još detalj ili dva...

STATUS - može biti
__________________
A - admin
P- predavaè (ne budemo radili razliku izmeðu profesora i asistenta, osim ako inzistirate)
S -student

ZVANJE
______
id (autonumber odnosno serial)
naziv
kategorija - ovo se odnosi na kategoriju kojom se osoba bavi. Ljudi se uglavnom
	     specijaliziraju za neke stvari i rijetko kad su eksperti za više kategorija

nazivi i poèetni rejting za skillove iz njihove kategorije su sljedeæi:
_______________________________________________________________________
1. pristupnik			200
2. struèni pristupnik		200
3. dipl. ing.			200  (alternativa 300)
4. magistar struke		400
5. magistar znanosti		400  (alternativa 500)
6. doktor			600
7. docent			800
8. profesor			1000


KATEGORIJA ima više SKILLOVA, a SKILL može pripadati u više kategorija. (to je valjda jasno)
Nema potrebe da mi sad unosimo sve podatke, samo neke za testiranje - ostalo nek si unose sami,
bitno da su ovi toèni (koje mi unesemo). Moramo se raspitati kaj s tim kategorijama, posebno u 
predmetima tipa matematika, gospodarstvo, organizacija i sl. Možda bi mogli na konzultacije, svaki
kod nekog:)?

OCJENE
______
Kao što vidite, nema te tablice, zato jer je nepotrebna. OCjene su od 1 do 5, s tim da je 1 najviša,
a 5 najniža ocjena 

REJTING
________
Svaki korisnik kad se prijavi upisuje koje skillove ima. 



INICIJALNO DODJELJIVANJE REJTINGA
__________________________________
Za sve skillove koji spadaju u njegovu kategoriju
trigger mu automatski dodjeljuje rejting sukladno njegovom zvanju i gornjim bodovima. Za sve koji NE spadaju 
u njegovu kategoriju, dobiva 1 bod (zbog lakšeg izraèuna bodova preporuka). Nakon inicijalnog dodavanja bodova
rejting se može poveæati samo preporukama ili promjenom zvanja.

AŽURIRANJE ZVANJA
__________________
Kod prelaska iz zvanja u više zvanje (doktor->docent), rejting se automatski poveæava za razliku bodova (800-600=200).
Ako korisnik želi ažurirati ocjenu iz preporuke, a u meðuvremenu se dogodila promjena zvanja, ocjena se oduzima 
od nove ocjene i bodovi se ažuriraju za pozitivnu ili negativnu razliku.
(Npr. meni Schatten da 5 za baze kao magistar struke i onda doktorira i hoæe mi sniziti ocjenu na 4. Oduzima se 4-5=-1
1 se množi sa njegovim trenutnim bodovima (npr. 600/100) i zbraja mojem rejtingu za baze (-1*6=-6 bodova))
Ako nije došlo do promjene zvanja, onda se radi to isto samo kaj bodovi ostaju isti ko i za inicijalnu ocjenu (to je valjda jasno)

PREPORUKE
__________
Preporuke korisnika koji nemaju rejting iz nekog skila se praktièki ne boduju, ali se ipak spremaju (mislim,
boduju se, ali ti bodovi ne znaèe baš previše (kaj ako netko ima 1 bod...)) i kasnije se može sortirati prema
broju preporuka.

Inaèe, rejting za skill za koji je netko preporuèen se raèuna 

broj bodova preporuèitelja
__________________________
ocjena*100	          

(znaèi, npr. ja imam za msql rejting 200 jer sam upravo diplomirala. Rabuzin ima 1000 jer je profesor. On me preporuèi
sa ocjenom 2 za msql i meni se raèuna poveæanje rejtinga ( 1000/(2*100)=5 bodova) IAKO JE TO NEZGODNO JER JE IZMEÐU
1 I 2 RAZLIKA OD 5 BODOVA A PREMA 5 SE RAZLIKA SVE VIŠE SMANJUJE  -  MORAMO NAÆI REALNIJU FORMULU A DA NE UKLJUÈUJE ÈISTE BODOVE
(problemi kod ažuriranja)


PITANJA
*********************
Ažurirati ocjene ili ne?
_______________________
Možemo napraviti trigger koji nakon svake preporuke odnosno ažuriranja rejtinga automatski ažurira sve ocjene koje je korisnik
dodijelio drugim korisnicima kroz preporuke. Jasno vam je da nakon kaj se napuni baza to teèe SPORO.

Dodati nkj tipa "promjena_zaposlenja" da znamo dal neko hoæe promijenit zaposlenje il ne?

 


