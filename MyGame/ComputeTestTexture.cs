using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Graphics;
using MyGame.RootRenderFeatures;

namespace MyGame
{
    public class ComputeTestTexture : SyncScript
    {

        private GridTextureComponent cmp;

        
        public override void Start()
        {
            cmp = Entity.Get<GridTextureComponent>();
            cmp.InputTexture = Texture.New2D(GraphicsDevice, 256, 256, 1, PixelFormat.R32G32B32A32_Float, TextureFlags.UnorderedAccess | TextureFlags.ShaderResource);
            cmp.OutputTexture = Texture.New2D(GraphicsDevice, 256,256, 1, PixelFormat.R32G32B32A32_Float, TextureFlags.UnorderedAccess | TextureFlags.ShaderResource);
        }

        public override void Update()
        {
            // Do stuff every new frame
            var bytes = cmp.OutputTexture.GetData<Vector4>(Game.GraphicsContext.CommandList);
        }
    }
}
