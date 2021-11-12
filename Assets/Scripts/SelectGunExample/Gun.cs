using UnityEngine;
using JMRSDK.InputModule;

namespace JMRSDKExampleSnippets.SelectGunExample
{
    /// <summary>
    /// The Gun class handles the functionality of automatic and single shot firing by implementing the various select button functions.
    /// </summary>
    public class Gun : MonoBehaviour, ISelectHandler, ISelectClickHandler
    {
        /// <summary>
        /// The Damage inflicted by the bullet
        /// </summary>
        public float damage = 10f;
        /// <summary>
        /// The Range of the Raycast
        /// </summary>
        public float range = 100f;
        /// <summary>
        /// The Particle Effect for the muzzle flash
        /// </summary>
        public ParticleSystem muzzleFlash;
        /// <summary>
        /// The Particle Effect for bullet impact
        /// </summary>
        public GameObject impactEffect;
        /// <summary>
        /// The fire Delay Time between consequent fires
        /// </summary>
        public float fireDelayTime = 0.15f;
        /// <summary>
        /// The cooldown time for the gun
        /// </summary>
        public float cooldownTime = 3f;
        /// <summary>
        /// The no. of shoots allowed before the cooldown period required
        /// </summary>
        public int shootCountLimit = 30;
        /// <summary>
        /// The impact force given to the object being hit
        /// </summary>
        public float impactForce = 30f;
        /// <summary>
        /// The shoot audio clip sound
        /// </summary>
        public AudioClip shootSound;
        AudioSource audioSource;

        float timeSinceLastShoot = 0f;
        float timeSinceCooldown = 0f;
        int shootCount = 0;

        /// <summary>
        /// The UIManager Reference to handle UI functionality
        /// </summary>
        public UIManager uIManager;
        /// <summary>
        /// Set true if you want automatic firing mode or Set false for single shot firing mode
        /// </summary>
        public bool automatic = false;

        void Start()
        {
            // Adding a global listener for hearing select events globally
            JMRInputManager.Instance.AddGlobalListener(gameObject);
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            AutomaticFiringMode();
        }

        /// <summary>
        /// If Automatic Firing Mode is true then uses the Source Button Down Getter Function to make sure to trigger firing as long
        /// as Select Button is pressed down
        /// </summary>
        public void AutomaticFiringMode()
        {
            if (automatic)
            {
                if (JMRInteraction.GetSourceDown(JMRSDK.InputModule.JMRInteractionSourceInfo.Select))
                {
                    if (timeSinceLastShoot >= fireDelayTime)
                    {
                        if (shootCount < shootCountLimit)
                        {
                            Shoot();
                            timeSinceLastShoot = 0;
                        }
                        else
                        {
                            uIManager.WeaponCooldown(true);
                        }
                    }
                }
                else if (JMRInteraction.GetSourceUp(JMRSDK.InputModule.JMRInteractionSourceInfo.Select))
                {
                    timeSinceCooldown += Time.deltaTime;
                    if (timeSinceCooldown > cooldownTime)
                    {
                        uIManager.WeaponCooldown(false);
                        shootCount = 0;
                        timeSinceCooldown = 0;
                    }
                }
                timeSinceLastShoot += Time.deltaTime;
            }
        }

        /// <summary>
        /// Handles shoot functionality
        /// </summary>
        public void Shoot()
        {
            if(automatic)
            {
                shootCount++;
            }
            muzzleFlash.Play();
            audioSource.PlayOneShot(shootSound, 0.7f);

            RaycastHit hit;
            if (Physics.Raycast(JMRPointerManager.Instance.GetCurrentRay(), out hit, range))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impactGO;
                if (hit.transform.tag == "Target")
                {
                    impactGO = Instantiate(impactEffect, hit.point - new Vector3(0, .3f, 2), Quaternion.LookRotation(hit.normal));
                }
                else
                {
                    impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
                Destroy(impactGO, 1f);
            }
        }

        /// <summary>
        /// When select button is pressed down then disable the cooling 
        /// </summary>
        /// <param name="eventData">Contains click details</param>
        public void OnSelectDown(SelectEventData eventData)
        {
            uIManager.Cooling(false);
        }

        /// <summary>
        /// When select button is not pressed then enable cooling
        /// </summary>
        /// <param name="eventData">Contains click details</param>
        public void OnSelectUp(SelectEventData eventData)
        {
            uIManager.Cooling(true);

        }

        /// <summary>
        /// Setting if firing mode is automatic or single shot
        /// </summary>
        /// <param name="value">automatic setting boolean value</param>
        public void SetAutomatic(bool value)
        {
            automatic = value;
        }

        /// <summary>
        /// When Select button clicked then trigger single shot functionality if not in automatic firing mode
        /// </summary>
        /// <param name="eventData">Contains click details</param>
        public void OnSelectClicked(SelectClickEventData eventData)
        {
            if (!automatic)
            {
                Shoot();
            }
        }
    }
}
