using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.EventHandlers;
using Smod2.Events;
using scp4aiur;
using Smod2.API;

namespace ChaosvsNtf
{
    partial class Game : IEventHandlerPlayerDie, IEventHandlerSetRole, IEventHandlerCheckRoundEnd
    {

        static Dictionary<string, int> Puntuacion = new Dictionary<string, int>();
        int chaos = 0;
        int ntf = 0;

        public void OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            ///quien gana//
            if((chaos != 100)||(ntf != 100))
            {
                ev.Status = Smod2.API.ROUND_END_STATUS.ON_GOING;
            }
            if(chaos == 100) {
                ev.Status = Smod2.API.ROUND_END_STATUS.CI_VICTORY; ntf = 0;
                Smod2.PluginManager.Manager.Server.Map.Broadcast(10, "Mejor jugador" + Puntuacion.Keys.Max() + "con <color=#C50000>" + Puntuacion.Values.Max() + "</color> puntos.", false); 
            }
            if (ntf == 100)
            {
                ev.Status = Smod2.API.ROUND_END_STATUS.MTF_VICTORY; chaos = 0;
                Smod2.PluginManager.Manager.Server.Map.Broadcast(10, "Mejor jugador" + Puntuacion.Keys.Max() + "con <color=#C50000>" + Puntuacion.Values.Max() + "</color> puntos.", false);
            }
           
        }

        public void OnPlayerDie(PlayerDeathEvent ev)
        {
            //Obtención de puntos//
            if (ntf == 25) { Smod2.PluginManager.Manager.Server.Map.Broadcast(5, "Los NTFs ya tienen 25 puntos", true); }
            if (chaos == 25) { Smod2.PluginManager.Manager.Server.Map.Broadcast(5, "Los chaos ya tienen 25 puntos", true); }
            if (ntf == 50) { Smod2.PluginManager.Manager.Server.Map.Broadcast(5, "Los chaos ya tienen 50 puntos", true); }
            if (chaos == 50) { Smod2.PluginManager.Manager.Server.Map.Broadcast(5, "Los chaos ya tienen 50 puntos", true); }
            if (ntf == 75) { Smod2.PluginManager.Manager.Server.Map.Broadcast(5, "Los chaos ya tienen 75 puntos, ojo Ganamoh?", true); }
            if (chaos == 75) { Smod2.PluginManager.Manager.Server.Map.Broadcast(5, "Los chaos ya tienen 75 puntos, GG easy?", true); }
            if ((ev.Player.TeamRole.Role == Smod2.API.Role.CHAOS_INSURGENCY)&&(ev.Killer.TeamRole.Role == Smod2.API.Role.NTF_LIEUTENANT))
            {
                ev.Killer.GiveItem(ItemType.MEDKIT);
                ntf += 1;
                Puntuacion[ev.Killer.Name] += 1;
                if(ev.DamageTypeVar == Smod2.API.DamageType.FRAG)
                {
                    ntf += 1;
                    Puntuacion[ev.Killer.Name] += 1;
                }
            }
            if ((ev.Player.TeamRole.Role == Smod2.API.Role.NTF_LIEUTENANT) && (ev.Killer.TeamRole.Role == Smod2.API.Role.CHAOS_INSURGENCY))
            {
                ev.Killer.GiveItem(ItemType.MEDKIT);
                Puntuacion[ev.Killer.Name] += 1;
                chaos += 1;
                if (ev.DamageTypeVar == Smod2.API.DamageType.FRAG)
                {
                    chaos += 1;
                    Puntuacion[ev.Killer.Name] += 1;
                }
            }
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            //Asignar puntos//
            if (!Puntuacion.ContainsKey(ev.Player.SteamId)) { Puntuacion.Add(ev.Player.Name, 0); }
        }
    }
}
