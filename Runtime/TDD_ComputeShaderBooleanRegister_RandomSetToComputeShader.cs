using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_ComputeShaderBooleanRegister_RandomSetToComputeShader : MonoBehaviour
{
    public ComputeShaderBooleanRegister m_register;
    public UI_DebugComputeShaderBoolRegister m_computeDebug;
    public BooleanArraySize m_size= BooleanArraySize._128x128;
    private uint index=0;
    private int maxCount=0;
    
    void Update()
    {
        if (m_register != null && maxCount >0 && m_computeDebug!=null)
        {

            bool value = (UnityEngine.Random.Range(0, 50) % 2 == 0);
            m_register.SetValue(in index, in value);
            uint i  =(uint) (UnityEngine.Random.Range(0, maxCount));
            m_register.SetValue(in i, in value);
            m_computeDebug.PushIn(in m_register);
            index++;
            if (index >= maxCount)
                index = 0;
        }

    }
    public void Start()
    {
        m_register = new ComputeShaderBooleanRegister(m_size);
        index = 0;
        maxCount = (int) m_size;
    }
}
