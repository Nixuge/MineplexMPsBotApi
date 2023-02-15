//MCCScript 1.0

//using System.Threading.Tasks;

//dll Newtonsoft.Json.dll
//using Newtonsoft.Json.Linq;


MCC.LoadBot(new ExampleChatBot());

//MCCScript Extensions
class ExampleChatBot : ChatBot
{
    public override void Initialize()
    {
        LogToConsole("Working");
        if (!GetInventoryEnabled())
        {
            LogToConsole("InventoryHandle disabled ! Can't interract w containers !");
        }
    }

    /// <summary>
    /// Prints something in minecraft chat as a bot
    /// </summary>
    public void PrintChat(string text)
    {
        SendText("[B] " + text);
    }

    /// <summary>
    /// Clones a dictionary of int, items. 
    /// Not really efficient nor elegent but avoids conccurency issues
    /// </summary>
    public Dictionary<int, Item> newDictionary(Dictionary<int, Item> dict)
    {
        return dict.ToDictionary(
            entry => entry.Key,
            entry => entry.Value
        );
    }


    /// <summary>
    /// Updates the inventoryId of the class when an inventory is needed
    /// </summary>
    /// <remarks>Dirty method, to rework if possible</remarks>

    ///
    public override void OnInventoryOpen(int inventoryId)
    {
        if (this.inventoryNeeded)
        {
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
    public async Task<int> waitForInventoryId(int delay = 50)
    {
        this.inventoryNeeded = true;
        while (this.inventoryId == 0)
        {
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
    public async Task<Container> waitForInventory(string name, int delay = 50)
    {
        while (true)
        {
            int newId = await waitForInventoryId(delay);
            Container newInv = GetInventories()[newId];
            if (GetVerbatim(newInv.Title.ToLower()) == name.ToLower())
                return newInv;
        }
        return null;
    }


    /// <summary>
    /// Clicks an item with a specific name inside a given inventory
    /// </summary>
    /// <remarks>Can specify the type of click and if the inventory needs to be closed after (by default LeftClick and yes)</remarks>
    private void clickInventory(
        Container container, string itemName,
        WindowActionType actionType = WindowActionType.LeftClick, bool close = true)
    {
        foreach (KeyValuePair<int, Item> entry in newDictionary(container.Items))
        {
            if (GetVerbatim(entry.Value.DisplayName.ToLower()) == itemName.ToLower())
            {
                clickInventory(container, entry.Key, actionType, close);
            }
        }
    }

    /// <summary>
    /// Clicks an item at a given index inside a given inventory
    /// </summary>
    /// <remarks>Can specify the type of click and if the inventory needs to be closed after (by default LeftClick and yes)</remarks>
    private void clickInventory(
        Container container, int itemIndex,
        WindowActionType actionType = WindowActionType.LeftClick, bool close = true
    )
    {

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
    )
    {
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
    )
    {
        clickInventory(container, itemIndex, actionType, close);
        return await waitForInventory(containerName);
    }

    /// <summary>
    /// Prints nicely all items inside a container
    /// </summary>
    private void listItemNames(Container container)
    {
        LogToConsole("Here's a list of all items in your container:");
        foreach (KeyValuePair<int, Item> entry in container.Items)
        {
            Item item = entry.Value;
            LogToConsole(
                entry.Key + ": " + GetVerbatim(item.DisplayName) + " (" + item.Type.ToString() + ")"
            );
        }
    }

    /// <summary>
    /// Prints nicely all containers open
    /// </summary>
    private void listContainers()
    {
        LogToConsole("Here's a list of all containers currently open:");
        foreach (KeyValuePair<int, Container> entry in GetInventories())
        {
            Container container = entry.Value;
            LogToConsole(entry.Key + ": " + GetVerbatim(container.Title));
        }
    }

    /// <summary>
    /// Opens the /game menu by right clicking the melon
    /// </summary>
    private async Task<Container> openMelon()
    {
        ChangeSlot(7);
        UseItemInHand();
        return await waitForInventory("Game Panel");
    }

    /// <summary>
    /// Gives Co Owner perms to all players specified
    /// </summary>
    private async void giveCoOwn(List<string> players)
    {
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
    /// Adds all specified players to the servers whitelist
    /// </summary>
    /// <remarks>Can specify the type of click and if the inventory needs to be closed after (by default LeftClick and yes)</remarks>
    private void addToWhitelist(List<string> players)
    {
        //note: nasty bug w that one since "dxrrymxxnkid" is in another color 
        if (players.Count == 0)
        {
            players = new List<string> { "nixuge", "a4y", "fc0", "wf0", "dxrrymxxnkid" };
        }
        SendText("/whitelist " + String.Join(" ", players.ToArray()));
    }

    private void closeAll()
    {
        foreach (int invId in GetInventories().Keys)
        {
            CloseInventory(invId);
        }
        PrintChat("Closed all inventories");
    }

    private void setIndex(List<string> args)
    {
        if (this.currentGame == "")
        {
            PrintChat("Please set a game before");
            return;
        }

        if (args.Count == 0)
        {
            PrintChat("No value provided, setting to first element (10)");
            this.currentSlot = FIRST_SLOT;
            return;
        }

        this.currentSlot = int.Parse(args[0]);
        PrintChat("Set slot to " + args[0]);

        if (args.Count > 1)
        {
            this.currentPage = int.Parse(args[1]);
            PrintChat("Set page to " + args[1]);
        }
    }

    private async Task clickNextMap(Container maps)
    {
        // IMPORTANT NOTE:
        // Due to a nasty bug (-1h30) having an inventory open with the same
        // name as the previous one doesn't call OnInventoryOpen(...)
        // and actually keeps the same id BUT not the same object
        // So just setting an arbitrary (pretty high) delay here to 
        // get through that
        clickInventory(maps, this.NEXT_PAGE_INDEX, close: false);
        await Task.Delay(this.NEXT_INVENTORY_DELAY);
    }

    private async void clickOnMap(bool incrementSlot)
    {
        if (this.currentGame == "")
        {
            PrintChat("Please set a game before");
            return;
        }

        Container melon = await openMelon();
        Container games = await clickInventoryContainer(melon, "Set Game", "Set Game");
        Container maps = await clickInventoryContainer(games, this.currentGame, "Set Map", WindowActionType.RightClick);
        if (this.currentPage > 0)
        {
            for (int i = 0; i < this.currentPage; i++)
            {
                await clickNextMap(maps);
            }
        }

        if (incrementSlot)
        {
            this.currentSlot++;

            if (this.EDGE_SLOTS.Contains(currentSlot))
            {
                this.currentSlot += 2;
            }

            // if cursor not on any item (or not paper)
            if (!maps.Items.ContainsKey(currentSlot) || maps.Items[currentSlot].Type != ItemType.Paper)
            {
                // if at end & has a "next page", goto next
                if (currentSlot > 43 && maps.Items.ContainsKey(53))
                {
                    currentPage += 1;
                    currentSlot = FIRST_SLOT;
                    await clickNextMap(maps);
                    // else stop
                }
                else
                {
                    PrintChat("No more maps !");
                    return;
                }

            }
        }

        clickInventory(maps, currentSlot);
        // for some reason sometimes it doesn't seem to detect the item?
        string mapName = maps.Items.ContainsKey(currentSlot) ? GetVerbatim(maps.Items[currentSlot].DisplayName) : "map";
        PrintChat("Successfully set map to \"" + mapName + "\" (slot " + currentSlot + ")");
    }

    private async void searchGamePageClick(Container container, string gameName)
    {
        int newPageIndex = 0;
        foreach (KeyValuePair<int, Item> entry in container.Items)
        {
            if (GetVerbatim(entry.Value.DisplayName.ToLower()) == gameName)
            {
                LogToConsole(entry.Value.DisplayName);
                this.currentGame = gameName;
                this.currentPage = 0;
                clickInventory(container, entry.Key);
                CloseInventory(container.ID);
                PrintChat("Successfully set game to " + this.currentGame);
                return;
            }
            else if (GetVerbatim(entry.Value.DisplayName.ToLower()) == "next page")
            { //todo: check if next page item doesnt have any padding
                newPageIndex = entry.Key;
            }
        }
        if (newPageIndex != 0)
        {
            // No need to bother w returns here honestly
            //Todo: use index instead of searching again
            searchGamePageClick(await clickInventoryContainer(container, "next page", "Set Map"), gameName);
        }
        else
        {
            PrintChat("Specified game invalid (" + gameName + ")");
        }
    }
    private async void chooseGame(List<string> args)
    {
        if (args.Count == 0)
        {
            PrintChat("No game specified !");
            return;
        }

        string gameName = String.Join(" ", args.ToArray()).ToLower();

        if (this.currentGame.ToLower() == gameName)
        {
            PrintChat("Game already specified !");
            return;
        }

        Container melon = await openMelon();
        Container setGame = await clickInventoryContainer(melon, "Set Game", "Set Game");

        searchGamePageClick(setGame, gameName);
    }

    private async void chooseMap(List<string> args)
    {
        if (args.Count == 0)
        {
            PrintChat("No map specified !");
            return;
        }
        PrintChat("Unimplemented !");
    }

    // ===== VARS HERE =====
    private int[] EDGE_SLOTS = { 17, 26, 35, 44 };
    private int NEXT_PAGE_INDEX = 53;
    private int FIRST_SLOT = 10;
    private int NEXT_INVENTORY_DELAY = 500;
    private int currentSlot = 10;
    private int currentPage = 0;
    private string currentGame = "";

    private void runCmd(string cmd, List<string> args)
    {
        LogToConsole("§6Received command: " + cmd);
        if (cmd.ToLower() == "reco")
        {
            ReconnectToTheServer();
            return;
        }

        switch (cmd.ToLower())
        {
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

            case "start":
            case "red":
            case "redo":
                clickOnMap(false);
                break;

            case "cnt":
            case "continue":
            case "next":
                clickOnMap(true);
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

            default:
                break;
        }
    }

    public override void GetText(string text, string? json)
    {
        if (json.Length < 20)
        {
            return;
        }

        //todo: ithink there's still a way to crash w nasty errors here
        //check if i can fix it (need to enable logging jsons below)
        // LogToConsole(json);

        JObject rss = JObject.Parse(json);

        if (!rss.ContainsKey("extra"))
        {
            LogToConsole("Nasty error log");
            return;
        }

        var items = (JArray)rss["extra"];
        int count = items.Count;

        if (count < 3)
        {
            return;
        }
        //could just do if contains > but making sure
        string[] annoying_chat = { "Portal> ", "Communities> ", "Track> " };
        if (annoying_chat.Contains(GetVerbatim(items[0]["text"].ToString())))
        {
            return;
        }

        string username = items[count - 2]["text"].ToString();
        string message = items[count - 1]["extra"][0].ToString();

        if (new string[] { "wf0", "dxrrymxxnkid", "nixuge", "fc0", "a4y" }.Contains(username.ToLower()))
        {
            List<string> args = message.Split(' ').ToList();
            string command = args.First();
            args.RemoveAt(0);

            runCmd(command, args);
        }
    }
}
