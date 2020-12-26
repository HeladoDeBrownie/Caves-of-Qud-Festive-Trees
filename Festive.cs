using System;
using XRL;
using XRL.Core;
using XRL.World;
using static XRL.World.Parts.helado_FestiveTrees_Festive;

namespace XRL.World.Parts
{
    [Serializable]
    public class helado_FestiveTrees_Festive : IPart
    {
        public const string GAME_STATE_NAME = "helado_Festive Trees_Festive";

        public string Tile = null;
        public string ColorString = "&G";
        public string DetailColor = "R";

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

            base.Attach();
        }

        public override bool WantEvent(int id, int cascade)
        {
            return
                id == GetDisplayNameEvent.ID ||
                id == GetShortDescriptionEvent.ID ||
                id == GetShortDisplayNameEvent.ID ||
            base.WantEvent(id, cascade);
        }

        public override bool HandleEvent(GetShortDescriptionEvent @event)
        {
            @event.Postfix.Append("\nThis object has been decorated for some festive purpose by an {{M-K distribution|unknown entity}}.\n");
            return base.HandleEvent(@event);
        }

        public override bool HandleEvent(IDisplayNameEvent @event)
        {
            @event.AddAdjective("{{G-Y-R-E sequence|festive}}");
            return base.HandleEvent(@event);
        }

        public override bool Render(RenderEvent @event)
        {
            @event.ColorString = ColorString;
            @event.DetailColor = DetailColor;

            if (ParentObject.Understood() && Tile != null)
            {
                @event.Tile = Tile;
                return true;
            }
            else
            {
                return base.Render(@event);
            }
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
            Game.SetBooleanGameState(GAME_STATE_NAME, DateTime.Now.Month == 12);
        }
    }
}
