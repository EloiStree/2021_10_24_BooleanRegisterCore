
    
using BooleanRegisterCoreAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace BooleanRegisterCoreAPI.Core
{
    public class BooleanComputeBufferArrayRegisterStructBytes :
        IBooleanRegisterAccess
        , IBooleanRegisterSet
        , IBooleanArrayRegister
    {
        public struct QuatroBytesBool {
            public bool m_0;
            public bool m_1;
            public bool m_2;    
            public bool m_3;
        }
        private uint m_sizeChoosed = 4;
        private uint m_sizeChoosedDiv4 = 1;
        private QuatroBytesBool[] m_value = new QuatroBytesBool[1];
        private ComputeBuffer m_valueAsComputeBuffer = null;

        public int m_Test;

        public BooleanComputeBufferArrayRegisterStructBytes(in BooleanArraySize size)
        {
            bool[] b = new bool[(int)size];
            SetSize(in b);
            ThrowExceptionIfArrayOfZeroSize();
        }
        public BooleanComputeBufferArrayRegisterStructBytes(in int size)
        {
            bool[] b = new bool[size];
            SetSize(in b);
            ThrowExceptionIfArrayOfZeroSize();
        }
        public BooleanComputeBufferArrayRegisterStructBytes(in bool[] value)
        {
           
            SetSize(in value);
            ThrowExceptionIfArrayOfZeroSize();
        }

        ~BooleanComputeBufferArrayRegisterStructBytes()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (m_valueAsComputeBuffer != null && m_valueAsComputeBuffer.IsValid())
                m_valueAsComputeBuffer.Dispose();
        }

        private void SetSize(in bool[] value)
        {
            m_sizeChoosed = (uint) value.Length;
            m_sizeChoosedDiv4 = m_sizeChoosed / 4;
            m_value = new QuatroBytesBool[m_sizeChoosedDiv4];
            for (uint i = 0; i < value.Length; i++)
            {
                SetValue(i, value[i]);
            }
            Dispose();
            m_valueAsComputeBuffer = new ComputeBuffer(
                (int)m_sizeChoosedDiv4,
                Marshal.SizeOf(typeof(QuatroBytesBool)),
                ComputeBufferType.Default);

            ThrowExceptionIfArrayOfZeroSize();
        }

        public void ApplyData()
        {

            m_valueAsComputeBuffer.SetData(m_value);
        }
        public void GetComputeBufferRef(out ComputeBuffer buffer, bool applyBefore = true)
        {
            if (applyBefore)
                ApplyData();
            buffer = m_valueAsComputeBuffer;

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
            GetIndex((int) index, out int halfIndex, out int offset);
            if (offset == 0)
                value = m_value[halfIndex].m_0;
            else if (offset == 1)
                value = m_value[halfIndex].m_1;
            else if (offset == 2)
                value = m_value[halfIndex].m_2;
            else if (offset == 3)
                value = m_value[halfIndex].m_3;
            else value = false;
        }

        public void IsIndexValide(in uint index, out bool isValide)
        {
            isValide = index < m_sizeChoosed;
        }

        public void SetValue(in uint index, in bool value)
        {
            GetIndex((int)index, out int halfIndex, out int offset);
            if (offset == 0)
                m_value[halfIndex].m_0= value;
            else if (offset == 1)
                m_value[halfIndex].m_1 = value;
            else if (offset == 2)
                m_value[halfIndex].m_2 = value;
            else if (offset == 3)
                 m_value[halfIndex].m_3 = value;
        }

        public void GetIndex(in int index, out int halfIndex, out int offset) {
            halfIndex = index / 4;
            offset = index % 4;
        }
    }
}
