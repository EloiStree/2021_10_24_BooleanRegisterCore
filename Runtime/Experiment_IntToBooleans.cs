using BooleanRegisterCoreAPI.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoolToIntUtility{
    public static void C_8BoolToByte(ref byte result, params bool[] bits)
    {
        if (bits.Length != 8)
        {
            throw new ArgumentException("illegal number of bits");
        }

        result = 0;
        if (bits[7]) result++;
        if (bits[6]) result += 2;
        if (bits[5]) result += 4;
        if (bits[4]) result += 8;
        if (bits[3]) result += 16;
        if (bits[2]) result += 32;
        if (bits[1]) result += 64;
        if (bits[0]) result += 128;
    }
    public static void C_8BoolsToByte(ref byte result, in bool b0_128, in bool b1, in bool b2, in bool b3, in bool b4, in bool b5, in bool b6, in bool b7)
    {
        result = 0;
        if (b7) result++;
        if (b6) result += 2;
        if (b5) result += 4;
        if (b4) result += 8;
        if (b3) result += 16;
        if (b2) result += 32;
        if (b1) result += 64;
        if (b0_128) result += 128;
    }
    public static void C_4BytesToInt(ref int fourBytesAsInt, in byte b0, in byte b1, in byte b2, in byte b3)
    {
        //encoding on the cpu
        fourBytesAsInt = 0;
        fourBytesAsInt += (int)b0;
        fourBytesAsInt += (int)(b1 << 8);
        fourBytesAsInt += (int)(b2 << 16);
        fourBytesAsInt += (int)(b3 << 24);
    }
    public static void C_IntTo4Bytes(in int fourBytesAsInt, ref int byteA, ref int byteB, ref int byteC, ref int byteD)
    {
        //decoding on the gpu
        byteA = fourBytesAsInt & 0xFF;
        byteB = (fourBytesAsInt >> 8) & 0xFF;
        byteC = (fourBytesAsInt >> 16) & 0xFF;
        byteD = (fourBytesAsInt >> 24) & 0xFF;
    }
    public static void C_IntTo4Bytes(in int fourBytesAsInt, ref byte byteA, ref byte byteB, ref byte byteC, ref byte byteD)
    {
        //decoding on the gpu
        byteA = (byte)((fourBytesAsInt & 0xFF));
        byteB = (byte)((fourBytesAsInt >> 8) & 0xFF);
        byteC = (byte)((fourBytesAsInt >> 16) & 0xFF);
        byteD = (byte)((fourBytesAsInt >> 24) & 0xFF);
    }
    public static void C_ByteTo8Bools(in byte theByte, ref bool[] eightBools)
    {
        for (int i = 0; i < 8; i++)
            eightBools[i] =
                ((theByte & (1 << i)) != 0);

    }
    public static void C_ByteTo8Bools(in byte theByte, ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5, ref bool b6, ref bool b7)
    {
        //TO TEST
        b0 = ((theByte & (1 << 7)) != 0);
        b1 = ((theByte & (1 << 6)) != 0);
        b2 = ((theByte & (1 << 5)) != 0);
        b3 = ((theByte & (1 << 4)) != 0);
        b4 = ((theByte & (1 << 3)) != 0);
        b5 = ((theByte & (1 << 2)) != 0);
        b6 = ((theByte & (1 << 1)) != 0);
        b7 = ((theByte & (1 << 0)) != 0);
    }
    public static void C_32BoolsToInt(in bool[] m_32BoolsSouce, ref int m_32BoolgsAsInt)
    {
        byte m_0 = 0;
        byte m_1 = 0;
        byte m_2 = 0;
        byte m_3 = 0;
        BoolToIntUtility.C_8BoolsToByte(ref m_0, in m_32BoolsSouce[7], in m_32BoolsSouce[6], in m_32BoolsSouce[5], in m_32BoolsSouce[4], in m_32BoolsSouce[3], in m_32BoolsSouce[2], in m_32BoolsSouce[1], in m_32BoolsSouce[0]);
        BoolToIntUtility.C_8BoolsToByte(ref m_1, in m_32BoolsSouce[15], in m_32BoolsSouce[14], in m_32BoolsSouce[13], in m_32BoolsSouce[12], in m_32BoolsSouce[11], in m_32BoolsSouce[10], in m_32BoolsSouce[9], in m_32BoolsSouce[8]);
        BoolToIntUtility.C_8BoolsToByte(ref m_2, in m_32BoolsSouce[23], in m_32BoolsSouce[22], in m_32BoolsSouce[21], in m_32BoolsSouce[20], in m_32BoolsSouce[19], in m_32BoolsSouce[18], in m_32BoolsSouce[17], in m_32BoolsSouce[16]);
        BoolToIntUtility.C_8BoolsToByte(ref m_3, in m_32BoolsSouce[31], in m_32BoolsSouce[30], in m_32BoolsSouce[29], in m_32BoolsSouce[28], in m_32BoolsSouce[27], in m_32BoolsSouce[26], in m_32BoolsSouce[25], in m_32BoolsSouce[24]);
        BoolToIntUtility.C_4BytesToInt(ref m_32BoolgsAsInt, in m_0, in m_1, in m_2, in m_3);
    }
    public static void C_IntTo32Bools(in int m_32BoolgsAsInt, ref bool[] m_32BoolsSouce)
        {
            byte m_0 = 0;
            byte m_1 = 0;
            byte m_2 = 0;
            byte m_3 = 0;
        BoolToIntUtility.C_IntTo4Bytes(in m_32BoolgsAsInt, ref m_0, ref m_1, ref m_2, ref m_3);
        BoolToIntUtility.C_ByteTo8Bools(in m_0, ref m_32BoolsSouce[7], ref m_32BoolsSouce[6], ref m_32BoolsSouce[5], ref m_32BoolsSouce[4], ref m_32BoolsSouce[3], ref m_32BoolsSouce[2], ref m_32BoolsSouce[1], ref m_32BoolsSouce[0]);
        BoolToIntUtility.C_ByteTo8Bools(in m_1, ref m_32BoolsSouce[15], ref m_32BoolsSouce[14], ref m_32BoolsSouce[13], ref m_32BoolsSouce[12], ref m_32BoolsSouce[11], ref m_32BoolsSouce[10], ref m_32BoolsSouce[9], ref m_32BoolsSouce[8]);
        BoolToIntUtility.C_ByteTo8Bools(in m_2, ref m_32BoolsSouce[23], ref m_32BoolsSouce[22], ref m_32BoolsSouce[21], ref m_32BoolsSouce[20], ref m_32BoolsSouce[19], ref m_32BoolsSouce[18], ref m_32BoolsSouce[17], ref m_32BoolsSouce[16]);
        BoolToIntUtility.C_ByteTo8Bools(in m_3, ref m_32BoolsSouce[31], ref m_32BoolsSouce[30], ref m_32BoolsSouce[29], ref m_32BoolsSouce[28], ref m_32BoolsSouce[27], ref m_32BoolsSouce[26], ref m_32BoolsSouce[25], ref m_32BoolsSouce[24]);
    }
    public static void C_32BoolsToInt(in bool[] m_32BoolsSouce, ref int m_32BoolgsAsInt, ref byte bTmp0, ref byte bTmp1, ref byte bTmp2, ref byte bTmp3)
    {
        BoolToIntUtility.C_8BoolsToByte(ref bTmp0, in m_32BoolsSouce[7], in m_32BoolsSouce[6], in m_32BoolsSouce[5], in m_32BoolsSouce[4], in m_32BoolsSouce[3], in m_32BoolsSouce[2], in m_32BoolsSouce[1], in m_32BoolsSouce[0]);
        BoolToIntUtility.C_8BoolsToByte(ref bTmp1, in m_32BoolsSouce[15], in m_32BoolsSouce[14], in m_32BoolsSouce[13], in m_32BoolsSouce[12], in m_32BoolsSouce[11], in m_32BoolsSouce[10], in m_32BoolsSouce[9], in m_32BoolsSouce[8]);
        BoolToIntUtility.C_8BoolsToByte(ref bTmp2, in m_32BoolsSouce[23], in m_32BoolsSouce[22], in m_32BoolsSouce[21], in m_32BoolsSouce[20], in m_32BoolsSouce[19], in m_32BoolsSouce[18], in m_32BoolsSouce[17], in m_32BoolsSouce[16]);
        BoolToIntUtility.C_8BoolsToByte(ref bTmp3, in m_32BoolsSouce[31], in m_32BoolsSouce[30], in m_32BoolsSouce[29], in m_32BoolsSouce[28], in m_32BoolsSouce[27], in m_32BoolsSouce[26], in m_32BoolsSouce[25], in m_32BoolsSouce[24]);
        BoolToIntUtility.C_4BytesToInt(ref m_32BoolgsAsInt, in bTmp0, in bTmp1, in bTmp2, in bTmp3);
    }
    public static void C_IntTo32Bools(in int m_32BoolgsAsInt, ref bool[] m_32BoolsDestination, byte bTmp0, ref byte bTmp1, ref byte bTmp2, ref byte bTmp3)
    {
        BoolToIntUtility.C_IntTo4Bytes(in m_32BoolgsAsInt, ref bTmp0, ref bTmp1, ref bTmp2, ref bTmp3);
        BoolToIntUtility.C_ByteTo8Bools(in bTmp0, ref m_32BoolsDestination[7], ref m_32BoolsDestination[6], ref m_32BoolsDestination[5], ref m_32BoolsDestination[4], ref m_32BoolsDestination[3], ref m_32BoolsDestination[2], ref m_32BoolsDestination[1], ref m_32BoolsDestination[0]);
        BoolToIntUtility.C_ByteTo8Bools(in bTmp1, ref m_32BoolsDestination[15], ref m_32BoolsDestination[14], ref m_32BoolsDestination[13], ref m_32BoolsDestination[12], ref m_32BoolsDestination[11], ref m_32BoolsDestination[10], ref m_32BoolsDestination[9], ref m_32BoolsDestination[8]);
        BoolToIntUtility.C_ByteTo8Bools(in bTmp2, ref m_32BoolsDestination[23], ref m_32BoolsDestination[22], ref m_32BoolsDestination[21], ref m_32BoolsDestination[20], ref m_32BoolsDestination[19], ref m_32BoolsDestination[18], ref m_32BoolsDestination[17], ref m_32BoolsDestination[16]);
        BoolToIntUtility.C_ByteTo8Bools(in bTmp3, ref m_32BoolsDestination[31], ref m_32BoolsDestination[30], ref m_32BoolsDestination[29], ref m_32BoolsDestination[28], ref m_32BoolsDestination[27], ref m_32BoolsDestination[26], ref m_32BoolsDestination[25], ref m_32BoolsDestination[24]);
    }
}

