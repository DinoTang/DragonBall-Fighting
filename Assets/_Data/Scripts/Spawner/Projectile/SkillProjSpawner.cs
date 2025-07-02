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

    public SkillProjectile GetSkill(SkillSlot skillSlot)
    {
        foreach (SkillProjectile skillProj in this.prefabs)
        {
            if (skillProj.SkillSlot == skillSlot)
                return skillProj;
        }
        return null;
    }
    public BeamSkillProjectile GetBeamSkill(SkillSlot skillSlot)
    {
        foreach (var proj in this.prefabs)
        {
            BeamSkillProjectile beamProj = proj as BeamSkillProjectile;
            if (beamProj != null && beamProj.SkillSlot == skillSlot)
                return beamProj;
        }
        return null;
    }

    public void StartBeamSkill(SkillSlot slot, Vector2 startPos, Vector2 direction, CharacterCtrl owner)
    {
        BeamSkillProjectile beamPrefab = GetBeamSkill(slot);
        if (beamPrefab == null)
        {
            Debug.LogWarning("No beam skill found for slot: " + slot);
            return;
        }

        StartCoroutine(SpawnBeamCoroutine(beamPrefab, startPos, direction.normalized, owner));
    }

    private IEnumerator SpawnBeamCoroutine(BeamSkillProjectile beamPrefab, Vector2 pos, Vector2 dir, CharacterCtrl owner)
    {
        // === HEAD ===
        if (beamPrefab.BeamHead != null)
        {
            Spawn(beamPrefab.BeamHead, pos, owner);
            pos += dir * beamPrefab.BeamPartLength;
            yield return new WaitForSeconds(beamPrefab.BeamSpawnInterval);
        }

        // === BODY ===
        int spawned = 0;
        while (spawned < beamPrefab.BeamMaxParts)
        {
            Spawn(beamPrefab, pos, owner);
            pos += dir * beamPrefab.BeamPartLength;
            spawned++;

            yield return new WaitForSeconds(beamPrefab.BeamSpawnInterval);
        }

        // === TAIL ===
        Vector2 tailPos = pos;
        tailPos.x += owner.transform.localScale.x < 0 ? (+1.5f) : (-1.5f);
        if (beamPrefab.BeamTail != null)
        {
            Spawn(beamPrefab.BeamTail, tailPos, owner);
        }
        yield return new WaitForSeconds(beamPrefab.BeamDuration);
        owner.Animator.SetBool("IsBeaming", false);
    }
}
