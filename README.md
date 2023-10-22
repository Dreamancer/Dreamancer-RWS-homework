## Vypracované zadanie od RWS
### Autor: Jakub Horniak <horniakj@gmailcom>

### Toto repo obsahuje:
- #### Folder s pôvodným zadaním:
  - Pôvodný chybný kód v súbore **BuggyProgram.cs** s mojimi poznámkami o chybách ako komentáre (v angličtine)
  - 'Opravený' kód v súbore **CorrectedProgram.cs** kde som sa pokúsil o aspoň workable kód
  - Pdf so zadaním
- #### Moje riešenie zadania - Solution **Moravia.Homework** obsahujúcu 2 projekty:
 
  - **Moravia.Homework** - .NET 6 Konzolová aplikácia založená na moduloch poskytovaných pomocou dependency injection a nastavovaná pomocou konfiguračného json súboru.
    
    - O IO operácie sa starajú triedy implementujúce rozhranie *Moravia.Homework.DAL.IDocumentRepo*/abstraktnú triedu *Moravia.Homework.DAL.DocumentRepoBase*
      - Podľa zadania som implementoval jeden typ modulu pre prácu so súborovými systémami *Moravia.Homework.DAL.FileSystemDocumentRepo*, no nie je problém pridať iné typy implementovaním IDocumentRepo/DocumentRepoBase
      - O tvorbu týchto modulov sa stará *Moravia.Homework.DAL.Factory.DocumentRepoFactory* ktoré ich tvorí pomocou nastavení poskytnutých z konfiguračného json súboru. Táto factory je poskytovaná pomocou DI.

    - O serializáciu sa starajú triedy implementujúce rohranie *Moravia.Homework.Serialization.IDocumentSerializer*/abstraktnú triedu *Moravia.Homework.Serialization.DocumentSerializerBase*
      - Obdobne som implementoval serializáciu json a xml formátov pomocou tried *Moravia.Homework.Serialization.JsonDocumentSerializer* a *Moravia.Homework.Serialization.XmlDocumentSerializer*, no tiež nie je problém pridať iné formáty implementovaním IDocumentSerializer/DocumentSerializerBase
      - O tvorbu týchto modulov sa stará továreň *Moravia.Homework.Serialization.Factory.DocumentSerializerFactory* ktorá ich obdobne tvorí pomocou nastavení poskytnutých z konfiguračného json súboru a je poskytovaná pomocou DI.

    - Triedy dediace z *Moravia.Homework.Models.IDocument* predstavujú modely/šablóny serializovaných dokumentov
    - Aplikácia používa Serilog na logovanie INF + ERR úrovňe na štandartný konzolový výstup a INF + DBG + ERR úrovňe do log súborov. Serilog je nastavobaný zo samostatného (priložebého) json súboru, no aplikácia obsahuje aj default inicializáciu logeru v prípade žiadnych nastavení.
    - Nastavenie aplikácie **appsettings.json**:
      - **ConvertorAppSettings** obsahuje 2 objekty **Input** a **Output** rovnakého typu (*DataFormatConversionSettings*). Tieto obsahujú nastavenie pre repository obsahujúce zdrojový/cieľový súbor a serializačné nastavenia.
        - **SerializerTypeName** Meno triedy serializéru, ktorý chceme použiť. Tiež nastavuje typ formátu serializovaného súboru. Môže byť *(potenciálne zatiaľ)* *JsonDocumentSerializer* alebo *XmlDocumentSerializer*.
        - **DocumentTypeName** Meno modelu serializovaného dokumentu. Môže byť *(potenciálne zatiaľ)* len *BaseDocument*.
        - **RepoSettings** Obsahuje nastavenia pre IO operácie.
          - **Location** Lokácia zdrojového alebo cieľového súboru. V našej implementácií je to cesta k súboru v súborovom systéme.
          - **Mode** Slúźi ako safeguard pri IO inicializácií *(FileSystemDocumentRepo)* aby sa zabránilo nechcenému prepisovaniu súborov. Môže byť *Read/Write*.
          - **DocumentRepoTypeName** Meno repository triedy, ktorá sa stará o IO operácie. Tiež určuje typ súborového systému kde je zdrojový/cieľový súbor uložený.
    - Projekt obsahuje aj powershell skript **RunMultiple.ps1**, ktorý predstavuje ukážku ako by sa aplikácia dala pustit hromadne s rôznymi nastaveniami.
    - Kód je dúfam dostatočne okomentovaný apoň na úrovni metód a tried, kde som (ak som uznal za vhodné) aj odôvodnil moj návrh.
  - Projekt **Moravia.Homework.Tests** obsahuje basic NUnit testy.
