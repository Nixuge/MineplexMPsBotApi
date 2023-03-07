//MCCScript 1.0

//using System.Threading.Tasks;

//using MinecraftClient.CommandHandler;

//dll ./scripts/libs/Newtonsoft.Json.dll
//using Newtonsoft.Json.Linq;

MCC.LoadBot(new MineplexBot());

//MCCScript Extensions

class ChatBotPlus : ChatBot {

    // ===== RANDOM UTILS =====

    /// <summary>
    /// Prints something in minecraft chat as a bot
    /// </summary>
    public void PrintChat(string text) {
        SendText("[B] " + text);
    }


    /// <summary>
    /// Clones a dictionary of int, items. 
    /// Not really efficient nor elegent but avoids conccurency issues
    /// </summary>
    public Dictionary<int, Item> newDictionary(Dictionary<int, Item> dict) {
        return dict.ToDictionary(
            entry => entry.Key,
            entry => entry.Value
        );
    }


    /// <summary>
    /// return if an item display name & a string (both lowered & verbatimed) match
    /// </summary>
    public bool MatchesNoCap(Item item, string string2) {
        return MatchesNoCap(item.DisplayName, string2);
    }


    /// <summary>
    /// return if 2 strings lowered & verbatimed match
    /// </summary>
    public bool MatchesNoCap(string string1, string string2) {
        return GetVerbatim(string1).ToLower() == GetVerbatim(string2).ToLower();
    }


    // ===== ON INVENTORY OPEN / INVENTORY WAIT MANAGEMENT =====

    private bool inventoryNeeded = false;
    private int inventoryId = 0;

    /// <summary>
    /// Updates the inventoryId of the class when an inventory is needed
    /// </summary>
    /// <remarks>Dirty method, to rework if possible</remarks>
    public override void OnInventoryOpen(int inventoryId) {
        if (this.inventoryNeeded) {
            this.inventoryId = inventoryId;
        }
    }

    /// <summary>
    /// Grabs the Id of the first inventory that pops after the function is called
    /// </summary>
    /// <remarks>Dirty method, to rework if possible</remarks>
    /// <returns>Inventory Id of a new inventory</returns>
    protected async Task<int> waitForInventoryId(int delay = 50, int maxtries = 20) {
        int tries = 0;
        this.inventoryNeeded = true;
        while (this.inventoryId == 0) {
            await Task.Delay(50);
            tries++;
            if (tries > maxtries)
                return 0;
        }
        int newId = this.inventoryId;
        this.inventoryId = 0;
        this.inventoryNeeded = false;
        return newId;
    }

    /// <summary>
    /// Grabs the container of the first inventory that pops after the function is called whose name matches the one given
    /// </summary>
    /// <remarks>Dirty method, to rework if possible</remarks>
    /// <returns>Container of the new inventory</returns>
    protected async Task<Container> waitForInventory(string name, int delay = 50, int maxTries = 3) {
        int tries = 0;
        while (tries < maxTries) {
            int newId = await waitForInventoryId(delay);
            Container newInv = GetInventories()[newId];
            if (MatchesNoCap(newInv.Title, name))
                return newInv;
            tries++;
        }
        throw new Exception("Inventory not found after timeout !");
        return null;
    }

    /// <summary>
    /// Closes all open inventories
    /// </summary>
    protected void closeAllInventories() {
        foreach (int invId in GetInventories().Keys) {
            CloseInventory(invId);
        }
        PrintChat("Closed all inventories");
    }
    

    // ===== INVENTORY CLICKS FUNCTIONS =====

     /// <summary>
    /// Clicks an item with a specific name inside a given inventory
    /// </summary>
    /// <remarks>Can specify the type of click and if the inventory needs to be closed after (by default LeftClick and yes)</remarks>
    protected void clickInventory(
        Container container, string itemName,
        WindowActionType actionType = WindowActionType.LeftClick, bool close = true) {

        foreach ((int index, Item item) in newDictionary(container.Items)) {
            if (MatchesNoCap(item.DisplayName, itemName))
                clickInventory(container, index, actionType, close);
        }
    }

