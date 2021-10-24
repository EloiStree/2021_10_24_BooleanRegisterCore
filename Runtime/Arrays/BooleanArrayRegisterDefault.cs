using BooleanRegisterCoreAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterCoreAPI.Core
{
    public class BooleanArrayRegisterDefault :
        IBooleanRegisterAccess
        , IBooleanRegisterSet
        , IBooleanArrayRegister
    {

        private uint m_sizeChoosed=4;
        private bool[] m_value = new bool[4];
        public BooleanArrayRegisterDefault(in BooleanArraySize size)
        {
            m_sizeChoosed = (uint)size;
            m_value = new bool[(uint)size];
            ThrowExceptionIfArrayOfZeroSize();
        }
        public BooleanArrayRegisterDefault(in int size)
        {
            m_sizeChoosed = (uint)size;
            m_value = new bool[size];
            ThrowExceptionIfArrayOfZeroSize();
        }
        public BooleanArrayRegisterDefault(in bool[] value)
        {
            m_sizeChoosed = (uint)value.Length;
            m_value = value.ToArray();
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
            value = m_value[index];
        }

        public void IsIndexValide(in uint index, out bool isValide)
        {
            isValide = index < m_sizeChoosed;
        }

        public void SetValue(in uint index, in bool value)
        {
            m_value[index] = value;
        }
    }
}
