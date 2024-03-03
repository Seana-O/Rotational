using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    public int Width {get; private set;} = 4;
    public int Height {get; private set;} = 4;
    public string Layout {get; private set;} = 
        "poxo"+
        "xooo"+
        "obxo"+
        "obxe";

    public int numberOfSpikeSets = 2;

    public string SpikeSets {get; private set;} = 
        "wn-n"+
        "----"+
        "w--e"+
        "s--e"
        +
        "----"+
        "----"+
        "---w"+
        "----";
}
