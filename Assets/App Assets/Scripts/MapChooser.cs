using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapChooser : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Image UIBackImage;
    [SerializeField] private Image UIDetailsBackImage;
    public int CurMapIndex{get; set;} = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIBackImage.sprite = backgrounds[CurMapIndex];
        UIBackImage.sprite = backgrounds[CurMapIndex];
    }

    public void MoveToNextMap()
    {
        CurMapIndex = CurMapIndex == backgrounds.Length - 1 ? 0 : CurMapIndex + 1;
        UIBackImage.sprite = backgrounds[CurMapIndex]; 
        UIDetailsBackImage.sprite = backgrounds[CurMapIndex]; 
    }
    
    public void MoveToPrevMap()
    {
        CurMapIndex = CurMapIndex == 0 ? backgrounds.Length - 1 : CurMapIndex - 1;
        UIBackImage.sprite = backgrounds[CurMapIndex];
        UIDetailsBackImage.sprite = backgrounds[CurMapIndex];
    }
    
}