    /// <summary>
    /// Clicks an item at a given index inside a given inventory
    /// </summary>
    /// <remarks>Can specify the type of click and if the inventory needs to be closed after (by default LeftClick and yes)</remarks>
    protected void clickInventory(
        Container container, int itemIndex,
        WindowActionType actionType = WindowActionType.LeftClick, bool close = true
    ) {

        WindowAction(container.ID, itemIndex, actionType);
        if (close)
            CloseInventory(container.ID); // avoid having random leftover inventories

    }

    /// <summary>
    /// Clicks an item with a specific name inside a given inventory 
    /// and
    /// Grabs the container of the first inventory that pops after the items is clicked whose name matches the one given
    /// (combination of waitForInventory and clickInventory)
    /// </summary>
    /// <remarks>Can specify the type of click and if the inventory needs to be closed after (by default LeftClick and yes)</remarks>
    /// <returns>Container of the new inventory</returns>
    protected async Task<Container> clickInventoryContainer(
        Container container, string itemName, string containerName,
        WindowActionType actionType = WindowActionType.LeftClick, bool close = true
    ) {
        clickInventory(container, itemName, actionType, close);
        return await waitForInventory(containerName);
    }

    /// <summary>
    /// Clicks an item at a given index inside a given inventory
    /// and
    /// Grabs the container of the first inventory that pops after the items is clicked whose name matches the one given
    /// (combination of waitForInventory and clickInventory)
    /// </summary>
    /// <remarks>Can specify the type of click and if the inventory needs to be closed after (by default LeftClick and yes)</remarks>
    /// <returns>Container of the new inventory</returns>
    protected async Task<Container> clickInventoryContainer(
        Container container, int itemIndex, string containerName,
        WindowActionType actionType = WindowActionType.LeftClick, bool close = true
    ) {
        clickInventory(container, itemIndex, actionType, close);
        return await waitForInventory(containerName);
    }


    // ===== GET CAPITALIZED ITEMNAMES =====
    
    /// <summary>
    /// Returns the name of an item inside a provided container but
    /// with its capitalization from the container
    /// Not really needed but looks a bit nicer
    /// </summary>
    protected string getCapitalizedItemName(Container container, string name) {
        foreach ((int _, Item item) in container.Items) {
            string itemName = GetVerbatim(item.DisplayName);

            if (itemName.ToLower() == name.ToLower())
                return itemName;
        }
        return "NONE";
    }

    /// <summary>
    /// Returns the name of an item with a specified index 
    /// inside a provided container
    /// Not really needed but looks a bit nicer
    /// </summary>
    protected string getCapitalizedItemName(Container container, int index) {
        if (!container.Items.ContainsKey(index)) {
            return "NONE";
        }
        return GetVerbatim(container.Items[index].DisplayName);
    }


    // ===== FANCY PRINT FUNCTIONS =====

    /// <summary>
    /// Prints nicely all items inside a container
    /// </summary>
    protected void listItemNames(Container container) {
        LogToConsole("Here's a list of all items in your container:");
        foreach ((int index, Item item) in container.Items) {
            LogToConsole(
                index + ": " + GetVerbatim(item.DisplayName) + " (" + item.Type.ToString() + ")"
            );
        }
    }

    /// <summary>
    /// Prints nicely all containers open
    /// </summary>
    protected void listContainers() {
        LogToConsole("Here's a list of all containers currently open:");
        foreach ((int index, Container container) in GetInventories()) {
            LogToConsole(index + ": " + GetVerbatim(container.Title));
        }
    }


    // ===== CONTAINER SEARCH UTILS =====

    /// <summary>
    /// Checks if an item is at a specified index with a specified index in a Container
    /// </summary>
    protected bool hasItemAtIndex(Container container, int index, string name) {
        return container.Items.ContainsKey(index) &&
                MatchesNoCap(
                    container.Items[index].DisplayName,
                    name
                );
    }


    // ===== END =====
    // Note: could add searchItemContainerMultiPage(... here)
    // as it could be useful for other servers
}


