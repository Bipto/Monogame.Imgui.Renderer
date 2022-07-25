using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame.Imgui.Renderer.Sample.DX
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ImGuiRenderer _imGuiRenderer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _imGuiRenderer = new ImGuiRenderer(this);
            _imGuiRenderer.RebuildFontAtlas();

            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _imGuiRenderer.BeforeLayout(this, gameTime);

            ImGuiWindowFlags windowFlags = ImGuiWindowFlags.MenuBar | ImGuiWindowFlags.NoDocking;

            ImGui.SetNextWindowPos(new System.Numerics.Vector2(0.0f, 0.0f), ImGuiCond.Always);
            ImGui.SetNextWindowSize(new System.Numerics.Vector2(
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            windowFlags |= ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus | ImGuiWindowFlags.AlwaysAutoResize;

            var myBool = true;

            if (ImGui.Begin("Dockspace Demo", ref myBool, windowFlags))
            {
                ImGui.PopStyleVar(2);
                ImGui.DockSpace(ImGui.GetID("Dockspace"));
                ImGui.DockSpaceOverViewport(ImGui.GetCurrentContext());

                ImGui.ShowDemoWindow();
                ImGui.End();
            }

            _imGuiRenderer.AfterLayout();

            base.Draw(gameTime);
        }
    }
}