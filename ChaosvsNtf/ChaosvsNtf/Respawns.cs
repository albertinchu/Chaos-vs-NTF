using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using scp4aiur;

namespace ChaosvsNtf
{
    partial class Respawns : IEventHandlerSetSCPConfig, IEventHandlerSetConfig, IEventHandlerSetRole, IEventHandlerPlayerDie, IEventHandlerPlayerDropItem, IEventHandlerMedkitUse
    {
        static Dictionary<string, Role> Rolesss = new Dictionary<string, Role>();
        int contador = 0;
        //respawn//
        public static IEnumerable<float> respawn(Player player)
        {
            
            yield return 3f;
            player.ChangeRole(Rolesss[player.SteamId]);
        }

        public void OnMedkitUse(PlayerMedkitUseEvent ev)
        {
            ev.RecoverHealth *= 2;
        }

        public void OnPlayerDie(PlayerDeathEvent ev)
            {
            Timing.Run(respawn(ev.Player));
            ev.SpawnRagdoll = false;
            
            }

        public void OnPlayerDropItem(PlayerDropItemEvent ev)
        {
            if(ev.Item.ItemType != ItemType.MEDKIT) { ev.ChangeTo = ItemType.NULL; }
           
        }

        public void OnSetConfig(SetConfigEvent ev)
        {
            switch (ev.Key)
            {
                
                
                case "auto_warhead_start_lock":
                    ev.Value = false;
                    break;
                case "minimum_MTF_time_to_spawn":
                    ev.Value = 10000;
                    break;
                case "maximum_MTF_time_to_spawn":
                    ev.Value = 10000;
                    break;
            }
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {//asignar roles///
            if (!Rolesss.ContainsKey(ev.Player.SteamId))
            {
                if(contador == 0)
                {
                    ev.Player.ChangeRole(Role.CHAOS_INSURGENCY);
                    contador = 1;
                    Rolesss.Add(ev.Player.SteamId, ev.Player.TeamRole.Role);

                }
                if (contador == 1)
                {
                    ev.Player.ChangeRole(Role.NTF_LIEUTENANT);
                    contador = 0;
                    Rolesss.Add(ev.Player.SteamId, ev.Player.TeamRole.Role);

                }

            }
            if(ev.Player.TeamRole.Role == Role.CHAOS_INSURGENCY)
            {
                ev.Player.GiveItem(ItemType.E11_STANDARD_RIFLE);
                ev.Player.SetAmmo(AmmoType.DROPPED_5, 1000);
                ev.Player.SetAmmo(AmmoType.DROPPED_7, 1000);
                ev.Player.SetAmmo(AmmoType.DROPPED_9, 1000);
            }
            if (ev.Player.TeamRole.Role == Role.NTF_LIEUTENANT)
            {
                ev.Player.GiveItem(ItemType.LOGICER);
                ev.Player.SetAmmo(AmmoType.DROPPED_5, 1000);
                ev.Player.SetAmmo(AmmoType.DROPPED_7, 1000);
                ev.Player.SetAmmo(AmmoType.DROPPED_9, 1000);
            }
        }

        public void OnSetSCPConfig(SetSCPConfigEvent ev)
        {
            ev.Ban049 = true;
            ev.Ban079 = true;
            ev.Ban096 = true;
            ev.Ban106 = true;
            ev.Ban173 = true;
            ev.Ban939_53 = true;
            ev.Ban939_89 = true;
        }
    }
}