class MineplexBot : ChatBotPlus {
    public override void Initialize() {
        // Note: may try to say "Done loading!" while in spawn,
        // altho since you can't talk in it without moving it doesn't matter
        JoinServer();
        PrintChat("Done loading !");
        if (!GetInventoryEnabled()) {
            LogToConsole("§4InventoryHandle disabled ! Can't interract w containers !");
            UnloadBot();
        }
        RetryManagement retrier = new RetryManagement();
        if (retrier.hasContent()) {
            this.currentGame = retrier.getGame();
            this.currentMapName = retrier.getMapName();
            this.currentSlot = retrier.getSlot();
            this.currentPage = retrier.getPage();
            this.savedMapCount = retrier.getSavedMapCount();
            printCurrentData(prefix:"Recovered data");
            RetryManagement.eraseFile();
        }
    }

    public void printCurrentData(string prefix = "Current data") {
        PrintChat(prefix + ": map \"" + this.currentMapName  + "\" for game \"" + this.currentGame  + "\" | slot " + this.currentSlot + "p" + this.currentPage );
    }

    public async override void AfterGameJoined() {
        JoinServer();
        await Task.Delay(500); // just in case server lags (which happens often)
        JoinServer();
    }

    private void reloadBot(List<string> args) {
        if (args == null || args.Count == 0)
            RetryManagement.saveData(this.currentGame, this.currentMapName, this.currentSlot, this.currentPage, this.savedMapCount);
        closeAllInventories();
        PrintChat("Reloading bot");
        PerformInternalCommand("script ./MineplexBot.cs");
        UnloadBot();
    }

    /// <summary>
    /// Joins the private server
    /// </summary>
    public void JoinServer() {
        SendText("/sv " + this.SERVER_NAME);
    }

    /// <summary>
    /// Opens the /game menu by right clicking the melon
    /// Can choose to either use the item or command (command by default)
    /// The command is always available, the item is (looks to be) faster
    /// </summary>
    private async Task<Container> openMelon(bool useItem = false) {
        if (useItem) {
            ChangeSlot(7);
            UseItemInHand();
        } else {
            SendText("/game");
        }
        return await waitForInventory("Game Panel");
    }

    /// <summary>
    /// Gives Co Owner perms to all players specified
    /// </summary>
    private async Task giveCoOwn(List<string> players) {
        Container melon = await openMelon();
        Container coOwnContainer = await clickInventoryContainer(
            melon,
            "Give Co-Host",
            "Give Co-Host"
        );
        foreach (String player in this.TRUSTED_PLAYERS)
        {
            clickInventory(coOwnContainer, player);
        }
        PrintChat("Done setting all trusted players to co owners !");
    }

    /// <summary>
    /// Toggles all specified players from the servers whitelist
    /// </summary>
    /// <remarks>If none specified, toggles all players in TRUSTED_PLAYERS</remarks>
    private void addToWhitelist(List<string> players) {
        //note: nasty bug w that one since "dxrrymxxnkid" is in another color 
        if (players.Count == 0)
            SendText("/whitelist " + String.Join(" ", this.TRUSTED_PLAYERS));
        else
            SendText("/whitelist " + String.Join(" ", players.ToArray()));
    }


    /// <summary>
    /// Sets the map inventory index for the current game
    /// </summary>
    /// <remarks>1st arg = index; 2nd arg (optional) = page</remarks>
    private void setIndex(List<string> args) {
        if (this.currentGame == "") {
            PrintChat("Please set a game before");
            return;
        }

        if (args.Count == 0) {
            PrintChat("No value provided, setting to first element (10)");
            this.currentSlot = FIRST_SLOT;
            return;
        }

        this.currentSlot = int.Parse(args[0]);
        PrintChat("Set slot to " + args[0]);

        if (args.Count > 1) {
            this.currentPage = int.Parse(args[1]);
            PrintChat("Set page to " + args[1]);
        }
    }


    /// <summary>
    /// hasItemAtIndex(...) but for the next button specifically
    /// </summary>
    private bool hasNextButton(Container maps) {
        return hasItemAtIndex(maps, this.NEXT_PAGE_INDEX, "next page");
    }

