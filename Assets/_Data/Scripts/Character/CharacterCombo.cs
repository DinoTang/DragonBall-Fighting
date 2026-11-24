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
        if (GameManager.Instance.IsGameOver) return;
        if (InputManager.Instance.GetDownInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.Down);
        if (InputManager.Instance.GetLeftInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.Left);
        if (InputManager.Instance.GetRightInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.Right);
        if (InputManager.Instance.GetHitInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.Hit);
        if (InputManager.Instance.GetKickInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.Kick);
        if (InputManager.Instance.GetChargeInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.ChargeKi);
        if (InputManager.Instance.GetKiBlastInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.Skill);
        if (InputManager.Instance.GetRushAttackInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.U);
        if (InputManager.Instance.GetPowerSurgeInput(this.characterCtrl.PlayerType)) this.inputBuffer.AddInput(InputKey.O);
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
        this.PlaySkill();

    }
    protected void PlaySkill()
    {
        this.SkillShootBaseKiAirCombo();
        this.SkillShootBaseKiGroundCombo();
        this.SkillOneCombo();
        this.SkillTwoCombo();
        this.SkillFourCombo();
        this.SkillFiveCombo();
        this.SkillBurstCombo();
        this.SkillUltimateCombo();
    }
    public void ClearCurrentSkill()
    {
        this.currentSkill = "";
    }
    public void SpawnEffectWhenTele()
    {
        TeleFX_Spawner.Instance.Spawn(TeleFXNameEnum.Vegeta.ToString(), this.characterCtrl.transform.position);
        SmokeLandedFXSpawner.Instance.Spawn(SmokeLandedFXSpawner.Instance.nameFX, this.characterCtrl.transform.position);
    }
    protected void Teleing(float dirTele)
    {
        Vector3 char_Pos = this.characterCtrl.transform.position;
        this.characterCtrl.transform.position = new Vector3(char_Pos.x + this.distanceTele * dirTele, char_Pos.y, char_Pos.z);
    }
    // protected void TeleCombo()
    // {
    //     if (this.currentSkill != "") return;
    //     if (this.combo == "Left+Left")
    //     {
    //         this.SpawnEffectWhenTele();
    //         if (this.characterCtrl.CharacterFlip.IsFacingRight)
    //             this.characterCtrl.StateManager.SetNextState(new Tele2());
    //         else this.characterCtrl.StateManager.SetNextState(new Tele1());
    //         this.Teleing(-1);
    //         this.inputBuffer.ClearCombo();
    //     }
    //     if (this.combo == "Right+Right")
    //     {
    //         this.SpawnEffectWhenTele();
    //         if (this.characterCtrl.CharacterFlip.IsFacingRight)
    //             this.characterCtrl.StateManager.SetNextState(new Tele1());
    //         else this.characterCtrl.StateManager.SetNextState(new Tele2());
    //         this.Teleing(1);
    //         this.inputBuffer.ClearCombo();
    //     }
    // }
    protected bool PlayingCurrentSkill(SkillNameEnum skill)
    {
        if (!this.characterCtrl.CharacterSkillManager.TryUseSkill(skill)) return false;

        SkillDataSO skillData = characterCtrl.CharacterSkillManager.CurrentSkill;
        if (skillData.skillName != SkillNameEnum.Skill_BasicProjectile_Ground &&
        skillData.skillName != SkillNameEnum.Skill_BasicProjectile_Air &&
        skillData.skillName != SkillNameEnum.Skill_Burst) this.SpawnHitFlash();
        
        CharacterState skillState = skillData.GetSkillState();
        characterCtrl.StateManager.SetNextState(skillState);

        return true;
    }
    protected void HandleSkillCombo(string expectedCombo, int expectedScale, SkillNameEnum skill)
    {
        if (this.combo == expectedCombo && this.characterCtrl.transform.localScale.x == expectedScale)
        {
            this.currentSkill = this.PlayingCurrentSkill(skill) ? skill.ToString() : "";
            this.inputBuffer.ClearCombo();
        }
    }
    protected void HandleSkillBurstCombo(string expectedCombo, SkillNameEnum skill)
    {
        if (this.combo.Contains(expectedCombo))
        {
            this.currentSkill = this.PlayingCurrentSkill(skill) ? skill.ToString() : "";
            this.inputBuffer.ClearCombo();
        }
    }
    protected void SpawnHitFlash()
    {
        // Hiệu ứng lóe lên khi dùng skill
        Vector2 pos = this.characterCtrl.transform.position;
        ExplosionFXSpawner.Instance.Spawn(ExplosionFXEnum.Skill_Cast_Flash.ToString(), pos, this.characterCtrl);
    }

    protected void SkillShootBaseKiGroundCombo()
    {
        if (this.currentSkill != "") return;
        if (this.characterCtrl.StateManager.mainStateType is AirState) return;
        HandleSkillCombo("Skill", 1, SkillNameEnum.Skill_BasicProjectile_Ground);
        HandleSkillCombo("Skill", -1, SkillNameEnum.Skill_BasicProjectile_Ground);
    }
    protected void SkillShootBaseKiAirCombo()
    {
        if (this.currentSkill != "") return;
        if (this.characterCtrl.StateManager.mainStateType is IdleState) return;
        HandleSkillCombo("Skill", 1, SkillNameEnum.Skill_BasicProjectile_Air);
        HandleSkillCombo("Skill", -1, SkillNameEnum.Skill_BasicProjectile_Air);
    }
    protected void SkillOneCombo()
    {
        if (this.currentSkill != "") return;
        HandleSkillCombo("Right+Skill", 1, SkillNameEnum.Skill_1);
        HandleSkillCombo("Left+Skill", -1, SkillNameEnum.Skill_1);
    }

    protected void SkillTwoCombo()
    {
        if (this.currentSkill != "") return;
        this.characterCtrl.Animator.SetBool("IsBeaming", true);
        HandleSkillCombo("Right+Skill", -1, SkillNameEnum.Skill_2);
        HandleSkillCombo("Left+Skill", 1, SkillNameEnum.Skill_2);
    }

    protected void SkillFourCombo()
    {
        if (this.currentSkill != "") return;
        HandleSkillCombo("Down+Right+Hit", 1, SkillNameEnum.Skill_4);
        HandleSkillCombo("Down+Left+Hit", -1, SkillNameEnum.Skill_4);
    }

    protected void SkillFiveCombo()  //Rush Attack
    {
        if (this.characterCtrl.StateManager.CurrentState is AirHit3State
        ) return;
        if (this.currentSkill != "") return;
        HandleSkillCombo("U", 1, SkillNameEnum.Skill_5);
        HandleSkillCombo("U", -1, SkillNameEnum.Skill_5);
    }

    protected void SkillBurstCombo()
    {
        if (this.currentSkill != "") return;
        HandleSkillBurstCombo("O", SkillNameEnum.Skill_Burst);
    }
    protected void SkillUltimateCombo()
    {
        if (this.currentSkill != "") return;
        HandleSkillCombo("Down+Skill", 1, SkillNameEnum.Skill_Ultimate);
        HandleSkillCombo("Down+Skill", -1, SkillNameEnum.Skill_Ultimate);
    }
}
