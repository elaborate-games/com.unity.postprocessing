using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering.PostProcessing
{
    internal class AOComposite : IAmbientOcclusionMethod
    {
        private readonly IEnumerable<IAmbientOcclusionMethod> methods;

        public AOComposite(params IAmbientOcclusionMethod[] methods)
        {
            this.methods = methods.Where(m => m != this);
        }
        
        public DepthTextureMode GetCameraFlags()
        {
            var mode = DepthTextureMode.None;
            foreach (var method in methods)
                mode |= method.GetCameraFlags();
            return mode;
        }

        public void RenderAfterOpaque(PostProcessRenderContext context)
        {
            foreach (var method in methods)
                method.RenderAfterOpaque(context);
        }

        public void RenderAmbientOnly(PostProcessRenderContext context)
        {
            foreach (var method in methods)
                method.RenderAmbientOnly(context);
        }

        public void CompositeAmbientOnly(PostProcessRenderContext context)
        {
            foreach (var method in methods)
                method.CompositeAmbientOnly(context);
        }

        public void Release()
        {
            foreach(var method in methods) 
                method.Release();
        }
    }
}