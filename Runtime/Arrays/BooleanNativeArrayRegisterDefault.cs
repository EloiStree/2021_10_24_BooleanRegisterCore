using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BooleanNativeArrayRegisterDefault 
{
    private uint m_sizeChoosed = 4;
    private NativeArray<bool> m_value;
    public BooleanNativeArrayRegisterDefault(in bool[] value)
    {
        SetSize(value);
    }
    ~BooleanNativeArrayRegisterDefault()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (m_value != null && m_value.IsCreated)
            m_value.Dispose();
    }

    private void SetSize(in bool[] value)
    {
        Dispose();
        m_sizeChoosed = (uint)value.Length;
        m_value = new NativeArray<bool>(value, Allocator.Persistent);
        ThrowExceptionIfArrayOfZeroSize();
    }



    private void ThrowExceptionIfArrayOfZeroSize()
    {
        if (m_sizeChoosed < 1)
            throw new Exception("Size of boolean register can't be zero");
    }


    public void GetMaxSize(out uint arraySize)
    {
        arraySize = m_sizeChoosed;
    }

    public void GetValue(in uint index, out bool value)
    {
        value = m_value[(int)index];
    }

    public void IsIndexValide(in uint index, out bool isValide)
    {
        isValide = index < m_sizeChoosed;
    }

    public void SetValue(in uint index, in bool value)
    {
        m_value[(int)index] = value;
    }
}
