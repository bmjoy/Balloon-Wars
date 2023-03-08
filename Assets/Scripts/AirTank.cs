using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirTank : MonoBehaviour
{
    [SerializeField] [Range(5f, 100f)] float reduceAirSpeed = 10f;
    [SerializeField] [Range(5f, 100f)] float addAirSpeed = 10f;
    [SerializeField] TextMeshProUGUI AirPercentage;
    [SerializeField] private Image AirAmountImage;

    public int AirAmount{set;get;} = 100; 
    private float AIR_DELTA;

    private void Start()
    {
        AIR_DELTA = AirAmountImage.rectTransform.sizeDelta.x / 100f;
        updatePercentageTextToAirAmount();
    }

    private float timeBetweenReduces()
    {
        return 1f / reduceAirSpeed;
    }

    private float timeBetweenIncrements()
    {
        return 1f / addAirSpeed;
    }

    private void updatePercentageTextToAirAmount()
    {
        string percentageText = AirAmount.ToString() + "%";
        AirPercentage.SetText(percentageText);
    }

    private IEnumerator reduceAir()
    {
        while(AirAmount != 0)
        {
            yield return new WaitForSeconds(timeBetweenReduces());
            AirAmount--;
            updatePercentageTextToAirAmount();
            addWidthToImage(AirAmountImage, -AIR_DELTA);
        }
        StopAllCoroutines();
    }

    private IEnumerator addAir()
    {
        while(AirAmount != 100)
        {
            yield return new WaitForSeconds(timeBetweenIncrements());
            AirAmount++;
            updatePercentageTextToAirAmount();
            addWidthToImage(AirAmountImage, AIR_DELTA);
        }
        StopAllCoroutines();
    }

    private void addWidthToImage(Image image, float widthToAdd)
    {
        Vector2 curSize = image.rectTransform.sizeDelta;
        image.rectTransform.sizeDelta = new Vector2(curSize.x + widthToAdd, curSize.y);
        image.rectTransform.position = new Vector3(
            AirAmountImage.rectTransform.position.x + widthToAdd / 2f, AirAmountImage.rectTransform.position.y, 0);
    }

    public void startReduceAir()
    {
        StartCoroutine(reduceAir());
    }

    public void startAddAir()
    {
        StartCoroutine(addAir());
    }
}
