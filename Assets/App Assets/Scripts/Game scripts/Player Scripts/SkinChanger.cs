using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Linq;
using System;

public class SkinChanger : MonoBehaviour
{
    private List<SpriteResolver> m_Resolvers;
    private List<string> m_CharactersNames = Enum.GetNames(typeof(Skins.Characters)).ToList();
    
    private void Awake()
    {
        m_Resolvers = GetComponentsInChildren<SpriteResolver>().ToList();
    }

    public void changeCharacter(Skins.Characters character)
    {
        foreach (SpriteResolver resolver in m_Resolvers)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(),character.ToString());
        }
    }

    public void changeCharacter(int characterNumber)
    {
        characterNumber %= m_CharactersNames.Count;
        foreach (SpriteResolver resolver in m_Resolvers)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(),m_CharactersNames[characterNumber]);
        }
    }

    public void changeCharacter(string characterName)
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
