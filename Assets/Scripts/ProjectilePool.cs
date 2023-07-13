using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    #region Singleton
    public static ProjectilePool sharedInstance;

    void Awake()
    {
        sharedInstance = this;
        if (this != sharedInstance)
        {
            Debug.Log("Warning! More than 1 instance of ProjectilePool has been detected");
        }
    }
    #endregion


    [Header("OBJECT POOL")]
    public ProjectileType[] projectileTypes;
    public GameObject[] spawnPositions;
    public GameObject[] projectileSetups;

    public ProjectileType selectedProjectileType;

    public ProjectileTypesPanel typesPanel;

    Transform orientation;
    //Initializes the Object Pool. Executes at the very start of the application
    public void InitializeObjectPool(Transform _orientation)
    {
        orientation = _orientation;

        selectProjectile(projectileTypes[0]);
    }

    //Changes the selected projectile type
    public void selectProjectile(ProjectileType projectileType)
    {
        //Loop through the current Object Pool for an object with the same name AND is not currently active
        for (int i = 0; i < projectileTypes.Length; i++)
        {
            if (projectileTypes[i] == projectileType)
            {
                //Select Projectile
                selectedProjectileType = projectileType;

                //Also enable their setup
                projectileSetups[i].gameObject.SetActive(true);

                //Enable this projectile
                selectedProjectileType.gameObject.SetActive(true);

                //Spawn it onto the spawn position
                selectedProjectileType.transform.position = spawnPositions[i].transform.position;

                //Set the orientation location to the spawn location as well
                orientation.position = spawnPositions[i].transform.position;

                //Parent to setup object
                selectedProjectileType.transform.parent = projectileSetups[i].transform;

                selectedProjectileType.transform.localRotation = Quaternion.identity;

                //Disable the rigidbody first to prevent it from falling
                selectedProjectileType.GetComponent<Rigidbody>().useGravity = false;
                selectedProjectileType.GetComponent<Rigidbody>().velocity = Vector3.zero;
                selectedProjectileType.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


            }
            else
            {
                //Disable all other projectiles and their setups
                projectileTypes[i].gameObject.SetActive(false);
                projectileSetups[i].gameObject.SetActive(false);
            }
        }
    }
}
