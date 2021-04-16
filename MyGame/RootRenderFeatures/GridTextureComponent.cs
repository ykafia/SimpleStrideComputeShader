using Stride.Core;
using Stride.Engine;
using Stride.Engine.Design;
using Stride.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.RootRenderFeatures
{
    [DataContract("GridTextureCompnent")]
    [Display("GridTextureCompnent", Expand = ExpandRule.Once)]
    [DefaultEntityComponentRenderer(typeof(GridTextureEntityProcessor))]
    [ComponentOrder(100)]
    public class GridTextureComponent : ActivableEntityComponent
    {
        [DataMember(10)]
        [Display("Input")]
        public Texture InputTexture { get; set; } 


        [DataMember(20)]
        [Display("Output")]
        public Texture OutputTexture { get; set; }
    }
}
