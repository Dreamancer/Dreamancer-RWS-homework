##Vypracované zadanie od RWS
##Autor: Jakub Horniak

Toto repo obsahuje:
- Folder s pôvodným zadaním:
  - Pôvodný chybný kód v súbore **BuggyProgram.cs** s mojimi poznámkami o chybách ako komentáre (v angličtine)
  - 'Opravený' kód v súbore **CorrectedProgram.cs** kde som sa pokúsil o aspoň workable kód
  - Pdf so zadaním
- Moje riešenie zadania - Solution **Moravia.Homework** obsahujúcu 2 projekty:
  - **Moravia.Homework** - .NET 6 Konzolová aplikácia založená na moduloch poskytovaných pomocou dependency injection a nastavovaná pomocou konfiguračného json súboru.
    - O IO operácie sa starajú triedy implementujúce rozhranie *Moravia.Homework.DAL.IDocumentRepo*/abstraktnú triedu *Moravia.Homework.DAL.DocumentRepoBase*
      - Podľa zadania som implementoval jeden typ modulu pre prácu so súborovými systémami *Moravia.Homework.DAL.FileSystemDocumentRepo*, no nie je problém pridať iné typy implementovaním ^
    - O serializáciu sa starajú triedy implementujúce rohranie *Moravia.Homework.Serialization.IDocumentSerializer*/abstraktnú triedu *Moravia.Homework.Serialization.DocumentSerializerBase*
      - Obdobne som implementoval serializáciu json a xml formátov pomocou tried *Moravia.Homework.Serialization.JsonDocumentSerializer* a *Moravia.Homework.Serialization.XmlDocumentSerializer*, no
