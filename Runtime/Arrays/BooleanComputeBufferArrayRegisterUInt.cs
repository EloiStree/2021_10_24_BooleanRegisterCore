
using BooleanRegisterCoreAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace BooleanRegisterCoreAPI.Core
{
    public class BooleanComputeBufferArrayRegisterUInt :
        IBooleanRegisterAccess
        , IBooleanRegisterSet
        , IBooleanArrayRegister
    {

        private uint m_sizeChoosed = 4;
        private uint[] m_value = new uint[4];
        private ComputeBuffer m_valueAsComputeBuffer=null;
    
        public BooleanComputeBufferArrayRegisterUInt(in BooleanArraySize size)
        {
            m_sizeChoosed = (uint)size;
            m_value = new uint[(uint)size];
            SetSize(in m_value);
            ThrowExceptionIfArrayOfZeroSize();
        }
        public BooleanComputeBufferArrayRegisterUInt(in int size)
        {
            m_sizeChoosed = (uint)size;
            m_value = new uint[size];
            SetSize(in m_value);
            ThrowExceptionIfArrayOfZeroSize();
        }
        public BooleanComputeBufferArrayRegisterUInt(in bool[] value)
        {
            m_sizeChoosed = (uint)value.Length;
            m_value = value.Select(k=>(uint)(k?1:0)).ToArray();
            SetSize(in m_value);
            ThrowExceptionIfArrayOfZeroSize();
        }

        ~BooleanComputeBufferArrayRegisterUInt()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (m_valueAsComputeBuffer != null && m_valueAsComputeBuffer.IsValid())
                m_valueAsComputeBuffer.Dispose();
        }

        private void SetSize(in uint[] value)
        {
            Dispose();
            m_sizeChoosed = (uint)value.Length;
            m_valueAsComputeBuffer = new ComputeBuffer(
                (int) m_sizeChoosed, 
                sizeof(uint), 
                ComputeBufferType.Default);
           
            ThrowExceptionIfArrayOfZeroSize();
        }

        public void ApplyData() {

            m_valueAsComputeBuffer.SetData(m_value);
        }
        public void GetComputeBufferRef(out ComputeBuffer buffer, bool applyBefore = true) {
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
            value = m_value[(int)index]==1;
        }

        public void IsIndexValide(in uint index, out bool isValide)
        {
            isValide = index < m_sizeChoosed;
        }

        public void SetValue(in uint index, in bool value)
        {
            m_value[(int)index] =(ushort) (value?1:0);
        }
    }
}
