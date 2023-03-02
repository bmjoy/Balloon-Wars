using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapChooser : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Image UIBackImage;
    private int curMapIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIBackImage.sprite = backgrounds[curMapIndex];
    }

    public void MoveToNextMap()
    {
        curMapIndex = curMapIndex == backgrounds.Length - 1 ? 0 : curMapIndex + 1;
        UIBackImage.sprite = backgrounds[curMapIndex]; 
    }
    
    public void MoveToPrevMap()
    {
        curMapIndex = curMapIndex == 0 ? backgrounds.Length - 1 : curMapIndex - 1;
        UIBackImage.sprite = backgrounds[curMapIndex]; 
    }
    
}
