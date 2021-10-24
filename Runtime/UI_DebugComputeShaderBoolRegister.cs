using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DebugComputeShaderBoolRegister : MonoBehaviour
{

    public ComputeShader m_pixelGridBoolRegister;
    public Renderer m_debugRenderer;
    public RawImage m_debugUIRenderer;
    
    [Header("Debug UI")]
    public RenderTexture m_renderer;
    ComputeBuffer m_computeBuffer;
    [Header("Debug Data")]
    public uint m_size;
    public int m_width;
    public int[] m_32BoolAsInt;



    public void PushIn(in ComputeShaderBooleanRegister register)
    {
        register.GetMaxSize(out uint size);
        if (size != m_size) {
            Dispose();
            m_size = size;
            m_width = (int)Math.Sqrt(m_size);
            m_32BoolAsInt = new int[register.m_register.m_size];

            m_computeBuffer = new ComputeBuffer(m_32BoolAsInt.Length, sizeof(int), ComputeBufferType.Default);
            m_renderer = new RenderTexture(m_width, m_width, 24, RenderTextureFormat.ARGB32);
            m_renderer.enableRandomWrite = true;
            m_renderer.filterMode = FilterMode.Point;
            m_renderer.Create();
            if (m_debugRenderer != null)
                m_debugRenderer.sharedMaterial.mainTexture = m_renderer;
            if (m_debugUIRenderer != null)
                m_debugUIRenderer.texture = m_renderer;
        }

        byte b1=0, b2=0, b3=0, b4=0;
        for (int i = 0; i < register.m_register.m_register.Length; i++)
        {
            BoolToIntUtility.C_32BoolsToInt(register.m_register.m_register[i].GetRefOfBools(),
                ref m_32BoolAsInt[i], ref b1, ref b2, ref b3, ref b4);

        }
        ComputeTexture(in m_32BoolAsInt);
    }
    private void ComputeTexture(in int [] m_32BoolsAsInt)
    {
        int computeKernel = 0;// m_computeShader.FindKernel("CSMain");
        m_computeBuffer.SetData(m_32BoolsAsInt);
        m_pixelGridBoolRegister.SetInt("m_width", m_width);
        m_pixelGridBoolRegister.SetBuffer(computeKernel, "m_32ByteInInt", m_computeBuffer);
        m_pixelGridBoolRegister.SetTexture(computeKernel, "m_result", m_renderer);

        m_pixelGridBoolRegister.Dispatch(
            computeKernel,
            m_width / 8,
            m_width / 8, 
            1);
    }

    private void Dispose()
    {
        if (m_computeBuffer != null) { 
            m_computeBuffer.Release();
            m_computeBuffer.Dispose();
        }
    }

    private void OnDestroy()
    {
        Dispose();
    }

}
