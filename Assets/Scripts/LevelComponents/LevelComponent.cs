using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    public ComponentType type;
}
public enum ComponentType
{
    Player,
    Box,
    SpikeBox,
    Spike,
    StickySurface
}