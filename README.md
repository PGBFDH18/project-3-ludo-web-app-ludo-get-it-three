# Fia Web App

Detta projekt bygger vidre på projektet Fia Web API. Eran nya Web App ska använda ert Web API från det förra projekt, man kan säga att målet är att bygga på ett grafisk använder gränssnitt till ert API.

Allt ni gör skall göras i ert GitHub repo (båda kod och dokumentation), som ligger på ert Team. Ni skall använda en ["commit tidigt och ofta"](https://blog.codinghorror.com/check-in-early-check-in-often/) ([1](https://sethrobertson.github.io/GitBestPractices/)) strategi, såklart bör ni endast commita kod som kan kompileras.
Ert repositiory ska vara konfigurerat så att det enda sätt att få in kod i master branchen är via pull requests (detta göras av underviseren), detta betyder att ni inte kan jobba direkt i master men får skåpa en ny branch som ni jobber i ett rekomenderat namn är: *Development*

Ni kan sen göra så många branches baseret på Development som ni önsker, och göra pull requests internt i teamen till *Development*-branchen. Men när projektet är slut är det innehållet av master som räcknas, så ni behöver att göra minst ett pull request från Devleopment till master under projektet med reviewers från ett annat team.

# Dokumentation

Se till att skriv och uppdatera eran [user stories](https://www.mountaingoatsoftware.com/agile/user-stories) (i docs mappen), så att dom passer med eran Web App. Om ni använder någon externa källor (båda kod och annat) ange dom i dokumentationen.

Dokumentation ska skrivas med markdown (.md), ni väljer själv om ni vill skriva på svenska eller engelska, markdown filerna placeras i **docs** mappen.

Gör också ett dokument som beskriver hur man spelar ert spel.

# Programmering
I detta projekt ska ni implementera en Web App till ert Fia spel (Ludo på engelska). Spelet ska vara en .NET Core Web App.

Kod ska ligga i mappen **src**, varje team får enbart ha **en kodbas**. Där finns i detta repository en *gitignore*-fil som passer till Visual Studio, i **src**-mappen finns även en tom solution, denna kan ni använda till att lägga till eran projekt (Web app, Web API och GameEngine)

Det rekomeneras att ni kopiere in ert Web Api och eran game engine i eran **src**-mapp.

## Grundtanken 
Det ska vara möjligt att spela spelet för mellan två och fyra spelare.

Det ska bara möjligt att starta fler samtidiga spel, på servern.

Det ska vara möjligt att logga in

Spill brädan ska uppdatera sig async med SignalR

# Bygg

Konfigurer byggen av eran API i Azure DevOps

Och deploy till en Azure Webservice

# Betygsättning
Detta projekt är betygsgrundande.

## Pull request

## För G

## För VG (G + minst 3 för VG)

