using BooleanRegisterCoreAPI.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityBooleanRegisterDefault
{
    public BooleanComputeBufferArrayRegisterUInt m_computeBufferRegister;
    public BooleanComputeBufferArrayRegisterStructBytes m_computeBufferStructRegister;
    public BooleanNativeArrayRegisterDefault        m_nativeRegister;
    public BooleanArrayRegisterDefault              m_booleanRegister;


    public UnityBooleanRegisterDefault(uint size) {

        int uSize = (int) size;
        bool [] b = new bool[uSize];
        m_booleanRegister = new BooleanArrayRegisterDefault(in b);
        //m_computeBufferRegister = new BooleanComputeBufferArrayRegisterUInt(in b);
        m_nativeRegister = new BooleanNativeArrayRegisterDefault(in b);
        m_computeBufferStructRegister = new BooleanComputeBufferArrayRegisterStructBytes(in b);
    }
    ~UnityBooleanRegisterDefault() {
        m_computeBufferRegister.Dispose();
        m_nativeRegister.Dispose();
        m_computeBufferStructRegister.Dispose();
    }
}
