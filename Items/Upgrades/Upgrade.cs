using StarSailor.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarSailor.Items.Upgrades
{
    abstract class Upgrade : ModItem
    {
        public abstract Item InputItem { get; }
        public abstract Item OutputItem { get; }
        public abstract string UpgradeTooltip { get; }
        public override void SetStaticDefaults()
        {
            string ttip = UpgradeTooltip + "\nRight click to use.";
            Tooltip.SetDefault(ttip);
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 1;
            item.value = 100;
        }
        public override bool CanRightClick()
        {
            return IndexOfItem(InputItem, Main.LocalPlayer) != -1;
        }
        public override void RightClick(Player player)
        {
            int ind = IndexOfItem(InputItem, player);
            if (ind != -1) 
            {
                player.inventory[ind].type = ItemID.None;
                player.inventory[ind].SetDefaults();
                Item.NewItem(player.position, OutputItem.type);

                //player.inventory[ind].SetDefaults();
                //int j = IndexOfItem(item, player);
                //player.inventory[j] = null;
            }
            base.RightClick(player);
        }
        public int IndexOfItem(Item i, Player p)
        {
            Item[] list = p.inventory;
            for (int q = 0; q < list.Length; q++)
            {
                if (i.Name == list[q].Name) return q;
            }
            return -1;
        }
    }
    #region Pistol
    class PistolV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PistolV1>().item;

        public override Item OutputItem => ModContent.GetInstance<PistolV2>().item;

        public override string UpgradeTooltip => "Increases shooting speed.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class PistolV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PistolV2>().item;

        public override Item OutputItem => ModContent.GetInstance<PistolV3>().item;

        public override string UpgradeTooltip => "Increases critical strike chance.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class PistolVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PistolV3>().item;

        public override Item OutputItem => ModContent.GetInstance<PistolVMax>().item;

        public override string UpgradeTooltip => "Bullets cause bleeding damage over time.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Assault Rifle
    class AssaultRifleV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<AssaultRifleV1>().item;

        public override Item OutputItem => ModContent.GetInstance<AssaultRifleV2>().item;

        public override string UpgradeTooltip => "Increases shooting speed.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class AssaultRifleV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<AssaultRifleV2>().item;

        public override Item OutputItem => ModContent.GetInstance<AssaultRifleV3>().item;

        public override string UpgradeTooltip => "Increases damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class AssaultRifleVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<AssaultRifleV3>().item;

        public override Item OutputItem => ModContent.GetInstance<AssaultRifleVMax>().item;

        public override string UpgradeTooltip => "Bullets pierce 3 enemies.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Shotgun
    class ShotgunV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ShotgunV1>().item;

        public override Item OutputItem => ModContent.GetInstance<ShotgunV2>().item;

        public override string UpgradeTooltip => "Increases damage and decreases bullet spread.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class ShotgunV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ShotgunV2>().item;

        public override Item OutputItem => ModContent.GetInstance<ShotgunV3>().item;

        public override string UpgradeTooltip => "Increases damage and critical strike chance.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class ShotgunVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ShotgunV3>().item;

        public override Item OutputItem => ModContent.GetInstance<ShotgunVMax>().item;

        public override string UpgradeTooltip => "Hitting enemies causes subsequent bullets to deal increased damage";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Rocket Launcher
    class RocketLauncherV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<RocketLauncherV1>().item;

        public override Item OutputItem => ModContent.GetInstance<RocketLauncherV2>().item;

        public override string UpgradeTooltip => "Increases damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class RocketLauncherV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<RocketLauncherV2>().item;

        public override Item OutputItem => ModContent.GetInstance<RocketLauncherV3>().item;

        public override string UpgradeTooltip => "Rockets cause fire damage over time.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class RocketLauncherVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<RocketLauncherV3>().item;

        public override Item OutputItem => ModContent.GetInstance<RocketLauncherVMax>().item;

        public override string UpgradeTooltip => "Gives rockets homing capabilities.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Kraken Gun
    class KrakenGunV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<KrakenGunV1>().item;

        public override Item OutputItem => ModContent.GetInstance<KrakenGunV2>().item;

        public override string UpgradeTooltip => "Increases number of active tentacles.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class KrakenGunV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<KrakenGunV2>().item;

        public override Item OutputItem => ModContent.GetInstance<KrakenGunV3>().item;

        public override string UpgradeTooltip => "Increases number of active tentacles.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class KrakenGunVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<KrakenGunV3>().item;

        public override Item OutputItem => ModContent.GetInstance<KrakenGunVMax>().item;

        public override string UpgradeTooltip => "Increases number of active tentacles and damage per tentacle.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Lightning Bolt
    class LightningBoltV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<LightningBoltV1>().item;

        public override Item OutputItem => ModContent.GetInstance<LightningBoltV2>().item;

        public override string UpgradeTooltip => "Slows enemies on hit.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class LightningBoltV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<LightningBoltV2>().item;

        public override Item OutputItem => ModContent.GetInstance<LightningBoltV3>().item;

        public override string UpgradeTooltip => "Increases damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class LightningBoltVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<LightningBoltV3>().item;

        public override Item OutputItem => ModContent.GetInstance<LightningBoltVMax>().item;

        public override string UpgradeTooltip => "Lightning has a chance to arc to nearby enemies on hit.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Meteor Bomb
    class MeteorBombV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<MeteorBombV1>().item;

        public override Item OutputItem => ModContent.GetInstance<MeteorBombV2>().item;

        public override string UpgradeTooltip => "Increases firing distance.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class MeteorBombV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<MeteorBombV2>().item;

        public override Item OutputItem => ModContent.GetInstance<MeteorBombV3>().item;

        public override string UpgradeTooltip => "Increases the size of the explosion.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class MeteorBombVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<MeteorBombV3>().item;

        public override Item OutputItem => ModContent.GetInstance<MeteorBombVMax>().item;

        public override string UpgradeTooltip => "Increases the damage of explosions. Causes fire damage over time and slows on hit.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Laser Rifle
    class LaserRifleV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<LaserRifleV1>().item;

        public override Item OutputItem => ModContent.GetInstance<LaserRifleV2>().item;

        public override string UpgradeTooltip => "Increases damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class LaserRifleV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<LaserRifleV2>().item;

        public override Item OutputItem => ModContent.GetInstance<LaserRifleV3>().item;

        public override string UpgradeTooltip => "Increases damage and critical strike chance.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class LaserRifleVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<LaserRifleV3>().item;

        public override Item OutputItem => ModContent.GetInstance<LaserRifleVMax>().item;

        public override string UpgradeTooltip => "Deals massive damage to slow-moving targets.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Cryogun
    class CryogunV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<CryogunV1>().item;

        public override Item OutputItem => ModContent.GetInstance<CryogunV2>().item;

        public override string UpgradeTooltip => "Increases firing distance";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class CryogunV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<CryogunV2>().item;

        public override Item OutputItem => ModContent.GetInstance<CryogunV3>().item;

        public override string UpgradeTooltip => "Increases slow effect.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class CryogunVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<CryogunV3>().item;

        public override Item OutputItem => ModContent.GetInstance<CryogunVMax>().item;

        public override string UpgradeTooltip => "Increases firing distance. Freezes enemies in place.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Orb Of Vitality
    class OrbOfVitalityV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<OrbOfVitalityV1>().item;

        public override Item OutputItem => ModContent.GetInstance<OrbOfVitalityV2>().item;

        public override string UpgradeTooltip => "Increases duration.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class OrbOfVitalityV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<OrbOfVitalityV2>().item;

        public override Item OutputItem => ModContent.GetInstance<OrbOfVitalityV3>().item;

        public override string UpgradeTooltip => "Slows on hit.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class OrbOfVitalityVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<OrbOfVitalityV3>().item;

        public override Item OutputItem => ModContent.GetInstance<OrbOfVitalityVMax>().item;

        public override string UpgradeTooltip => "Leeches electricity on hit.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Hand Blade
    class HandBladeV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<HandBladeV1>().item;

        public override Item OutputItem => ModContent.GetInstance<HandBladeV2>().item;

        public override string UpgradeTooltip => "Increases attack speed.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class HandBladeV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<HandBladeV2>().item;

        public override Item OutputItem => ModContent.GetInstance<HandBladeV3>().item;

        public override string UpgradeTooltip => "Increases damage and causes poison damage over time.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class HandBladeVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<HandBladeV3>().item;

        public override Item OutputItem => ModContent.GetInstance<HandBladeVMax>().item;

        public override string UpgradeTooltip => "Deals massive damage to low health targets.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Chain Whip
    class ChainWhipV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ChainWhipV1>().item;

        public override Item OutputItem => ModContent.GetInstance<ChainWhipV2>().item;

        public override string UpgradeTooltip => "Increases damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class ChainWhipV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ChainWhipV2>().item;

        public override Item OutputItem => ModContent.GetInstance<ChainWhipV3>().item;

        public override string UpgradeTooltip => "Increases attacking distance.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class ChainWhipVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ChainWhipV3>().item;

        public override Item OutputItem => ModContent.GetInstance<ChainWhipVMax>().item;

        public override string UpgradeTooltip => "Will stick to enemies, dealing damage over time. Press <right> to pull the enemy towards the player.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Power Glove
    class PowerGloveV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PowerGloveV1>().item;

        public override Item OutputItem => ModContent.GetInstance<PowerGloveV2>().item;

        public override string UpgradeTooltip => "Increases damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class PowerGloveV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PowerGloveV2>().item;

        public override Item OutputItem => ModContent.GetInstance<PowerGloveV3>().item;

        public override string UpgradeTooltip => "Increases damage and critical strike chance.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class PowerGloveVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PowerGloveV3>().item;

        public override Item OutputItem => ModContent.GetInstance<PowerGloveVMax>().item;

        public override string UpgradeTooltip => "Critical strikes give the player increased movement speed and damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Rod Of Distortion
    class RodOfDistortionV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<RodOfDistortionV1>().item;

        public override Item OutputItem => ModContent.GetInstance<RodOfDistortionV2>().item;

        public override string UpgradeTooltip => "Increases knockback and damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class RodOfDistortionV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<RodOfDistortionV2>().item;

        public override Item OutputItem => ModContent.GetInstance<RodOfDistortionV3>().item;

        public override string UpgradeTooltip => "Slows enemies on hit.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class RodOfDistortionVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<RodOfDistortionV3>().item;

        public override Item OutputItem => ModContent.GetInstance<RodOfDistortionVMax>().item;

        public override string UpgradeTooltip => "Press <right> to deal extra damage and stun.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Shield Charger
    class ShieldChargerV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ShieldChargerV1>().item;

        public override Item OutputItem => ModContent.GetInstance<ShieldChargerV2>().item;

        public override string UpgradeTooltip => "Increases player armor on use.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class ShieldChargerV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ShieldChargerV2>().item;

        public override Item OutputItem => ModContent.GetInstance<ShieldChargerV3>().item;

        public override string UpgradeTooltip => "Decreases electricity cost.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class ShieldChargerVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<ShieldChargerV3>().item;

        public override Item OutputItem => ModContent.GetInstance<ShieldChargerVMax>().item;

        public override string UpgradeTooltip => "Reflects enemy projectiles.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Juice Jacker
    class JuiceJackerV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<JuiceJackerV1>().item;

        public override Item OutputItem => ModContent.GetInstance<JuiceJackerV2>().item;

        public override string UpgradeTooltip => "Increases use speed.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class JuiceJackerV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<JuiceJackerV2>().item;

        public override Item OutputItem => ModContent.GetInstance<JuiceJackerV3>().item;

        public override string UpgradeTooltip => "Increases electricity drain.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class JuiceJackerVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<JuiceJackerV3>().item;

        public override Item OutputItem => ModContent.GetInstance<JuiceJackerVMax>().item;

        public override string UpgradeTooltip => "Press <right> to fire a projectile that drains electricity.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
}
