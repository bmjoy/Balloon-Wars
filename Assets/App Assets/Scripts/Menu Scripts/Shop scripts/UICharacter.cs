using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacter : MonoBehaviour
{
    private SkinChanger m_SkinChanger;
private CharacterItemChooser m_ItemCzahooser = CharacterItemChooser.Instance;
    void Start()
    {
        m_SkinChanger = GetComponent<SkinChanger>();
        CharacterItemChooser.Instance.CharacterItemChanged += m_SkinChanger.changeItem;
        m_SkinChanger.changeItem(Skins.Catagories.Character, CharacterItemChooser.Instance.CurCharacter.ToString());
        m_SkinChanger.changeItem(Skins.Catagories.Hat, CharacterItemChooser.Instance.CurHat.ToString());
        m_SkinChanger.changeItem(Skins.Catagories.Weapon, CharacterItemChooser.Instance.CurWeapon.ToString());
        m_SkinChanger.changeItem(Skins.Catagories.Shirt, CharacterItemChooser.Instance.CurShirt.ToString());
        m_SkinChanger.changeItem(Skins.Catagories.Pants, CharacterItemChooser.Instance.CurPants.ToString());
    }

    private void OnDestroy()
    {
        CharacterItemChooser.Instance.CharacterItemChanged -= m_SkinChanger.changeItem;
    }
}
