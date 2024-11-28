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

        [SerializeField] GameObject playButton, invalidText;

        public GameObject componentParent, addOnParent;

        void Start()
        {
            playButton.SetActive(false);
            invalidText.SetActive(true);

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

            gridInitializer.ClearGrid();
            gridInitializer.CreateGridBase(closedTilePrefab, lc_openTilePrefab, cornerPrefab);

            grid = FindObjectsOfType<LC_GridTile>();

            CheckValidity();
        }

        public void ClearGrid()
        {
            foreach(Transform child in componentParent.transform)
            {
                if(child.TryGetComponent<LC_Component>(out LC_Component c))
                    c.RemoveFromCurrentTile();
                Destroy(child.gameObject);
            }
            foreach(Transform child in addOnParent.transform)
            {
                if(child.TryGetComponent<LC_AddOnComponent>(out LC_AddOnComponent c))
                    c.RemoveFromCurrentTile();
                Destroy(child.gameObject);
            }

            grid = new LC_GridTile[width * height];
        }

        public void CheckValidity()
        {
            if (IsValid())
            {
                playButton.SetActive(true);
                invalidText.SetActive(false);
            }
            else
            {
                playButton.SetActive(false);
                invalidText.SetActive(true);
            }
        }

        bool IsValid()
        {
            int playerCount = 0;
            int finishCount = 0;
            foreach(LC_GridTile t in grid)
            {
                if(t.component != null)
                {
                    if(t.component.GetComponentInChildren<FallingComponent>(true) != null)                          // if component is falling object
                    {
                        Vector3[] v = new Vector3[4];
                        t.GetComponent<RectTransform>().GetWorldCorners(v);                                         // get world space position of the tile's corners
                        float size = Mathf.Abs(v[2].x - v[0].x);                                                    // get object size (width and height should be the same)

                        RaycastHit2D[] hits = Physics2D.RaycastAll(t.transform.position, Vector2.down, size/2 + 1); // shoot ray downwards
                        
                        if(hits.Length <= 1) return false;                                                          // if object is floating, level is not valid
                    }

                    if(t.component.CompareTag("Player"))
                        playerCount++;
                    else if(t.component.CompareTag("Finish"))
                        finishCount++;
                }
            }
            
            return playerCount == 1 && finishCount == 1;
        }

        public void StartPlaying()
        {
            foreach(LC_GridTile tile in grid)
            {
                if(tile.component != null)
                {
                    tile.component.Activate();
                    if(tile.component.CompareTag("Finish"))
                        tile.SetIsEndGridTile(true);
                }

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
                {
                    tile.component.Deactivate();
                    if(tile.component.CompareTag("Finish"))
                        tile.SetIsEndGridTile(false);
                }

                foreach(LC_Component addOn in tile.addOns)
                    if(addOn != null)
                        addOn.Deactivate();
            }

            FindObjectOfType<LevelRotater>().transform.rotation = Quaternion.identity;

            lc_gameController.SetActive(false);
        }

        public void ResetPlaying()
        {
            StopPlaying();
            StartPlaying();
        }
    }
}