    /// <summary>
    /// Clicks on the "Next Map" arrow on the provided Container
    /// </summary>
    /// <remarks>See the commentary inside the function to see why this is required</remarks>
    private async Task clickNextButton(Container maps) {
        // IMPORTANT NOTE:
        // Due to a nasty bug (-1h30) having an inventory open with the same
        // name as the previous one doesn't call OnInventoryOpen(...)
        // and actually keeps the same id BUT not the same object
        // So just setting an arbitrary (pretty high) delay here to 
        // get through that
        clickInventory(maps, this.NEXT_PAGE_INDEX, close: false);
        await Task.Delay(this.NEXT_INVENTORY_DELAY);
    }


    /// <summary>
    /// Selects a map from the current game using the current index & page. 
    /// incrementSlot makes it so that if yes it goes to the next map before selecting it
    /// If no args, it'll also start the map
    /// </summary>
    private async Task clickOnMap(bool incrementSlot, List<string> args) {
        if (this.currentGame == "") {
            PrintChat("Please set a game before");
            return;
        }

        (Container games, int index) = await searchGamePage(null, this.currentGame);

        Container maps = await clickInventoryContainer(games, index, "Set Map", WindowActionType.RightClick);

        if (this.currentPage > 0) {
            for (int i = 0; i < this.currentPage; i++) {
                await clickNextButton(maps);
            }
        }

        // start only, if slot is 0 make it so that it starts at 10
        if (this.currentSlot == 0) {
            this.currentSlot = incrementSlot ? 9 : 10;
        }

        if (incrementSlot) {
            this.currentSlot++;
            this.savedMapCount++;

            if (this.EDGE_SLOTS.Contains(currentSlot)) {
                this.currentSlot += 2;
            }

            // if cursor not on any item (or not paper)
            if (!maps.Items.ContainsKey(currentSlot) || maps.Items[currentSlot].Type != ItemType.Paper) {
                // if at end & has a "next page", goto next
                if (currentSlot > 43 && hasNextButton(maps)) {
                    currentPage += 1;
                    currentSlot = FIRST_SLOT;
                    await clickNextButton(maps);
                    // else stop
                } else {
                    PrintChat("No more maps ! (" + this.savedMapCount + " saved)");
                    return;
                }
            }
        }

        // for some reason sometimes it doesn't seem to detect the item?
        string mapName = maps.Items.ContainsKey(currentSlot) ? GetVerbatim(maps.Items[currentSlot].DisplayName) : "map";

        clickInventory(maps, currentSlot);

        PrintChat("Successfully set map to \"" + mapName + "\" (slot " + currentSlot + ")");
        this.currentMapName = mapName;

        if (args.Count == 0) {
            startGame(null);
            PrintChat("Successfully started game");
        }
        RetryManagement.saveData(this.currentGame, this.currentMapName, this.currentSlot, this.currentPage, this.savedMapCount);
    }

    /// <summary>
    /// Searches for a specified item inside a specified Container
    /// Works with multi-page menus
    /// </summary>
    /// <remarks>May be moved to the "ChatBotPlus" class</remarks>
    /// <returns>A tuple with the open Container containing the item and the item's index</returns>
    private async Task<(Container, int)> searchItemContainerMultiPage(Container container, string itemName) {
        foreach ((int index, Item item) in container.Items) {
            if (MatchesNoCap(item.DisplayName, itemName))
                return (container, index);
        }

        if (hasNextButton(container)) {
            await clickNextButton(container);
            return await searchGamePage(container, itemName);
        }

        return (null, 0);
    }

    /// <summary>
    /// searchItemContainerMultiPage(...) wrapper for game searching specifically
    /// </summary>
    /// <remarks>Could remove as this wrapper doesn't do much more than the base function</remarks>
    private async Task<(Container, int)> searchGamePage(Container container, string gameName) {
        if (container == null) {
            container = await clickInventoryContainer(await openMelon(), "Set Game", "Set Game");
        }
        return await searchItemContainerMultiPage(container, gameName);
    }

