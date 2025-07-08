using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : DinoBehaviour
{
    public CharacterCtrl player1;
    public CharacterCtrl player2;

    protected override void LoadComponent()
    {
        CharacterCtrl[] characters = FindObjectsOfType<CharacterCtrl>();
        if (characters.Length >= 2)
        {
            player1 = characters[0];
            player2 = characters[1];

            player1.SetOpponent(player2);
            player2.SetOpponent(player1);
        }
    }

}
