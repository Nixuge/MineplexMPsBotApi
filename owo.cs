//MCCScript 1.0

//using System.Threading.Tasks;

//dll Newtonsoft.Json.dll
//using Newtonsoft.Json.Linq;


MCC.LoadBot(new MineplexBot());

//MCCScript Extensions
class MineplexBot : ChatBot {
    public override void Initialize() {
        // Note: this may try to say "Done loading!" while in spawn,
        // altho since you can't talk in it without moving it doesn't matter
        JoinServer();
        PrintChat("Done loading !");
        if (!GetInventoryEnabled()) {
            LogToConsole("§4InventoryHandle disabled ! Can't interract w containers !");
        }
    }

    public virtual void AfterGameJoined() {
        JoinServer();
    }


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

    /// <summary>
    /// Joins the private server
    /// </summary>
    public void JoinServer() {
        SendText("/sv " + this.SERVER_NAME);
    }

    /// <summary>
    /// Updates the inventoryId of the class when an inventory is needed
    /// </summary>
    /// <remarks>Dirty method, to rework if possible</remarks>
    public override void OnInventoryOpen(int inventoryId) {
        if (this.inventoryNeeded) {
            this.inventoryId = inventoryId;
        }
    }
    bool inventoryNeeded = false;
    int inventoryId = 0;

