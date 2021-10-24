using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTest_BooleanArayToJobAndComputeShader : MonoBehaviour
{
    public BooleanArraySize m_size = BooleanArraySize._64x64;
    public UnityBooleanRegisterDefault m_register;

    public int m_width;
    public int m_total;
    public Texture2D m_fromMainThread;
    public Texture2D m_fromJobSystem;
    public Texture2D m_fromComputeUshortShader;
    public Texture2D m_fromComputeDoubleByteShader;

    public ComputeShader m_computeShade;
    // Start is called before the first frame update
    void Start()
    {
        m_width = GetSizeOf(m_size);
        m_total = (int) m_size;
        m_register = new UnityBooleanRegisterDefault( (uint) (m_width* m_width) );
        m_fromMainThread = new Texture2D(m_width, m_width, TextureFormat.RGBA32, true);
        m_fromJobSystem = new Texture2D(m_width, m_width, TextureFormat.RGBA32, true);
        m_fromComputeUshortShader = new Texture2D(m_width, m_width, TextureFormat.RGBA32, true);
        m_fromComputeDoubleByteShader = new Texture2D(m_width, m_width, TextureFormat.RGBA32, true);



    }

    private int GetSizeOf(BooleanArraySize m_size)
    {
        switch (m_size)
        {
            case BooleanArraySize._4x4: return 4;
            case BooleanArraySize._16x16: return 16;
            case BooleanArraySize._32x32: return 32;
            case BooleanArraySize._64x64: return 64;
            case BooleanArraySize._128x128: return 128;
            case BooleanArraySize._256x256: return 256;
            case BooleanArraySize._512x512: return 512;
            case BooleanArraySize._1024x1024: return 1024;
            default: return 4;
        }
    }

    public void Update()
    {
        int booleanStateId = Shader.PropertyToID("m_booleanState");
        int sizeId = Shader.PropertyToID("m_size");
        int textureId = Shader.PropertyToID("m_result");
        m_computeShade.SetTexture(0, textureId, m_fromComputeDoubleByteShader);
        m_computeShade.SetInt(booleanStateId, (int) m_total);
        m_register.m_computeBufferStructRegister.GetComputeBufferRef( out ComputeBuffer buffer, true);
        m_computeShade.SetBuffer(0, booleanStateId, buffer);
        
        int groups = Mathf.CeilToInt(((int)m_total) / 8f);
        m_computeShade.Dispatch(0, groups, groups, 1);
    }


}
