using System;

namespace Archero.InputSystem
{
    public enum EGameKeyCode
    {
        KeyCode_Left,
        KeyCode_Right, 
        KeyCode_Up, 
        KeyCode_Down
    }

    public interface IInputSystem
    {
        event Action<EGameKeyCode> EventKeyDown;
        event Action<EGameKeyCode> EventKeyUp;
        
    }
}
