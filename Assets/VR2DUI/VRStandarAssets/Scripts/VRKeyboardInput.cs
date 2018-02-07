using System;
using UnityEngine;

namespace VRStandardAssets.Utils
{
    // This class encapsulates all the input required for most VR games.
    // It has events that can be subscribed to by classes that need specific input.
    // This class must exist in every scene and so can be attached to the main
    // camera for ease.
    public class VRKeyboardInput : MonoBehaviour
    {
        public event Action<KeyCode> OnKeyPress;                    // Called when KeyCode is pressed.
        public event Action<KeyCode> OnKeyRelease;                  // Called when KeyCode is released.

        public event Action<KeyCode> OnKeyDoublePress;              // Called when KeyCode is pressed two times in a row.


        [SerializeField] private float m_DoubleClickTime = 0.3f;    // The max time allowed between double clicks

        private float m_LastMouseUpTime;                            // The time when the key was last released.

        public float DoubleClickTime{ get { return m_DoubleClickTime; } }


        private void Update()
        {
            CheckInput();
        }


        private void CheckInput()
        {
            if (Input.GetKeyDown("Ctrl"))
            {
                // If anything has subscribed to OnDown call it.
                if (OnKeyPress != null)
                    OnKeyPress(Event.KeyboardEvent("Ctrl").keyCode);
            }

            // This if statement is to trigger events based on the information gathered before.
            if (Input.GetKeyUp("Ctrl"))
            {
                // If anything has subscribed to OnKeyRelease call it.
                if (OnKeyRelease != null)
                    OnKeyRelease(Event.KeyboardEvent("Ctrl").keyCode);

                // If the time between the last release of the key and now is less
                // than the allowed double click time then it's a double click.
                if (Time.time - m_LastMouseUpTime < m_DoubleClickTime)
                {
                    // If anything has subscribed to OnDoubleClick call it.
                    if (OnKeyDoublePress != null)
                        OnKeyDoublePress(Event.KeyboardEvent("Ctrl").keyCode);
                }
                
                // Record the time when Fire1 is released.
                m_LastMouseUpTime = Time.time;
            }
        }

        private void OnDestroy()
        {
            // Ensure that all events are unsubscribed when this is destroyed.
            OnKeyPress = null;
            OnKeyRelease = null;
        }
    }
}