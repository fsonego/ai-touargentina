using System;
using System.Collections.Generic;
using System.Linq;

namespace aibot_adaptivecards
{
    public class HelperCards
    {
        private aibotContext _db;

        public HelperCards()
        {
            _db = new aibotContext();
        }
        public WelcomeCard GetWelcomeCard() {
            
            var randomizer = new Random();

            OptionCardModel cardResult = (from opt in _db.Options
                                          where opt.Type == "WELCOME"
                                          let rand = randomizer.Next()
                                          orderby rand
                                          select new OptionCardModel()
                                          {
                                              Title = opt.Title,
                                              Description = opt.Body,
                                              OptionId = opt.OptionId,
                                              ParentOptionId = opt.ParentOptionId,
                                              Result = opt.Result,
                                              Options = (from opt2 in _db.Options where opt2.ParentOptionId == opt.OptionId select opt2).ToList()
                                          }).Take(1).SingleOrDefault();
            
            var welcomeCard = new WelcomeCard();
            welcomeCard.BotName = "Botty";
            welcomeCard.Title = cardResult.Title;
            welcomeCard.UrlImage = "https://aibotstoragearg.blob.core.windows.net/img-aitour/bot-avatar_.png";
            welcomeCard.Description = cardResult.Description;

            var listActions = GetListActions(cardResult.Options);
            welcomeCard.Actions.AddRange(listActions);

            return welcomeCard;
        }

        public StandardCard GetStandardCard(Guid OptionId) {
           
            OptionCardModel cardResult = (from opt in _db.Options
                                          where opt.OptionId == OptionId                                                       
                                          select new OptionCardModel()
                                          {
                                              Title = opt.Title,
                                              Description = opt.Body,
                                              OptionId = opt.OptionId,
                                              ParentOptionId = opt.ParentOptionId,
                                              Result = opt.Result,
                                              Options = (from opt2 in _db.Options where opt2.ParentOptionId == opt.OptionId select opt2).ToList()
                                          }).SingleOrDefault();

            var standardCard = new StandardCard();
            standardCard.BotName = "Botty";
            standardCard.Title = cardResult.Title;
            standardCard.UrlImage = "https://aibotstoragearg.blob.core.windows.net/img-aitour/bot-avatar_.png";
            standardCard.Description = cardResult.Description;

            var listActions = GetListActions(cardResult.Options);
            standardCard.Actions.AddRange(listActions);

            return standardCard;
        }

        public GalleryCard GetGalleryCard(Guid OptionId)
        {

            OptionCardModel cardResult = (from opt in _db.Options
                                          where opt.OptionId == OptionId
                                          select new OptionCardModel()
                                          {
                                              Title = opt.Title,
                                              Description = opt.Body,
                                              OptionId = opt.OptionId,
                                              ParentOptionId = opt.ParentOptionId,
                                              Result = opt.Result,
                                              Options = (from opt2 in _db.Options where opt2.ParentOptionId == opt.OptionId select opt2).ToList()
                                          }).SingleOrDefault();

            var galleryCard = new GalleryCard();
            galleryCard.BotName = "Botty";
            galleryCard.Title = cardResult.Title;
            galleryCard.UrlImage = "https://aibotstoragearg.blob.core.windows.net/img-aitour/bot-avatar_.png";
            galleryCard.Description = cardResult.Description;

            var listActions = GetListActions(cardResult.Options);
            galleryCard.Actions.AddRange(listActions);

            return galleryCard;
        }

        public FormMendozaCard GetFormCard(Guid OptionId)
        {

            OptionCardModel cardResult = (from opt in _db.Options
                                          where opt.OptionId == OptionId
                                          select new OptionCardModel()
                                          {
                                              Title = opt.Title,
                                              Description = opt.Body,
                                              OptionId = opt.OptionId,
                                              ParentOptionId = opt.ParentOptionId,
                                              Result = opt.Result,
                                              Options = (from opt2 in _db.Options where opt2.ParentOptionId == opt.OptionId select opt2).ToList()
                                          }).SingleOrDefault();

            var formCard = new FormMendozaCard();
            formCard.BotName = "Botty";
            formCard.Title = cardResult.Title;
            formCard.UrlImage = "https://aibotstoragearg.blob.core.windows.net/img-aitour/bot-avatar_.png";
            formCard.Description = cardResult.Description;

            var listActions = GetListActions(cardResult.Options);
            formCard.Actions.AddRange(listActions);

            return formCard;
        }

        public VideoCard GetVideoCard(Guid OptionId)
        {

            OptionCardModel cardResult = (from opt in _db.Options
                                          where opt.OptionId == OptionId
                                          select new OptionCardModel()
                                          {
                                              Title = opt.Title,
                                              Description = opt.Body,
                                              OptionId = opt.OptionId,
                                              ParentOptionId = opt.ParentOptionId,
                                              Result = opt.Result,
                                              Options = (from opt2 in _db.Options where opt2.ParentOptionId == opt.OptionId select opt2).ToList()
                                          }).SingleOrDefault();

            var videoCard = new VideoCard();
            videoCard.BotName = "Botty";
            videoCard.Title = cardResult.Title;
            videoCard.UrlImage = "https://aibotstoragearg.blob.core.windows.net/img-aitour/bot-avatar_.png";
            videoCard.Description = cardResult.Description;

            var listActions = GetListActions(cardResult.Options);
            videoCard.Actions.AddRange(listActions);

            return videoCard;
        }


        private IList<Action> GetListActions(IList<Options> list)
        {

            var result = from l in list
                         select new Action()
                         {
                             Title = l.Title,
                             ActionId = l.OptionId,
                             TypeCard = (TypeCards)TypeCards.Parse(typeof(TypeCards), l.Type, true),
                             Result = l.Result
                         };

            return result.ToList();

        }
    }

}
