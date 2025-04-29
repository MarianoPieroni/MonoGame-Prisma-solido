using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace coca_cola
{
    class ClsCoca
    {
        VertexPositionNormalTexture[] vertices_cima;
        VertexPositionNormalTexture[] vertices_lado;
        VertexPositionColorTexture[] vertice_plano;

        BasicEffect effect_lado;
        BasicEffect effect_plano;
        BasicEffect effect_cima;

        public Matrix worldMatrix;
        public Matrix worldMatrix_plano;

        VertexBuffer vertexBuffer_cima;
        VertexBuffer vertexBuffer_lado;
        IndexBuffer indexBuffer;

        int indicesCount;


        public ClsCoca(GraphicsDevice device,Texture2D texture_lado,Texture2D texture_cima, Texture2D texture_plano , int nSides, float r, float h)
        {
            effect_lado = new BasicEffect(device);
            effect_plano = new BasicEffect(device);
            effect_cima = new BasicEffect(device);




            //  Calcula a aspectRatio, a view matrix e a projeção
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;

            effect_lado.View = Matrix.CreateLookAt(new Vector3(10.0f, 7f, 5.0f), Vector3.Zero, Vector3.Up);
            effect_lado.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), aspectRatio, 0.1f, 1000.0f);
            effect_lado.VertexColorEnabled = false;
            effect_lado.TextureEnabled = true;
            effect_lado.Texture = texture_lado;

            effect_lado.LightingEnabled = true;
            effect_lado.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);
            effect_lado.DirectionalLight0.Enabled = true;
            effect_lado.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
            effect_lado.DirectionalLight0.SpecularColor = new Vector3(-0.5f, -0.5f, -0.5f);

            Vector3 d0 = new Vector3(1.0f, -1.0f, -1.0f);
            d0.Normalize();
            effect_lado.DirectionalLight0.Direction = d0;


            effect_plano.View = Matrix.CreateLookAt(new Vector3(10.0f, 7f, 5.0f), Vector3.Zero, Vector3.Up);
            effect_plano.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), aspectRatio, 0.1f, 1000.0f);
            effect_plano.LightingEnabled = false;
            effect_lado.EnableDefaultLighting();
            effect_plano.VertexColorEnabled = false;
            effect_plano.TextureEnabled = true;
            effect_plano.Texture = texture_plano;
            
            
            effect_cima.View = Matrix.CreateLookAt(new Vector3(10.0f, 7f, 5.0f), Vector3.Zero, Vector3.Up);
            effect_cima.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), aspectRatio, 0.1f, 1000.0f);
            effect_cima.LightingEnabled = false;
            effect_cima.EnableDefaultLighting();
            effect_cima.VertexColorEnabled = false;
            effect_cima.TextureEnabled = true;
            effect_cima.Texture = texture_cima;



            worldMatrix = Matrix.Identity;
            worldMatrix_plano = Matrix.Identity;
            CreateGeometry(device ,nSides, r, h);
        }

        private void CreateGeometry(GraphicsDevice device ,int nSides, float r, float h)
        {

            int vertexCount = (nSides) * 6;
            vertices_lado = new VertexPositionNormalTexture[vertexCount];


            for (int i = 0; i <= nSides; i++)
            {
                float angulo = MathHelper.ToRadians(i * (360.0f / nSides));
                float angulo_seguinte = MathHelper.ToRadians((i + 1) * (360.0f / nSides));

                float x = r * (float)System.Math.Cos((double)angulo);
                float z = -r * (float)System.Math.Sin((double)angulo);

                float texX = 1.0f / nSides;

                vertices_lado[2 * i + 0] = new VertexPositionNormalTexture(new Vector3(x, 0, z), Vector3.Zero, new Vector2(texX * i,0f));
                Vector3 n = vertices_lado[2 * i + 0].Position - Vector3.Zero;
                n.Normalize();
                vertices_lado[2 * i + 0].Normal = n;

                vertices_lado[2 * i + 1] = new VertexPositionNormalTexture(new Vector3(x, h, z), Vector3.Zero, new Vector2(texX * i, 1f));
                vertices_lado[2 * i + 1].Normal = n;

            }
            vertexBuffer_lado = new VertexBuffer(device, typeof(VertexPositionNormalTexture), vertexCount, BufferUsage.None); // Cria o vertexBuffer no qual só desenha apenas 1 vez 
            vertexBuffer_lado.SetData<VertexPositionNormalTexture>(vertices_lado);

            short[] indices;
            indicesCount = nSides * 2;
            indices = new short[nSides * 2 + 2];

            for(int j = 1; j <= nSides; j++)
            {
                indices[2 * j + 0] = (short)j;
                indices[2 * j + 1] = 0;
            }
            indexBuffer = new IndexBuffer(device, typeof(short), nSides * 2, BufferUsage.None);
            indexBuffer.SetData<short>(indices);



            vertices_cima = new VertexPositionNormalTexture[vertexCount];

            for (int i = 0; i <= nSides; i++)
            {
                float angulo = MathHelper.ToRadians(i * (360.0f / nSides));

                float x = r * (float)System.Math.Cos((double)angulo);
                float z = -r * (float)System.Math.Sin((double)angulo);

                float u = 0.5f + 0.5f * (float)System.Math.Cos((double)angulo);
                float v = 0.5f - 0.5f * (float)System.Math.Sin((double)angulo);
                float texX = 1.0f / nSides;

                vertices_cima[2 * i + 0] = new VertexPositionNormalTexture(new Vector3(x, h, z), Vector3.UnitY, new Vector2(0.5f + (0.5f * x),0.5f + (0.5f * z)));
                vertices_cima[2 * i + 1] = new VertexPositionNormalTexture(new Vector3(0, h, 0), Vector3.UnitY, new Vector2(0.5f, 0.5f));
            }
            vertexBuffer_cima = new VertexBuffer(device, typeof(VertexPositionNormalTexture), vertexCount, BufferUsage.None); // Cria o vertexBuffer no qual só desenha apenas 1 vez 
            vertexBuffer_cima.SetData<VertexPositionNormalTexture>(vertices_cima);


            vertice_plano = new VertexPositionColorTexture[8];
            float Dplano = 1f;

            vertice_plano[0] = new VertexPositionColorTexture(new Vector3(-Dplano, 0.0f, -Dplano), Color.White, new Vector2(0.0f, 0.0f));
            vertice_plano[1] = new VertexPositionColorTexture(new Vector3(+Dplano, 0.0f, -Dplano), Color.White, new Vector2(1.0f, 0.0f));
            vertice_plano[2] = new VertexPositionColorTexture(new Vector3(-Dplano, 0.0f, +Dplano), Color.White, new Vector2(0f, 1.0f));
            vertice_plano[3] = new VertexPositionColorTexture(new Vector3(+Dplano, 0.0f, +Dplano), Color.White, new Vector2(1f, 1f));

        }

        public void Draw(GraphicsDevice device)
        {
            // World Matrix
            effect_lado.World = worldMatrix;
            effect_cima.World = worldMatrix;
            effect_plano.World = worldMatrix_plano;


            effect_plano.CurrentTechnique.Passes[0].Apply();
            device.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleStrip, vertice_plano, 0,vertice_plano.Length - 2);
            
            effect_cima.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(vertexBuffer_cima);
            device.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleStrip, vertices_cima, 0, vertices_cima.Length - 2);

            effect_lado.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(vertexBuffer_lado);
            device.Indices = indexBuffer;
            device.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleStrip, vertices_lado, 0, vertices_lado.Length -2);

            // falta a textura da tampa

        }
    }
}
