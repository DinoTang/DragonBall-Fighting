using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjSpawner : Spawner<SkillProjectile>
{
    protected static SkillProjSpawner instance;
    public static SkillProjSpawner Instance => instance;

    [Header("Shot Skill Spawner")]
    [SerializeField] protected CharacterSelector characterSelector;
    [SerializeField] protected List<SkillDataSO> skillList;
    public List<SkillDataSO> SkillList => skillList;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogWarning("Only 1 ShotSkillSpawner allows to exists");
        instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterSelector();
        this.LoadSkillList();
    }

    protected void LoadCharacterSelector()
    {
        if (this.characterSelector != null) return;
        this.characterSelector = GetComponent<CharacterSelector>();
        Debug.Log(transform.name + ": LoadCharacterSelector", gameObject);
    }

    protected void LoadSkillList()
    {
        string path = $"SkillData/{this.characterSelector.GetName()}";
        SkillDataSO[] skillDatas = Resources.LoadAll<SkillDataSO>(path);
        this.skillList = new List<SkillDataSO>(skillDatas);
        Debug.Log(transform.name + ": LoadSkillList", gameObject);
    }

    public SkillProjectile GetSkill(SkillNameEnum skillSlot)
    {
        foreach (SkillProjectile skillProj in this.prefabs)
        {
            if (skillProj.SkillName == skillSlot)
                return skillProj;
        }
        return null;
    }
    public BeamSkillProjectile GetBeamSkill(SkillNameEnum skillSlot)
    {
        foreach (var proj in this.prefabs)
        {
            BeamSkillProjectile beamProj = proj as BeamSkillProjectile;
            if (beamProj != null && beamProj.SkillName == skillSlot)
                return beamProj;
        }
        return null;
    }

    public void StartStraightBeamSkill(SkillNameEnum slot, Vector2 startPos, Vector2 direction, CharacterCtrl owner)
    {
        BeamSkillProjectile beamPrefab = GetBeamSkill(slot);
        if (beamPrefab == null)
        {
            Debug.LogWarning("No beam skill found for slot: " + slot);
            return;
        }
        // StartCoroutine(SpawnBeamCoroutine(beamPrefab, startPos, direction.normalized, owner));
        SpawnBeamInstant(beamPrefab, startPos, direction.normalized, owner);
    }
    public void StartDiagonalBeamSkill(SkillNameEnum slot, Vector2 startPos, Vector2 direction, CharacterCtrl owner)
    {
        BeamSkillProjectile beamPrefab = GetBeamSkill(slot);
        if (beamPrefab == null)
        {
            Debug.LogWarning("No beam skill found for slot: " + slot);
            return;
        }
        // Góc bắn = -45 độ
        float angle = -45f * Mathf.Deg2Rad;

        // Hướng bắn theo góc -45
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        // Nếu nhân vật quay trái thì lật X
        dir.x *= direction.x;

        // StartCoroutine(SpawnBeamCoroutine(beamPrefab, startPos, dir, owner));
        SpawnBeamInstant(beamPrefab, startPos, dir, owner);
    }

    private void SpawnBeamInstant(BeamSkillProjectile beamPrefab, Vector2 pos, Vector2 dir, CharacterCtrl owner)
    {
        // === HEAD ===
        if (beamPrefab.BeamHead != null)
        {
            var head = this.Spawn(beamPrefab.BeamHead, pos, owner);
            head.SkillProjCtrl.Owner = owner;
            pos += dir * beamPrefab.BeamHeadLength;
        }

        // === BODY ===
        for (int i = 0; i < beamPrefab.BeamMaxParts; i++)
        {
            var body = this.Spawn(beamPrefab, pos, owner);
            body.SkillProjCtrl.Owner = owner;
            if (i == beamPrefab.BeamMaxParts - 1)
            {
                pos += dir * beamPrefab.BeamTailLength;
                break;
            }
            pos += dir * beamPrefab.BeamBodyLength;
        }

        // === TAIL ===
        if (beamPrefab.BeamTail != null)
        {
            var tail = this.Spawn(beamPrefab.BeamTail, pos, owner);
            tail.SkillProjCtrl.Owner = owner;
        }

        // reset animation sau khi duration xong
        owner.StartCoroutine(DisableBeamAnim(owner, beamPrefab.SKillDuration));
    }

    private IEnumerator DisableBeamAnim(CharacterCtrl owner, float delay)
    {
        yield return new WaitForSeconds(delay);
        owner.Animator.SetBool("IsBeaming", false);
    }

}
