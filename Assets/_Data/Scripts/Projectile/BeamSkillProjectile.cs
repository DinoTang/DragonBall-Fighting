using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeamSkillProjectile : SkillProjectile
{
    [Header("Beam Skill Settings")]
    [SerializeField] protected BeamSkillProjectile beamHead;
    public BeamSkillProjectile BeamHead => beamHead;
    [SerializeField] protected BeamSkillProjectile beamTail;
    public BeamSkillProjectile BeamTail => beamTail;
    [SerializeField] protected float skillDuration;
    public float SKillDuration => skillDuration;
    [SerializeField] private float beamSpawnInterval = 0.01f;
    [SerializeField] private int beamMaxParts = 5;
    [SerializeField] private float beamPartLength = 4.68f;
    public float BeamSpawnInterval => beamSpawnInterval;
    public int BeamMaxParts => beamMaxParts;
    public float BeamPartLength => beamPartLength;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (!this.skillProjCtrl)
        {
            Debug.Log("Đây là obj beam head hoặc tail nên ko cần set skillProjCtrl");
            return;
        }
        this.LoadSkillDuration();
        this.LoadBeamHead();
        this.LoadBeamTail();
        this.LoadBeamSettings();
    }

    protected override void OnUpdate()
    {

    }
    protected void LoadSkillDuration()
    {
        var beamSkillDataSO = this.skillProjCtrl.SkillData as BeamSkillDataSO;
        this.skillDuration = beamSkillDataSO.duration;
        Debug.Log(transform.name + ": LoadSkillDuration");
    }
    protected void LoadBeamHead()
    {
        if (this.beamHead != null) return;
        BeamSkillDataSO beamData = this.skillProjCtrl.SkillData as BeamSkillDataSO;
        this.beamHead = beamData.beamHead.GetComponent<BeamSkillProjectile>();
        Debug.Log(transform.name + ": LoadHeadBeam");
    }
    protected void LoadBeamTail()
    {
        if (this.beamTail != null) return;
        BeamSkillDataSO beamData = this.skillProjCtrl.SkillData as BeamSkillDataSO;
        this.beamTail = beamData.beamTail.GetComponent<BeamSkillProjectile>();
        Debug.Log(transform.name + ": LoadBeamTail");
    }
    protected void LoadBeamSettings()
    {
        BeamSkillDataSO beamData = this.skillProjCtrl.SkillData as BeamSkillDataSO;
        this.beamSpawnInterval = beamData.beamSpawnInterval;
        this.beamMaxParts = beamData.beamMaxParts;
        this.beamPartLength = beamData.beamPartLength;
        Debug.Log(transform.name + ": LoadBeamSettings");
    }
}
