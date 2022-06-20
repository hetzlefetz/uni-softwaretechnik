### Projekt aufbau

Das Projekt ist wie folgt aufgebaut:

```
<ROOT>
├── build <-- Build Toolchain
├── src   <-- Code der gebaut wird
└── test  <-- Testprojekt  
```

Ruft man das build script (unter build/) nun wie folgt auf: `.\build.(ps1|sh) --target=All --base-path="Pfad zu <ROOT>"` werden alle Buildschritte ausgeführt. Einzelne schritte kann man durch Angabe eines geeigneten Targets anwählen.

Gibt man kein Target an so werden einem die Verfügbaren targets angegeben

