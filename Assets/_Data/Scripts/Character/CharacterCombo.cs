using System.Collections.Generic;
using UnityEngine;

public class CharacterCombo : CharacterAbstract
{
    [SerializeField] protected InputBuffer inputBuffer;
    [SerializeField] protected string combo = "";
    [SerializeField] protected float distanceTele = 10;
    [SerializeField] protected string currentSkill = "";
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadInputBuffer();
    }
    protected void LoadInputBuffer()
    {
        if (this.inputBuffer != null) return;
        this.inputBuffer = transform.parent.GetComponentInChildren<InputBuffer>();
        Debug.Log(transform.name + ": LoadInputBuffer", gameObject);
    }
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
        // this.TeleCombo();
        this.SkillOneCombo();
        this.SkillTwoCombo();
        this.SkillFourCombo();
    }
    public void ClearCurrentSkill()
    {
        this.currentSkill = "";
    }
    public void SpawnEffectWhenTele()
    {
        TeleFX_Spawner.Instance.Spawn(TeleFX_Spawner.Instance.nameFX, this.characterCtrl.transform.position);
        SmokeLandedFXSpawner.Instance.Spawn(SmokeLandedFXSpawner.Instance.nameFX, this.characterCtrl.transform.position);
    }
    protected void Teleing(float dirTele)
    {
        Vector3 char_Pos = this.characterCtrl.transform.position;
        this.characterCtrl.transform.position = new Vector3(char_Pos.x + this.distanceTele * dirTele, char_Pos.y, char_Pos.z);
    }
    protected void TeleCombo()
    {
        if (this.currentSkill != "") return;
        if (this.combo == "Left+Left")
        {
            this.SpawnEffectWhenTele();
            if (this.characterCtrl.CharacterFlip.IsFacingRight)
                this.characterCtrl.StateManager.SetNextState(new Tele2());
            else this.characterCtrl.StateManager.SetNextState(new Tele1());
            this.Teleing(-1);
            this.inputBuffer.ClearCombo();
        }
        if (this.combo == "Right+Right")
        {
            this.SpawnEffectWhenTele();
            if (this.characterCtrl.CharacterFlip.IsFacingRight)
                this.characterCtrl.StateManager.SetNextState(new Tele1());
            else this.characterCtrl.StateManager.SetNextState(new Tele2());
            this.Teleing(1);
            this.inputBuffer.ClearCombo();
        }
    }
    protected bool PlayingCurrentSkill(SkillSlot skill)
    {
        if (!this.characterCtrl.CharacterSkillManager.TryUseSkill(skill)) return false;

        SkillDataSO skillData = characterCtrl.CharacterSkillManager.CurrentSkill;
        State skillState = skillData.GetSkillState();
        characterCtrl.StateManager.SetNextState(skillState);

        return true;
    }
    protected void HandleSkillCombo(string expectedCombo, int expectedScale, SkillSlot skill)
    {
        if (this.combo == expectedCombo && this.characterCtrl.transform.localScale.x == expectedScale)
        {
            this.currentSkill = this.PlayingCurrentSkill(skill) ? skill.ToString() : "";
            this.inputBuffer.ClearCombo();
        }
    }

    protected void SkillOneCombo()
    {
        if (this.currentSkill != "") return;
        HandleSkillCombo("Right+Skill", 1, SkillSlot.Skill_1);
        HandleSkillCombo("Left+Skill", -1, SkillSlot.Skill_1);
    }

    protected void SkillTwoCombo()
    {
        if (this.currentSkill != "") return;
        this.characterCtrl.Animator.SetBool("IsBeaming", true);
        HandleSkillCombo("Right+Skill", -1, SkillSlot.Skill_2);
        HandleSkillCombo("Left+Skill", 1, SkillSlot.Skill_2);
    }

    protected void SkillFourCombo()
    {
        if (this.currentSkill != "") return;
        HandleSkillCombo("Down+Right+Hit", 1, SkillSlot.Skill_4);
        HandleSkillCombo("Down+Left+Hit", -1, SkillSlot.Skill_4);
    }
}
