# Pizza Fight - 2D Multiplayer Brawler

## 📝 Descrizione del Progetto

**Pizza Fight** è un videogioco 2D platform-brawler, sviluppato come *Proof of Concept* (PoC) e progetto di studio per il corso di Sistemi e Reti (A.S. 2024/2025). 

L'obiettivo principale del progetto è studiare e implementare le logiche di sincronizzazione di un videogioco multiplayer, testando la comunicazione tra client attraverso protocolli di rete. Attualmente ispirato alle meccaniche di *Super Smash Bros*, il gioco vede due giocatori scontrarsi in un'arena utilizzando proiettili (pizze) per azzerare i punti vita dell'avversario.

## ⚙️ Architettura e Sviluppo Tecnico

Il progetto è sviluppato in **Unity** (C#) ed è strutturato per separare le logiche di rendering e input locali da quelle che, in futuro, verranno gestite dal server di gioco. 

### Stato Attuale dello Sviluppo
Al momento, la build include il core loop del gameplay locale:
* **Fisica e Input:** Implementazione del movimento del *Player Controller* tramite `Rigidbody2D` e gestione delle collisioni.
* **Combat System:** Logica di *instantiation* dei proiettili (pizze) con vettori di forza applicati, rilevamento degli hitbox e calcolo del danno.
* **Gestione UI e Stato:** Interfaccia utente reattiva che aggiorna la *Health Bar* (sistema a cuori) in tempo reale sfruttando il pattern Observer o gli eventi di Unity per disaccoppiare la UI dalla logica del giocatore.

### Obiettivi di Rete (In Sviluppo)
Essendo un progetto di Sistemi e Reti, il focus futuro è lo sviluppo del comparto **Multiplayer Online**:
* Implementazione di un'architettura Client-Server (o Peer-to-Peer) basata su **socket TCP**.
* Serializzazione e trasmissione dei pacchetti di dati contenenti lo stato del giocatore (posizione, input, instanziazione dei proiettili).
* Gestione della latenza e sincronizzazione dello stato di gioco tra i vari nodi connessi.

## 🚀 Prerequisiti e Avvio del Progetto

Per esplorare il codice sorgente o testare la build corrente all'interno dell'editor, è necessario configurare l'ambiente di sviluppo:

* **Unity Hub** e **Unity Editor** (versione 2022.x o superiore).
* **Git** per il controllo di versione.

### Installazione

1. Clona il repository in locale:
   ```bash
   git clone [link repo]

Apri Unity Hub.

Clicca su Add project from disk (o Open) e seleziona la cartella radice del progetto clonato.

Apri la scena principale (tipicamente situata in Assets/Scenes) e premi il tasto Play nell'Editor per avviare la simulazione.

### 🤝 Contribuire al Progetto Didattico

Essendo un progetto di studio, la revisione del codice e l'implementazione di nuove logiche sono incoraggiate. È possibile contribuire in diversi modi:

* Segnalazione Bug: Aprire una Issue per tracciare anomalie fisiche, glitch visivi o futuri problemi di desincronizzazione di rete.
* Miglioramento del Codice (Refactoring): Proporre Pull Request per ottimizzare gli script C# esistenti, migliorare l'uso dei design pattern o implementare nuove meccaniche.
* Sviluppo Asset: Aggiungere nuovi sprite, mappe o implementare armi aggiuntive configurando i relativi prefab.
