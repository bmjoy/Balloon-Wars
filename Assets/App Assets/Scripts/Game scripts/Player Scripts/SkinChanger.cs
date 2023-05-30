using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Linq;
using System;

public class SkinChanger : MonoBehaviour
{
    private List<SpriteResolver> m_Resolvers;
    private int m_CurCharacter = 0;
    private List<string> m_CharactersNames = Enum.GetNames(typeof(Skins.Characters)).ToList();
    
    private void Awake()
    {
        m_Resolvers = GetComponentsInChildren<SpriteResolver>().ToList();
    }

    public void ChangeToNextCharacter()
    {
        m_CurCharacter = m_CurCharacter == m_CharactersNames.Count - 1? 0 : m_CurCharacter + 1;
        changeCharacter(m_CurCharacter);
    }

    public void ChangeToPrevCharacter()
    {
        m_CurCharacter = m_CurCharacter == 0? m_CharactersNames.Count - 1 : m_CurCharacter - 1;
        changeCharacter(m_CurCharacter);
    }

    private void changeCharacter(Skins.Characters character)
    {
        foreach (SpriteResolver resolver in m_Resolvers)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(),character.ToString());
        }
    }

    private void changeCharacter(int characterNumber)
    {
        characterNumber %= m_CharactersNames.Count;
        foreach (SpriteResolver resolver in m_Resolvers)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(),m_CharactersNames[characterNumber]);
        }
    }

    private void changeCharacter(string characterName)
    {
        if(m_CharactersNames.Contains(characterName))
        {
            foreach (SpriteResolver resolver in m_Resolvers)
            {
                resolver.SetCategoryAndLabel(resolver.GetCategory(),characterName);
            }
        }
    }
}
