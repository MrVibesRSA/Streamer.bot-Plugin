# Streamer.bot Plugin
 Streamer.bot plugin for Mecro Deck.
---
## Connecting Macro Deck to Streamer Bot Websocket Server
To enhance your streaming experience, you can connect Macro Deck to a Streamer Bot websocket server. This allows you to control various aspects of your stream directly from your mobile device using Macro Deck's intuitive interface.

### Step 1: Obtain WebSocket Server Details
First, ensure you have the WebSocket server details provided by your Streamer.bot app. This typically includes the server address,port number and endpoint.

Example:
![Alt Text](images/Connection to Streamer.bot B.png)

### Step 2: Configure Macro Deck Streamer.bot plugin
Open Macro Deck app and navigate to the plugin section and open the configeration settings.

Example:
![Alt Text](images/Connection to Streamer.bot A.png)

### Step 3: Enter Server Details
Enter the WebSocket server address, port number and endpoint provided by your Streamer.bot app into the appropriate fields in Macro Deck Streamer.bot Plugin settings and click Connect. You will now be able to add Macro Deck buttons to run Streamer.bot acctions.

Example:
![Alt Text](images/Streamer.bot Actions.png)
---
## Using Streamr.bot global varialbes in Macro Deck

### Stap 1: Getting Streamer.bot's global variable to Macro Deck
Do to so first copy the Streamer.bot action code from the plugin config and past it in to the Streamer.bot import action section. you will now have a action that triggeers every time a Global Variable up dates.

Example:
![Alt Text](images/Import Code.png)

### Stap 2: Using global variable in Macro Deck
Once Global Variable up dates the plugin will catch them in a data grid. If you would like to use the variables in Macro Deck all you need to do is check  them and they will be available to Macro Deck.

Example:
![Alt Text](images/Globals Data Grid.png)

Example:
![Alt Text](images/Macro Deck Variales.png)


***
### Useful links
+ [Streamer.bot](https://streamer.bot/)
+ [Macro Deck](https://macrodeck.org/)

### Streamer.bot - Actions
 A collection of MrVibes_RSA's streamer.bot actions.
 
+ [Ask Magic 8-ball](Magic-8-ball/README.md)
+ [Improved Deathcounter](Improved-Deathcounter/README.md)
+ [Shadows-Lurker](Shadows-Lurker/README.md)
+ [Speed Run Timer](Speed-Run-Timer/README.md)
+ [Twitch Clip to Discord](Clip-To-Discord/README.md)