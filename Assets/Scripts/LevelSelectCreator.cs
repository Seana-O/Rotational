using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCreator : MonoBehaviour
{
    [SerializeField] GameObject levelButton;
    [SerializeField] GameObject startPosObject;
    readonly int numberOfRows = 5;

    void Start()
    {
        SetupPicker setupPicker = GetComponent<SetupPicker>();
        ButtonHandler buttonHandler = FindObjectOfType<ButtonHandler>();

        int numberOfLevels = setupPicker.NumberOfLevels();
        
        Vector2 startPos = startPosObject.transform.position;
        float distance = Mathf.Abs(startPos.x * 2);
        float posDist = distance/numberOfRows;

        for (int i = 0; i < numberOfLevels; i++)
        {
            GameObject buttonObject = Instantiate(levelButton, gameObject.transform);
            buttonObject.transform.position = startPos + new Vector2(posDist * (i%numberOfRows), -posDist * (i/numberOfRows));
            int levelNumber = i;
            buttonObject.GetComponent<Button>().onClick.AddListener(() => buttonHandler.GoToLevel(levelNumber));
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = (levelNumber+1).ToString();
        }
    }
}