public class Experiment_IntToBooleans : MonoBehaviour
{

    public bool m_b0;
    public bool m_b1;
    public bool m_b2;
    public bool m_b3;
    public bool m_b4;
    public bool m_b5;
    public bool m_b6;
    public bool m_b7;

    public byte m_0;
    public byte m_1;
    public byte m_2;
    public byte m_3;
    
    public int m_produceInt;
    public byte m_a;
    public byte m_b;
    public byte m_c;
    public byte m_d;

    public bool[] m_aAsbits = new bool[8];


    public int m_bitIndex = 2;
    public bool m_bitState;

    public bool[] m_32BoolsSouce = new bool[32];
    public int m_32BoolsAsInt;
    public bool[] m_32BoolsReceive = new bool[32];

    public ComputeShader m_computeShader;
    public RenderTexture m_result;
    public MeshRenderer m_renderer;
    private void OnValidate()
    {
        BoolToIntUtility.C_8BoolsToByte(ref m_0, in m_b7, in m_b6, in m_b5, in m_b4, in m_b3, in m_b2, in m_b1, in m_b0);
        BoolToIntUtility.C_4BytesToInt(ref m_produceInt, in m_0, in m_1, in m_2, in m_3);
        BoolToIntUtility.C_IntTo4Bytes(in m_produceInt, ref m_a, ref m_b, ref m_c, ref m_d);
        BoolToIntUtility.C_ByteTo8Bools(in m_a,  ref m_aAsbits);

        BoolToIntUtility.C_32BoolsToInt(in m_32BoolsSouce, ref m_32BoolsAsInt);
        BoolToIntUtility.C_IntTo32Bools(in m_32BoolsAsInt , ref m_32BoolsReceive);

        m_bitState = (m_32BoolsAsInt & (1 << m_bitIndex)) != 0;

        ComputeTexture();




    }

