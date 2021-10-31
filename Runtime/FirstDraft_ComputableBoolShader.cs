using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDraft_ComputableBoolShader : MonoBehaviour
{

    public FirstDraft_BoolHistory m_source;


    public ComputeShader m_computeShader;
    public RenderTexture m_result;
    public MeshRenderer m_renderer;

    public uint valueBufferCurrentCount;
    ComputeBuffer valueBuffer;

    public uint offsetBufferCurrentCount;
    ComputeBuffer offsetBuffer;
    private void Update()
    {
        ComputeTexture(m_source.m_array.m_indexes.m_indexes,
            m_source.m_array.m_values.m_values
            , m_source.m_column
            , m_source.m_line
            ); 
    }


    private void ComputeTexture(in uint[] indexes, in int[] values, in uint columns, in uint lines)
    {
        if (values.Length <= 0 || indexes.Length < 0)
            return;

        bool sizeChanged=false;
        if (valueBuffer == null || (valueBuffer!=null && valueBufferCurrentCount!=values.Length) )
        {
            if (valueBuffer != null) {

                valueBuffer.Release();
            }
            valueBuffer = new ComputeBuffer(values.Length,
                sizeof(int), ComputeBufferType.Default);
            valueBufferCurrentCount = (uint) values.Length;
            sizeChanged = true;

        }
        if (offsetBuffer == null || (offsetBuffer != null && offsetBufferCurrentCount != indexes.Length))
        {
            if (offsetBuffer != null) {

                offsetBuffer.Release();
            }
            offsetBuffer = new ComputeBuffer(indexes.Length,
                sizeof(uint), ComputeBufferType.Default);
            offsetBufferCurrentCount = (uint)indexes.Length;
            sizeChanged = true;
        }
    

        if (sizeChanged) { 
            m_result = new RenderTexture((int)columns, (int)lines, 24, RenderTextureFormat.ARGB32);
            m_result.enableRandomWrite = true;
            m_result.filterMode = FilterMode.Point;
            m_result.Create();

        }
        int computeKernel = 0;
        valueBuffer.SetData( values );
        offsetBuffer.SetData( indexes );

        m_result.enableRandomWrite = true;
        m_result.Create();

        if (m_renderer != null)
            m_renderer.sharedMaterial.mainTexture = m_result;

        m_computeShader.SetInt("m_lineCount", (int)lines);
        m_computeShader.SetInt("m_columnCount", (int)columns);
        m_computeShader.SetInt("m_totalCellsCount", (int)(columns * lines));
        m_computeShader.SetBuffer(computeKernel, "m_32ByteInInt", valueBuffer );
        m_computeShader.SetBuffer(computeKernel, "m_columnOffset", offsetBuffer );
        m_computeShader.SetTexture(computeKernel, "m_result", m_result );

        //        int groups = Mathf.CeilToInt( ((int)64) / 8f);
        m_computeShader.Dispatch(computeKernel, m_result.width / 8, m_result.height / 8, 1);
    }
}
