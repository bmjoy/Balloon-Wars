using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharcterMovment : MonoBehaviour
{
    [SerializeField] Sprite[] animationImages;
    [SerializeField] Image animationImage;
    [SerializeField] [Range(0.01f, 0.1f)] float animationSpeed = 0.02f;
    [SerializeField] Image Shadow;
    private int curImageIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        animationImage.sprite = animationImages[curImageIndex];
        StartCoroutine(startAnimation());
    }

    private IEnumerator startAnimation()
    {
        yield return new WaitForSeconds(animationSpeed);
        curImageIndex = curImageIndex == animationImages.Length -1 ? 0 : curImageIndex + 1;
        animationImage.sprite = animationImages[curImageIndex];
        Shadow.sprite = animationImage.sprite;
        StartCoroutine(startAnimation());
    }
}