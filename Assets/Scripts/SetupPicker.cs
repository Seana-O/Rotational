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
        List<LevelSetup> levels = new();
        string layout;
        string spikeSets;
        
        //------------------------
        layout = 
            "xe"+
            "po";
        spikeSets =
            "--"+
            "--";
        levels.Add(new LevelSetup(2, 2, layout, 1, spikeSets, tutorialBoxes[0]));
        //------------------------
        layout = 
            "oe"+
            "po";
        spikeSets =
            "w-"+
            "-e";
        levels.Add(new LevelSetup(2, 2, layout, 1, spikeSets, tutorialBoxes[1]));
        //------------------------
        layout = 
            "poo"+
            "xoo"+
            "obe";
        spikeSets =
            "n--"+
            "--e"+
            "---";
        levels.Add(new LevelSetup(3, 3, layout, 1, spikeSets, tutorialBoxes[2]));
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
        levels.Add(new LevelSetup(4, 4, layout, 2, spikeSets, tutorialBoxes[3]));
        //------------------------
        layout = 
            "oobob"+
            "exxox"+
            "xxxoo"+
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

    public GameObject TutorialBox {get; set;}

    public LevelSetup(int width, int height, string layout, int numberOfSpikeSets, string spikeSets, GameObject tutorialBox = null)
    {
        Width = width;
        Height = height;
        Layout = layout;
        NumberOfSpikeSets = numberOfSpikeSets;
        SpikeSets = spikeSets;
        TutorialBox = tutorialBox;
    }
}