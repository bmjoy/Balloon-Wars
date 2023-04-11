using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class sliderFollower : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_ValueText;
    Slider m_Slider;
    public int SliderValue{get; set;}

    void Start()
    {
        m_Slider = GetComponent<Slider>();
        setSliderValue();
    }

    public void setSliderValue()
    {
        SliderValue = (int)m_Slider.value;
        m_ValueText.SetText(SliderValue.ToString());
    }
}
