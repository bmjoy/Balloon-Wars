using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class CharacterChooser : MonoBehaviour
{
    [SerializeField] private SpriteResolver m_SpriteResolver;
    [SerializeField] private Image m_UICharacterImage;
    [SerializeField] private TextMeshProUGUI m_CharacterName;
    private SpriteRenderer m_CharacterImage;
    private List<string> m_CharactersNames = Enum.GetNames(typeof(Skins.Characters)).ToList();
    public int CurCharacterIndex{get; set;} = 0;

    void Start()
    {
        m_CharacterImage = GetComponentInChildren<SpriteRenderer>();
        changeCharacter(CurCharacterIndex);
    }

    public void ChangeToNextCharacter()
    {
        CurCharacterIndex = CurCharacterIndex == m_CharactersNames.Count - 1? 0 : CurCharacterIndex + 1;
        changeCharacter(CurCharacterIndex);
    }

    public void ChangeToPrevCharacter()
    {
        CurCharacterIndex = CurCharacterIndex == 0? m_CharactersNames.Count - 1 : CurCharacterIndex - 1;
        changeCharacter(CurCharacterIndex);
    }

    private void changeCharacter(int characterNumber)
    {
        characterNumber %= m_CharactersNames.Count;
        m_SpriteResolver.SetCategoryAndLabel(m_SpriteResolver.GetCategory(),m_CharactersNames[characterNumber]);
        m_UICharacterImage.sprite = m_CharacterImage.sprite;
        m_CharacterName.SetText(m_CharactersNames[characterNumber]);
    }
}
