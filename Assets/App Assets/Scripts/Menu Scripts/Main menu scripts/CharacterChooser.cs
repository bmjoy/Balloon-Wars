using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterChooser : MonoBehaviour
{
    public static CharacterChooser Instance { get; private set; }
    public event Action<int> CharacterChanged;
    public List<string> CharactersNames{get; private set;} = Enum.GetNames(typeof(Skins.Characters)).ToList();
    public int CurCharacterIndex{get; set;} = 0;

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

    public void ChangeToNextCharacter()
    {
        CurCharacterIndex = CurCharacterIndex == CharactersNames.Count - 1? 0 : CurCharacterIndex + 1;
        changeCharacter(CurCharacterIndex);
    }

    public void ChangeToPrevCharacter()
    {
        CurCharacterIndex = CurCharacterIndex == 0? CharactersNames.Count - 1 : CurCharacterIndex - 1;
        changeCharacter(CurCharacterIndex);
    }

    public void changeCharacter(int characterNumber)
    {
        CharacterChanged?.Invoke(characterNumber);
    }
}
