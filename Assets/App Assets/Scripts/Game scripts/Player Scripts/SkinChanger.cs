using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Linq;
using System;

public class SkinChanger : MonoBehaviour
{
    private Dictionary<Skins.Catagories, List<SpriteResolver>> m_Resolvers;
    
    private void Awake()
    {
        m_Resolvers = new Dictionary<Skins.Catagories, List<SpriteResolver>>()
        {
            {Skins.Catagories.Character, new List<SpriteResolver>()},
            {Skins.Catagories.Hat, new List<SpriteResolver>()},
            {Skins.Catagories.Weapon, new List<SpriteResolver>()},
            {Skins.Catagories.Shirt, new List<SpriteResolver>()},
            {Skins.Catagories.Pants, new List<SpriteResolver>()}
        };

        foreach (SpriteResolver resolver in GetComponentsInChildren<SpriteResolver>())
        {
            switch (resolver.GetCategory())
            {
                case "Hat":
                    m_Resolvers[Skins.Catagories.Hat].Add(resolver);
                    break;
                case "Weapon":
                    m_Resolvers[Skins.Catagories.Weapon].Add(resolver);
                    break;
                case "Shirt Body":
                case "Shirt Left Arm":
                case "Shirt Right Arm":
                    m_Resolvers[Skins.Catagories.Shirt].Add(resolver);
                    break;
                case "Pants Body":
                case "Pants Left Leg":
                case "Pants Right Leg":
                    m_Resolvers[Skins.Catagories.Pants].Add(resolver);
                    break;
                default:
                    m_Resolvers[Skins.Catagories.Character].Add(resolver);
                    break;
            }
        }
    }

    public void changeItem(Skins.Catagories catagory, string item)
    {
        foreach (SpriteResolver resolver in m_Resolvers[catagory])
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(),item);
        }
    }
}
