using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterItemChooser : MonoBehaviour
{
    public static CharacterItemChooser Instance { get; private set; }
    public event Action<Skins.Catagories, string> CharacterItemChanged;
    public Skins.Characters CurCharacter{get; set;} = Skins.Characters.Cat;
    public Skins.Hats CurHat{get; set;} = Skins.Hats.None;
    public Skins.Weapons CurWeapon{get; set;} = Skins.Weapons.None;
    public Skins.Shirts CurShirt{get; set;} = Skins.Shirts.None;
    public Skins.Pants CurPants{get; set;} = Skins.Pants.None;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void changeItem(Skins.Catagories itemCatagory, string itemName)
    {
        CharacterItemChanged?.Invoke(itemCatagory, itemName);
    }
    
    public void SelectItem(Skins.Catagories itemCatagory, string itemName)
    {
        switch(itemCatagory)
        {
            case Skins.Catagories.Character:
                CurCharacter = Enum.Parse<Skins.Characters>(itemName);
                break;
            case Skins.Catagories.Hat:
                CurHat = Enum.Parse<Skins.Hats>(itemName);
                break;
            case Skins.Catagories.Weapon:
                CurWeapon = Enum.Parse<Skins.Weapons>(itemName);
                break;
            case Skins.Catagories.Shirt:
                CurShirt = Enum.Parse<Skins.Shirts>(itemName);
                break;
            case Skins.Catagories.Pants:
                CurPants = Enum.Parse<Skins.Pants>(itemName);
                break;
        }
    }

    public void ResetItemsToCurrent() {
        changeItem(Skins.Catagories.Character, CurCharacter.ToString());
        changeItem(Skins.Catagories.Hat, CurHat.ToString());
        changeItem(Skins.Catagories.Weapon, CurWeapon.ToString());
        changeItem(Skins.Catagories.Shirt, CurShirt.ToString());
        changeItem(Skins.Catagories.Pants, CurPants.ToString());
    }
}
