﻿using EFT.UI;
using JsonType;
using SivaEftCheat.Data;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class ItemEsp : MonoBehaviour
    {
        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawItems && Main.Camera != null)
                {
                    Render.DrawString(new Vector2(20, 40), $"Loot Items Amount: {Main.LootItems.Count}", Color.white, false);

                    foreach (var lootItem in Main.LootItems)
                    {
                        if (lootItem.Distance > MiscVisualsOptions.DrawItemRange || !lootItem.IsOnScreen)
                            continue;

                        Color itemColor = default;
                        switch (lootItem.LootItem.Item.Template.Rarity)
                        {
                            case ELootRarity.Common:
                                itemColor = MiscVisualsOptions.CommonColor;
                                break;
                            case ELootRarity.Rare:
                                itemColor = MiscVisualsOptions.RareColor;
                                break;
                            case ELootRarity.Superrare:
                                itemColor = MiscVisualsOptions.SuperRareColor;
                                break;
                        }

                        if (lootItem.LootItem.Item.QuestItem)
                            itemColor = MiscVisualsOptions.QuestItemsColor;

                        if (GameUtils.IsSpecialLootItem(lootItem.LootItem.TemplateId))
                            itemColor = MiscVisualsOptions.SpecialColor;

                        if (GameUtils.IsMedItem(lootItem.LootItem.TemplateId))
                            itemColor = MiscVisualsOptions.MedColor;

                        string text = $"{lootItem.LootItem.Item.Name.Localized()} {lootItem.FormattedDistance}";
                        Render.DrawString(lootItem.ScreenPosition, text, itemColor);
                    }
                }
            }
            catch { }
        }
    }
}