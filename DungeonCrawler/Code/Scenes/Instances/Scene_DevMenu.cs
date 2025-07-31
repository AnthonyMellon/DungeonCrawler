using DungeonCrawler.Code.UI;
using DungeonCrawler.Code.UI.Utils;
using DungeonCrawler.Code.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawler.Code.Scenes.Instances
{
    internal class Scene_DevMenu : Scene
    {
        public override void Init(ContentManager content, Game game)
        {
            AddChild(BuildUI());
        }

        private UI_Panel BuildUI()
        {
            UI_Panel menuuPanel = new UI_Panel(
                DefaultContent.DefaultRectangle,
                new Color(0, 255, 0, 150),
                GameConstants.GameLayers.Top,
                AnchorPoints.Center,
                new Size(250, 250),
                Offset.Zero,
                DynamicRectangle.FitTypes.Screen,
                DynamicRectangle.GrowFroms.Auto,
                this);

            UI_Panel testPanel = new UI_Panel(
                DefaultContent.DefaultRectangle,
                new Color(255, 0, 0, 150),
                GameConstants.GameLayers.Top,
                AnchorPoints.BottomLeft,
                new Size(50, 50),
                Offset.Zero,
                DynamicRectangle.FitTypes.Parent,
                DynamicRectangle.GrowFroms.Center,
                this);
            menuuPanel.AddChild(testPanel);

            /*            UI_TextInput testTextInput = new UI_TextInput(
                            AnchorPoints.TopLeft,
                            new Size(128, 32),
                            Offset.Zero,
                            DrawManager.DrawTargets.Development,
                            UIComponent.FitTypes.Parent
                            );
                        _developmentPanel.AddChild(testTextInput);*/

            return menuuPanel;
        }
    }
}
