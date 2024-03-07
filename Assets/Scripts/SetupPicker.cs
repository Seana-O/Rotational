using System.Collections.Generic;
using UnityEngine;

public class SetupPicker : MonoBehaviour
{
    public LevelSetup GetSetup()
    {
        int level = FindObjectOfType<SceneSwitcher>().CurrentLevel;
        return GetSetupList()[level];
    }

    private List<LevelSetup> GetSetupList()
    {
        List<LevelSetup> levels = new();
        string layout;
        string spikeSets;

        //------------------------
        layout = 
            "poxo"+
            "xooo"+
            "ooxo"+
            "xooe";
        spikeSets =
            "----"+
            "----"+
            "----"+
            "----";
        levels.Add(new LevelSetup(4, 4, layout, 1, spikeSets));
        //------------------------
        layout = 
            "poxo"+
            "xooo"+
            "ooxo"+
            "xooe";
        spikeSets =
            "----"+
            "----"+
            "----"+
            "----";
        levels.Add(new LevelSetup(4, 4, layout, 1, spikeSets));
        //------------------------
        layout = 
            "poxo"+
            "xooo"+
            "ooxo"+
            "bbxe";
        spikeSets =
            "-n-n"+
            "----"+
            "w--e"+
            "s--e"
            +
            "----"+
            "----"+
            "---w"+
            "----";
        levels.Add(new LevelSetup(4, 4, layout, 2, spikeSets));
        

        return levels;
    }
    
}

public class LevelSetup
{
    public int Width {get; set;}
    public int Height {get; set;}
    public string Layout {get; set;}

    public int NumberOfSpikeSets {get; set;}

    public string SpikeSets {get; set;}

    public LevelSetup(int width, int height, string layout, int numberOfSpikeSets, string spikeSets)
    {
        Width = width;
        Height = height;
        Layout = layout;
        NumberOfSpikeSets = numberOfSpikeSets;
        SpikeSets = spikeSets;
    }
}