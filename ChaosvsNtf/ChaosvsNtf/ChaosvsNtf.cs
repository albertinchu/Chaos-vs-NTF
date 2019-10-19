using Smod2;
using Smod2.Attributes;
using scp4aiur;
using System;

namespace ChaosvsNtf
{
    [PluginDetails(
        author = "Albertinchu",
        name = "ChaosvsNTF",
        description = "NTF vs Chaos",
        id = "albertinchu.gamemode.ChaosvsNTF",
        version = "3.5.0",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 0
        )]
    public class ChaosvsNtF : Plugin
    {

        public override void OnDisable()
        {
            this.Info("ChaosvsNtf - Desactivado");
        }

        public override void OnEnable()
        {
            this.Info("ChaosvsNtf - Activado");
        }

        public override void Register()
        {
            this.AddEventHandlers(new Game(this));
            Timing.Init(this);
            this.AddEventHandlers(new Respawns(this));

            GamemodeManager.Manager.RegisterMode(this);

        }

  

        public void RefreshConfig()
        {


        }
    }

}
