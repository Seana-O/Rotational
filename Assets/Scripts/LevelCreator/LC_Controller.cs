using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreation
{
    public class LC_Controller : MonoBehaviour
    {
        int width = 3, height = 3;
    
        [SerializeField] GameObject closedTilePrefab, lc_openTilePrefab, cornerPrefab;

        [SerializeField] TMP_InputField xInput, yInput;

        LC_GridTile[] grid;

        GridInitializer gridInitializer;

        [SerializeField] GameObject lc_gameController;

        public GameObject componentParent;

        void Start()
        {
            lc_gameController.SetActive(false);

            xInput.onValueChanged.AddListener(delegate {SetGridSize(); });
            yInput.onValueChanged.AddListener(delegate {SetGridSize(); });

            gridInitializer = gameObject.GetComponent<GridInitializer>();
            gridInitializer.SetGridSize(width, height);
            ResetGrid();
        }

        void SetGridSize()
        {
            width = int.Parse(xInput.text);
            height = int.Parse(yInput.text);

            gridInitializer.SetGridSize(width, height);

            ResetGrid();
        }

        public void ResetGrid()
        {
            ClearGrid();

            gridInitializer.CreateGridBase(closedTilePrefab, lc_openTilePrefab, cornerPrefab);

            grid = FindObjectsOfType<LC_GridTile>();
        }

        void ClearGrid()
        {
            foreach(Transform child in componentParent.transform)
                Destroy(child.gameObject);

            grid = new LC_GridTile[width * height];
        }

        public void StartPlaying()
        {
            foreach(LC_GridTile tile in grid)
            {
                if(tile.component != null)
                    tile.component.Activate();

                foreach(LC_Component addOn in tile.addOns)
                    if(addOn != null)
                        addOn.Activate();
            }

            lc_gameController.SetActive(true);
        }

        public void StopPlaying()
        {
            foreach(LC_GridTile tile in grid)
            {
                if(tile.component != null)
                    tile.component.Deactivate();

                foreach(LC_Component addOn in tile.addOns)
                    if(addOn != null)
                        addOn.Deactivate();
            }

            lc_gameController.SetActive(false);
        }
    }
}
