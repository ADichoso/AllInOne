using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProjectileTypesPanel : MonoBehaviour
{
    public Button[] ProjectileTypeButtons;
    public GameObject orientation;

    public void onProjectileButtonClick(ProjectileType projectileType)
    {
        if (!GameController.sharedInstance.levelsSystem.isPaused && !GameController.sharedInstance.playerController.followBall)
        {
            Debug.Log("SWITCHING TO" + projectileType.projectile_name);
            ProjectilePool.sharedInstance.selectProjectile(projectileType);
        }
    }
}
