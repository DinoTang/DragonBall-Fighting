using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance => instance;
    [SerializeField] protected CharacterCtrl player1;
    [SerializeField] protected CharacterCtrl player2;
    public CharacterCtrl Player1 => player1;
    public CharacterCtrl Player2 => player2;
    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogWarning("Only 1 GameManager allows to exist");
        instance = this;
    }
    protected override void LoadComponent()
    {
        CharacterCtrl[] characters = FindObjectsOfType<CharacterCtrl>();
        if (characters.Length >= 2)
        {
            this.player1 = characters[0];
            this.player2 = characters[1];

            this.player1.SetOpponent(this.player2);
            this.player2.SetOpponent(this.player1);
        }
    }

}
