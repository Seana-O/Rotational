using System.Collections.Generic;
using UnityEngine;

public class SetupPicker : MonoBehaviour
{
    public List<GameObject> tutorialBoxes = new();

    public LevelSetup GetSetup()
    {
        int level = FindObjectOfType<SceneSwitcher>().CurrentLevel;
        return GetSetupList()[level];
    }
    public int NumberOfLevels()
    {
        return GetSetupList().Count;
    }
    public bool IsLastLevel()
    {
        int level = FindObjectOfType<SceneSwitcher>().CurrentLevel;
        return level == NumberOfLevels() - 1;
    }

    private List<LevelSetup> GetSetupList()
    {
        // o = open
        // x = closed
        // e = finish
        // b = block
        // s = spike block
        // p = player

        List<LevelSetup> levels = new();
        string layout;
        string spikeSets;
        string stickySets;
        
        //------------------------
        layout = 
            "xe"+
            "po";
        levels.Add(new LevelSetup(2, 2, layout, 0, null, null, tutorialBoxes[0]));
        //------------------------
        layout = 
            "pox"+
            "xoe"+
            "xoo";
        stickySets =
            "---"+
            "---"+
            "-w-";
        levels.Add(new LevelSetup(3, 3, layout, 1, null, stickySets));
        //------------------------
        layout = 
            "poox"+
            "xooo"+
            "oooo"+
            "xexx";
        levels.Add(new LevelSetup(4, 4, layout, 1));
        //------------------------
        layout = 
            "oe"+
            "po";
        spikeSets =
            "w-"+
            "-e";
        levels.Add(new LevelSetup(2, 2, layout, 1, spikeSets, null, tutorialBoxes[1]));
        //------------------------
        layout = 
            "poo"+
            "xoo"+
            "obe";
        spikeSets =
            "n--"+
            "--e"+
            "---";
        levels.Add(new LevelSetup(3, 3, layout, 1, spikeSets, null, tutorialBoxes[2]));
        //------------------------
        layout = 
            "xox"+
            "xex"+
            "bpx";
        levels.Add(new LevelSetup(3, 3, layout, 1));
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
        levels.Add(new LevelSetup(4, 4, layout, 2, spikeSets, null, tutorialBoxes[3]));
        //------------------------
        layout = 
            "oobob"+
            "exxox"+
            "xxooo"+
            "xxpoo"+
            "xxxxo";
        spikeSets =
            "-ss-n"+
            "-----"+
            "----e"+
            "--w--"+
            "----e";
        levels.Add(new LevelSetup(5, 5, layout, 1, spikeSets));
        //------------------------
        layout = 
            "oooxoe"+
            "opoxox"+
            "oxoxoo"+
            "booboo"+
            "xxoxoo"+
            "xxxxoo";
        spikeSets =
            "----w-"+
            "----w-"+
            "--w---"+
            "-----e"+
            "--s---"+
            "----ss";
        levels.Add(new LevelSetup(6, 6, layout, 1, spikeSets));

        //------------------------sticky surfaces
        layout = 
            "pe"+
            "oo";
        stickySets =
            "w-"+
            "--";
        levels.Add(new LevelSetup(2, 2, layout, 1, null, stickySets));

        //------------------------spike boxes
        layout = 
            "pooo"+
            "xxoo"+
            "oxoo"+
            "osoe";
        levels.Add(new LevelSetup(4, 4, layout, 1, null, null, tutorialBoxes[2]));

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

    public string StickySets {get; set;}

    public GameObject TutorialBox {get; set;}

    public LevelSetup(int width, int height, string layout, int numberOfSpikeSets, string spikeSets = null, string stickySets = null, GameObject tutorialBox = null)
    {
        Width = width;
        Height = height;
        Layout = layout;
        NumberOfSpikeSets = numberOfSpikeSets;
        SpikeSets = spikeSets;
        StickySets = stickySets;
        TutorialBox = tutorialBox;
    }
}