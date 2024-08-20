using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeStart : MonoBehaviour
{
    public Transform tf;
    private List<Character> listCharacter = new();
    private bool isReady = true;

    private void Awake()
    {
        this.RegisterListener(EventID.OnCharacterDead, (param) =>
        {
            RemoveCharacter((Character)param);
        });
    }

    public void AddCharacter(Character character)
    {
        listCharacter.Add(character);
        isReady = false;

        if (!isReady)
        {
            this.PostEvent(EventID.OnNodeStartBusy, tf);
        }
    }

    public void RemoveCharacter(Character character)
    {
        listCharacter.Remove(character);

        if (listCharacter.Count == 0)
        {
            isReady = true;
            this.PostEvent(EventID.OnNodeStartReady, tf);
        }
    }
}
