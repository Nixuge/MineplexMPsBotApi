using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

public enum ItemType {
    Unknown = -2, // Unsupported item type (Forge mod custom item...)
    Null = -1, // Unspecified item type (Used in the network protocol)

    AcaciaBoat,
    AcaciaButton,
    AcaciaChestBoat,
    AcaciaDoor,
    AcaciaFence,
    AcaciaFenceGate,
    AcaciaHangingSign,
    AcaciaLeaves,
    AcaciaLog,
    AcaciaPlanks,
    AcaciaPressurePlate,
    AcaciaSapling,
    AcaciaSign,
    AcaciaSlab,
    AcaciaStairs,
    AcaciaTrapdoor,
    AcaciaWood,
    ActivatorRail,
    Air,
    AllaySpawnEgg,
    Allium,
    AmethystBlock,
    AmethystCluster,
    AmethystShard,
    AncientDebris,
    Andesite,
    AndesiteSlab,
    AndesiteStairs,
    AndesiteWall,
    Anvil,
    Apple,
    ArmorStand,
    Arrow,
    AxolotlBucket,
    AxolotlSpawnEgg,
    Azalea,
    AzaleaLeaves,
    AzureBluet,
    BakedPotato,
    Bamboo,
    BambooBlock,
    BambooButton,
    BambooChestRaft,
    BambooDoor,
    BambooFence,
    BambooFenceGate,
    BambooHangingSign,
    BambooMosaic,
    BambooMosaicSlab,
    BambooMosaicStairs,
    BambooPlanks,
    BambooPressurePlate,
    BambooRaft,
    BambooSign,
    BambooSlab,
    BambooStairs,
    BambooTrapdoor,
    Barrel,
    Barrier,
    Basalt,
    BatSpawnEgg,
    Beacon,
    Bedrock,
    BeeNest,
    BeeSpawnEgg,
    Beef,
    Beehive,
    Beetroot,
    BeetrootSeeds,
    BeetrootSoup,
    Bell,
    BigDripleaf,
    BirchBoat,
    BirchButton,
    BirchChestBoat,
    BirchDoor,
    BirchFence,
    BirchFenceGate,
    BirchHangingSign,
    BirchLeaves,
    BirchLog,
    BirchPlanks,
    BirchPressurePlate,
    BirchSapling,
    BirchSign,
    BirchSlab,
    BirchStairs,
    BirchTrapdoor,
    BirchWood,
    BlackBanner,
    BlackBed,
    BlackCandle,
    BlackCarpet,
    BlackConcrete,
    BlackConcretePowder,
    BlackDye,
    BlackGlazedTerracotta,
    BlackShulkerBox,
    BlackStainedGlass,
    BlackStainedGlassPane,
    BlackTerracotta,
    BlackWool,
    Blackstone,
    BlackstoneSlab,
    BlackstoneStairs,
    BlackstoneWall,
    BlastFurnace,
    BlazePowder,
    BlazeRod,
    BlazeSpawnEgg,
    BlueBanner,
    BlueBed,
    BlueCandle,
    BlueCarpet,
    BlueConcrete,
    BlueConcretePowder,
    BlueDye,
    BlueGlazedTerracotta,
    BlueIce,
    BlueOrchid,
    BlueShulkerBox,
    BlueStainedGlass,
    BlueStainedGlassPane,
    BlueTerracotta,
    BlueWool,
    Bone,
    BoneBlock,
    BoneMeal,
    Book,
    Bookshelf,
    Bow,
    Bowl,
    BrainCoral,
    BrainCoralBlock,
    BrainCoralFan,
    Bread,
    BrewingStand,
    Brick,
    BrickSlab,
    BrickStairs,
    BrickWall,
    Bricks,
    BrownBanner,
    BrownBed,
    BrownCandle,
    BrownCarpet,
    BrownConcrete,
    BrownConcretePowder,
    BrownDye,
    BrownGlazedTerracotta,
    BrownMushroom,
    BrownMushroomBlock,
    BrownShulkerBox,
    BrownStainedGlass,
    BrownStainedGlassPane,
    BrownTerracotta,
    BrownWool,
    BubbleCoral,
    BubbleCoralBlock,
    BubbleCoralFan,
    Bucket,
    BuddingAmethyst,
    Bundle,
    Cactus,
    Cake,
    Calcite,
    CamelSpawnEgg,
    Campfire,
    Candle,
    Carrot,
    CarrotOnAStick,
    CartographyTable,
    CarvedPumpkin,
    CatSpawnEgg,
    Cauldron,
    CaveSpiderSpawnEgg,
    Chain,
    ChainCommandBlock,
    ChainmailBoots,
    ChainmailChestplate,
    ChainmailHelmet,
    ChainmailLeggings,
    Charcoal,
    Chest,
    ChestMinecart,
    Chicken,
    ChickenSpawnEgg,
    ChippedAnvil,
    ChiseledBookshelf,
    ChiseledDeepslate,
    ChiseledNetherBricks,
    ChiseledPolishedBlackstone,
    ChiseledQuartzBlock,
    ChiseledRedSandstone,
    ChiseledSandstone,
    ChiseledStoneBricks,
    ChorusFlower,
    ChorusFruit,
    ChorusPlant,
    Clay,
    ClayBall,
    Clock,
    Coal,
    CoalBlock,
    CoalOre,
    CoarseDirt,
    CobbledDeepslate,
    CobbledDeepslateSlab,
    CobbledDeepslateStairs,
    CobbledDeepslateWall,
    Cobblestone,
    CobblestoneSlab,
    CobblestoneStairs,
    CobblestoneWall,
    Cobweb,
    CocoaBeans,
    Cod,
    CodBucket,
    CodSpawnEgg,
    CommandBlock,
    CommandBlockMinecart,
    Comparator,
    Compass,
    Composter,
    Conduit,
    CookedBeef,
    CookedChicken,
    CookedCod,
    CookedMutton,
    CookedPorkchop,
    CookedRabbit,
    CookedSalmon,
    Cookie,
    CopperBlock,
    CopperIngot,
    CopperOre,
    Cornflower,
    CowSpawnEgg,
    CrackedDeepslateBricks,
    CrackedDeepslateTiles,
    CrackedNetherBricks,
    CrackedPolishedBlackstoneBricks,
    CrackedStoneBricks,
    CraftingTable,
    CreeperBannerPattern,
    CreeperHead,
    CreeperSpawnEgg,
    CrimsonButton,
    CrimsonDoor,
    CrimsonFence,
    CrimsonFenceGate,
    CrimsonFungus,
    CrimsonHangingSign,
    CrimsonHyphae,
    CrimsonNylium,
    CrimsonPlanks,
    CrimsonPressurePlate,
    CrimsonRoots,
    CrimsonSign,
    CrimsonSlab,
    CrimsonStairs,
    CrimsonStem,
    CrimsonTrapdoor,
    Crossbow,
    CryingObsidian,
    CutCopper,
    CutCopperSlab,
    CutCopperStairs,
    CutRedSandstone,
    CutRedSandstoneSlab,
    CutSandstone,
    CutSandstoneSlab,
    CyanBanner,
    CyanBed,
    CyanCandle,
    CyanCarpet,
    CyanConcrete,
    CyanConcretePowder,
    CyanDye,
    CyanGlazedTerracotta,
    CyanShulkerBox,
    CyanStainedGlass,
    CyanStainedGlassPane,
    CyanTerracotta,
    CyanWool,
    DamagedAnvil,
    Dandelion,
    DarkOakBoat,
    DarkOakButton,
    DarkOakChestBoat,
    DarkOakDoor,
    DarkOakFence,
    DarkOakFenceGate,
    DarkOakHangingSign,
    DarkOakLeaves,
    DarkOakLog,
    DarkOakPlanks,
    DarkOakPressurePlate,
    DarkOakSapling,
    DarkOakSign,
    DarkOakSlab,
    DarkOakStairs,
    DarkOakTrapdoor,
    DarkOakWood,
    DarkPrismarine,
    DarkPrismarineSlab,
    DarkPrismarineStairs,
    DaylightDetector,
    DeadBrainCoral,
    DeadBrainCoralBlock,
    DeadBrainCoralFan,
    DeadBubbleCoral,
    DeadBubbleCoralBlock,
    DeadBubbleCoralFan,
    DeadBush,
    DeadFireCoral,
    DeadFireCoralBlock,
    DeadFireCoralFan,
    DeadHornCoral,
    DeadHornCoralBlock,
    DeadHornCoralFan,
    DeadTubeCoral,
    DeadTubeCoralBlock,
    DeadTubeCoralFan,
    DebugStick,
    Deepslate,
    DeepslateBrickSlab,
    DeepslateBrickStairs,
    DeepslateBrickWall,
    DeepslateBricks,
    DeepslateCoalOre,
    DeepslateCopperOre,
    DeepslateDiamondOre,
    DeepslateEmeraldOre,
    DeepslateGoldOre,
    DeepslateIronOre,
    DeepslateLapisOre,
    DeepslateRedstoneOre,
    DeepslateTileSlab,
    DeepslateTileStairs,
    DeepslateTileWall,
    DeepslateTiles,
    DetectorRail,
    Diamond,
    DiamondAxe,
    DiamondBlock,
    DiamondBoots,
    DiamondChestplate,
    DiamondHelmet,
    DiamondHoe,
    DiamondHorseArmor,
    DiamondLeggings,
    DiamondOre,
    DiamondPickaxe,
    DiamondShovel,
    DiamondSword,
    Diorite,
    DioriteSlab,
    DioriteStairs,
    DioriteWall,
    Dirt,
    DirtPath,
    DiscFragment5,
    Dispenser,
    DolphinSpawnEgg,
    DonkeySpawnEgg,
    DragonBreath,
    DragonEgg,
    DragonHead,
    DriedKelp,
    DriedKelpBlock,
    DripstoneBlock,
    Dropper,
    DrownedSpawnEgg,
    EchoShard,
    Egg,
    ElderGuardianSpawnEgg,
    Elytra,
    Emerald,
    EmeraldBlock,
    EmeraldOre,
    EnchantedBook,
    EnchantedGoldenApple,
    EnchantingTable,
    EndCrystal,
    EndPortalFrame,
    EndRod,
    EndStone,
    EndStoneBrickSlab,
    EndStoneBrickStairs,
    EndStoneBrickWall,
    EndStoneBricks,
    EnderChest,
    EnderDragonSpawnEgg,
    EnderEye,
    EnderPearl,
    EndermanSpawnEgg,
    EndermiteSpawnEgg,
    EvokerSpawnEgg,
    ExperienceBottle,
    ExposedCopper,
    ExposedCutCopper,
    ExposedCutCopperSlab,
    ExposedCutCopperStairs,
    Farmland,
    Feather,
    FermentedSpiderEye,
    Fern,
    FilledMap,
    FireCharge,
    FireCoral,
    FireCoralBlock,
    FireCoralFan,
    FireworkRocket,
    FireworkStar,
    FishingRod,
    FletchingTable,
    Flint,
    FlintAndSteel,
    FlowerBannerPattern,
    FlowerPot,
    FloweringAzalea,
    FloweringAzaleaLeaves,
    FoxSpawnEgg,
    FrogSpawnEgg,
    Frogspawn,
    Furnace,
    FurnaceMinecart,
    GhastSpawnEgg,
    GhastTear,
    GildedBlackstone,
    Glass,
    GlassBottle,
    GlassPane,
    GlisteringMelonSlice,
    GlobeBannerPattern,
    GlowBerries,
    GlowInkSac,
    GlowItemFrame,
    GlowLichen,
    GlowSquidSpawnEgg,
    Glowstone,
    GlowstoneDust,
    GoatHorn,
    GoatSpawnEgg,
    GoldBlock,
    GoldIngot,
    GoldNugget,
    GoldOre,
    GoldenApple,
    GoldenAxe,
    GoldenBoots,
    GoldenCarrot,
    GoldenChestplate,
    GoldenHelmet,
    GoldenHoe,
    GoldenHorseArmor,
    GoldenLeggings,
    GoldenPickaxe,
    GoldenShovel,
    GoldenSword,
    Granite,
    GraniteSlab,
    GraniteStairs,
    GraniteWall,
    Grass,
    GrassBlock,
    Gravel,
    GrayBanner,
    GrayBed,
    GrayCandle,
    GrayCarpet,
    GrayConcrete,
    GrayConcretePowder,
    GrayDye,
    GrayGlazedTerracotta,
    GrayShulkerBox,
    GrayStainedGlass,
    GrayStainedGlassPane,
    GrayTerracotta,
    GrayWool,
    GreenBanner,
    GreenBed,
    GreenCandle,
    GreenCarpet,
    GreenConcrete,
    GreenConcretePowder,
    GreenDye,
    GreenGlazedTerracotta,
    GreenShulkerBox,
    GreenStainedGlass,
    GreenStainedGlassPane,
    GreenTerracotta,
    GreenWool,
    Grindstone,
    GuardianSpawnEgg,
    Gunpowder,
    HangingRoots,
    HayBlock,
    HeartOfTheSea,
    HeavyWeightedPressurePlate,
    HoglinSpawnEgg,
    HoneyBlock,
    HoneyBottle,
    Honeycomb,
    HoneycombBlock,
    Hopper,
    HopperMinecart,
    HornCoral,
    HornCoralBlock,
    HornCoralFan,
    HorseSpawnEgg,
    HuskSpawnEgg,
    Ice,
    InfestedChiseledStoneBricks,
    InfestedCobblestone,
    InfestedCrackedStoneBricks,
    InfestedDeepslate,
    InfestedMossyStoneBricks,
    InfestedStone,
    InfestedStoneBricks,
    InkSac,
    IronAxe,
    IronBars,
    IronBlock,
    IronBoots,
    IronChestplate,
    IronDoor,
    IronGolemSpawnEgg,
    IronHelmet,
    IronHoe,
    IronHorseArmor,
    IronIngot,
    IronLeggings,
    IronNugget,
    IronOre,
    IronPickaxe,
    IronShovel,
    IronSword,
    IronTrapdoor,
    ItemFrame,
    JackOLantern,
    Jigsaw,
    Jukebox,
    JungleBoat,
    JungleButton,
    JungleChestBoat,
    JungleDoor,
    JungleFence,
    JungleFenceGate,
    JungleHangingSign,
    JungleLeaves,
    JungleLog,
    JunglePlanks,
    JunglePressurePlate,
    JungleSapling,
    JungleSign,
    JungleSlab,
    JungleStairs,
    JungleTrapdoor,
    JungleWood,
    Kelp,
    KnowledgeBook,
    Ladder,
    Lantern,
    LapisBlock,
    LapisLazuli,
    LapisOre,
    LargeAmethystBud,
    LargeFern,
    LavaBucket,
    Lead,
    Leather,
    LeatherBoots,
    LeatherChestplate,
    LeatherHelmet,
    LeatherHorseArmor,
    LeatherLeggings,
    Lectern,
    Lever,
    Light,
    LightBlueBanner,
    LightBlueBed,
    LightBlueCandle,
    LightBlueCarpet,
    LightBlueConcrete,
    LightBlueConcretePowder,
    LightBlueDye,
    LightBlueGlazedTerracotta,
    LightBlueShulkerBox,
    LightBlueStainedGlass,
    LightBlueStainedGlassPane,
    LightBlueTerracotta,
    LightBlueWool,
    LightGrayBanner,
    LightGrayBed,
    LightGrayCandle,
    LightGrayCarpet,
    LightGrayConcrete,
    LightGrayConcretePowder,
    LightGrayDye,
    LightGrayGlazedTerracotta,
    LightGrayShulkerBox,
    LightGrayStainedGlass,
    LightGrayStainedGlassPane,
    LightGrayTerracotta,
    LightGrayWool,
    LightWeightedPressurePlate,
    LightningRod,
    Lilac,
    LilyOfTheValley,
    LilyPad,
    LimeBanner,
    LimeBed,
    LimeCandle,
    LimeCarpet,
    LimeConcrete,
    LimeConcretePowder,
    LimeDye,
    LimeGlazedTerracotta,
    LimeShulkerBox,
    LimeStainedGlass,
    LimeStainedGlassPane,
    LimeTerracotta,
    LimeWool,
    LingeringPotion,
    LlamaSpawnEgg,
    Lodestone,
    Loom,
    MagentaBanner,
    MagentaBed,
    MagentaCandle,
    MagentaCarpet,
    MagentaConcrete,
    MagentaConcretePowder,
    MagentaDye,
    MagentaGlazedTerracotta,
    MagentaShulkerBox,
    MagentaStainedGlass,
    MagentaStainedGlassPane,
    MagentaTerracotta,
    MagentaWool,
    MagmaBlock,
    MagmaCream,
    MagmaCubeSpawnEgg,
    MangroveBoat,
    MangroveButton,
    MangroveChestBoat,
    MangroveDoor,
    MangroveFence,
    MangroveFenceGate,
    MangroveHangingSign,
    MangroveLeaves,
    MangroveLog,
    MangrovePlanks,
    MangrovePressurePlate,
    MangrovePropagule,
    MangroveRoots,
    MangroveSign,
    MangroveSlab,
    MangroveStairs,
    MangroveTrapdoor,
    MangroveWood,
    Map,
    MediumAmethystBud,
    Melon,
    MelonSeeds,
    MelonSlice,
    MilkBucket,
    Minecart,
    MojangBannerPattern,
    MooshroomSpawnEgg,
    MossBlock,
    MossCarpet,
    MossyCobblestone,
    MossyCobblestoneSlab,
    MossyCobblestoneStairs,
    MossyCobblestoneWall,
    MossyStoneBrickSlab,
    MossyStoneBrickStairs,
    MossyStoneBrickWall,
    MossyStoneBricks,
    Mud,
    MudBrickSlab,
    MudBrickStairs,
    MudBrickWall,
    MudBricks,
    MuddyMangroveRoots,
    MuleSpawnEgg,
    MushroomStem,
    MushroomStew,
    MusicDisc11,
    MusicDisc13,
    MusicDisc5,
    MusicDiscBlocks,
    MusicDiscCat,
    MusicDiscChirp,
    MusicDiscFar,
    MusicDiscMall,
    MusicDiscMellohi,
    MusicDiscOtherside,
    MusicDiscPigstep,
    MusicDiscStal,
    MusicDiscStrad,
    MusicDiscWait,
    MusicDiscWard,
    Mutton,
    Mycelium,
    NameTag,
    NautilusShell,
    NetherBrick,
    NetherBrickFence,
    NetherBrickSlab,
    NetherBrickStairs,
    NetherBrickWall,
    NetherBricks,
    NetherGoldOre,
    NetherQuartzOre,
    NetherSprouts,
    NetherStar,
    NetherWart,
    NetherWartBlock,
    NetheriteAxe,
    NetheriteBlock,
    NetheriteBoots,
    NetheriteChestplate,
    NetheriteHelmet,
    NetheriteHoe,
    NetheriteIngot,
    NetheriteLeggings,
    NetheritePickaxe,
    NetheriteScrap,
    NetheriteShovel,
    NetheriteSword,
    Netherrack,
    NoteBlock,
    OakBoat,
    OakButton,
    OakChestBoat,
    OakDoor,
    OakFence,
    OakFenceGate,
    OakHangingSign,
    OakLeaves,
    OakLog,
    OakPlanks,
    OakPressurePlate,
    OakSapling,
    OakSign,
    OakSlab,
    OakStairs,
    OakTrapdoor,
    OakWood,
    Observer,
    Obsidian,
    OcelotSpawnEgg,
    OchreFroglight,
    OrangeBanner,
    OrangeBed,
    OrangeCandle,
    OrangeCarpet,
    OrangeConcrete,
    OrangeConcretePowder,
    OrangeDye,
    OrangeGlazedTerracotta,
    OrangeShulkerBox,
    OrangeStainedGlass,
    OrangeStainedGlassPane,
    OrangeTerracotta,
    OrangeTulip,
    OrangeWool,
    OxeyeDaisy,
    OxidizedCopper,
    OxidizedCutCopper,
    OxidizedCutCopperSlab,
    OxidizedCutCopperStairs,
    PackedIce,
    PackedMud,
    Painting,
    PandaSpawnEgg,
    Paper,
    ParrotSpawnEgg,
    PearlescentFroglight,
    Peony,
    PetrifiedOakSlab,
    PhantomMembrane,
    PhantomSpawnEgg,
    PigSpawnEgg,
    PiglinBannerPattern,
    PiglinBruteSpawnEgg,
    PiglinHead,
    PiglinSpawnEgg,
    PillagerSpawnEgg,
    PinkBanner,
    PinkBed,
    PinkCandle,
    PinkCarpet,
    PinkConcrete,
    PinkConcretePowder,
    PinkDye,
    PinkGlazedTerracotta,
    PinkShulkerBox,
    PinkStainedGlass,
    PinkStainedGlassPane,
    PinkTerracotta,
    PinkTulip,
    PinkWool,
    Piston,
    PlayerHead,
    Podzol,
    PointedDripstone,
    PoisonousPotato,
    PolarBearSpawnEgg,
    PolishedAndesite,
    PolishedAndesiteSlab,
    PolishedAndesiteStairs,
    PolishedBasalt,
    PolishedBlackstone,
    PolishedBlackstoneBrickSlab,
    PolishedBlackstoneBrickStairs,
    PolishedBlackstoneBrickWall,
    PolishedBlackstoneBricks,
    PolishedBlackstoneButton,
    PolishedBlackstonePressurePlate,
    PolishedBlackstoneSlab,
    PolishedBlackstoneStairs,
    PolishedBlackstoneWall,
    PolishedDeepslate,
    PolishedDeepslateSlab,
    PolishedDeepslateStairs,
    PolishedDeepslateWall,
    PolishedDiorite,
    PolishedDioriteSlab,
    PolishedDioriteStairs,
    PolishedGranite,
    PolishedGraniteSlab,
    PolishedGraniteStairs,
    PoppedChorusFruit,
    Poppy,
    Porkchop,
    Potato,
    Potion,
    PowderSnowBucket,
    PoweredRail,
    Prismarine,
    PrismarineBrickSlab,
    PrismarineBrickStairs,
    PrismarineBricks,
    PrismarineCrystals,
    PrismarineShard,
    PrismarineSlab,
    PrismarineStairs,
    PrismarineWall,
    Pufferfish,
    PufferfishBucket,
    PufferfishSpawnEgg,
    Pumpkin,
    PumpkinPie,
    PumpkinSeeds,
    PurpleBanner,
    PurpleBed,
    PurpleCandle,
    PurpleCarpet,
    PurpleConcrete,
    PurpleConcretePowder,
    PurpleDye,
    PurpleGlazedTerracotta,
    PurpleShulkerBox,
    PurpleStainedGlass,
    PurpleStainedGlassPane,
    PurpleTerracotta,
    PurpleWool,
    PurpurBlock,
    PurpurPillar,
    PurpurSlab,
    PurpurStairs,
    Quartz,
    QuartzBlock,
    QuartzBricks,
    QuartzPillar,
    QuartzSlab,
    QuartzStairs,
    Rabbit,
    RabbitFoot,
    RabbitHide,
    RabbitSpawnEgg,
    RabbitStew,
    Rail,
    RavagerSpawnEgg,
    RawCopper,
    RawCopperBlock,
    RawGold,
    RawGoldBlock,
    RawIron,
    RawIronBlock,
    RecoveryCompass,
    RedBanner,
    RedBed,
    RedCandle,
    RedCarpet,
    RedConcrete,
    RedConcretePowder,
    RedDye,
    RedGlazedTerracotta,
    RedMushroom,
    RedMushroomBlock,
    RedNetherBrickSlab,
    RedNetherBrickStairs,
    RedNetherBrickWall,
    RedNetherBricks,
    RedSand,
    RedSandstone,
    RedSandstoneSlab,
    RedSandstoneStairs,
    RedSandstoneWall,
    RedShulkerBox,
    RedStainedGlass,
    RedStainedGlassPane,
    RedTerracotta,
    RedTulip,
    RedWool,
    Redstone,
    RedstoneBlock,
    RedstoneLamp,
    RedstoneOre,
    RedstoneTorch,
    ReinforcedDeepslate,
    Repeater,
    RepeatingCommandBlock,
    RespawnAnchor,
    RootedDirt,
    RoseBush,
    RottenFlesh,
    Saddle,
    Salmon,
    SalmonBucket,
    SalmonSpawnEgg,
    Sand,
    Sandstone,
    SandstoneSlab,
    SandstoneStairs,
    SandstoneWall,
    Scaffolding,
    Sculk,
    SculkCatalyst,
    SculkSensor,
    SculkShrieker,
    SculkVein,
    Scute,
    SeaLantern,
    SeaPickle,
    Seagrass,
    Shears,
    SheepSpawnEgg,
    Shield,
    Shroomlight,
    ShulkerBox,
    ShulkerShell,
    ShulkerSpawnEgg,
    SilverfishSpawnEgg,
    SkeletonHorseSpawnEgg,
    SkeletonSkull,
    SkeletonSpawnEgg,
    SkullBannerPattern,
    SlimeBall,
    SlimeBlock,
    SlimeSpawnEgg,
    SmallAmethystBud,
    SmallDripleaf,
    SmithingTable,
    Smoker,
    SmoothBasalt,
    SmoothQuartz,
    SmoothQuartzSlab,
    SmoothQuartzStairs,
    SmoothRedSandstone,
    SmoothRedSandstoneSlab,
    SmoothRedSandstoneStairs,
    SmoothSandstone,
    SmoothSandstoneSlab,
    SmoothSandstoneStairs,
    SmoothStone,
    SmoothStoneSlab,
    Snow,
    SnowBlock,
    SnowGolemSpawnEgg,
    Snowball,
    SoulCampfire,
    SoulLantern,
    SoulSand,
    SoulSoil,
    SoulTorch,
    Spawner,
    SpectralArrow,
    SpiderEye,
    SpiderSpawnEgg,
    SplashPotion,
    Sponge,
    SporeBlossom,
    SpruceBoat,
    SpruceButton,
    SpruceChestBoat,
    SpruceDoor,
    SpruceFence,
    SpruceFenceGate,
    SpruceHangingSign,
    SpruceLeaves,
    SpruceLog,
    SprucePlanks,
    SprucePressurePlate,
    SpruceSapling,
    SpruceSign,
    SpruceSlab,
    SpruceStairs,
    SpruceTrapdoor,
    SpruceWood,
    Spyglass,
    SquidSpawnEgg,
    Stick,
    StickyPiston,
    Stone,
    StoneAxe,
    StoneBrickSlab,
    StoneBrickStairs,
    StoneBrickWall,
    StoneBricks,
    StoneButton,
    StoneHoe,
    StonePickaxe,
    StonePressurePlate,
    StoneShovel,
    StoneSlab,
    StoneStairs,
    StoneSword,
    Stonecutter,
    StraySpawnEgg,
    StriderSpawnEgg,
    String,
    StrippedAcaciaLog,
    StrippedAcaciaWood,
    StrippedBambooBlock,
    StrippedBirchLog,
    StrippedBirchWood,
    StrippedCrimsonHyphae,
    StrippedCrimsonStem,
    StrippedDarkOakLog,
    StrippedDarkOakWood,
    StrippedJungleLog,
    StrippedJungleWood,
    StrippedMangroveLog,
    StrippedMangroveWood,
    StrippedOakLog,
    StrippedOakWood,
    StrippedSpruceLog,
    StrippedSpruceWood,
    StrippedWarpedHyphae,
    StrippedWarpedStem,
    StructureBlock,
    StructureVoid,
    Sugar,
    SugarCane,
    Sunflower,
    SuspiciousStew,
    SweetBerries,
    TadpoleBucket,
    TadpoleSpawnEgg,
    TallGrass,
    Target,
    Terracotta,
    TintedGlass,
    TippedArrow,
    Tnt,
    TntMinecart,
    Torch,
    TotemOfUndying,
    TraderLlamaSpawnEgg,
    TrappedChest,
    Trident,
    TripwireHook,
    TropicalFish,
    TropicalFishBucket,
    TropicalFishSpawnEgg,
    TubeCoral,
    TubeCoralBlock,
    TubeCoralFan,
    Tuff,
    TurtleEgg,
    TurtleHelmet,
    TurtleSpawnEgg,
    TwistingVines,
    VerdantFroglight,
    VexSpawnEgg,
    VillagerSpawnEgg,
    VindicatorSpawnEgg,
    Vine,
    WanderingTraderSpawnEgg,
    WardenSpawnEgg,
    WarpedButton,
    WarpedDoor,
    WarpedFence,
    WarpedFenceGate,
    WarpedFungus,
    WarpedFungusOnAStick,
    WarpedHangingSign,
    WarpedHyphae,
    WarpedNylium,
    WarpedPlanks,
    WarpedPressurePlate,
    WarpedRoots,
    WarpedSign,
    WarpedSlab,
    WarpedStairs,
    WarpedStem,
    WarpedTrapdoor,
    WarpedWartBlock,
    WaterBucket,
    WaxedCopperBlock,
    WaxedCutCopper,
    WaxedCutCopperSlab,
    WaxedCutCopperStairs,
    WaxedExposedCopper,
    WaxedExposedCutCopper,
    WaxedExposedCutCopperSlab,
    WaxedExposedCutCopperStairs,
    WaxedOxidizedCopper,
    WaxedOxidizedCutCopper,
    WaxedOxidizedCutCopperSlab,
    WaxedOxidizedCutCopperStairs,
    WaxedWeatheredCopper,
    WaxedWeatheredCutCopper,
    WaxedWeatheredCutCopperSlab,
    WaxedWeatheredCutCopperStairs,
    WeatheredCopper,
    WeatheredCutCopper,
    WeatheredCutCopperSlab,
    WeatheredCutCopperStairs,
    WeepingVines,
    WetSponge,
    Wheat,
    WheatSeeds,
    WhiteBanner,
    WhiteBed,
    WhiteCandle,
    WhiteCarpet,
    WhiteConcrete,
    WhiteConcretePowder,
    WhiteDye,
    WhiteGlazedTerracotta,
    WhiteShulkerBox,
    WhiteStainedGlass,
    WhiteStainedGlassPane,
    WhiteTerracotta,
    WhiteTulip,
    WhiteWool,
    WitchSpawnEgg,
    WitherRose,
    WitherSkeletonSkull,
    WitherSkeletonSpawnEgg,
    WitherSpawnEgg,
    WolfSpawnEgg,
    WoodenAxe,
    WoodenHoe,
    WoodenPickaxe,
    WoodenShovel,
    WoodenSword,
    WritableBook,
    WrittenBook,
    YellowBanner,
    YellowBed,
    YellowCandle,
    YellowCarpet,
    YellowConcrete,
    YellowConcretePowder,
    YellowDye,
    YellowGlazedTerracotta,
    YellowShulkerBox,
    YellowStainedGlass,
    YellowStainedGlassPane,
    YellowTerracotta,
    YellowWool,
    ZoglinSpawnEgg,
    ZombieHead,
    ZombieHorseSpawnEgg,
    ZombieSpawnEgg,
    ZombieVillagerSpawnEgg,
    ZombifiedPiglinSpawnEgg,
}
public class Item {
    /// <summary>
    /// Item Type
    /// </summary>
    public ItemType Type;

