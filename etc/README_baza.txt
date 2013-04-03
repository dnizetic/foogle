Da detaljnije opi�em logiku baze i samog progmrama i da dogovorimo jo� detalj ili dva...

STATUS - mo�e biti
__________________
A - admin
P- predava� (ne budemo radili razliku izme�u profesora i asistenta, osim ako inzistirate)
S -student

ZVANJE
______
id (autonumber odnosno serial)
naziv
kategorija - ovo se odnosi na kategoriju kojom se osoba bavi. Ljudi se uglavnom
	     specijaliziraju za neke stvari i rijetko kad su eksperti za vi�e kategorija

nazivi i po�etni rejting za skillove iz njihove kategorije su sljede�i:
_______________________________________________________________________
1. pristupnik			200
2. stru�ni pristupnik		200
3. dipl. ing.			200  (alternativa 300)
4. magistar struke		400
5. magistar znanosti		400  (alternativa 500)
6. doktor			600
7. docent			800
8. profesor			1000


KATEGORIJA ima vi�e SKILLOVA, a SKILL mo�e pripadati u vi�e kategorija. (to je valjda jasno)
Nema potrebe da mi sad unosimo sve podatke, samo neke za testiranje - ostalo nek si unose sami,
bitno da su ovi to�ni (koje mi unesemo). Moramo se raspitati kaj s tim kategorijama, posebno u 
predmetima tipa matematika, gospodarstvo, organizacija i sl. Mo�da bi mogli na konzultacije, svaki
kod nekog:)?

OCJENE
______
Kao �to vidite, nema te tablice, zato jer je nepotrebna. OCjene su od 1 do 5, s tim da je 1 najvi�a,
a 5 najni�a ocjena 

REJTING
________
Svaki korisnik kad se prijavi upisuje koje skillove ima. 



INICIJALNO DODJELJIVANJE REJTINGA
__________________________________
Za sve skillove koji spadaju u njegovu kategoriju
trigger mu automatski dodjeljuje rejting sukladno njegovom zvanju i gornjim bodovima. Za sve koji NE spadaju 
u njegovu kategoriju, dobiva 1 bod (zbog lak�eg izra�una bodova preporuka). Nakon inicijalnog dodavanja bodova
rejting se mo�e pove�ati samo preporukama ili promjenom zvanja.

A�URIRANJE ZVANJA
__________________
Kod prelaska iz zvanja u vi�e zvanje (doktor->docent), rejting se automatski pove�ava za razliku bodova (800-600=200).
PROBLEMI KOD AŽURIRANJA OCJENE


PREPORUKE
__________
Preporuke korisnika koji nemaju rejting iz nekog skila se prakti�ki ne boduju, ali se ipak spremaju (mislim,
boduju se, ali ti bodovi ne zna�e ba� previ�e (kaj ako netko ima 1 bod...)) i kasnije se mo�e sortirati prema
broju preporuka.

Ina�e, rejting za skill za koji je netko preporu�en se ra�una 

broj bodova preporu�itelja
__________________________
ocjena*100	          

(zna�i, npr. ja imam za msql rejting 200 jer sam upravo diplomirala. Rabuzin ima 1000 jer je profesor. On me preporu�i
sa ocjenom 2 za msql i meni se ra�una pove�anje rejtinga ( 1000/(2*100)=5 bodova) IAKO JE TO NEZGODNO JER JE IZME�U
1 I 2 RAZLIKA OD 5 BODOVA A PREMA 5 SE RAZLIKA SVE VI�E SMANJUJE  -  MORAMO NA�I REALNIJU FORMULU A DA NE UKLJU�UJE �ISTE BODOVE
(problemi kod a�uriranja)


PITANJA
*********************
A�urirati ocjene ili ne?
_______________________
Mo�emo napraviti trigger koji nakon svake preporuke odnosno a�uriranja rejtinga automatski a�urira sve ocjene koje je korisnik
dodijelio drugim korisnicima kroz preporuke. Jasno vam je da nakon kaj se napuni baza to te�e SPORO.

Dodati nkj tipa "promjena_zaposlenja" da znamo dal neko ho�e promijenit zaposlenje il ne?

 