    /// <summary>
    /// Grabs the Id of the first inventory that pops after the function is called
    /// </summary>
    /// <remarks>Dirty method, to rework if possible</remarks>
    /// <returns>Inventory Id of a new inventory</returns>
    public async Task<int> waitForInventoryId(int delay = 50) {
        this.inventoryNeeded = true;
        while (this.inventoryId == 0) {
            await Task.Delay(50);
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
    public async Task<Container> waitForInventory(string name, int delay = 50, int maxTries = 3) {
        int tries = 1;
        while (true) {
            if (tries > maxTries)
                return null;
            int newId = await waitForInventoryId(delay);
            Container newInv = GetInventories()[newId];
            if (MatchesNoCap(newInv.Title, name))
                return newInv;
            tries++;
        }
        return null;
    }


    /// <summary>
    /// Clicks an item with a specific name inside a given inventory
    /// </summary>
    /// <remarks>Can specify the type of click and if the inventory needs to be closed after (by default LeftClick and yes)</remarks>
    private void clickInventory(
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
    private void clickInventory(
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
    private async Task<Container> clickInventoryContainer(
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
    private async Task<Container> clickInventoryContainer(
        Container container, int itemIndex, string containerName,
        WindowActionType actionType = WindowActionType.LeftClick, bool close = true
    ) {
        clickInventory(container, itemIndex, actionType, close);
        return await waitForInventory(containerName);
    }

    /// <summary>
    /// Prints nicely all items inside a container
    /// </summary>
    private void listItemNames(Container container) {
        LogToConsole("Here's a list of all items in your container:");
        foreach ((int index, Item item) in container.Items) {
            LogToConsole(
                index + ": " + GetVerbatim(item.DisplayName) + " (" + item.Type.ToString() + ")"
            );
        }
    }

    /// <summary>
    /// Returns the name of an item inside a provided container but
    /// with its capitalization from the container
    /// Not really needed but looks a bit nicer
    /// </summary>
    private string getCapitalizedItemName(Container container, string name) {
        foreach ((int _, Item item) in container.Items) {
            string itemName = GetVerbatim(item.DisplayName);

            if (itemName.ToLower() == name.ToLower())
                return itemName;
        }
        return null;
    }

    /// <summary>
    /// Prints nicely all containers open
    /// </summary>
    private void listContainers() {
        LogToConsole("Here's a list of all containers currently open:");
        foreach ((int index, Container container) in GetInventories()) {
            LogToConsole(index + ": " + GetVerbatim(container.Title));
        }
    }

    /// <summary>
    /// Opens the /game menu by right clicking the melon
    /// Can choose to either use the item or command (item by default)
    /// The command is always available, the item is (looks to be) faster
    /// </summary>
    private async Task<Container> openMelon(bool useItem = true) {
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
    private async void giveCoOwn(List<string> players) {
        Container melon = await openMelon();
        Container coOwnContainer = await clickInventoryContainer(
            melon,
            "Give Co-Host",
            "Give Co-Host"
        );
        listItemNames(coOwnContainer);
        PrintChat("Not implemented yet !");
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
    /// Closes all open inventories
    /// </summary>
    /// <remarks>Not really useful</remarks>
    private void closeAll() {
        //TODO: test & fix
        foreach (int invId in GetInventories().Keys) {
            CloseInventory(invId);
        }
        PrintChat("Closed all inventories");
    }

    /// <summary>
    /// Reloads the bot
    /// </summary>
    private void reloadBot() {
        PrintChat("Reloading bot");
        PerformInternalCommand("script ./owo.cs");
        UnloadBot();
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
    /// Clicks on the "Next Map" arrow (index 53) on the provided Container
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
    private async void clickOnMap(bool incrementSlot, List<string> args) {
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

        // start only
        if (this.currentSlot == 9 && !incrementSlot) {
            this.currentSlot++;
        }

        if (incrementSlot) {
            this.currentSlot++;

            if (this.EDGE_SLOTS.Contains(currentSlot)) {
                this.currentSlot += 2;
            }

            // if cursor not on any item (or not paper)
            if (!maps.Items.ContainsKey(currentSlot) || maps.Items[currentSlot].Type != ItemType.Paper) {
                // if at end & has a "next page", goto next
                if (currentSlot > 43 && maps.Items.ContainsKey(53)) {
                    currentPage += 1;
                    currentSlot = FIRST_SLOT;
                    await clickNextButton(maps);
                    // else stop
                } else {
                    PrintChat("No more maps !");
                    return;
                }

            }
        }

        // for some reason sometimes it doesn't seem to detect the item?
        string mapName = maps.Items.ContainsKey(currentSlot) ? GetVerbatim(maps.Items[currentSlot].DisplayName) : "map";

        clickInventory(maps, currentSlot);

        PrintChat("Successfully set map to \"" + mapName + "\" (slot " + currentSlot + ")");

        if (args.Count == 0) {
            startGame(null);
            PrintChat("Successfully started game");
        }
    }

    /// <summary>
    /// Searches for a specified game inside a specified Container
    /// </summary>
    /// <remarks>container can be null and will automaticallt get the "set game" page | UNTESTED AS I DONT HAVE ACCESS</remarks>
    /// <returns>A tuple with the tuple containing the game item and the item's index</returns>
    private async Task<(Container, int)> searchGamePage(Container container, string gameName) {
        if (container == null) {
            container = await clickInventoryContainer(await openMelon(), "Set Game", "Set Game");
        }
        bool nextPage = false;
        foreach ((int index, Item item) in container.Items) {
            if (MatchesNoCap(item.DisplayName, gameName))
                return (container, index);
            else if (MatchesNoCap(item.DisplayName, "next page"))
                nextPage = true;

        }
        if (nextPage) {
            await clickNextButton(container);
            return await searchGamePage(container, gameName);
        }
        return (null, 0);
    }

    /// <summary>
    /// Chooses a game and sets the this.currentGame value
    /// </summary>
    private async void chooseGame(List<string> args) {
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
        }
    }

    /// <summary>
    /// Starts a game
    /// If nothing in args, will run instantly (shiftclick)
    /// Otherwise, will click normally (and so wait 10s)
    /// </summary>
    private async void startGame(List<string> args) {
        WindowActionType click = (args == null || args.Count == 0) ? WindowActionType.ShiftClick : WindowActionType.LeftClick;
        Container melon = await openMelon();
        // clickInventory(melon, 10, actionType:click);
        clickInventory(melon, "Start Game", actionType: click);
    }

    /// <summary>
    /// Stops the game
    /// </summary>
    private async void stopGame() {
        Container melon = await openMelon(useItem: false);
        // clickInventory(melon, 19, actionType:click);
        clickInventory(melon, "Stop Game");
    }

    /// <summary>
    /// Lets you choose a map
    /// </summary>
    /// <remarks>UNIMPLEMENTED</remarks>
    private async void chooseMap(List<string> args) {
        if (args.Count == 0) {
            PrintChat("No map specified !");
            return;
        }
        PrintChat("Unimplemented !");
    }

    // ========== VARS HERE ==========
    // name of the private server to join
    private string SERVER_NAME = "COM-BridgesForever-1";
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
    // current map slot index
    private int currentSlot = 9;
    // current page 
    private int currentPage = 0;
    // current game
    private string currentGame = "";

    /// <summary>
    /// Runs the functions based on the commands ran 
    /// </summary>
    private void runCmd(string cmd, List<string> args) {
        LogToConsole("§6Received command: " + cmd);
        if (cmd.ToLower() == "reco") {
            ReconnectToTheServer();
            return;
        }

        switch (cmd.ToLower()) {
            case "game":
                chooseGame(args);
                break;

            case "map":
                chooseMap(args);
                break;

            case "whitelist":
                addToWhitelist(args);
                break;

            case "qt":
            case "quit":
                LogToConsole("Unloading bot");
                UnloadBot();
                break;

            case "red":
            case "redo":
                clickOnMap(false, args);
                break;

            case "cnt":
            case "continue":
            case "next":
                clickOnMap(true, args);
                break;

            case "setindex":
            case "sind":
            case "slot":
                setIndex(args);
                break;

            case "coown":
                giveCoOwn(args);
                break;

            case "caa":
            case "closeall":
                closeAll();
                break;

            case "start":
                startGame(args);
                break;

            case "stop":
                stopGame();
                break;

            case "rl":
            case "reload":
                reloadBot();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Gets every text message, reject invalid entries and get its json items
    /// Then sends it to grabCommand() and grabMapAuthor()
    /// </summary>
    public override void GetText(string text, string? json) {
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


        grabCommand(items, count);
        grabMapAuthor(items);
    }

    private void grabCommand(JArray items, int count) {
        string username = items[count - 2]["text"].ToString();

        if (!this.TRUSTED_PLAYERS.Contains(username.ToLower()))
            return;

        string message = items[count - 1]["extra"][0].ToString();
        List<string> args = message.Split(' ').ToList();
        string command = args.First();
        args.RemoveAt(0);

        runCmd(command, args);
    }

    private void grabMapAuthor(JArray items) {
        LogToConsole(items);
    }
}
