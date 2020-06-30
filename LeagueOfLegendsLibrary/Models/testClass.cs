using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfLegendsLibrary.Models
{
    class testClass
    {
    }
}


public class Rootobject
{
    public string Type { get; set; }
    public string Format { get; set; }
    public string Version { get; set; }
    //public Data Data { get; set; }
    public Dictionary<string, ChampionData> Data { get; set; }
}

public class Data
{
    public ChampionData ChampionData { get; set; }
}

public class ChampionData
{
    public string Id { get; set; }
    public long Key { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public Images Image { get; set; }
    public Skin[] Skins { get; set; }
    public string Lore { get; set; }
    public string Blurb { get; set; }
    public string[] Allytips { get; set; }
    public string[] Enemytips { get; set; }
    public string[] Tags { get; set; }
    public string Partype { get; set; }
    public Info Info { get; set; }
    public Stats Stats { get; set; }
    public Spell[] Spells { get; set; }
    public Passive Passive { get; set; }
    public Recommended[] Recommended { get; set; }
}

public class Images
{
    public string full { get; set; }
    public string sprite { get; set; }
    public string group { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int w { get; set; }
    public int h { get; set; }
}

public class Info
{
    public int attack { get; set; }
    public int defense { get; set; }
    public int magic { get; set; }
    public int difficulty { get; set; }
}

public class Stats
{
    public float hp { get; set; }
    public int hpperlevel { get; set; }
    public float mp { get; set; }
    public float mpperlevel { get; set; }
    public int movespeed { get; set; }
    public float armor { get; set; }
    public float armorperlevel { get; set; }
    public float spellblock { get; set; }
    public float spellblockperlevel { get; set; }
    public int attackrange { get; set; }
    public float hpregen { get; set; }
    public float hpregenperlevel { get; set; }
    public float mpregen { get; set; }
    public float mpregenperlevel { get; set; }
    public int crit { get; set; }
    public int critperlevel { get; set; }
    public float attackdamage { get; set; }
    public float attackdamageperlevel { get; set; }
    public float attackspeedperlevel { get; set; }
    public float attackspeed { get; set; }
}

public class Passive
{
    public string name { get; set; }
    public string description { get; set; }
    public Image1 image { get; set; }
}

public class Image1
{
    public string full { get; set; }
    public string sprite { get; set; }
    public string group { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int w { get; set; }
    public int h { get; set; }
}

public class Skin
{
    public string id { get; set; }
    public int num { get; set; }
    public string name { get; set; }
    public bool chromas { get; set; }
}

public class Spell
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string tooltip { get; set; }
    public Leveltip leveltip { get; set; }
    public int maxrank { get; set; }
    public float[] cooldown { get; set; }
    public string cooldownBurn { get; set; }
    public int[] cost { get; set; }
    public string costBurn { get; set; }
    public Datavalues datavalues { get; set; }
    public int[][] effect { get; set; }
    public string[] effectBurn { get; set; }
    public object[] vars { get; set; }
    public string costType { get; set; }
    public string maxammo { get; set; }
    public int[] range { get; set; }
    public string rangeBurn { get; set; }
    public Image2 image { get; set; }
    public string resource { get; set; }
}

public class Leveltip
{
    public string[] label { get; set; }
    public string[] effect { get; set; }
}

public class Datavalues
{
}

public class Image2
{
    public string full { get; set; }
    public string sprite { get; set; }
    public string group { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int w { get; set; }
    public int h { get; set; }
}

public class Recommended
{
    public string champion { get; set; }
    public string title { get; set; }
    public string map { get; set; }
    public string mode { get; set; }
    public string type { get; set; }
    public string customTag { get; set; }
    public int sortrank { get; set; }
    public bool extensionPage { get; set; }
    public bool useObviousCheckmark { get; set; }
    public object customPanel { get; set; }
    public Block[] blocks { get; set; }
}

public class Block
{
    public string type { get; set; }
    public bool recMath { get; set; }
    public bool recSteps { get; set; }
    public int minSummonerLevel { get; set; }
    public int maxSummonerLevel { get; set; }
    public string showIfSummonerSpell { get; set; }
    public string hideIfSummonerSpell { get; set; }
    public string appendAfterSection { get; set; }
    public string[] visibleWithAllOf { get; set; }
    public string[] hiddenWithAnyOf { get; set; }
    public Item[] items { get; set; }
}

public class Item
{
    public string id { get; set; }
    public int count { get; set; }
    public bool hideCount { get; set; }
}
