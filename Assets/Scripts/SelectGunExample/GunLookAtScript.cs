using UnityEngine;
using JMRSDK.InputModule;

namespace JMRSDKExampleSnippets.SelectGunExample
{
    /// <summary>
    /// The GunLookAtScript class handles the functionality for making the gun look towards the pointer.
    /// </summary>
    public class GunLookAtScript : MonoBehaviour
    {
        Transform target;

        void Start()
        {
            //Sets the target to the pointer/cursor
            target = JMRPointerManager.Instance.GetCursorTransform();
        }

        void Update()
        {
            LookAtPointer();
        }

        /// <summary>
        /// Make Gun Look At the Pointer
        /// </summary>
        public void LookAtPointer()
        {
            transform.LookAt(target);
        }
    }
}

