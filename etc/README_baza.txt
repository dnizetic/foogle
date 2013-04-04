Da detaljnije opisem logiku baze i samog progmrama i da dogovorimo jos detalj ili dva...

STATUS - mora biti
__________________
A - admin
P- predavac (ne budemo radili razliku izmedu profesora i asistenta, osim ako inzistirate)
S -student

ZVANJE
______
id (autonumber odnosno serial)
naziv
kategorija - ovo se odnosi na kategoriju kojom se osoba bavi. Ljudi se uglavnom
	     specijaliziraju za neke stvari i rijetko kad su eksperti za vise kategorija

nazivi i pocetni rejting za skillove iz njihove kategorije su sljedeci:
_______________________________________________________________________
1. pristupnik			200
2. strucni pristupnik		200
3. dipl. ing.			200  (alternativa 300)
4. magistar struke		400
5. magistar znanosti		400  (alternativa 500)
6. doktor			600
7. docent			800
8. profesor			1000


KATEGORIJA ima vise SKILLOVA, a SKILL moze pripadati u vise kategorija. (to je valjda jasno)
Nema potrebe da mi sad unosimo sve podatke, samo neke za testiranje - ostalo nek si unose sami,
bitno da su ovi tocni (koje mi unesemo). Moramo se raspitati kaj s tim kategorijama, posebno u 
predmetima tipa matematika, gospodarstvo, organizacija i sl. Mozda bi mogli na konzultacije, svaki
kod nekog:)?

OCJENE
______
Kao sto vidite, nema te tablice, zato jer je nepotrebna. OCjene su od 1 do 5, s tim da je 1 najvisa,
a 5 najniza ocjena 

REJTING
________
Svaki korisnik kad se prijavi upisuje koje skillove ima. 



INICIJALNO DODJELJIVANJE REJTINGA
__________________________________
Za sve skillove koji spadaju u njegovu kategoriju
trigger mu automatski dodjeljuje rejting sukladno njegovom zvanju i gornjim bodovima. Za sve koji NE spadaju 
u njegovu kategoriju, dobiva 1 bod (zbog lakseg izracuna bodova preporuka). Nakon inicijalnog dodavanja bodova
rejting se moze povezati samo preporukama ili promjenom zvanja.

AZURIRANJE ZVANJA
__________________
Kod prelaska iz zvanja u vise zvanje (doktor->docent), rejting se automatski povecava za razliku bodova (800-600=200).
PROBLEMI KOD AŽURIRANJA OCJENE


PREPORUKE
__________
Preporuke korisnika koji nemaju rejting iz nekog skila se prakticki ne boduju, ali se ipak spremaju (mislim,
boduju se, ali ti bodovi ne znace bas previse (kaj ako netko ima 1 bod...)) i kasnije se moze sortirati prema
broju preporuka.

Inace, rejting za skill za koji je netko preporucen se racuna 

broj bodova preporucitelja
__________________________
ocjena*100	          

(znaci, npr. ja imam za msql rejting 200 jer sam upravo diplomirala. Rabuzin ima 1000 jer je profesor. On me preporuci
sa ocjenom 2 za msql i meni se racuna povecanje rejtinga ( 1000/(2*100)=5 bodova) IAKO JE TO NEZGODNO JER JE IZMEDU
1 I 2 RAZLIKA OD 5 BODOVA A PREMA 5 SE RAZLIKA SVE VISE SMANJUJE  -  MORAMO NACI REALNIJU FORMULU A DA NE   UKLJUCUJE ISTE BODOVE U
(problemi kod azuriranja)


PITANJA
*********************
Azurirati ocjene ili ne?
_______________________
Mozemo napraviti trigger koji nakon svake preporuke odnosno azuriranja rejtinga automatski azurira sve ocjene koje je korisnik
dodijelio drugim korisnicima kroz preporuke. Jasno vam je da nakon kaj se napuni baza to tece SPORO.

Dodati nkj tipa "promjena_zaposlenja" da znamo dal neko hoce promijenit zaposlenje il ne?

 