    /// <summary>
    /// Item Count
    /// </summary>
    public int Count;

    /// <summary>
    /// Item Metadata
    /// </summary>
    public Dictionary<string, object>? NBT;

    /// <summary>
    /// Create an item with ItemType, Count and Metadata
    /// </summary>
    /// <param name="itemType">Type of the item</param>
    /// <param name="count">Item Count</param>
    /// <param name="nbt">Item Metadata</param>
    public Item(ItemType itemType, int count, Dictionary<string, object>? nbt) {
        Type = itemType;
        Count = count;
        NBT = nbt;
    }

    /// <summary>
    /// Check if the item slot is empty
    /// </summary>
    /// <returns>TRUE if the item is empty</returns>
    public bool IsEmpty {
        get { return Type == ItemType.Air || Count == 0; }
    }

    /// <summary>
    /// Retrieve item display name from NBT properties. NULL if no display name is defined.
    /// </summary>
    public string? DisplayName {
        get {
            if (NBT != null && NBT.ContainsKey("display")) {
                if (
                    NBT["display"] is Dictionary<string, object> displayProperties
                    && displayProperties.ContainsKey("Name")
                ) {
                    string? displayName = displayProperties["Name"] as string;
                    if (!String.IsNullOrEmpty(displayName))
                        return MinecraftClient.Protocol.ChatParser.ParseText(
                            displayProperties["Name"].ToString() ?? string.Empty
                        );
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Retrieve item lores from NBT properties. Returns null if no lores is defined.
    /// </summary>
    public string[]? Lores {
        get {
            List<string> lores = new();
            if (NBT != null && NBT.ContainsKey("display")) {
                if (
                    NBT["display"] is Dictionary<string, object> displayProperties
                    && displayProperties.ContainsKey("Lore")
                ) {
                    object[] displayName = (object[])displayProperties["Lore"];
                    lores.AddRange(
                        from string st in displayName
                        let str = MinecraftClient.Protocol.ChatParser.ParseText(st.ToString())
                        select str
                    );
                    return lores.ToArray();
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Retrieve item damage from NBT properties. Returns 0 if no damage is defined.
    /// </summary>
    public int Damage {
        get {
            if (NBT != null && NBT.ContainsKey("Damage")) {
                object damage = NBT["Damage"];
                if (damage != null) {
                    return int.Parse(damage.ToString() ?? string.Empty);
                }
            }
            return 0;
        }
    }

    public override string ToString() {
        StringBuilder sb = new();

        sb.AppendFormat("x{0,-2} {1}", Count, Type.ToString());

        string? displayName = DisplayName;
        if (!String.IsNullOrEmpty(displayName))
            sb.AppendFormat(" - {0}§8", displayName);

        int damage = Damage;
        if (damage != 0)
            sb.AppendFormat(" | {0}: {1}", Translations.Get("cmd.inventory.damage"), damage);

        return sb.ToString();
    }
}
public enum Hand {
    MainHand = 0,
    OffHand = 1,
}
public class Container {
    /// <summary>
    /// ID of the container on the server
    /// </summary>
    public int ID;

    /// <summary>
    /// Type of container
    /// </summary>
    public ContainerType Type;

    /// <summary>
    /// title of container
    /// </summary>
    public string? Title;

    /// <summary>
    /// state of container
    /// </summary>
    public int StateID;

    /// <summary>
    /// Container Items
    /// </summary>
    public Dictionary<int, Item> Items;

    /// <summary>
    /// Container Properties
    /// Used for Frunaces, Enchanting Table, Beacon, Brewing stand, Stone cutter, Loom and Lectern
    /// More info about: https://wiki.vg/Protocol#Set_Container_Property
    /// </summary>
    public Dictionary<int, short> Properties;

    /// <summary>
    /// Create an empty container with ID, Type and Title
    /// </summary>
    /// <param name="id">Container ID</param>
    /// <param name="type">Container Type</param>
    /// <param name="title">Container Title</param>
    public Container(int id, ContainerType type, string title) {
        ID = id;
        Type = type;
        Title = title;
        Items = new Dictionary<int, Item>();
        Properties = new Dictionary<int, short>();
    }

    /// <summary>
    /// Create a container with ID, Type, Title and Items
    /// </summary>
    /// <param name="id">Container ID</param>
    /// <param name="type">Container Type</param>
    /// <param name="title">Container Title</param>
    /// <param name="items">Container Items (key: slot ID, value: item info)</param>
    public Container(int id, ContainerType type, string? title, Dictionary<int, Item> items) {
        ID = id;
        Type = type;
        Title = title;
        Items = items;
        Properties = new Dictionary<int, short>();
    }

    /// <summary>
    /// Create an empty container with ID, Type and Title
    /// </summary>
    /// <param name="id">Container ID</param>
    /// <param name="type">Container Type</param>
    /// <param name="title">Container title</param>
    public Container(int id, ContainerTypeOld type, string title) {
        ID = id;
        Title = title;
        Type = ConvertType.ToNew(type);
        Items = new Dictionary<int, Item>();
        Properties = new Dictionary<int, short>();
    }

    /// <summary>
    /// Create an empty container with ID, Type and Title
    /// </summary>
    /// <param name="id">Container ID</param>
    /// <param name="typeID">Container Type</param>
    /// <param name="title">Container Title</param>
    public Container(int id, int typeID, string title) {
        ID = id;
        Type = GetContainerType(typeID);
        Title = title;
        Items = new Dictionary<int, Item>();
        Properties = new Dictionary<int, short>();
    }

    /// <summary>
    /// Create an empty container with Type
    /// </summary>
    /// <param name="type">Container Type</param>
    public Container(ContainerType type) {
        ID = -1;
        Type = type;
        Title = null;
        Items = new Dictionary<int, Item>();
        Properties = new Dictionary<int, short>();
    }

    /// <summary>
    /// Create an empty container with T^ype and Items
    /// </summary>
    /// <param name="type">Container Type</param>
    /// <param name="items">Container Items (key: slot ID, value: item info)</param>
    public Container(ContainerType type, Dictionary<int, Item> items) {
        ID = -1;
        Type = type;
        Title = null;
        Items = items;
        Properties = new Dictionary<int, short>();
    }

    /// <summary>
    /// Get container type from Type ID
    /// </summary>
    /// <param name="typeID">Container Type ID</param>
    /// <returns>Container Type</returns>
    public static ContainerType GetContainerType(int typeID) {
        // https://wiki.vg/Inventory didn't state the inventory ID, assume that list start with 0
        return typeID switch {
            0 => ContainerType.Generic_9x1,
            1 => ContainerType.Generic_9x2,
            2 => ContainerType.Generic_9x3,
            3 => ContainerType.Generic_9x4,
            4 => ContainerType.Generic_9x5,
            5 => ContainerType.Generic_9x6,
            6 => ContainerType.Generic_3x3,
            7 => ContainerType.Anvil,
            8 => ContainerType.Beacon,
            9 => ContainerType.BlastFurnace,
            10 => ContainerType.BrewingStand,
            11 => ContainerType.Crafting,
            12 => ContainerType.Enchantment,
            13 => ContainerType.Furnace,
            14 => ContainerType.Grindstone,
            15 => ContainerType.Hopper,
            16 => ContainerType.Lectern,
            17 => ContainerType.Loom,
            18 => ContainerType.Merchant,
            19 => ContainerType.ShulkerBox,
            20 => ContainerType.Smoker,
            21 => ContainerType.Cartography,
            22 => ContainerType.Stonecutter,
            _ => ContainerType.Unknown,
        };
    }

    /// <summary>
    /// Search an item in the container
    /// </summary>
    /// <param name="itemType">The item to search</param>
    /// <returns>An array of slot ID</returns>
    public int[] SearchItem(ItemType itemType) {
        List<int> result = new();
        if (Items != null) {
            foreach (var item in Items) {
                if (item.Value.Type == itemType)
                    result.Add(item.Key);
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// List empty slots in the container
    /// </summary>
    /// <returns>An array of slot ID</returns>
    /// <remarks>Also depending on the container type, some empty slots cannot be used e.g. armor slots. This might cause issues.</remarks>
    public int[] GetEmpytSlots() {
        List<int> result = new();
        for (int i = 0; i < Type.SlotCount(); i++) {
            result.Add(i);
        }
        foreach (var item in Items) {
            result.Remove(item.Key);
        }
        return result.ToArray();
    }

    /// <summary>
    /// Check the given slot ID is a hotbar slot and give the hotbar number
    /// </summary>
    /// <param name="slotId">The slot ID to check</param>
    /// <param name="hotbar">Zero-based, 0-8. -1 if not a hotbar</param>
    /// <returns>True if given slot ID is a hotbar slot</returns>
    public bool IsHotbar(int slotId, out int hotbar) {
        int hotbarStart = Type.SlotCount() - 9;
        // Remove offhand slot
        if (Type == ContainerType.PlayerInventory)
            hotbarStart--;
        if ((slotId >= hotbarStart) && (slotId <= hotbarStart + 9)) {
            hotbar = slotId - hotbarStart;
            return true;
        } else {
            hotbar = -1;
            return false;
        }
    }
}
public abstract class ChatBot {
    public enum DisconnectReason {
        InGameKick,
        LoginRejected,
        ConnectionLost,
        UserLogout
    };

    //Handler will be automatically set on bot loading, don't worry about this
    public void SetHandler(McClient handler) {
        _handler = handler;
    }

    protected void SetMaster(ChatBot master) {
        this.master = master;
    }

    protected void LoadBot(ChatBot bot) {
        Handler.BotUnLoad(bot);
        Handler.BotLoad(bot);
    }

    protected List<ChatBot> GetLoadedChatBots() {
        return Handler.GetLoadedChatBots();
    }

    protected void UnLoadBot(ChatBot bot) {
        Handler.BotUnLoad(bot);
    }

    private McClient? _handler = null;
    private ChatBot? master = null;
    private readonly List<string> registeredPluginChannels = new();
    private readonly object delayTasksLock = new();
    private readonly List<TaskWithDelay> delayedTasks = new();
    protected McClient Handler {
        get {
            if (master != null)
                return master.Handler;
            if (_handler != null)
                return _handler;
            throw new InvalidOperationException(Translations.exception_chatbot_init);
        }
    }

    /// <summary>
    /// Will be called every ~100ms.
    /// </summary>
    /// <remarks>
    /// <see cref="Update"/> method can be overridden by child class so need an extra update method
    /// </remarks>
    public void UpdateInternal() {
        lock (delayTasksLock) {
            if (delayedTasks.Count > 0) {
                List<int> tasksToRemove = new();
                for (int i = 0; i < delayedTasks.Count; i++) {
                    if (delayedTasks[i].Tick()) {
                        delayedTasks[i].Task();
                        tasksToRemove.Add(i);
                    }
                }
                if (tasksToRemove.Count > 0) {
                    tasksToRemove.Sort((a, b) => b.CompareTo(a)); // descending sort
                    foreach (int index in tasksToRemove) {
                        delayedTasks.RemoveAt(index);
                    }
                }
            }
        }
    }

    /* ================================================== */
    /*   Main methods to override for creating your bot   */
    /* ================================================== */

    /// <summary>
    /// Anything you want to initialize your bot, will be called on load by MinecraftCom
    /// This method is called only once, whereas AfterGameJoined() is called once per server join.
    ///
    /// NOTE: Chat messages cannot be sent at this point in the login process.
    /// If you want to send a message when the bot is loaded, use AfterGameJoined.
    /// </summary>
    public virtual void Initialize() { }

    /// <summary>
    /// This method is called when the bot is being unloaded, you can use it to free up resources like DB connections
    /// </summary>
    public virtual void OnUnload() { }

    /// <summary>
    /// Called after the server has been joined successfully and chat messages are able to be sent.
    /// This method is called again after reconnecting to the server, whereas Initialize() is called only once.
    ///
    /// NOTE: This is not always right after joining the server - if the bot was loaded after logging
    /// in this is still called.
    /// </summary>
    public virtual void AfterGameJoined() { }

    /// <summary>
    /// Will be called every ~100ms (10fps) if loaded in MinecraftCom
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// Will be called every player break block in gamemode 0
    /// </summary>
    /// <param name="entity">Player</param>
    /// <param name="location">Block location</param>
    /// <param name="stage">Destroy stage, maximum 255</param>
    public virtual void OnBlockBreakAnimation(Entity entity, Location location, byte stage) { }

    /// <summary>
    /// Will be called every animations of the hit and place block
    /// </summary>
    /// <param name="entity">Player</param>
    /// <param name="animation">0 = LMB, 1 = RMB (RMB Corrent not work)</param>
    public virtual void OnEntityAnimation(Entity entity, byte animation) { }

    /// <summary>
    /// Any text sent by the server will be sent here by MinecraftCom
    /// </summary>
    /// <param name="text">Text from the server</param>
    public virtual void GetText(string text) { }

    /// <summary>
    /// Any text sent by the server will be sent here by MinecraftCom (extended variant)
    /// </summary>
    /// <remarks>
    /// You can use Json.ParseJson() to process the JSON string.
    /// </remarks>
    /// <param name="text">Text from the server</param>
    /// <param name="json">Raw JSON from the server. This parameter will be NULL on MC 1.5 or lower!</param>
    public virtual void GetText(string text, string? json) { }

    /// <summary>
    /// Is called when the client has been disconnected fom the server
    /// </summary>
    /// <param name="reason">Disconnect Reason</param>
    /// <param name="message">Kick message, if any</param>
    /// <returns>Return TRUE if the client is about to restart</returns>
    public virtual bool OnDisconnect(DisconnectReason reason, string message) {
        return false;
    }

    /// <summary>
    /// Called when a plugin channel message is received.
    /// The given channel must have previously been registered with RegisterPluginChannel.
    /// This can be used to communicate with server mods or plugins.  See wiki.vg for more
    /// information about plugin channels: http://wiki.vg/Plugin_channel
    /// </summary>
    /// <param name="channel">The name of the channel</param>
    /// <param name="data">The payload for the message</param>
    public virtual void OnPluginMessage(string channel, byte[] data) { }

    /// <summary>
    /// Called when properties for the Player entity are received from the server
    /// </summary>
    /// <param name="prop">Dictionary of player properties</param>
    public virtual void OnPlayerProperty(Dictionary<string, double> prop) { }

    /// <summary>
    /// Called when server TPS are recalculated by MCC based on world time updates
    /// </summary>
    /// <param name="tps">New estimated server TPS (between 0 and 20)</param>
    public virtual void OnServerTpsUpdate(double tps) { }

    /// <summary>
    /// Called when a time changed
    /// </summary>
    /// <param name="WorldAge">World age</param>
    /// <param name="TimeOfDay">Time</param>
    public virtual void OnTimeUpdate(long WorldAge, long TimeOfDay) { }

    /// <summary>
    /// Called when an entity moved nearby
    /// </summary>
    /// <param name="entity">Entity with updated location</param>
    public virtual void OnEntityMove(Entity entity) { }

    /// <summary>
    /// Called after an internal MCC command has been performed
    /// </summary>
    /// <param name="commandName">MCC Command Name</param>
    /// <param name="commandParams">MCC Command Parameters</param>
    /// <param name="Result">MCC command result</param>
    public virtual void OnInternalCommand(
        string commandName,
        string commandParams,
        CmdResult Result
    ) { }

    /// <summary>
    /// Called when an entity spawned nearby
    /// </summary>
    /// <param name="entity">New Entity</param>
    public virtual void OnEntitySpawn(Entity entity) { }

    /// <summary>
    /// Called when an entity despawns/dies nearby
    /// </summary>
    /// <param name="entity">Entity wich has just disappeared</param>
    public virtual void OnEntityDespawn(Entity entity) { }

    /// <summary>
    /// Called when the player held item has changed
    /// </summary>
    /// <param name="slot">New slot ID</param>
    public virtual void OnHeldItemChange(byte slot) { }

    /// <summary>
    /// Called when the player health has been updated
    /// </summary>
    /// <param name="health">New player health</param>
    /// <param name="food">New food level</param>
    public virtual void OnHealthUpdate(float health, int food) { }

    /// <summary>
    /// Called when an explosion occurs on the server
    /// </summary>
    /// <param name="explode">Explosion location</param>
    /// <param name="strength">Explosion strength</param>
    /// <param name="recordcount">Amount of blocks blown up</param>
    public virtual void OnExplosion(Location explode, float strength, int recordcount) { }

    /// <summary>
    /// Called when experience updates
    /// </summary>
    /// <param name="Experiencebar">Between 0 and 1</param>
    /// <param name="Level">Level</param>
    /// <param name="TotalExperience">Total Experience</param>
    public virtual void OnSetExperience(float Experiencebar, int Level, int TotalExperience) { }

    /// <summary>
    /// Called when the Game Mode has been updated for a player
    /// </summary>
    /// <param name="playername">Player Name</param>
    /// <param name="uuid">Player UUID</param>
    /// <param name="gamemode">New Game Mode (0: Survival, 1: Creative, 2: Adventure, 3: Spectator).</param>
    public virtual void OnGamemodeUpdate(string playername, Guid uuid, int gamemode) { }

    /// <summary>
    /// Called when the Latency has been updated for a player
    /// </summary>
    /// <param name="playername">Player Name</param>
    /// <param name="uuid">Player UUID</param>
    /// <param name="latency">Latency.</param>
    public virtual void OnLatencyUpdate(string playername, Guid uuid, int latency) { }

    /// <summary>
    /// Called when the Latency has been updated for a player
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <param name="playername">Player Name</param>
    /// <param name="uuid">Player UUID</param>
    /// <param name="latency">Latency.</param>
    public virtual void OnLatencyUpdate(
        Entity entity,
        string playername,
        Guid uuid,
        int latency
    ) { }

    /// <summary>
    /// Called when an update of the map is sent by the server, take a look at https://wiki.vg/Protocol#Map_Data for more info on the fields
    /// Map format and colors: https://minecraft.fandom.com/wiki/Map_item_format
    /// </summary>
    /// <param name="mapid">Map ID of the map being modified</param>
    /// <param name="scale">A scale of the Map, from 0 for a fully zoomed-in map (1 block per pixel) to 4 for a fully zoomed-out map (16 blocks per pixel)</param>
    /// <param name="trackingposition">Specifies whether player and item frame icons are shown </param>
    /// <param name="locked">True if the map has been locked in a cartography table </param>
    /// <param name="icons">A list of MapIcon objects of map icons, send only if trackingPosition is true</param>
    /// <param name="columnsUpdated">Numbs of columns that were updated (map width) (NOTE: If it is 0, the next fields are not used/are set to default values of 0 and null respectively)</param>
    /// <param name="rowsUpdated">Map height</param>
    /// <param name="mapCoulmnX">x offset of the westernmost column</param>
    /// <param name="mapRowZ">z offset of the northernmost row</param>
    /// <param name="colors">a byte array of colors on the map</param>
    public virtual void OnMapData(
        int mapid,
        byte scale,
        bool trackingPosition,
        bool locked,
        List<MapIcon> icons,
        byte columnsUpdated,
        byte rowsUpdated,
        byte mapCoulmnX,
        byte mapRowZ,
        byte[]? colors
    ) { }

    /// <summary>
    /// Called when tradeList is received from server
    /// </summary>
    /// <param name="windowID">Window ID</param>
    /// <param name="trades">List of trades.</param>
    /// <param name="villagerInfo">Contains Level, Experience, IsRegularVillager and CanRestock .</param>
    public virtual void OnTradeList(
        int windowID,
        List<VillagerTrade> trades,
        VillagerInfo villagerInfo
    ) { }

    /// <summary>
    /// Called when received a title from the server
    /// <param name="action"> 0 = set title, 1 = set subtitle, 3 = set action bar, 4 = set times and display, 4 = hide, 5 = reset</param>
    /// <param name="titletext"> title text</param>
    /// <param name="subtitletext"> suntitle text</param>
    /// <param name="actionbartext"> action bar text</param>
    /// <param name="fadein"> Fade In</param>
    /// <param name="stay"> Stay</param>
    /// <param name="fadeout"> Fade Out</param>
    /// <param name="json"> json text</param>
    public virtual void OnTitle(
        int action,
        string titletext,
        string subtitletext,
        string actionbartext,
        int fadein,
        int stay,
        int fadeout,
        string json
    ) { }

    /// <summary>
    /// Called when an entity equipped
    /// </summary>
    /// <param name="entity"> Entity</param>
    /// <param name="slot"> Equipment slot. 0: main hand, 1: off hand, 2–5: armor slot (2: boots, 3: leggings, 4: chestplate, 5: helmet)</param>
    /// <param name="item"> Item)</param>
    public virtual void OnEntityEquipment(Entity entity, int slot, Item? item) { }

    /// <summary>
    /// Called when an entity has effect applied
    /// </summary>
    /// <param name="entity">entity</param>
    /// <param name="effect">effect id</param>
    /// <param name="amplifier">effect amplifier</param>
    /// <param name="duration">effect duration</param>
    /// <param name="flags">effect flags</param>
    public virtual void OnEntityEffect(
        Entity entity,
        Effects effect,
        int amplifier,
        int duration,
        byte flags
    ) { }

    /// <summary>
    /// Called when a scoreboard objective updated
    /// </summary>
    /// <param name="objectivename">objective name</param>
    /// <param name="mode">0 to create the scoreboard. 1 to remove the scoreboard. 2 to update the display text.</param>
    /// <param name="objectivevalue">Only if mode is 0 or 2. The text to be displayed for the score</param>
    /// <param name="type">Only if mode is 0 or 2. 0 = "integer", 1 = "hearts".</param>
    public virtual void OnScoreboardObjective(
        string objectivename,
        byte mode,
        string objectivevalue,
        int type,
        string json
    ) { }

    /// <summary>
    /// Called when a scoreboard updated
    /// </summary>
    /// <param name="entityname">The entity whose score this is. For players, this is their username; for other entities, it is their UUID.</param>
    /// <param name="action">0 to create/update an item. 1 to remove an item.</param>
    /// <param name="objectivename">The name of the objective the score belongs to</param>
    /// <param name="value">The score to be displayed next to the entry. Only sent when Action does not equal 1.</param>
    public virtual void OnUpdateScore(
        string entityname,
        int action,
        string objectivename,
        int value
    ) { }

    /// <summary>
    /// Called when an inventory/container was updated by server
    /// </summary>
    /// <param name="inventoryId"></param>
    public virtual void OnInventoryUpdate(int inventoryId) { }

    /// <summary>
    /// Called when a container was opened
    /// </summary>
    /// <param name="inventoryId"></param>
    public virtual void OnInventoryOpen(int inventoryId) { }

    /// <summary>
    /// Called when a container was closed
    /// </summary>
    /// <param name="inventoryId"></param>
    public virtual void OnInventoryClose(int inventoryId) { }

    /// <summary>
    /// When received inventory/container/window properties from the server.
    /// Used for Frunaces, Enchanting Table, Beacon, Brewing stand, Stone cutter, Loom and Lectern
    /// More info about: https://wiki.vg/Protocol#Set_Container_Property
    /// </summary>
    /// <param name="inventoryID">Inventory ID</param>
    /// <param name="propertyId">Property ID</param>
    /// <param name="propertyValue">Property Value</param>
    public virtual void OnInventoryProperties(
        byte inventoryID,
        short propertyId,
        short propertyValue
    ) { }

    /// <summary>
    /// When received enchantments from the server this method is called
    /// Enchantment levels are the levels of enchantment (eg. I, II, III, IV, V) (eg. Smite IV, Power III, Knockback II ..)
    /// Enchantment level requirements are the levels that player needs to have in order to enchant the item
    /// </summary>
    /// <param name="topEnchantment">Enchantment in the top most slot</param>
    /// <param name="middleEnchantment">Enchantment in the middle slot</param>
    /// <param name="bottomEnchantment">Enchantment in the bottom slot</param>
    /// <param name="topEnchantmentLevel">Enchantment level for the enchantment in the top most slot</param>
    /// <param name="middleEnchantmentLevel">Enchantment level for the enchantment in the middle slot</param>
    /// <param name="bottomEnchantmentLevel">Enchantment level for the enchantment in the bottom slot</param>
    /// <param name="topEnchantmentLevelRequirement">Levels required by player for the enchantment in the top most slot</param>
    /// <param name="middleEnchantmentLevelRequirement">Levels required by player for the enchantment in the middle slot</param>
    /// <param name="bottomEnchantmentLevelRequirement">Levels required by player for the enchantment in the bottom slot</param>
    public virtual void OnEnchantments(
        Enchantment topEnchantment,
        Enchantment middleEnchantment,
        Enchantment bottomEnchantment,
        short topEnchantmentLevel,
        short middleEnchantmentLevel,
        short bottomEnchantmentLevel,
        short topEnchantmentLevelRequirement,
        short middleEnchantmentLevelRequirement,
        short bottomEnchantmentLevelRequirement
    ) { }

    /// <summary>
    /// When received enchantments from the server this method is called
    /// Enchantment levels are the levels of enchantment (eg. I, II, III, IV, V) (eg. Smite IV, Power III, Knockback II ..)
    /// Enchantment level requirements are the levels that player needs to have in order to enchant the item
    /// </summary>
    /// <param name="enchantment">Enchantment data/info</param>
    public virtual void OnEnchantments(EnchantmentData enchantment) { }

    /// <summary>
    /// Called when a player joined the game
    /// </summary>
    /// <param name="uuid">UUID of the player</param>
    /// <param name="name">Name of the player</param>
    public virtual void OnPlayerJoin(Guid uuid, string name) { }

    /// <summary>
    /// Called when a player left the game
    /// </summary>
    /// <param name="uuid">UUID of the player</param>
    /// <param name="name">Name of the player</param>
    public virtual void OnPlayerLeave(Guid uuid, string? name) { }

    /// <summary>
    /// This method is called when a player has been killed by another entity
    /// </summary>
    /// <param name="killerEntity">Killer's entity</param>
    /// <param name="chatMessage">message sent in chat when player is killed</param>
    public virtual void OnKilled(Entity killerEntity, string chatMessage) { }

    /// <summary>
    /// Called when the player dies
    /// For getting the info about the player/entity who killed the player use OnPlayerKilled
    /// </summary>
    public virtual void OnDeath() { }

    /// <summary>
    /// Called when the player respawns
    /// </summary>
    public virtual void OnRespawn() { }

    /// <summary>
    /// Called when the health of an entity changed
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <param name="health">The health of the entity</param>
    public virtual void OnEntityHealth(Entity entity, float health) { }

    /// <summary>
    /// Called when the metadata of an entity changed
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <param name="metadata">The metadata of the entity</param>
    public virtual void OnEntityMetadata(Entity entity, Dictionary<int, object?> metadata) { }

    /// <summary>
    /// Called when the status of client player have been changed
    /// </summary>
    /// <param name="statusId"></param>
    public virtual void OnPlayerStatus(byte statusId) { }

    /// <summary>
    /// Called when a network packet received or sent
    /// </summary>
    /// <remarks>
    /// You need to enable this event by calling <see cref="SetNetworkPacketEventEnabled(bool)"/> with True before you can use this event
    /// </remarks>
    /// <param name="packetID">Packet ID</param>
    /// <param name="packetData">A copy of Packet Data</param>
    /// <param name="isLogin">The packet is login phase or playing phase</param>
    /// <param name="isInbound">The packet is received from server or sent by client</param>
    public virtual void OnNetworkPacket(
        int packetID,
        List<byte> packetData,
        bool isLogin,
        bool isInbound
    ) { }

    /// <summary>
    /// Called when the rain level have been changed
    /// </summary>
    /// <param name="level"></param>
    public virtual void OnRainLevelChange(float level) { }

    /// <summary>
    /// Called when the thunder level have been changed
    /// </summary>
    /// <param name="level"></param>
    public virtual void OnThunderLevelChange(float level) { }

    /// <summary>
    /// Called when a block is changed.
    /// </summary>
    /// <param name="location">The location of the block.</param>
    /// <param name="block">The block</param>
    public virtual void OnBlockChange(Location location, Block block) { }

    /* =================================================================== */
    /*  ToolBox - Methods below might be useful while creating your bot.   */
    /*  You should not need to interact with other classes of the program. */
    /*  All the methods in this ChatBot class should do the job for you.   */
    /* =================================================================== */

    /// <summary>
    /// Send text to the server. Can be anything such as chat messages or commands
    /// </summary>
    /// <param name="text">Text to send to the server</param>
    /// <param name="sendImmediately">Bypass send queue (Deprecated, still there for compatibility purposes but ignored)</param>
    /// <returns>TRUE if successfully sent (Deprectated, always returns TRUE for compatibility purposes with existing scripts)</returns>
    protected bool SendText(string text, bool sendImmediately = false) {
        LogToConsole("Sending '" + text + "'");
        Handler.SendText(text);
        return true;
    }

    /// <summary>
    /// Perform an internal MCC command (not a server command, use SendText() instead for that!)
    /// </summary>
    /// <param name="command">The command to process</param>
    /// <param name="localVars">Local variables passed along with the command</param>
    /// <returns>TRUE if the command was indeed an internal MCC command</returns>
    protected bool PerformInternalCommand(
        string command,
        Dictionary<string, object>? localVars = null
    ) {
        CmdResult temp = new();
        return Handler.PerformInternalCommand(command, ref temp, localVars);
    }

    /// <summary>
    /// Perform an internal MCC command (not a server command, use SendText() instead for that!)
    /// </summary>
    /// <param name="command">The command to process</param>
    /// <param name="response_msg">May contain a confirmation or error message after processing the command, or "" otherwise.</param>
    /// <param name="localVars">Local variables passed along with the command</param>
    /// <returns>TRUE if the command was indeed an internal MCC command</returns>
    protected bool PerformInternalCommand(
        string command,
        ref CmdResult result,
        Dictionary<string, object>? localVars = null
    ) {
        return Handler.PerformInternalCommand(command, ref result, localVars);
    }

    /// <summary>
    /// Remove color codes ("§c") from a text message received from the server
    /// </summary>
    public static string GetVerbatim(string? text) {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        int idx = 0;
        var data = new char[text.Length];

        for (int i = 0; i < text.Length; i++)
            if (text[i] != '§')
                data[idx++] = text[i];
            else
                i++;

        return new string(data, 0, idx);
    }

    /// <summary>
    /// Verify that a string contains only a-z A-Z 0-9 and _ characters.
    /// </summary>
    public static bool IsValidName(string username) {
        if (string.IsNullOrEmpty(username))
            return false;

        foreach (char c in username)
            if (!(c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c >= '0' && c <= '9' || c == '_'))
                return false;

        return true;
    }

    /// <summary>
    /// Returns true if the text passed is a private message sent to the bot
    /// </summary>
    /// <param name="text">text to test</param>
    /// <param name="message">if it's a private message, this will contain the message</param>
    /// <param name="sender">if it's a private message, this will contain the player name that sends the message</param>
    /// <returns>Returns true if the text is a private message</returns>
    protected static bool IsPrivateMessage(string text, ref string message, ref string sender) {
        if (string.IsNullOrEmpty(text))
            return false;

        text = GetVerbatim(text);

        //User-defined regex for private chat messages
        if (Config.ChatFormat.UserDefined && !string.IsNullOrWhiteSpace(Config.ChatFormat.Private)) {
            Match regexMatch = Regex.Match(text, Config.ChatFormat.Private);
            if (regexMatch.Success && regexMatch.Groups.Count >= 3) {
                sender = regexMatch.Groups[1].Value;
                message = regexMatch.Groups[2].Value;
                return IsValidName(sender);
            }
        }

        //Built-in detection routine for private messages
        if (Config.ChatFormat.Builtins) {
            string[] tmp = text.Split(' ');
            try {
                //Detect vanilla /tell messages
                //Someone whispers message (MC 1.5)
                //Someone whispers to you: message (MC 1.7)
                if (tmp.Length > 2 && tmp[1] == "whispers") {
                    if (tmp.Length > 4 && tmp[2] == "to" && tmp[3] == "you:") {
                        message = text[(tmp[0].Length + 18)..]; //MC 1.7
                    } else
                        message = text[(tmp[0].Length + 10)..]; //MC 1.5
                    sender = tmp[0];
                    return IsValidName(sender);
                }
                //Detect Essentials (Bukkit) /m messages
                //[Someone -> me] message
                //[~Someone -> me] message
                else if (
                    text[0] == '['
                    && tmp.Length > 3
                    && tmp[1] == "->"
                    && (tmp[2].ToLower() == "me]" || tmp[2].ToLower() == "moi]")
                ) //'me' is replaced by 'moi' in french servers
                {
                    message = text[(tmp[0].Length + 4 + tmp[2].Length + 1)..];
                    sender = tmp[0][1..];
                    if (sender[0] == '~') {
                        sender = sender[1..];
                    }
                    return IsValidName(sender);
                }
                //Detect Modified server messages. /m
                //[Someone @ me] message
                else if (
                    text[0] == '['
                    && tmp.Length > 3
                    && tmp[1] == "@"
                    && (tmp[2].ToLower() == "me]" || tmp[2].ToLower() == "moi]")
                ) //'me' is replaced by 'moi' in french servers
                {
                    message = text[(tmp[0].Length + 4 + tmp[2].Length + 0)..];
                    sender = tmp[0][1..];
                    if (sender[0] == '~') {
                        sender = sender[1..];
                    }
                    return IsValidName(sender);
                }
                //Detect Essentials (Bukkit) /me messages with some custom prefix
                //[Prefix] [Someone -> me] message
                //[Prefix] [~Someone -> me] message
                else if (
                    text[0] == '['
                    && tmp[0][^1] == ']'
                    && tmp[1][0] == '['
                    && tmp.Length > 4
                    && tmp[2] == "->"
                    && (tmp[3].ToLower() == "me]" || tmp[3].ToLower() == "moi]")
                ) {
                    message = text[(tmp[0].Length + 1 + tmp[1].Length + 4 + tmp[3].Length + 1)..];
                    sender = tmp[1][1..];
                    if (sender[0] == '~') {
                        sender = sender[1..];
                    }
                    return IsValidName(sender);
                }
                //Detect Essentials (Bukkit) /me messages with some custom rank
                //[Someone [rank] -> me] message
                //[~Someone [rank] -> me] message
                else if (
                    text[0] == '['
                    && tmp.Length > 3
                    && tmp[2] == "->"
                    && (tmp[3].ToLower() == "me]" || tmp[3].ToLower() == "moi]")
                ) {
                    message = text[(tmp[0].Length + 1 + tmp[1].Length + 4 + tmp[2].Length + 1)..];
                    sender = tmp[0][1..];
                    if (sender[0] == '~') {
                        sender = sender[1..];
                    }
                    return IsValidName(sender);
                }
                //Detect HeroChat PMsend
                //From Someone: message
                else if (text.StartsWith("From ")) {
                    sender = text[5..].Split(':')[0];
                    message = text[(text.IndexOf(':') + 2)..];
                    return IsValidName(sender);
                } else
                    return false;
            } catch (IndexOutOfRangeException) { /* Not an expected chat format */
            } catch (ArgumentOutOfRangeException) { /* Same here */
            }
        }

        return false;
    }

    /// <summary>
    /// Returns true if the text passed is a public message written by a player on the chat
    /// </summary>
    /// <param name="text">text to test</param>
    /// <param name="message">if it's message, this will contain the message</param>
    /// <param name="sender">if it's message, this will contain the player name that sends the message</param>
    /// <returns>Returns true if the text is a chat message</returns>
    protected static bool IsChatMessage(string text, ref string message, ref string sender) {
        if (string.IsNullOrEmpty(text))
            return false;

        text = GetVerbatim(text);

        //User-defined regex for public chat messages
        if (Config.ChatFormat.UserDefined && !string.IsNullOrWhiteSpace(Config.ChatFormat.Public)) {
            Match regexMatch = Regex.Match(text, Config.ChatFormat.Public);
            if (regexMatch.Success && regexMatch.Groups.Count >= 3) {
                sender = regexMatch.Groups[1].Value;
                message = regexMatch.Groups[2].Value;
                return IsValidName(sender);
            }
        }

        //Built-in detection routine for public messages
        if (Config.ChatFormat.Builtins) {
            string[] tmp = text.Split(' ');

            //Detect vanilla/factions Messages
            //<Someone> message
            //<*Faction Someone> message
            //<*Faction Someone>: message
            //<*Faction ~Nicknamed>: message
            if (text[0] == '<') {
                try {
                    text = text[1..];
                    string[] tmp2 = text.Split('>');
                    sender = tmp2[0];
                    message = text[(sender.Length + 2)..];
                    if (message.Length > 1 && message[0] == ' ') {
                        message = message[1..];
                    }
                    tmp2 = sender.Split(' ');
                    sender = tmp2[^1];
                    if (sender[0] == '~') {
                        sender = sender[1..];
                    }
                    return IsValidName(sender);
                } catch (IndexOutOfRangeException) { /* Not a vanilla/faction message */
                } catch (ArgumentOutOfRangeException) { /* Same here */
                }
            }
            //Detect HeroChat Messages
            //Public chat messages
            //[Channel] [Rank] User: Message
            else if (text[0] == '[' && text.Contains(':') && tmp.Length > 2) {
                try {
                    int name_end = text.IndexOf(':');
                    int name_start = text[..name_end].LastIndexOf(']') + 2;
                    sender = text[name_start..name_end];
                    message = text[(name_end + 2)..];
                    return IsValidName(sender);
                } catch (IndexOutOfRangeException) { /* Not a herochat message */
                } catch (ArgumentOutOfRangeException) { /* Same here */
                }
            }
            //Detect (Unknown Plugin) Messages
            //**Faction<Rank> User : Message
            else if (
                text[0] == '*'
                && text.Length > 1
                && text[1] != ' '
                && text.Contains('<')
                && text.Contains('>')
                && text.Contains(' ')
                && text.Contains(':')
                && text.IndexOf('*') < text.IndexOf('<')
                && text.IndexOf('<') < text.IndexOf('>')
                && text.IndexOf('>') < text.IndexOf(' ')
                && text.IndexOf(' ') < text.IndexOf(':')
            ) {
                try {
                    string prefix = tmp[0];
                    string user = tmp[1];
                    string semicolon = tmp[2];
                    if (
                        prefix.All(
                            c =>
                                char.IsLetterOrDigit(c)
                                || new char[] { '*', '<', '>', '_' }.Contains(c)
                        )
                        && semicolon == ":"
                    ) {
                        message = text[(prefix.Length + user.Length + 4)..];
                        return IsValidName(user);
                    }
                } catch (IndexOutOfRangeException) { /* Not a <unknown plugin> message */
                } catch (ArgumentOutOfRangeException) { /* Same here */
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Returns true if the text passed is a teleport request (Essentials)
    /// </summary>
    /// <param name="text">Text to parse</param>
    /// <param name="sender">Will contain the sender's username, if it's a teleport request</param>
    /// <returns>Returns true if the text is a teleport request</returns>
    protected static bool IsTeleportRequest(string text, ref string sender) {
        if (string.IsNullOrEmpty(text))
            return false;

        text = GetVerbatim(text);

        //User-defined regex for teleport requests
        if (
            Config.ChatFormat.UserDefined
            && !string.IsNullOrWhiteSpace(Config.ChatFormat.TeleportRequest)
        ) {
            Match regexMatch = Regex.Match(text, Config.ChatFormat.TeleportRequest);
            if (regexMatch.Success && regexMatch.Groups.Count >= 2) {
                sender = regexMatch.Groups[1].Value;
                return IsValidName(sender);
            }
        }

        //Built-in detection routine for teleport requests
        if (Config.ChatFormat.Builtins) {
            string[] tmp = text.Split(' ');

            //Detect Essentials teleport requests, prossibly with
            //nicknamed names or other modifications such as HeroChat
            if (
                text.EndsWith("has requested to teleport to you.")
                || text.EndsWith("has requested that you teleport to them.")
            ) {
                //<Rank> Username has requested...
                //[Rank] Username has requested...
                if (
                    (
                        tmp[0].StartsWith("<") && tmp[0].EndsWith(">")
                        || tmp[0].StartsWith("[") && tmp[0].EndsWith("]")
                    )
                    && tmp.Length > 1
                )
                    sender = tmp[1];
                else //Username has requested..
                    sender = tmp[0];

                //~Username has requested...
                if (sender.Length > 1 && sender[0] == '~')
                    sender = sender[1..];

                //Final check on username validity
                return IsValidName(sender);
            }
        }

        return false;
    }

    /// <summary>
    /// Write some text in the console. Nothing will be sent to the server.
    /// </summary>
    /// <param name="text">Log text to write</param>
    protected void LogToConsole(object? text) {
        string botName =
            Translations.ResourceManager.GetString("botname." + GetType().Name) ?? GetType().Name;
        if (_handler == null || master == null)
            ConsoleIO.WriteLogLine(string.Format("[{0}] {1}", botName, text));
        else
            Handler.Log.Info(string.Format("[{0}] {1}", botName, text));
        string logfile = Config.AppVar.ExpandVars(Config.Main.Advanced.ChatbotLogFile);

        if (!string.IsNullOrEmpty(logfile)) {
            if (!File.Exists(logfile)) {
                try {
                    Directory.CreateDirectory(Path.GetDirectoryName(logfile)!);
                } catch {
                    return; /* Invalid path or access denied */
                }
                try {
                    File.WriteAllText(logfile, "");
                } catch {
                    return; /* Invalid file name or access denied */
                }
            }

            File.AppendAllLines(logfile, new string[] { GetTimestamp() + ' ' + text });
        }
    }

    protected static void LogToConsole(string originBotName, object? text) {
        string botName = Translations.ResourceManager.GetString(originBotName) ?? originBotName;
        ConsoleIO.WriteLogLine(string.Format("[{0}] {1}", botName, text));
        string logfile = Config.AppVar.ExpandVars(Config.Main.Advanced.ChatbotLogFile);

        if (!string.IsNullOrEmpty(logfile)) {
            if (!File.Exists(logfile)) {
                try {
                    Directory.CreateDirectory(Path.GetDirectoryName(logfile)!);
                } catch {
                    return; /* Invalid path or access denied */
                }
                try {
                    File.WriteAllText(logfile, "");
                } catch {
                    return; /* Invalid file name or access denied */
                }
            }

            File.AppendAllLines(logfile, new string[] { GetTimestamp() + ' ' + text });
        }
    }

    /// <summary>
    /// Write some text in the console, but only if DebugMessages is enabled in INI file. Nothing will be sent to the server.
    /// </summary>
    /// <param name="text">Debug log text to write</param>
    protected void LogDebugToConsole(object text) {
        if (Config.Logging.DebugMessages)
            LogToConsole(text);
    }

    /// <summary>
    /// Write the translated text in the console by giving a translation key. Nothing will be sent to the server.
    /// </summary>
    /// <param name="key">Translation key</param>
    /// <param name="args"></param>
    protected void LogToConsoleTranslated(string key, params object[] args) {
        LogToConsole(string.Format(Translations.ResourceManager.GetString(key) ?? key, args));
    }

    /// <summary>
    /// Write the translated text in the console by giving a translation key, but only if DebugMessages is enabled in INI file. Nothing will be sent to the server.
    /// </summary>
    /// <param name="key">Translation key</param>
    /// <param name="args"></param>
    protected void LogDebugToConsoleTranslated(string key, params object?[] args) {
        LogDebugToConsole(string.Format(Translations.ResourceManager.GetString(key) ?? key, args));
    }

    /// <summary>
    /// Disconnect from the server and restart the program
    /// It will unload and reload all the bots and then reconnect to the server
    /// </summary>
    /// <param name="ExtraAttempts">In case of failure, maximum extra attempts before aborting</param>
    /// <param name="delaySeconds">Optional delay, in seconds, before restarting</param>
    protected void ReconnectToTheServer(
        int ExtraAttempts = 3,
        int delaySeconds = 0,
        bool keepAccountAndServerSettings = false
    ) {
        if (Config.Logging.DebugMessages) {
            string botName =
                Translations.ResourceManager.GetString("botname." + GetType().Name)
                ?? GetType().Name;
            ConsoleIO.WriteLogLine(string.Format(Translations.chatbot_reconnect, botName));
        }
        McClient.ReconnectionAttemptsLeft = ExtraAttempts;
        Program.Restart(delaySeconds, keepAccountAndServerSettings);
    }

    /// <summary>
    /// Disconnect from the server and exit the program
    /// </summary>
    protected void DisconnectAndExit() {
        Program.Exit();
    }

    /// <summary>
    /// Unload the chatbot, and release associated memory.
    /// </summary>
    protected void UnloadBot() {
        Handler.BotUnLoad(this);
    }

    /// <summary>
    /// Send a private message to a player
    /// </summary>
    /// <param name="player">Player name</param>
    /// <param name="message">Message</param>
    protected void SendPrivateMessage(string player, string message) {
        SendText(
            string.Format("/{0} {1} {2}", Config.Main.Advanced.PrivateMsgsCmdName, player, message)
        );
    }

    /// <summary>
    /// Run a script from a file using a Scripting bot
    /// </summary>
    /// <param name="filename">File name</param>
    /// <param name="playername">Player name to send error messages, if applicable</param>
    /// <param name="localVars">Local variables for use in the Script</param>
    protected void RunScript(
        string filename,
        string? playername = null,
        Dictionary<string, object>? localVars = null
    ) {
        Handler.BotLoad(new ChatBots.Script(filename, playername, localVars));
    }

    /// <summary>
    /// Load an additional ChatBot
    /// </summary>
    /// <param name="chatBot">ChatBot to load</param>
    protected void BotLoad(ChatBot chatBot) {
        Handler.BotLoad(chatBot);
    }

    /// <summary>
    /// Check whether Terrain and Movements is enabled.
    /// </summary>
    /// <returns>Enable status.</returns>
    public bool GetTerrainEnabled() {
        return Handler.GetTerrainEnabled();
    }

    /// <summary>
    /// Enable or disable Terrain and Movements.
    /// Please note that Enabling will be deferred until next relog, respawn or world change.
    /// </summary>
    /// <param name="enabled">Enabled</param>
    /// <returns>TRUE if the setting was applied immediately, FALSE if delayed.</returns>
    public bool SetTerrainEnabled(bool enabled) {
        return Handler.SetTerrainEnabled(enabled);
    }

    /// <summary>
    /// Get entity handling status
    /// </summary>
    /// <returns></returns>
    /// <remarks>Entity Handling cannot be enabled in runtime (or after joining server)</remarks>
    public bool GetEntityHandlingEnabled() {
        return Handler.GetEntityHandlingEnabled();
    }

    /// <summary>
    /// start Sneaking
    /// </summary>
    protected bool Sneak(bool on) {
        return SendEntityAction(
            on ? Protocol.EntityActionType.StartSneaking : Protocol.EntityActionType.StopSneaking
        );
    }

    /// <summary>
    /// Send Entity Action
    /// </summary>
    private bool SendEntityAction(Protocol.EntityActionType entityAction) {
        return Handler.SendEntityAction(entityAction);
    }

    /// <summary>
    /// Attempt to dig a block at the specified location
    /// </summary>
    /// <param name="location">Location of block to dig</param>
    /// <param name="swingArms">Also perform the "arm swing" animation</param>
    /// <param name="lookAtBlock">Also look at the block before digging</param>
    protected bool DigBlock(Location location, bool swingArms = true, bool lookAtBlock = true) {
        return Handler.DigBlock(location, swingArms, lookAtBlock);
    }

    /// <summary>
    /// SetSlot
    /// </summary>
    protected void SetSlot(int slotNum) {
        Handler.ChangeSlot((short)slotNum);
    }

    /// <summary>
    /// Get the current Minecraft World
    /// </summary>
    /// <returns>Minecraft world or null if associated setting is disabled</returns>
    protected World GetWorld() {
        return Handler.GetWorld();
    }

    /// <summary>
    /// Get all Entities
    /// </summary>
    /// <returns>All Entities</returns>
    protected Dictionary<int, Entity> GetEntities() {
        return Handler.GetEntities();
    }

    /// <summary>
    /// Get all players Latency
    /// </summary>
    /// <returns>All players latency</returns>
    protected Dictionary<string, int> GetPlayersLatency() {
        return Handler.GetPlayersLatency();
    }

    /// <summary>
    /// Get the current location of the player (Feet location)
    /// </summary>
    /// <returns>Minecraft world or null if associated setting is disabled</returns>
    protected Location GetCurrentLocation() {
        return Handler.GetCurrentLocation();
    }

    /// <summary>
    /// Move to the specified location
    /// </summary>
    /// <param name="location">Location to reach</param>
    /// <param name="allowUnsafe">Allow possible but unsafe locations thay may hurt the player: lava, cactus...</param>
    /// <param name="allowDirectTeleport">Allow non-vanilla direct teleport instead of computing path, but may cause invalid moves and/or trigger anti-cheat plugins</param>
    /// <param name="maxOffset">If no valid path can be found, also allow locations within specified distance of destination</param>
    /// <param name="minOffset">Do not get closer of destination than specified distance</param>
    /// <param name="timeout">How long to wait before stopping computation (default: 5 seconds)</param>
    /// <remarks>When location is unreachable, computation will reach timeout, then optionally fallback to a close location within maxOffset</remarks>
    /// <returns>True if a path has been found</returns>
    protected bool MoveToLocation(
        Location location,
        bool allowUnsafe = false,
        bool allowDirectTeleport = false,
        int maxOffset = 0,
        int minOffset = 0,
        TimeSpan? timeout = null
    ) {
        return Handler.MoveTo(
            location,
            allowUnsafe,
            allowDirectTeleport,
            maxOffset,
            minOffset,
            timeout
        );
    }

    /// <summary>
    /// Check if the client is currently processing a Movement.
    /// </summary>
    /// <returns>true if a movement is currently handled</returns>
    protected bool ClientIsMoving() {
        return Handler.ClientIsMoving();
    }

    /// <summary>
    /// Look at the specified location
    /// </summary>
    /// <param name="location">Location to look at</param>
    protected void LookAtLocation(Location location) {
        Handler.UpdateLocation(Handler.GetCurrentLocation(), location);
    }

    /// <summary>
    /// Look at the specified location
    /// </summary>
    /// <param name="yaw">Yaw to look at</param>
    /// <param name="pitch">Pitch to look at</param>
    protected void LookAtLocation(float yaw, float pitch) {
        Handler.UpdateLocation(Handler.GetCurrentLocation(), yaw, pitch);
    }

    /// <summary>
    /// Find the block on the line of sight.
    /// </summary>
    /// <param name="maxDistance">Maximum distance from sight</param>
    /// <param name="includeFluids">Whether to detect fluid</param>
    /// <returns>Position of the block</returns>
    protected Tuple<bool, Location, Block> GetLookingBlock(
        double maxDistance = 4.5,
        bool includeFluids = false
    ) {
        return RaycastHelper.RaycastBlock(Handler, maxDistance, includeFluids);
    }

    /// <summary>
    /// Get a Y-M-D h:m:s timestamp representing the current system date and time
    /// </summary>
    protected static string GetTimestamp() {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// Get a h:m:s timestamp representing the current system time
    /// </summary>
    protected static string GetShortTimestamp() {
        return DateTime.Now.ToString("HH:mm:ss");
    }

    /// <summary>
    /// Load entries from a file as a string array, removing duplicates and empty lines
    /// </summary>
    /// <param name="file">File to load</param>
    /// <returns>The string array or an empty array if failed to load the file</returns>
    protected string[] LoadDistinctEntriesFromFile(string file) {
        if (File.Exists(file)) {
            //Read all lines from file, remove lines with no text, convert to lowercase,
            //remove duplicate entries, convert to a string array, and return the result.
            return File.ReadAllLines(file, Encoding.UTF8)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.ToLower())
                .Distinct()
                .ToArray();
        } else {
            LogToConsole("File not found: " + Path.GetFullPath(file));
            return Array.Empty<string>();
        }
    }

    /// <summary>
    /// Return the Server Port where the client is connected to
    /// </summary>
    /// <returns>Server Port where the client is connected to</returns>
    protected int GetServerPort() {
        return Handler.GetServerPort();
    }

    /// <summary>
    /// Return the Server Host where the client is connected to
    /// </summary>
    /// <returns>Server Host where the client is connected to</returns>
    protected string GetServerHost() {
        return Handler.GetServerHost();
    }

    /// <summary>
    /// Return the Username of the current account
    /// </summary>
    /// <returns>Username of the current account</returns>
    protected string GetUsername() {
        return Handler.GetUsername();
    }

    /// <summary>
    /// Return the Gamemode of the current account
    /// </summary>
    /// <returns>Username of the current account</returns>
    protected int GetGamemode() {
        return Handler.GetGamemode();
    }

    /// <summary>
    /// Return the head yaw of the client player
    /// </summary>
    /// <returns>Yaw of the client player</returns>
    protected float GetYaw() {
        return Handler.GetYaw();
    }

    /// <summary>
    /// Return the head pitch of the client player
    /// </summary>
    /// <returns>Pitch of the client player</returns>
    protected float GetPitch() {
        return Handler.GetPitch();
    }

    /// <summary>
    /// Return the UserUUID of the current account
    /// </summary>
    /// <returns>UserUUID of the current account</returns>
    protected string GetUserUUID() {
        return Handler.GetUserUuidStr();
    }

    /// <summary>
    /// Return the EntityID of the current player
    /// </summary>
    /// <returns>EntityID of the current player</returns>
    protected int GetPlayerEntityID() {
        return Handler.GetPlayerEntityID();
    }

    /// <summary>
    /// Return the list of currently online players
    /// </summary>
    /// <returns>List of online players</returns>
    protected string[] GetOnlinePlayers() {
        return Handler.GetOnlinePlayers();
    }

    /// <summary>
    /// Get a dictionary of online player names and their corresponding UUID
    /// </summary>
    /// <returns>
    ///     dictionary of online player whereby
    ///     UUID represents the key
    ///     playername represents the value</returns>
    protected Dictionary<string, string> GetOnlinePlayersWithUUID() {
        return Handler.GetOnlinePlayersWithUUID();
    }

    /// <summary>
    /// Registers the given plugin channel for use by this chatbot.
    /// </summary>
    /// <param name="channel">The name of the channel to register</param>
    protected void RegisterPluginChannel(string channel) {
        registeredPluginChannels.Add(channel);
        Handler.RegisterPluginChannel(channel, this);
    }

    /// <summary>
    /// Unregisters the given plugin channel, meaning this chatbot can no longer use it.
    /// </summary>
    /// <param name="channel">The name of the channel to unregister</param>
    protected void UnregisterPluginChannel(string channel) {
        registeredPluginChannels.RemoveAll(chan => chan == channel);
        Handler.UnregisterPluginChannel(channel, this);
    }

    /// <summary>
    /// Sends the given plugin channel message to the server, if the channel has been registered.
    /// See http://wiki.vg/Plugin_channel for more information about plugin channels.
    /// </summary>
    /// <param name="channel">The channel to send the message on.</param>
    /// <param name="data">The data to send.</param>
    /// <param name="sendEvenIfNotRegistered">Should the message be sent even if it hasn't been registered by the server or this bot?  (Some Minecraft channels aren't registered)</param>
    /// <returns>Whether the message was successfully sent.  False if there was a network error or if the channel wasn't registered.</returns>
    protected bool SendPluginChannelMessage(
        string channel,
        byte[] data,
        bool sendEvenIfNotRegistered = false
    ) {
        if (!sendEvenIfNotRegistered) {
            if (!registeredPluginChannels.Contains(channel)) {
                return false;
            }
        }
        return Handler.SendPluginChannelMessage(channel, data, sendEvenIfNotRegistered);
    }

    /// <summary>
    /// Get server current TPS (tick per second)
    /// </summary>
    /// <returns>tps</returns>
    protected double GetServerTPS() {
        return Handler.GetServerTPS();
    }

    /// <summary>
    /// Interact with an entity
    /// </summary>
    /// <param name="EntityID"></param>
    /// <param name="type">0: interact, 1: attack, 2: interact at</param>
    /// <param name="hand">Hand.MainHand or Hand.OffHand</param>
    /// <returns>TRUE in case of success</returns>
    [Obsolete("Prefer using InteractType enum instead of int for interaction type")]
    protected bool InteractEntity(int EntityID, int type, Hand hand = Hand.MainHand) {
        return Handler.InteractEntity(EntityID, (InteractType)type, hand);
    }

    /// <summary>
    /// Interact with an entity
    /// </summary>
    /// <param name="EntityID"></param>
    /// <param name="type">Interaction type (InteractType.Interact, Attack or AttackAt)</param>
    /// <param name="hand">Hand.MainHand or Hand.OffHand</param>
    /// <returns>TRUE in case of success</returns>
    protected bool InteractEntity(int EntityID, InteractType type, Hand hand = Hand.MainHand) {
        return Handler.InteractEntity(EntityID, type, hand);
    }

    /// <summary>
    /// Give Creative Mode items into regular/survival Player Inventory
    /// </summary>
    /// <remarks>(obviously) requires to be in creative mode</remarks>
    /// </summary>
    /// <param name="slot">Destination inventory slot</param>
    /// <param name="itemType">Item type</param>
    /// <param name="count">Item count</param>
    /// <returns>TRUE if item given successfully</returns>
    protected bool CreativeGive(
        int slot,
        ItemType itemType,
        int count,
        Dictionary<string, object>? nbt = null
    ) {
        return Handler.DoCreativeGive(slot, itemType, count, nbt);
    }

    /// <summary>
    /// Use Creative Mode to delete items from the regular/survival Player Inventory
    /// </summary>
    /// <remarks>(obviously) requires to be in creative mode</remarks>
    /// </summary>
    /// <param name="slot">Inventory slot to clear</param>
    /// <returns>TRUE if item cleared successfully</returns>
    protected bool CreativeDelete(int slot) {
        return CreativeGive(slot, ItemType.Null, 0, null);
    }

    /// <summary>
    /// Plays animation (Player arm swing)
    /// </summary>
    /// <param name="hand">Hand.MainHand or Hand.OffHand</param>
    /// <returns>TRUE if animation successfully done</returns>
    public bool SendAnimation(Hand hand = Hand.MainHand) {
        return Handler.DoAnimation((int)hand);
    }

    /// <summary>
    /// Use item currently in the player's hand (active inventory bar slot)
    /// </summary>
    /// <returns>TRUE if successful</returns>
    protected bool UseItemInHand() {
        return Handler.UseItemOnHand();
    }

    /// <summary>
    /// Use item currently in the player's hand (active inventory bar slot)
    /// </summary>
    /// <returns>TRUE if successful</returns>
    protected bool UseItemInLeftHand() {
        return Handler.UseItemOnLeftHand();
    }

    /// <summary>
    /// Check inventory handling enable status
    /// </summary>
    /// <returns>TRUE if inventory handling is enabled</returns>
    public bool GetInventoryEnabled() {
        return Handler.GetInventoryEnabled();
    }

    /// <summary>
    /// Place the block at hand in the Minecraft world
    /// </summary>
    /// <param name="location">Location to place block to</param>
    /// <param name="blockFace">Block face (e.g. Direction.Down when clicking on the block below to place this block)</param>
    /// <param name="hand">Hand.MainHand or Hand.OffHand</param>
    /// <returns>TRUE if successfully placed</returns>
    public bool SendPlaceBlock(Location location, Direction blockFace, Hand hand = Hand.MainHand) {
        return Handler.PlaceBlock(location, blockFace, hand);
    }

    /// <summary>
    /// Get the player's inventory. Do not write to it, will not have any effect server-side.
    /// </summary>
    /// <returns>Player inventory</returns>
    protected Container GetPlayerInventory() {
        Container container = Handler.GetPlayerInventory();
        return new Container(container.ID, container.Type, container.Title, container.Items);
    }

    /// <summary>
    /// Get all inventories, player and container(s). Do not write to them. Will not have any effect server-side.
    /// </summary>
    /// <returns>All inventories</returns>
    public Dictionary<int, Container> GetInventories() {
        return Handler.GetInventories();
    }

    /// <summary>
    /// Perform inventory action
    /// </summary>
    /// <param name="inventoryId">Inventory ID</param>
    /// <param name="slot">Slot ID</param>
    /// <param name="actionType">Action Type</param>
    /// <returns>TRUE in case of success</returns>
    protected bool WindowAction(int inventoryId, int slot, WindowActionType actionType) {
        return Handler.DoWindowAction(inventoryId, slot, actionType);
    }

    /// <summary>
    /// Get inventory action helper
    /// </summary>
    /// <param name="container">Inventory Container</param>
    /// <returns>ItemMovingHelper instance</returns>
    protected ItemMovingHelper GetItemMovingHelper(Container container) {
        return new ItemMovingHelper(container, Handler);
    }

    /// <summary>
    /// Change player selected hotbar slot
    /// </summary>
    /// <param name="slot">0-8</param>
    /// <returns>True if success</returns>
    protected bool ChangeSlot(short slot) {
        return Handler.ChangeSlot(slot);
    }

    /// <summary>
    /// Get current player selected hotbar slot
    /// </summary>
    /// <returns>0-8</returns>
    protected byte GetCurrentSlot() {
        return Handler.GetCurrentSlot();
    }

    /// <summary>
    /// Clean all inventory
    /// </summary>
    /// <returns>TRUE if the uccessfully clear</returns>
    protected bool ClearInventories() {
        return Handler.ClearInventories();
    }

    /// <summary>
    /// Update sign text
    /// </summary>
    /// <param name="location"> sign location</param>
    /// <param name="line1"> text one</param>
    /// <param name="line2"> text two</param>
    /// <param name="line3"> text three</param>
    /// <param name="line4"> text1 four</param>
    protected bool UpdateSign(
        Location location,
        string line1,
        string line2,
        string line3,
        string line4
    ) {
        return Handler.UpdateSign(location, line1, line2, line3, line4);
    }

    /// <summary>
    /// Selects villager trade
    /// </summary>
    /// <param name="selectedSlot">Trade slot to select, starts at 0.</param>
    protected bool SelectTrade(int selectedSlot) {
        return Handler.SelectTrade(selectedSlot);
    }

    /// <summary>
    /// Teleport to player in spectator mode
    /// </summary>
    /// <param name="entity">player to teleport to</param>
    protected bool SpectatorTeleport(Entity entity) {
        return Handler.Spectate(entity);
    }

    /// <summary>
    /// Teleport to player/entity in spectator mode
    /// </summary>
    /// <param name="uuid">uuid of entity to teleport to</param>
    protected bool SpectatorTeleport(Guid UUID) {
        return Handler.SpectateByUUID(UUID);
    }

    /// <summary>
    /// Update command block
    /// </summary>
    /// <param name="location">command block location</param>
    /// <param name="command">command</param>
    /// <param name="mode">command block mode</param>
    /// <param name="flags">command block flags</param>
    protected bool UpdateCommandBlock(
        Location location,
        string command,
        CommandBlockMode mode,
        CommandBlockFlags flags
    ) {
        return Handler.UpdateCommandBlock(location, command, mode, flags);
    }

    /// <summary>
    /// Close an opened inventory
    /// </summary>
    /// <param name="inventory">inventory to close</param>
    /// <returns>True if success</returns>
    protected bool CloseInventory(Container inventory) {
        return CloseInventory(inventory.ID);
    }
    /// <summary>
    /// Close a opened inventory
    /// </summary>
    /// <param name="inventoryID">ID of the inventory to close</param>
    /// <returns>True if success</returns>
    protected bool CloseInventory(int inventoryID) {
        return Handler.CloseInventory(inventoryID);
    }

    /// <summary>
    /// Get max length for chat messages
    /// </summary>
    /// <returns>Max length, in characters</returns>
    protected int GetMaxChatMessageLength() {
        return Handler.GetMaxChatMessageLength();
    }

    /// <summary>
    /// Respawn player
    /// </summary>
    protected bool Respawn() {
        if (Handler.GetHealth() <= 0)
            return Handler.SendRespawnPacket();
        else
            return false;
    }

    /// <summary>
    /// Enable or disable network packet event calling. If you want to capture every packet including login phase, please enable this in <see cref="Initialize()"/>
    /// </summary>
    /// <remarks>
    /// Enable this may increase memory usage.
    /// </remarks>
    /// <param name="enabled"></param>
    protected void SetNetworkPacketEventEnabled(bool enabled) {
        Handler.SetNetworkPacketCaptureEnabled(enabled);
    }

    /// <summary>
    /// Get the minecraft protcol number currently in use
    /// </summary>
    /// <returns>Protcol number</returns>
    protected int GetProtocolVersion() {
        return Handler.GetProtocolVersion();
    }

    /// <summary>
    /// Invoke a task on the main thread, wait for completion and retrieve return value.
    /// </summary>
    /// <param name="task">Task to run with any type or return value</param>
    /// <returns>Any result returned from task, result type is inferred from the task</returns>
    /// <example>bool result = InvokeOnMainThread(methodThatReturnsAbool);</example>
    /// <example>bool result = InvokeOnMainThread(() => methodThatReturnsAbool(argument));</example>
    /// <example>int result = InvokeOnMainThread(() => { yourCode(); return 42; });</example>
    /// <typeparam name="T">Type of the return value</typeparam>
    protected T InvokeOnMainThread<T>(Func<T> task) {
        return Handler.InvokeOnMainThread(task);
    }

    /// <summary>
    /// Invoke a task on the main thread and wait for completion
    /// </summary>
    /// <param name="task">Task to run without return value</param>
    /// <example>InvokeOnMainThread(methodThatReturnsNothing);</example>
    /// <example>InvokeOnMainThread(() => methodThatReturnsNothing(argument));</example>
    /// <example>InvokeOnMainThread(() => { yourCode(); });</example>
    protected void InvokeOnMainThread(Action task) {
        Handler.InvokeOnMainThread(task);
    }

    /// <summary>
    /// Schedule a task to run on the main thread, and do not wait for completion
    /// </summary>
    /// <param name="task">Task to run</param>
    /// <param name="delayTicks">Run the task after X ticks (1 tick delay = ~100ms). 0 for no delay</param>
    /// <example>
    /// <example>InvokeOnMainThread(methodThatReturnsNothing, 10);</example>
    /// <example>InvokeOnMainThread(() => methodThatReturnsNothing(argument), 10);</example>
    /// <example>InvokeOnMainThread(() => { yourCode(); }, 10);</example>
    /// </example>
    protected void ScheduleOnMainThread(Action task, int delayTicks = 0) {
        lock (delayTasksLock) {
            delayedTasks.Add(new TaskWithDelay(task, delayTicks));
        }
    }

    /// <summary>
    /// Schedule a task to run on the main thread, and do not wait for completion
    /// </summary>
    /// <param name="task">Task to run</param>
    /// <param name="delay">Run the task after the specified delay</param>
    protected void ScheduleOnMainThread(Action task, TimeSpan delay) {
        lock (delayTasksLock) {
            delayedTasks.Add(new TaskWithDelay(task, delay));
        }
    }

    /// <summary>
    /// Command runner definition.
    /// Returned string will be the output of the command
    /// </summary>
    /// <param name="command">Full command</param>
    /// <param name="args">Arguments in the command</param>
    /// <returns>Command result to display to the user</returns>
    public delegate string CommandRunner(string command, string[] args);

    /// <summary>
    /// Command class with constructor for creating command for ChatBots.
    /// </summary>
    public class ChatBotCommand : Command {
        public CommandRunner Runner;

        private readonly string _cmdName;
        private readonly string _cmdDesc;
        private readonly string _cmdUsage;

        public override string CmdName {
            get { return _cmdName; }
        }
        public override string CmdUsage {
            get { return _cmdUsage; }
        }
        public override string CmdDesc {
            get { return _cmdDesc; }
        }

        public override void RegisterCommand(CommandDispatcher<CmdResult> dispatcher) { }

        /// <summary>
        /// ChatBotCommand Constructor
        /// </summary>
        /// <param name="cmdName">Name of the command</param>
        /// <param name="cmdDesc">Description of the command. Support tranlation.</param>
        /// <param name="cmdUsage">Usage of the command</param>
        /// <param name="callback">Method for handling the command</param>
        public ChatBotCommand(
            string cmdName,
            string cmdDesc,
            string cmdUsage,
            CommandRunner callback
        ) {
            _cmdName = cmdName;
            _cmdDesc = cmdDesc;
            _cmdUsage = cmdUsage;
            Runner = callback;
        }
    }
}
public enum WindowActionType {
    /// <summary>
    /// Left click with mouse on a slot: grab or drop a whole item stack
    /// </summary>
    LeftClick,

    /// <summary>
    /// Right click with mouse on a slot: grab half a stack or drop a single item
    /// </summary>
    RightClick,

    /// <summary>
    /// Middle click with mouse on a slot: grab a full stack from creative inventory
    /// </summary>
    MiddleClick,

    /// <summary>
    /// Shift+Left click with mouse on a slot: send a whole item stack to the hotbar or other inventory
    /// </summary>
    ShiftClick,

    /// <summary>
    /// Drop a single item on ground
    /// </summary>
    DropItem,

    /// <summary>
    /// Drop a whole item stack on ground
    /// </summary>
    DropItemStack,

    /// <summary>
    /// Start hovering slots with left button pressed: Distribute evenly the stack on hovered slots
    /// </summary>
    StartDragLeft,

    /// <summary>
    /// Start hovering slots with right button pressed: Drop one item on each hovered slot
    /// </summary>
    StartDragRight,

    /// <summary>
    /// Start hovering slots with middle button pressed: Create one item stack on each hovered slot in creative mode
    /// </summary>
    StartDragMiddle,

    /// <summary>
    /// Hover a slot to distribute evenly an item stack
    /// </summary>
    AddDragLeft,

    /// <summary>
    /// Hover a slot to drop one item from an item stack
    /// </summary>
    AddDragRight,

    /// <summary>
    /// Hover a slot to create one item stack in creative mode
    /// </summary>
    AddDragMiddle,

    /// <summary>
    /// Stop hovering slots with left button pressed
    /// </summary>
    EndDragLeft,

    /// <summary>
    /// Stop hovering slots with right button pressed
    /// </summary>
    EndDragRight,

    /// <summary>
    /// Stop hovering slots with middble button pressed
    /// </summary>
    EndDragMiddle,
}


// ===== PARTS ABOVE ARE TO BE STRIPPED =====
// ===== RUN INSTALL.SH TO STRIP & MOVE =====
// =====    TO THE PROPER DIRECTORY     =====

//<SPLITHERE>
//MCCScript 1.0

//using System.Threading.Tasks;

//using MinecraftClient.CommandHandler;

//dll ./scripts/libs/Newtonsoft.Json.dll
//using Newtonsoft.Json.Linq;

//<LOADBOT>

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
    /// Closes an open inventory
    /// </summary>
    /// <param name="inventory">inventory to close</param>
    /// <returns>True if success</returns>
    protected bool CloseInventory(Container inventory) {
        return CloseInventory(inventory.ID);
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
            printCurrentData(prefix: "Recovered data");
            RetryManagement.eraseFile();
        }
    }

    public void printCurrentData(string prefix = "Current data") {
        PrintChat(prefix + ": map \"" + this.currentMapName + "\" for game \"" + this.currentGame + "\" (slot " + this.currentSlot + "p" + this.currentPage + ")");
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
        // List<string> VS string[], easier to do it like that
        if (players.Count == 0) {
            foreach (string player in this.TRUSTED_PLAYERS) {
                clickInventory(coOwnContainer, player);
            }
        } else {
            foreach (string player in players) {
                clickInventory(coOwnContainer, player);
            }
        }

        CloseInventory(coOwnContainer);
        PrintChat("Done setting all trusted players to co owners !");
        
    }

    /// <summary>
    /// Toggles all specified players from the servers whitelist
    /// </summary>
    /// <remarks>If none specified, toggles all players in TRUSTED_PLAYERS</remarks>
    private void addToWhitelist(List<string> players) {
        if (players.Count == 0) {
            SendText("/whitelist " + String.Join(" ", this.TRUSTED_PLAYERS));
            PrintChat("Done toggling trusted players from the whitelist");
        } else {
            // note: nasty bug w that one if one of the player is the player running the cmd
            // as the username will be gray and fuck up parsing (unneeded; won't fix for now) 
            SendText("/whitelist " + String.Join(" ", players.ToArray()));
            PrintChat("Done toggling " + String.Join(", ", players.ToArray()) + " from the whitelist");
        }
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
        this.currentPage = 0;
        // PrintChat("Set slot to " + args[0]);

        if (args.Count > 1) {
            this.currentPage = int.Parse(args[1]);
            // PrintChat("Set page to " + args[1]);
        }
        
        List<string> startArgs = new List<string>();

        if (args.Count < 2) {
            startArgs.Add("do not start game");
        }

        clickOnMap(false, startArgs);
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
    private async Task clickNextButton(Container maps, int nextPageIndex = -1, int delay = -1) {
        // IMPORTANT NOTE:
        // Due to a nasty bug (-1h30) having an inventory open with the same
        // name as the previous one doesn't call OnInventoryOpen(...)
        // and actually keeps the same id BUT not the same object
        // So just setting an arbitrary (pretty high) delay here to 
        // get through that
        if (nextPageIndex == -1) {
            nextPageIndex = this.NEXT_PAGE_INDEX; // Can't use that directly in the func declaration
        }
        if (delay == -1) {
            delay = this.NEXT_INVENTORY_DELAY;
        }
        clickInventory(maps, nextPageIndex, close: false);
        await Task.Delay(delay);
    }

    /// <summary>
    /// Saves all speed builders practice map names into a txt file
    /// </summary>
    private async Task saveAllSpeedBuilderPracticeMapNames() {
        // Get the inventory (opens at game start, no need to run a command to open it)
        Container inventory = null;
        foreach ((int index, Container container) in GetInventories()) {
            if (MatchesNoCap(container.Title, "Practice Menu")) {
                inventory = container;
                break;
            }
        }
        
        // Loop over every item in the inventory & save the names from Paper items
        int iteration = 0;
        while (true) {
            Dictionary<int, Item> items = inventory.Items;

            foreach ((int _, Item item) in items) {
                IEnumerable<string> fileLines = File.ReadLines("owo.txt");

                if (item.Type == ItemType.Paper) {
                    string name = GetVerbatim(item.DisplayName);
                    if (!fileLines.Contains(name)) {
                        File.AppendAllTextAsync("owo.txt", name + "\n");
                        LogToConsole(name);
                    }
                }
            }

            // If nothing/no paper in the 43rd slot (bottom-1;right-1)
            // It means the page isn't full and that it's the last one
            // In which case end the search
            if (!items.ContainsKey(43) || items[43].Type != ItemType.Paper) {
                LogToConsole("ENDED!");
                break;
            }

            // Otherwise click on the next button & increment the counter
            // Note that since the delay is quite low, it can cause page skips; that's on purpose,
            // it's kind of a bruteforce to avoid getting kicked mid save (thanks GWEN).
            // This should be run multiple times to be sure every page got looped through.
            // The rest of the function is made to handle multiple passes.
            iteration++;
            LogToConsole("=====ITERATION " + iteration + "=====");
            await clickNextButton(inventory, nextPageIndex:53, delay:50);
        }
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
                    CloseInventory(maps);
                    this.savedMapCount--; // cancel the add from right before
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
            return await searchItemContainerMultiPage(container, itemName);
        }

        if (container != null)
            CloseInventory(container);
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
            if (container != null)
                CloseInventory(container);
        } else {
            this.currentGame = getCapitalizedItemName(container, gameName);
            this.currentPage = 0;
            this.currentSlot = 0;
            this.savedMapCount = 0;
            this.currentMapName = "Unspecified";
            clickInventory(container, index);
            CloseInventory(container);
            PrintChat("Successfully set game to " + this.currentGame);
            RetryManagement.saveData(this.currentGame, this.currentMapName, this.currentSlot, this.currentPage, this.savedMapCount);
        }
    }

    /// <summary>
    /// Counts how many maps a game has
    /// </summary>
    private async Task countMapsGame(List<string> args) {
        if ((args.Count == 0) && (this.currentGame == "")) {
            PrintChat("No game specified !");
            return;
        }
        string gameName = (args.Count == 0) ?
            this.currentGame : String.Join(" ", args.ToArray()).ToLower();


        (Container container, int index) = await searchGamePage(null, gameName);

        if (container == null || index == 0) {
            PrintChat("Specified game invalid (" + gameName + ")");
            return;
        }

        Container mapChooseContainer = await clickInventoryContainer(container, index, "set map", WindowActionType.RightClick);

        (int mapCount, int pageCount) = await recurCountMapsGame(mapChooseContainer, 0, 1);
        CloseInventory(mapChooseContainer);

        string page = pageCount == 1 ? " page." : " pages.";

        PrintChat("Game \"" + gameName + "\" has " + mapCount + " maps spread across " + pageCount + page);

    }
    private async Task<(int, int)> recurCountMapsGame(Container container, int initialCount, int initialPage) {
        foreach ((int _, Item item) in container.Items) {
            if (item.Type == ItemType.Paper)
                initialCount++;
        }
        if (hasNextButton(container)) {
            initialPage++;
            await clickNextButton(container);
            return await recurCountMapsGame(container, initialCount, initialPage);
        }
        return (initialCount, initialPage);
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
        string[] shouldBeEnabled = { "Whitelist" };
        bool changed = false;
        Container melon = await openMelon();
        Container settings = await clickInventoryContainer(melon, "Server Settings", "Server Settings");
        foreach ((int index, Item item) in settings.Items) {
            // either if on & needs to be off or if off & needs to be on
            if ((item.Type == ItemType.LimeDye && !shouldBeEnabled.Contains(GetVerbatim(item.DisplayName))) ||
                item.Type == ItemType.GrayDye && shouldBeEnabled.Contains(GetVerbatim(item.DisplayName))) {
                clickInventory(settings, index, close: false);
                changed = true;
            }
        }
        CloseInventory(settings);
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
            this.currentSlot = mapItemindex;
            this.currentMapName = mapName;
            // Can't set page easily unfortunately
            clickInventory(mapsRightPage, mapItemindex);
            PrintChat("Selected map " + mapName + " for game " + this.currentGame + "(slot " + mapItemindex + ")");
            return true;
        }
    }

    // ========== VARS HERE ==========
    // name of the private server to join
    //private string SERVER_NAME = "COM-BridgesForever-1";
    private string SERVER_NAME = "<SERVERNAME>";
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
                await chooseMap(args);
                break;

            case "maps":
                if (await chooseMap(args))
                    await startGame(null);
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

            case "count":
            case "countm":
            case "countmaps":
                await countMapsGame(args);
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
            
            case "setup":
                await giveCoOwn(args);
                addToWhitelist(args);
                await setOptions();
                break;

            case "savespeedbuildernames":
                await saveAllSpeedBuilderPracticeMapNames();
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

        if (!items[count - 2].ToString().Contains("\"text\":"))
            return;
        if (!items[count - 1].ToString().Contains("\"extra\":"))
            return;

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
    private async Task handleRunCommandError(string command, List<string> args, int _try = 0) {
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
