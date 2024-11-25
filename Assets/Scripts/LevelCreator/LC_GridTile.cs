using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreation
{
    public class LC_GridTile : MonoBehaviour
    {
        public LC_Component component;  // component currently on this tile
        public LC_Component[] addOns;   // the addons on this tile

        void Start()
        {
            addOns = new LC_Component[4];
        }
    }
}
