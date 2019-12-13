using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCards;

namespace aibot_adaptivecards
{
    public class WelcomeCard : CardBase
    {
    public string UrlImage { get; set; }
    public string BotName { get; set; }

    public WelcomeCard() { }

    public AdaptiveCard GetCard()
    {

        var _cardResult = SetCard();
        return _cardResult;
    }
    private AdaptiveCard SetCard()
    {

        AdaptiveCard _card = new AdaptiveCard("1.0");

        var _container = new AdaptiveContainer();

        _container.Items.Add(new AdaptiveImage()
        {
            Url = new Uri(UrlImage),
            Size = AdaptiveImageSize.Stretch,
            Style = AdaptiveImageStyle.Person,
            PixelHeight = 200,
            PixelWidth = 200,
            
            HorizontalAlignment = AdaptiveHorizontalAlignment.Center,
            AltText = "Botty"
        });


        _container.Items.Add(new AdaptiveTextBlock()
        {
            Text = this.BotName,
            Size = AdaptiveTextSize.Large,
            Weight = AdaptiveTextWeight.Bolder,
            Color = AdaptiveTextColor.Default,
            Wrap = true,
            Spacing = AdaptiveSpacing.Default,
            HorizontalAlignment = AdaptiveHorizontalAlignment.Center
        });


        _container.Items.Add(new AdaptiveTextBlock()
        {
            Text = this.Title,
            Size = AdaptiveTextSize.ExtraLarge,
            Weight = AdaptiveTextWeight.Bolder,
            Color = AdaptiveTextColor.Default,
            Wrap = true,
            Spacing = AdaptiveSpacing.Default
        });

        _container.Items.Add(new AdaptiveTextBlock()
        {
            Text = DateTime.Now.ToString(),
            Size = AdaptiveTextSize.Small,
            Color = AdaptiveTextColor.Default,
            Wrap = true,
            Spacing = AdaptiveSpacing.Default,
        });

        _container.Items.Add(new AdaptiveTextBlock()
        {
            Text = this.Description,
            Size = AdaptiveTextSize.Normal,
            Weight = AdaptiveTextWeight.Normal,
            Color = AdaptiveTextColor.Default,
            Wrap = true,
            Spacing = AdaptiveSpacing.Default
        });

       //GenerateActionFromOptions(this.Order);

        _card.Body.Add(_container);
        _card.Actions.AddRange(this.GetAdaptiveActions());

        return _card;
    }
}
}

