using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackAction : MonoBehaviour, Action
{
    [Header("Ranged Attack Properties")]
    public int fireDelay = 30;
    public Transform bulletSpawn;

    // TODO: Move these to the bullet manager
    public GameObject bulletPrefab;
    public Transform bulletParent;

    private bool hasLOS;
    private PlayerDetection playerDetection;
    private SoundManager soundManager;

    private void Awake()
    {
        playerDetection = transform.parent.GetComponentInChildren<PlayerDetection>();
        soundManager = FindObjectOfType<SoundManager>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("[BULLETS]").transform;
    }

    // Update is called once per frame
    void Update()
    {
        hasLOS = playerDetection.LOS;
    }

    private void FixedUpdate()
    {
        if (hasLOS && Time.frameCount % fireDelay == 0)
        {
            Execute();
        }
    }

    public void Execute()
    {
        var bullet = Instantiate(bulletPrefab,bulletSpawn.position,Quaternion.identity,bulletParent);
        bullet.GetComponent<Bullet>().Activate();
        soundManager.PlaySoundFX(Sound.BULLET, Channel.BULLET);
    }

}