    private void Update()
    {
        ComputeTexture();
    }

    private void ComputeTexture()
    {
        int computeKernel = 0;// m_computeShader.FindKernel("CSMain");
        ComputeBuffer cBuff = new ComputeBuffer(1, sizeof(int), ComputeBufferType.Default);
        cBuff.SetData(new int[] { m_32BoolsAsInt });
                
            m_result = new RenderTexture(8, 8,24, RenderTextureFormat.ARGB32);
            m_result.enableRandomWrite = true;
            m_result.Create();

        if (m_renderer != null)
            m_renderer.sharedMaterial.mainTexture = m_result;

        m_computeShader.SetInt( "m_width", 8);
        m_computeShader.SetBuffer(computeKernel, "m_32ByteInInt", cBuff);
        m_computeShader.SetTexture(computeKernel, "m_result", m_result);

//        int groups = Mathf.CeilToInt( ((int)64) / 8f);
        m_computeShader.Dispatch(computeKernel, m_result.width/ 8, m_result.height /8, 1);
        cBuff.Release();
    }
}

public class BufferableBooleansAsInt {

    [SerializeField] bool[] m_32bools= new bool[32];
    
    public void SetValue(in uint index, in bool newValue) {
        m_32bools[index] = newValue;
    }
    public bool[] GetRefOfBools() { return m_32bools; }

