using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDraft_KeyboardToComputableBool : MonoBehaviour
{
    public FirstDraft_BoolHistory m_firstComputableHistory;
    public KeyCode[] m_touches = new KeyCode[] {
        KeyCode.Keypad0,  KeyCode.Keypad1, KeyCode.Keypad2, 
        KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5,
        KeyCode.Keypad6, KeyCode.Keypad7,KeyCode.Keypad8,
        KeyCode.Keypad9
    };
    
    void Update()
    {
        for (uint i = 0; i < m_touches.Length; i++)
        {
            m_firstComputableHistory.Set(i, Input.GetKey(m_touches[i]));
        }
    }
}
