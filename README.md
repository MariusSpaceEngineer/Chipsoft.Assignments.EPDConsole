### **Introductie**

### Dit document beschrijft de designkeuzes die zijn gemaakt bij de ontwikkeling van de console-applicatie. De gekozen architectuur is gebaseerd op de SOLID-principes, die ervoor zorgen dat de code onderhoudbaar, uitbreidbaar en testbaar blijft. Door het toepassen van deze principes worden de verantwoordelijkheden binnen de applicatie duidelijk gescheiden, wat leidt tot een robuust en flexibel systeem.

### **1\. Single Responsibility Principle (SRP)**

Elke klasse zou maar één reden tot verandering moeten hebben, wat betekent dat deze maar één taak of verantwoordelijkheid heeft.

* **Program Class:**  
   De `Program`\-klasse is verantwoordelijk voor het coördineren van de applicatiestroom en het afhandelen van gebruikersinteracties. Deze klasse delegeert specifieke taken aan services en helpers, waardoor SRP wordt nageleefd.

* **Service-interfaces en implementaties:**  
   Elke service-interface (`IPatientService`, `IPhysicianService`, `IAppointmentService`) definieert operaties die betrekking hebben op een specifieke entiteit. De implementaties van deze services handelen de businesslogica voor elke entiteit af.

* **Modellen:**  
   Elk model (`Patient`, `Physician`, `Appointment`) vertegenwoordigt een enkele entiteit in het systeem en bevat validatielogica die specifiek is voor die entiteit.

* **ConsoleHelper:**  
   Deze klasse is verantwoordelijk voor alle console-gerelateerde operaties, zoals het verkrijgen van gebruikersinvoer en het weergeven van berichten.  
  ---

### **2\. Open/Closed Principle (OCP)**

Software-entiteiten moeten open zijn voor uitbreiding, maar gesloten voor aanpassing.

* **Service-interfaces:**  
   Het gebruik van interfaces maakt het mogelijk om het systeem uit te breiden met nieuwe implementaties zonder bestaande code te wijzigen.

* **Validatie-attributen:**  
   Aangepaste validatie-attributen zoals `FutureDateAttribute` kunnen worden uitgebreid met nieuwe validatieregels zonder de bestaande attributen te wijzigen.

### **3\. Liskov Substitution Principle (LSP)**

Objecten van een superclass moeten vervangbaar zijn door objecten van een subclass zonder dat dit de juistheid van het programma beïnvloedt.

* **Service-interfaces:**  
   Elke implementatie van de service-interfaces (`IPatientService`, `IPhysicianService`, `IAppointmentService`) kan zonder problemen worden gebruikt, waardoor de correctheid van het programma behouden blijft.

* **Modellen en validatie:**  
   De modellen en hun validatie-attributen voldoen aan LSP doordat elke afgeleide klasse of uitgebreide validatielogica de bestaande functionaliteit niet breekt.  
  ---

### **4\. Interface Segregation Principle (ISP)**

Clients zouden niet gedwongen moeten worden om afhankelijk te zijn van interfaces die zij niet gebruiken.

* **Service-interfaces:**  
   De service-interfaces zijn specifiek ontworpen voor elke entiteit (`IPatientService`, `IPhysicianService`, `IAppointmentService`). Dit zorgt ervoor dat clients alleen afhankelijk zijn van de interfaces die relevant zijn voor hun behoeften.  
  ---

### **5\. Dependency Inversion Principle (DIP)**

Hoog-niveau modules zouden niet afhankelijk moeten zijn van laag-niveau modules. Beide moeten afhankelijk zijn van abstracties.

* **Dependency Injection:**  
   Het project maakt gebruik van Dependency Injection (DI) om afhankelijkheden te beheren. De `Program`\-klasse is afhankelijk van abstracties (service-interfaces) in plaats van concrete implementaties. De `Startup`\-klasse configureert de DI-container en de `ServiceProvider` lost de afhankelijkheden op.  
  ---

### **Conclusie**

De beschreven designkeuzes dragen bij aan een robuuste, onderhoudbare en uitbreidbare applicatie. Door het toepassen van de SOLID-principes zijn de verantwoordelijkheden binnen de applicatie duidelijk gescheiden, wat het eenvoudiger maakt om wijzigingen door te voeren en nieuwe functionaliteiten toe te voegen zonder de bestaande code te breken.