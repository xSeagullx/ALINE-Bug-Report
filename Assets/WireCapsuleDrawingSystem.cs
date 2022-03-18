using Drawing;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class WireCapsuleDrawingSystem : MonoBehaviour
{
    void Update()
    {
        Draw.ingame.WireCapsule(float3.zero, math.up(), 1, .4f, Color.green);
        var draw = DrawingManager.GetBuilder(true);
        var handle = new DrawWireCapsuleJob { draw = draw }.Schedule();
        draw.DisposeAfter(handle);
        handle.Complete();
    }

    [BurstCompile(CompileSynchronously = true)]
    private struct DrawWireCapsuleJob : IJob
    {
        public CommandBuilder draw;

        public void Execute()
        {
            draw.WireCapsule(new float3(1, 0, 0), math.up(), 1, .4f, Color.red);
            // Produces
            // (0,0): Burst error BC1091: External and internal calls are not allowed inside static constructors: UnityEngine.Quaternion.Internal_FromEulerRad_Injected(ref UnityEngine.Vector3 euler, ref UnityEngine.Quaternion ret)

            // While compiling job: System.Void Unity.Jobs.IJobExtensions/JobStruct`1<WireCapsuleDrawingSystem/DrawWireCapsuleJob>::Execute(T&,System.IntPtr,System.IntPtr,Unity.Jobs.LowLevel.Unsafe.JobRanges&,System.Int32)
            // at <empty>:line 0
        }
    }
}
