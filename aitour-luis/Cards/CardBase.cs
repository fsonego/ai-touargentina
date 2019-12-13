using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCards;
using Newtonsoft.Json;

namespace aitour_luis
{
    public class CardBase
    {

        public CardBase()
        {
            this.Actions = new List<Action>();
        }

        public string Title { get; set; }
        public string Description { get; set;  }
        public DateTime Date { get; set; }
        public List<Action> Actions {get; set;}
        public IList<AdaptiveAction> GetAdaptiveActions() {

            IList<AdaptiveAction> actionsItems = new List<AdaptiveAction>();

            foreach (Action actions in Actions)
            {
                switch (actions.TypeCard)
                {
                    case TypeCards.LINK:
                        actionsItems.Add(new AdaptiveOpenUrlAction()
                        {
                            Title = actions.Title,
                            Id = actions.ActionId.ToString(),
                            Url = new Uri(actions.Result)
                        });
                        break;

                    case TypeCards.GALLERYIMAGES:

                        actionsItems.Add(new AdaptiveSubmitAction()
                        {
                            Title = actions.Title,
                            Id = actions.ActionId.ToString(),
                            DataJson = JsonConvert.SerializeObject(new JsonDataAux()
                            {
                                From = TypeCards.GALLERYIMAGES.ToString(),
                                Selected = actions.ActionId.ToString(),
                                Action = TypeCards.GALLERYIMAGES.ToString()
                            })

                        });
                        break;

                    case TypeCards.VIDEOCARD:

                        actionsItems.Add(new AdaptiveSubmitAction()
                        {
                            Title = actions.Title,
                            Id = actions.ActionId.ToString(),
                            DataJson = JsonConvert.SerializeObject(new JsonDataAux()
                            {
                                From = TypeCards.VIDEOCARD.ToString(),
                                Selected = actions.ActionId.ToString(),
                                Action = TypeCards.VIDEOCARD.ToString()
                            })

                        });
                        break;
                    case TypeCards.FORM:

                        actionsItems.Add(new AdaptiveSubmitAction()
                        {
                            Title = actions.Title,
                            Id = actions.ActionId.ToString(),
                            DataJson = JsonConvert.SerializeObject(new JsonDataAux()
                            {
                                From = TypeCards.FORM.ToString(),
                                Selected = actions.ActionId.ToString(),
                                Action = TypeCards.FORM.ToString()
                            })

                        });
                        break;

                    case TypeCards.CARD:

                        actionsItems.Add(new AdaptiveSubmitAction()
                        {
                            Title = actions.Title,
                            Id = actions.ActionId.ToString(),
                            DataJson = JsonConvert.SerializeObject(new JsonDataAux() {
                                 From = TypeCards.CARD.ToString(),
                                 Selected = actions.ActionId.ToString(),
                                 Action = TypeCards.CARD.ToString()
                            })

                        });
                        break;
                }


            }

            return actionsItems;
        }
    }

    public class Action {
        public TypeCards TypeCard { get; set; }
        public Guid ActionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Result { get; set; }
    }

    public enum TypeCards {
        WELCOME,
        IMAGE,
        CARD,
        LINK,
        VIDEO,
        VIDEOCARD,
        GALLERYIMAGES,
        FORM
    }

}
