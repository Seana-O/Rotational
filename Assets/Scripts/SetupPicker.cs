using UnityEngine;

public class SetupPicker : MonoBehaviour
{
    [SerializeField] int level;

    public ILevelSetup GetSetup()
    {
        switch (level)
        {
            case 0:
                return new TutorialSetup();
            case 1:
                return new Level1Setup();
            case 2:
                return new Level2Setup();
            case 3:
                return new Level3Setup();
            default:
                return null;
        }
    }
    
}

public interface ILevelSetup
{
    public int Width {get; set;}
    public int Height {get; set;}
    public string Layout {get; set;}

    public int NumberOfSpikeSets {get; set;}

    public string SpikeSets {get; set;}
}

public class Level1Setup : ILevelSetup
{
    public int Width {get; set;} = 4;
    public int Height {get; set;} = 4;
    public string Layout {get; set;} = 
        "poxo"+
        "xooo"+
        "ooxo"+
        "xooe";

    public int NumberOfSpikeSets {get; set;} = 2;

    public string SpikeSets {get; set;} = 
        "----"+
        "----"+
        "----"+
        "----"
        +
        "----"+
        "----"+
        "----"+
        "----";
}

public class Level2Setup : ILevelSetup
{
    public int Width {get; set;} = 4;
    public int Height {get; set;} = 4;
    public string Layout {get; set;} = 
        "poxo"+
        "xooo"+
        "ooxo"+
        "bbxe";

    public int NumberOfSpikeSets {get; set;} = 2;

    public string SpikeSets {get; set;} = 
        "-n-n"+
        "----"+
        "w--e"+
        "s--e"
        +
        "----"+
        "----"+
        "---w"+
        "----";
}

public class Level3Setup : ILevelSetup
{
    public int Width {get; set;} = 4;
    public int Height {get; set;} = 4;
    public string Layout {get; set;} = 
        "poxo"+
        "xooo"+
        "ooxo"+
        "bbxe";

    public int NumberOfSpikeSets {get; set;} = 2;

    public string SpikeSets {get; set;} = 
        "-n-n"+
        "----"+
        "w--e"+
        "s--e"
        +
        "----"+
        "----"+
        "---w"+
        "----";
}

public class TutorialSetup : ILevelSetup
{
    public int Width {get; set;} = 4;
    public int Height {get; set;} = 4;
    public string Layout {get; set;} = 
        "poxo"+
        "xooo"+
        "ooxo"+
        "bbxe";

    public int NumberOfSpikeSets {get; set;} = 2;

    public string SpikeSets {get; set;} = 
        "-n-n"+
        "----"+
        "w--e"+
        "s--e"
        +
        "----"+
        "----"+
        "---w"+
        "----";
}