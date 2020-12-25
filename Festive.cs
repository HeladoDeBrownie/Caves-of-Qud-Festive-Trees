using XRL;
using XRL.Core;
using XRL.World;
using static XRL.World.Parts.helado_FestiveTrees_Festive;

namespace XRL.World.Parts
{
    [HasGameBasedStaticCache]
    public class helado_FestiveTrees_Festive : IPart
    {
        public const string GAME_STATE_NAME = "helado_Festive Trees_Festive";

        public string Tile = null;

        [GameBasedCacheInit]
        public static void GameBasedCacheInit()
        {
            var Game = XRLCore.Core?.Game;

            if (Game != null && !Game.HasBooleanGameState(GAME_STATE_NAME))
            {
                Game.SetBooleanGameState(GAME_STATE_NAME, true);
            }
        }

        public override void Attach()
        {
            var festive = false;
            XRLCore.Core?.Game?.TryGetBooleanGameState(GAME_STATE_NAME, out festive);

            if (festive)
            {
                base.Attach();
            }
            else
            {
                ParentObject.RemovePart(this);
            }
        }
        public override bool Render(RenderEvent @event)
        {
            if (Tile != null)
            {
                @event.Tile = Tile;
            }

            return base.Render(@event);
        }
    }
}

[PlayerMutator]
public class helado_FestiveTrees_FestiveInitializer : IPlayerMutator
{
    public void mutate(GameObject _)
    {
        var Game = XRLCore.Core?.Game;

        if (Game != null && !Game.HasBooleanGameState(GAME_STATE_NAME))
        {
            Game.SetBooleanGameState(GAME_STATE_NAME, true);
        }
    }
}