    /// <summary>
    /// Chooses a game and sets the this.currentGame value
    /// </summary>
    private async Task chooseGame(List<string> args) {
        if (args.Count == 0) {
            PrintChat("No game specified !");
            return;
        }

        string gameName = String.Join(" ", args.ToArray()).ToLower();

        if (this.currentGame.ToLower() == gameName) {
            PrintChat("Game already specified !");
            return;
        }

        (Container container, int index) = await searchGamePage(null, gameName);

        if (container == null || index == 0) {
            PrintChat("Specified game invalid (" + gameName + ")");
        } else {
            this.currentGame = getCapitalizedItemName(container, gameName);
            this.currentPage = 0;
            clickInventory(container, index);
            CloseInventory(container.ID);
            PrintChat("Successfully set game to " + this.currentGame);
            RetryManagement.saveData(this.currentGame, this.currentMapName, this.currentSlot, this.currentPage, this.savedMapCount);
        }
    }

    /// <summary>
    /// Starts a game
    /// If nothing in args, will run instantly (shiftclick)
    /// Otherwise, will click normally (and so wait 10s)
    /// </summary>
    private async Task startGame(List<string> args) {
        WindowActionType click = (args == null || args.Count == 0) ? WindowActionType.ShiftClick : WindowActionType.LeftClick;
        Container melon = await openMelon();
        // clickInventory(melon, 10, actionType:click);
        clickInventory(melon, "Start Game", actionType: click);
    }

    /// <summary>
    /// Stops the game
    /// </summary>
    private async Task stopGame() {
        Container melon = await openMelon(useItem: false);
        // clickInventory(melon, 19, actionType:click);
        clickInventory(melon, "Stop Game");
    }

    /// <summary>
    /// Sets the options
    /// </summary>
    private async Task setOptions() {
        bool changed = false;
        Container melon = await openMelon();
        Container settings = await clickInventoryContainer(melon, "Server Settings", "Server Settings");
        foreach ((int index, Item item) in settings.Items) {
            if (item.Type == ItemType.LimeDye) {
                clickInventory(settings, index, close: false);
                changed = true;
            }
        }
        CloseInventory(settings.ID);
        if (changed)
            PrintChat("Done changing all settings");
        else
            PrintChat("No settings were changed");
    }

    /// <summary>
    /// Lets you choose a map
    /// </summary>
    private async Task<bool> chooseMap(List<string> args) {
        if (args.Count == 0) {
            PrintChat("No map specified !");
            return false;
        }

        if (this.currentGame == "") {
            PrintChat("No game specified !");
            return false;
        }

        string mapName = String.Join(" ", args.ToArray()).ToLower();

        (Container games, int gameItemIndex) = await searchGamePage(null, this.currentGame);
        Container mapsFirstPage = await clickInventoryContainer(games, gameItemIndex, "Set Map", WindowActionType.RightClick);

        (Container mapsRightPage, int mapItemindex) = await searchItemContainerMultiPage(mapsFirstPage, mapName);

        mapName = getCapitalizedItemName(mapsRightPage, mapItemindex);

        if (mapsRightPage == null || mapItemindex == 0) {
            PrintChat("Specified map invalid (" + mapName + ") for game " + this.currentGame);
            return false;
        } else {
            clickInventory(mapsRightPage, mapItemindex);
            PrintChat("Selected map " + mapName + " for game " + this.currentGame);
            return true;
        }
    }

