using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        private IList<Item> Items;
        private GildedRose app;

        [OneTimeSetUp]
        public void Setup()
        {
            Items = new List<Item> {
                new Item { Name = "Normal Item", SellIn = 15, Quality = 15 },
                new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 20, Quality = 80 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 30, Quality = 10 },
            };
            app = new GildedRose(Items);
        }

        [Test]
        public void AllItems()
        {
            int NormalItemSellIn = 15;
            int NormalItemQuality = 15;

            int AgedBrieSellIn = 10;
            int AgedBrieQuality = 20;

            int SulfurasSellIn = 20;
            int SulfurasQuality = 80;

            int BackstagePassesSellIn = 30;
            int BackstagePassesQuality = 10;

            for (int day = 0; day < 31; ++day)
            {
                app.UpdateQuality();

                // Test Normal Item
                NormalItemSellIn--;
                if (NormalItemQuality > 0) NormalItemQuality--;

                Assert.That(Items[0].Name, Is.EqualTo("Normal Item"));
                Assert.That(Items[0].SellIn, Is.EqualTo(NormalItemSellIn));
                Assert.That(Items[0].Quality, Is.EqualTo(NormalItemQuality));


                // Test Aged Brie
                AgedBrieSellIn--;
                if (AgedBrieQuality < 50)
                {
                    AgedBrieQuality++;
                    if (AgedBrieSellIn < 0 && AgedBrieQuality < 50) AgedBrieQuality++;
                }

                Assert.That(Items[1].Name, Is.EqualTo("Aged Brie"));
                Assert.That(Items[1].SellIn, Is.EqualTo(AgedBrieSellIn));
                Assert.That(Items[1].Quality, Is.EqualTo(AgedBrieQuality));

                // Test Sulfuras
                Assert.That(Items[2].Name, Is.EqualTo("Sulfuras, Hand of Ragnaros"));
                Assert.That(Items[2].SellIn, Is.EqualTo(SulfurasSellIn));
                Assert.That(Items[2].Quality, Is.EqualTo(SulfurasQuality));

                // Test Backstage passes
                BackstagePassesSellIn--;
                BackstagePassesQuality++;
                if (BackstagePassesSellIn < 10) BackstagePassesQuality++;
                if (BackstagePassesSellIn < 5) BackstagePassesQuality++;
                if (BackstagePassesQuality > 50) BackstagePassesQuality = 50;
                if (BackstagePassesSellIn < 0) BackstagePassesQuality = 0;

                Assert.That(Items[3].Name, Is.EqualTo("Backstage passes to a TAFKAL80ETC concert"));
                Assert.That(Items[3].SellIn, Is.EqualTo(BackstagePassesSellIn));
                Assert.That(Items[3].Quality, Is.EqualTo(BackstagePassesQuality), $"{BackstagePassesSellIn}");
            }
        }
    }
}
