using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeamSkillProjectile : SkillProjectile
{
    [SerializeField] protected BeamSkillProjectile beamHead;
    public BeamSkillProjectile BeamHead => beamHead;
    [SerializeField] protected BeamSkillProjectile beamTail;
    public BeamSkillProjectile BeamTail => beamTail;
    [SerializeField] protected float beamDuration;
    public float BeamDuration => beamDuration;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBeamHead();
        this.LoadBeamTail();
        this.LoadDurantionBeam();
    }

    protected new void Update() { }
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
    protected void LoadDurantionBeam()
    {
        BeamSkillDataSO beamData = this.skillProjCtrl.SkillData as BeamSkillDataSO;
        this.beamDuration = beamData.beamDuration;
        Debug.Log(transform.name + ": LoadDurantionBeam");
    }
}
