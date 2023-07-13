using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    //Sliders for UI
    [SerializeField] private float _xRotation = 1f;
    [SerializeField] private float _launchPower = 1f;

    [SerializeField] private GameObject xRotationSlider;
    [SerializeField] private GameObject launchPowerSlider;
    [SerializeField] private TextMeshProUGUI xRotationText;
    public float xRotation
    {
        get { return _xRotation; }
        set
        {
            if (_xRotation == value) return;

            _xRotation = value;

            OnVariableChange(xRotationSlider, _xRotation);
        }
    }

    public float launchPower
    {
        get { return _launchPower; }
        set
        {
            if (_launchPower == value) return;

            _launchPower = value;

            OnVariableChange(launchPowerSlider, _launchPower);
        }
    }
    public delegate void OnVariableChangeDelegate(GameObject fillbar, float newVal);
    public event OnVariableChangeDelegate OnVariableChange;
    public void SetSliderValue(GameObject fillbar, float stamina)
    {
        if (fillbar.GetComponent<Slider>() != null)
        {
            Slider slider = fillbar.GetComponent<Slider>();
            slider.value = stamina;
        }
        else if (fillbar.GetComponent<Image>() != null)
        {
            Image image = fillbar.GetComponent<Image>();        

            float newStamina = -1 / stamina;

            newStamina = Mathf.Lerp(0, 0.231f, newStamina) * 10;
            image.fillAmount = newStamina;

            xRotationText.text = (int)-stamina + "°";
        }

    }

    public BallLauncher ballLauncher;
    public LineVisualizer lineVisualizer;
    public PlayerRotate playerRotate;
    public Transform initialCameraPosition;

    public void Start()
    {
        OnVariableChange += SetSliderValue;
        xRotation = 10;
        launchPower = 0;
    }

    public Transform camera_follower;
    public Transform cam_transform;
    public Vector3 cameraFollowOffset;

    public bool followBall = false;
    ProjectileType projectileType;

    bool isIncreasing = true;
    bool soundPlaying = false;


    public AudioClip chargeUpClip, releaseClip;
    void Update()
    {
        projectileType = ProjectilePool.sharedInstance.selectedProjectileType;

        if (!followBall)
        {
            xRotation = playerRotate.rotate();
            if (!GameController.sharedInstance.levelsSystem.isPaused)
            {
                //Launch the ball once spacebar was held
                if (Input.GetButton("Jump"))
                {
                    launchPowerSlider.GetComponent<Slider>().maxValue = projectileType.maxVelocity;

                    if (isIncreasing)
                    {
                        //Build up power while holding down the spacebar

                        launchPower += projectileType.buildUpSpeed * Time.deltaTime;
                        if (launchPower >= projectileType.maxVelocity)
                        {
                            isIncreasing = false;
                        }
                    }
                    else
                    {
                        //Go back in the other direction
                        launchPower -= projectileType.buildUpSpeed * Time.deltaTime;
                        if (launchPower <= 0)
                        {
                            isIncreasing = true;
                        }
                    }

                    //Play a looping audioclip
                    if (!soundPlaying)
                    {
                        SoundController.sharedInstance.playSound(chargeUpClip, true);
                        soundPlaying = true;
                    }
                }
                else
                {
                    //Player has let go of the space bar
                    //Check if the player has builtup power
                    if (launchPower > 0)
                    {
                        //Enable the gravity of the selected Projectile
                        projectileType.GetComponent<Rigidbody>().useGravity = true;

                        //Proceed to launch the ball
                        ballLauncher.launchBall(launchPower, projectileType.gameObject);
                        launchPower = 0;
                        //Proceed to parent the camera to the ball in order to follow it
                        followBall = true;

                        SoundController.sharedInstance.playSound(releaseClip, false);
                        soundPlaying = false;
                    }
                }
            }


        } else
        {
            camera_follower.position = Vector3.Lerp(camera_follower.position, projectileType.gameObject.transform.position, Time.time);
            cam_transform.parent = camera_follower;
            cam_transform.localPosition = cameraFollowOffset;
        }

        //Visualize the power of the shot
        lineVisualizer.VisualizeLine(launchPower);



        GameController.sharedInstance.checkProjectileMovement(projectileType.gameObject);
    }


    public void stopFollowingBall()
    {
        followBall = false;
        camera_follower.position = Vector3.Lerp(camera_follower.position, initialCameraPosition.position, Time.time);


        cam_transform.parent = transform;
        cam_transform.position = Vector3.Lerp(cam_transform.position, initialCameraPosition.position, Time.time);
    }
}
