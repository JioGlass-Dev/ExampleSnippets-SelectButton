using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace JMRSDKExampleSnippets.SelectGunExample
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject weaponCooldown;
        [SerializeField] GameObject cooling;

        void Start()
        {
            weaponCooldown.SetActive(false);
            cooling.SetActive(false);
        }
        /// <summary>
        /// Whether to enable cooldown gameobject or not
        /// </summary>
        /// <param name="value">Enable cooldown gameobject</param>
        public void WeaponCooldown(bool value)
        {
            weaponCooldown.SetActive(value);
        }
        /// <summary>
        /// Whether to enable Cooling gameobject or not
        /// </summary>
        /// <param name="value">Enable cooling gameobject</param>
        public void Cooling(bool value)
        {
            cooling.SetActive(value);
        }
    }
}


