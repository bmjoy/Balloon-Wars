using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowFollow : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;
    private Image myImage;
    private Image objectToFollowImage;

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        SetShadowToCurObjectImage();
    }

    public void SetShadowToCurObjectImage()
    {
        objectToFollowImage = objectToFollow.GetComponent<Image>();
        myImage.sprite = objectToFollowImage.sprite;
    }
}
