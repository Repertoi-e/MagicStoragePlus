using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MagicStoragePlus.Components
{
    public class CraftingAccess : StorageAccess
    {
        public override ModTileEntity GetTileEntity() => mod.GetTileEntity("TECraftingAccess");
        public override int ItemType(int frameX, int frameY) => mod.ItemType("CraftingAccess");
        public override bool HasSmartInteract() => true;

        public override TEStorageHeart GetHeart(int i, int j)
        {
            Point16 point = TEStorageComponent.FindStorageCenter(new Point16(i, j));
            if (point.X < 0 || point.Y < 0 || !TileEntity.ByPosition.ContainsKey(point))
                return null;

            TileEntity heart = TileEntity.ByPosition[point];
            if (!(heart is TEStorageCenter))
                return null;
            return ((TEStorageCenter)heart).GetHeart();
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (Main.tile[i, j].frameX > 0)
                i--;
            if (Main.tile[i, j].frameY > 0)
                j--;
            
            Point16 pos = new Point16(i, j);
            if (!TileEntity.ByPosition.ContainsKey(pos))
                return;
            
            TECraftingAccess access = TileEntity.ByPosition[new Point16(i, j)] as TECraftingAccess;
            if (access != null)
            {
                foreach (Item item in access.stations)
                {
                    if (!item.IsAir)
                    {
                        fail = true;
                        break;
                    }
                }
            }
        }
    }
}