    // ========== VARS HERE ==========
    // name of the private server to join
    //private string SERVER_NAME = "COM-BridgesForever-1";
    private string SERVER_NAME = "dxrrymxxnkid-1";
    // String to match for if we receive a DM
    private string DM_RECOGNIZE = " > a4y ";
    // if the mp is a nano mp or no
    private bool IS_NANO = false;
    // trusted players
    private string[] TRUSTED_PLAYERS = new string[] { "wf0", "dxrrymxxnkid", "nixuge", "fc0", "a4y" };
    // mp map selector only uses 7 slots, those are the slots outside
    private int[] EDGE_SLOTS = { 17, 26, 35, 44 };
    // next page arrow index
    private int NEXT_PAGE_INDEX = 53;
    // first map slot index
    private int FIRST_SLOT = 10;
    // inventory after clicking on the next page button in map selector
    private int NEXT_INVENTORY_DELAY = 500;
    // current map name
    private string currentMapName = "";
    // current map slot index
    private int currentSlot = 0;
    // current page 
    private int currentPage = 0;
    // current game
    private string currentGame = "";
    // number of saved maps
    private int savedMapCount = 0;

    // is a command running
    private bool isCommandRunning = false;

    /// <summary>
    /// Runs the functions based on the commands ran 
    /// </summary>
    private async Task runCommand(string cmd, List<string> args) {
        LogToConsole("§6Received command: " + cmd);

        if (this.isCommandRunning && cmd != "rl" && cmd != "reload") {
            PrintChat("Command currently running, please wait");
            return;
        }

        this.isCommandRunning = true;

        switch (cmd.ToLower()) {
            case "join":
                JoinServer();
                break;

            case "game":
                await chooseGame(args);
                break;

            case "map":
                if (await chooseMap(args))
                    await startGame(null);
                break;

            case "mapns":
            case "mapn":
            case "maps":
                await chooseMap(args);
                break;

            case "whitelist":
            case "wl":
                addToWhitelist(args);
                break;

            case "qt":
            case "quit":
                LogToConsole("Unloading bot");
                UnloadBot();
                break;
            
            case "pd":
            case "printdata":
                printCurrentData();
                break;

            case "rd":
            case "red":
            case "redo":
                await clickOnMap(false, args);
                break;

            case "cnt":
            case "continue":
            case "next":
                await clickOnMap(true, args);
                break;

            case "setindex":
            case "sind":
            case "slot":
                setIndex(args);
                break;

            case "coown":
            case "coowner":
            case "co-owner":
            case "co-own":
                await giveCoOwn(args);
                break;

            case "caa":
            case "closeall":
                closeAllInventories();
                break;

            case "start":
                await startGame(args);
                break;

            case "stop":
                await stopGame();
                break;

            case "rl":
            case "reload":
                reloadBot(args);
                break;

            case "setopts":
            case "setoptions":
                await setOptions();
                break;

            default:
                break;
        }
        this.isCommandRunning = false;
    }

    /// <summary>
    /// Gets every text message, reject invalid entries and get its json items
    /// Then sends it to grabCommand() and grabMapAuthor()
    /// </summary>
    public override async void GetText(string text, string? json) {
        //TODO: test below
        // if (json.Contains("You must leave spawn before you can chat!")) {
        //     JoinServer();
        //     Thread.Sleep(200);
        //     return;
        // }


        // THIS LOOKS WEIRD FOR SOME REASON
        // WHY?
        // I can't seem to be able to use .Contains() on json objects
        //// getting an Assembly error for Ithingy (prolly because of Newtonsoft Json)
        // so i basically have to just guess and hope 
        // hence why this normally simple thing is long and overcomplicated

        if (json.Contains(this.DM_RECOGNIZE)) {
            await runCommand("stop", null);
            return;
        }

        if (json.Length < 65
            || json.ToLower().Contains("shop")
            || GetVerbatim(json.Substring(0, 65)).Contains("> ")
        )
            return;

        // rejoin if kicked
        if (json.Contains("You were kicked from")) {
            JoinServer();
            return;
        }

        // bots don't react to their own messages
        if (json.Contains("[B]"))
            return;

        JObject rss = JObject.Parse(json);

        if (!rss.ContainsKey("extra"))
            return;

        var items = (JArray)rss["extra"];
        int count = items.Count;

        if (count < 3)
            return;

        // "1st place..." texts
        if (items[0].ToString() == " ")
            return;

        if (items[0]["text"].ToString() == "Map - ")
            grabMapAuthor(items);


        grabCommand(items, count);
    }

