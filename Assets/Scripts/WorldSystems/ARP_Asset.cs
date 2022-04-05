using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class ARP_Asset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new ARP();
    }
}
