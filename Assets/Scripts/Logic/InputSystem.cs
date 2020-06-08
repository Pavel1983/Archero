using System;
using UnityEngine;

namespace Archero.InputSystem
{
    public class InputSystem : MonoBehaviour, IInputSystem
    {
        public event Action<EGameKeyCode> EventKeyDown;
        public event Action<EGameKeyCode> EventKeyUp;

        public void SetPaused(bool paused)
        {
            this.paused = paused;
        }

        private void Update()
        {
            if (!paused)
            {
                if (Input.GetKey(KeyCode.A))
                    EventKeyDown?.Invoke(EGameKeyCode.KeyCode_Left);
                else if (Input.GetKeyUp(KeyCode.A))
                    EventKeyUp?.Invoke(EGameKeyCode.KeyCode_Left);
                else if (Input.GetKey(KeyCode.D))
                    EventKeyDown?.Invoke(EGameKeyCode.KeyCode_Right);
                else if (Input.GetKeyUp(KeyCode.D))
                    EventKeyUp?.Invoke(EGameKeyCode.KeyCode_Right);
                else if (Input.GetKey(KeyCode.S))
                    EventKeyDown?.Invoke(EGameKeyCode.KeyCode_Down);
                else if (Input.GetKeyUp(KeyCode.S))
                    EventKeyUp?.Invoke(EGameKeyCode.KeyCode_Down);
                else if (Input.GetKey(KeyCode.W))
                    EventKeyDown?.Invoke(EGameKeyCode.KeyCode_Up);
                else if (Input.GetKeyUp(KeyCode.W))
                    EventKeyUp?.Invoke(EGameKeyCode.KeyCode_Up);
            }
        }

        #region vars

        private bool paused = false;

        #endregion
    }
}
