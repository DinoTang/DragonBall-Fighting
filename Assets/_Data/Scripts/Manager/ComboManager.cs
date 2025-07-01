using System.Collections.Generic;
using UnityEngine;

public class ComboManager : DinoBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    [SerializeField] protected InputBuffer inputBuffer;
    [SerializeField] protected string combo = "";
    [SerializeField] protected float distanceTele = 10;
    [SerializeField] protected SkillSlot skill_1;
    [SerializeField] protected SkillSlot skill_2;
    [SerializeField] protected SkillSlot skill_3;
    [SerializeField] protected string currentSkill = "";
    protected void Update()
    {
        if (InputManager.Instance.GetDownInput()) this.inputBuffer.AddInput(InputKey.Down);
        if (InputManager.Instance.GetLeftInput()) this.inputBuffer.AddInput(InputKey.Left);
        if (InputManager.Instance.GetRightInput()) this.inputBuffer.AddInput(InputKey.Right);
        if (InputManager.Instance.GetHitInput()) this.inputBuffer.AddInput(InputKey.Hit);
        if (InputManager.Instance.GetKickInput()) this.inputBuffer.AddInput(InputKey.Kick);
        if (InputManager.Instance.GetPowerFiredInput()) this.inputBuffer.AddInput(InputKey.Skill);
        this.combo = this.inputBuffer.GetComboString();

        // if (this.combo == "Down+Left+Hit" || this.combo == "Down+Right+Hit")
        // {
        //     this.characterCtrl.StateManager.SetNextState(new Combo1());
        //     this.inputBuffer.ClearCombo();
        // }
        // if (this.combo == "Down+Left+Kick" || this.combo == "Down+Right+Kick")
        // {
        //     this.characterCtrl.StateManager.SetNextState(new Combo2());
        //     this.inputBuffer.ClearCombo();
        // }
        this.TeleCombo();
        this.SkillOneCombo();
        this.SkillTwoCombo();
    }

    protected void SpawnEffectWhenTele()
    {
        TeleFX_Spawner.Instance.Spawn(TeleFX_Spawner.Instance.nameFX, this.characterCtrl.transform.position);
        SmokeLandedFXSpawner.Instance.Spawn(SmokeLandedFXSpawner.Instance.nameFX, this.characterCtrl.transform.position);
    }
    protected void Teleing(float distanceTele)
    {
        Vector3 char_Pos = this.characterCtrl.transform.position;
        this.characterCtrl.transform.position = new Vector3(char_Pos.x + distanceTele, char_Pos.y, char_Pos.z);
    }
    protected void TeleCombo()
    {
        if (this.combo == "Left+Left")
        {
            this.SpawnEffectWhenTele();
            if (this.characterCtrl.transform.localScale.x == 1)
                this.characterCtrl.StateManager.SetNextState(new Tele2());
            else this.characterCtrl.StateManager.SetNextState(new Tele1());
            this.Teleing(-this.distanceTele);
            this.inputBuffer.ClearCombo();
        }
        if (this.combo == "Right+Right")
        {
            this.SpawnEffectWhenTele();
            if (this.characterCtrl.transform.localScale.x == 1)
                this.characterCtrl.StateManager.SetNextState(new Tele1());
            else this.characterCtrl.StateManager.SetNextState(new Tele2());
            this.Teleing(this.distanceTele);
            this.inputBuffer.ClearCombo();
        }
    }
    protected void PlayingCurrentSkill()
    {
        this.characterCtrl.SkillManager.TryUseSkill(this.currentSkill);
    }
    protected void HandleSkillCombo(string expectedCombo, int expectedScale, SkillSlot skill)
    {
        if (this.combo == expectedCombo && this.characterCtrl.transform.localScale.x == expectedScale)
        {
            this.currentSkill = skill.ToString();
            this.PlayingCurrentSkill();
            this.inputBuffer.ClearCombo(); // Nếu muốn reset luôn combo
        }
    }

    protected void SkillOneCombo()
    {
        HandleSkillCombo("Right+Skill", 1, this.skill_1);
        HandleSkillCombo("Left+Skill", -1, this.skill_1);
    }

    protected void SkillTwoCombo()
    {
        this.characterCtrl.Animator.SetBool("IsBeaming", true);
        HandleSkillCombo("Right+Skill", -1, this.skill_2);
        HandleSkillCombo("Left+Skill", 1, this.skill_2);
    }

}
