using System;
using Stride.Core;
using Stride.Rendering;
using Stride.Graphics;
using Stride.Shaders;
using Stride.Core.Mathematics;
using Buffer = Stride.Graphics.Buffer;

namespace MyGame
{
    
    internal static partial class ShaderMixins
    {
        internal partial class ComputeGridTextureEffect : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "ComputeGridTexture");
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("ComputeGridTextureEffect", new ComputeGridTextureEffect());
            }
        }
    }
}