    private void grabCommand(JArray items, int count) {
        string username = items[count - 2]["text"].ToString();

        if (!this.TRUSTED_PLAYERS.Contains(username.ToLower()))
            return;

        string message = items[count - 1]["extra"][0].ToString();
        List<string> args = message.Split(' ').ToList();
        string command = args.First();
        args.RemoveAt(0);

        handleRunCommandError(command, args);
    }
    private async Task handleRunCommandError(String command, List<string> args, int _try = 0) {
        //TODO: ensure async try catch works
        try {
            await runCommand(command, args);
        } catch (Exception e) {
            if (_try < 3) {
                PrintChat("Command had an exception, retrying (try " + (_try + 1) + "/3)");
                this.isCommandRunning = false;
                await handleRunCommandError(command, args, _try: _try + 1);
                return;
            }
            // Chat
            PrintChat("Something went wrong! See console for full stack trace");
            string errstr = "Error '" + e.Message + "' @ '" + e.TargetSite + "'";
            PrintChat(errstr.Substring(0, (errstr.Length <= 256) ? errstr.Length : 256));
            // Console
            LogToConsole("§4====================");
            LogToConsole("§cError '" + e.Message + "' near " + e.TargetSite);
            LogToConsole("§4====================");
            LogToConsole(e.StackTrace);
            LogToConsole("§4====================");

            closeAllInventories();
            this.isCommandRunning = false;
        }
    }

    private void grabMapAuthor(JArray items) {
        string map = items[1]["text"].ToString();
        string author = items[3]["text"].ToString();
        new CSVManagement(map, author, this.currentGame, this.IS_NANO).updateCSV();
    }
}


// No docs for this one as it's pretty straight forward & lazy
class RetryManagement {
    private static string filePath = ".data/reload.txt";
    public static void saveData(string game, string mapName, int slot, int page, int savedMapCount) {
        if (game == "") {
            eraseFile();
            return;
        }
        File.WriteAllText(filePath, game + "\t" + mapName + "\t" + slot + "\t" + page + "\t" + savedMapCount);
    }
    public static void eraseFile() {
        File.WriteAllText(filePath, "");
    }

    private string[] data;
    public RetryManagement() {
        string fileContent = File.ReadAllText(filePath);
        this.data = fileContent.Split("\t");
    }

    public bool hasContent() {
        return (this.data.Length == 5);
    }

    public string getGame() {
        return this.data[0];
    }
    public string getMapName() {
        return this.data[1];
    }
    public int getSlot() {
        return int.Parse(this.data[2]);
    }
    public int getPage() {
        return int.Parse(this.data[3]);
    }
    public int getSavedMapCount() {
        return int.Parse(this.data[4]);
    }
}


// No docs for this one either because same reason
class CSVManagement {
    private string map;
    private string author;
    private string game;
    private bool isNano;

    private static string standalonePath = ".data/info.txt";
    private string csvPath;
    
    private string csvLine;

    public CSVManagement(string map, string author, string game, bool isNano) {
        this.map = map;
        this.author = author;
        this.game = game;
        this.isNano = isNano;
        this.csvPath = "CSVs/" + game + ".csv";
        this.csvLine = genLine();
    }

    private void updateDataTxt() {
        File.WriteAllTextAsync(standalonePath, this.game + "\t" + this.csvLine);
    }

    private string genLine() {
        string full = "";
        if (this.isNano)
            full += this.game + "\t";
        full += this.map + "\t" + this.author + "\t";
        return full;
    }
    private string genHeader() {
        string full = "";
        if (this.isNano)
            full += "Minigame\t";
        full += "Name\tBuilder\tCommentaries";
        return full;
    }

    public void updateCSV() {
        Directory.CreateDirectory("CSVs");

        if (!File.Exists(this.csvPath)) {
            File.WriteAllText(this.csvPath, genHeader() + Environment.NewLine);
        }

        if (!File.ReadLines(this.csvPath).Contains(this.csvLine)) {
            File.AppendAllTextAsync(this.csvPath, this.csvLine + Environment.NewLine);
        }

        this.updateDataTxt();
    }
}
