// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using Stride.Core.Annotations;
using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.ComputeEffect;
using Stride.Rendering.Skyboxes;
using Stride.Streaming;

namespace MyGame
{
    /// <summary>
    /// This class will show up in the graphics compositor in the render feature list.
    /// </summary>
    /// <seealso cref="Stride.Rendering.RootRenderFeature" />
    public class GridTextureRenderFeature : RootRenderFeature
    {
        private MutablePipelineState pipelineState;
        private ComputeEffectShader myCustomShader;

        public override Type SupportedRenderObjectType => typeof(GridTextureObject);

        public GridTextureRenderFeature()
        {
            //pre adjust render priority, low numer is early, high number is late
            SortKey = 0;
        }

        protected override void InitializeCore()
        {
            // Initalize the shader
            myCustomShader = new ComputeEffectShader(Context) 
            { 
                ShaderSourceName = "ComputeGridTextureEffect"
            };
            myCustomShader.Initialize(Context);

            // Create the pipeline state and set properties that won't change
            pipelineState = new MutablePipelineState(Context.GraphicsDevice);
            pipelineState.State.SetDefaults();
            
        }

        public override void Prepare(RenderDrawContext context)
        {
            base.Prepare(context);

            // Register resources usage
            foreach (var renderObject in RenderObjects)
            {
                var GridTextureObject = (GridTextureObject)renderObject;
                //Context.StreamingManager?.StreamResources(GridTextureObject.inputTexture, StreamingOptions.LoadAtOnce);
                //Context.StreamingManager?.StreamResources(GridTextureObject.outputTexture, StreamingOptions.LoadAtOnce);
                // GridTextureObject.Prepare(context.GraphicsDevice);
            }
        }

        public override void Draw(RenderDrawContext context, RenderView renderView, RenderViewStage renderViewStage, int startIndex, int endIndex)
        {
            // First do everything that doesn't change per individual render object
            var graphicsDevice = context.GraphicsDevice;
            var graphicsContext = context.GraphicsContext;
            var commandList = context.GraphicsContext.CommandList;
            
            // Refresh shader, might have changed during runtime
            //myCustomShader.UpdateEffect(graphicsDevice);

            // Set common shader parameters if needed
            //myCustomShader.Parameters.Set(TransformationKeys.ViewProjection, renderView.ViewProjection);

            for (int index = startIndex; index < endIndex; index++)
            {
                var renderNodeReference = renderViewStage.SortedRenderNodes[index].RenderNode;
                var renderNode = GetRenderNode(renderNodeReference);
                var gtex = (GridTextureObject)renderNode.RenderObject;
                myCustomShader.ThreadGroupCounts = new Int3(256,256,1);
                // if (GridTextureObject.VertexBuffer == null)
                //     continue; //next render object

                // Assign shader parameters
                // myCustomShader.Parameters.Set(TransformationKeys.WorldViewProjection, GridTextureObject.WorldMatrix * renderView.ViewProjection);
                // myCustomShader.Parameters.Set(TexturingKeys.Texture0, GridTextureObject.Texture);
                myCustomShader.Parameters.Set(ComputeGridTextureKeys.input, gtex.inputTexture);
                myCustomShader.Parameters.Set(ComputeGridTextureKeys.output, gtex.outputTexture);

                pipelineState.State.Output.CaptureState(commandList);
                pipelineState.Update();
                commandList.SetPipelineState(pipelineState.CurrentState);

                // Apply the effect
                myCustomShader.Draw(context);
            }
        }
    }
}