    public void GetValue(in uint index, out bool value)
    {
        value = m_32bools[index] ;
    }
}
public class BufferableBooleanRegister {

    public uint m_size;
    
    public BufferableBooleansAsInt[] m_register;
    public BufferableBooleanRegister(BooleanArraySize size) {

        m_size = (uint)size;
        if (m_size < 64)
            throw new Exception("To be buffered the shader need int and so 32 booleans but as we prefere square value, it is better to give 64 minimum");

       int block= 1+(int)(m_size / 32);
        m_register = new BufferableBooleansAsInt[block];
        for (int i = 0; i < m_register.Length; i++)
        {
            m_register[i] = new BufferableBooleansAsInt();
        }

    }
}


public class ComputeShaderBooleanRegister : IBooleanRegisterAccess
        , IBooleanRegisterSet
        , IBooleanArrayRegister
{
    public BufferableBooleanRegister m_register= new BufferableBooleanRegister(BooleanArraySize._128x128);

    public ComputeShaderBooleanRegister(BooleanArraySize size)
    {
        SetSize(size);
    }

    public void SetSize(BooleanArraySize size) {
        m_register = new BufferableBooleanRegister(size);
    }
    public void GetMaxSize(out uint arraySize)
    {
        arraySize = m_register.m_size;
    }

    public void GetValue(in uint index, out bool value)
    {
        GetIndexOfBit(in index, out uint intIndex, out uint bitOffset);
        m_register.m_register[intIndex].GetValue(in bitOffset, out value);
    }

    private static void GetIndexOfBit(in uint index, out uint intIndex, out uint bitOffset)
    {

        intIndex = (uint)(index * 0.03125);
        bitOffset = index - intIndex*32;
    }

    public void IsIndexValide(in uint index, out bool isValide)
    {
       isValide= index < m_register.m_size;
    }

    public void SetValue(in uint index, in bool value)
    {
        GetIndexOfBit(in index, out uint intIndex, out uint bitOffset);
        m_register.m_register[intIndex].SetValue(in bitOffset, in value);
    }
}