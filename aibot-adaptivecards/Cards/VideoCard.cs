using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCards;

namespace aibot_adaptivecards
{
    public class VideoCard : CardBase
    {
        public string UrlImage { get; set; }
        public string BotName { get; set; }

        public VideoCard() { }

        public AdaptiveCard GetCard()
        {
            var _cardResult = SetCard();
            return _cardResult;
        }

        private AdaptiveCard SetCard()
        {

            AdaptiveCard _card = new AdaptiveCard("1.1");

            var _container = new AdaptiveContainer();

            var colum = new AdaptiveColumnSet();

            var _columnImage = new AdaptiveColumn()
            {
                Width = AdaptiveColumnWidth.Auto
            };

            _columnImage.Items.Add(new AdaptiveImage()
            {
                Url = new Uri(this.UrlImage),
                Size = AdaptiveImageSize.Small,
                Style = AdaptiveImageStyle.Person,
                AltText = "Bootty"
            });

            var _columnContent = new AdaptiveColumn()
            {
                Width = AdaptiveColumnWidth.Stretch
            };

            _columnContent.Items.Add(new AdaptiveTextBlock()
            {
                Text = "Booty",
                Size = AdaptiveTextSize.Medium,
                Weight = AdaptiveTextWeight.Default,
                Color = AdaptiveTextColor.Default,
                Wrap = true,
                Spacing = AdaptiveSpacing.Default
            });

            _columnContent.Items.Add(new AdaptiveTextBlock()
            {
                Text = DateTime.Now.ToString(),
                Size = AdaptiveTextSize.Small,
                Color = AdaptiveTextColor.Default,
                Wrap = true,
                IsSubtle = true,
                Spacing = AdaptiveSpacing.None
            });

            var _textMessage = new AdaptiveTextBlock()
            {
                Text = this.Title,
                Size = AdaptiveTextSize.Medium,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Bolder,
                Wrap = true,
                IsSubtle = false
            };

            var _textMessage2 = new AdaptiveTextBlock()
            {
                Text =this.Description,
                Size = AdaptiveTextSize.Small,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Default,
                Wrap = true,
                IsSubtle = false
            };
            

            colum.Columns.Add(_columnImage);
            colum.Columns.Add(_columnContent);
            _container.Items.Add(colum);

            _card.Body.Add(_container);
            _card.Body.Add(_textMessage);
            _card.Body.Add(_textMessage2);

            var video = this.Actions.Where(p => p.TypeCard == TypeCards.VIDEO).SingleOrDefault();

            if (video != null) {
                var _video = new AdaptiveMedia();
                _video.Sources.Add(new AdaptiveMediaSource()
                {
                     MimeType = "video/mp4",
                     Url = video.Result
                });
                                
                _card.Body.Add(_video);
            }
            

            _card.Actions.AddRange(this.GetAdaptiveActions());

            return _card;
        }
    }
}
