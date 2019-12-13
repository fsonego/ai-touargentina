using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCards;
using Newtonsoft.Json;

namespace aibot_adaptivecards
{
    public class FormMendozaCard : CardBase
    {
        public string UrlImage { get; set; }
        public string BotName { get; set; }

        public FormMendozaCard() { }

        public AdaptiveCard GetCard()
        {
            var _cardResult = SetCard();
            return _cardResult;
        }

        private AdaptiveCard SetCard()
        {

            AdaptiveCard _card = new AdaptiveCard("1.0");

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

            //Form

            var _texDataSubtitle = new AdaptiveTextBlock()
            {
                Text = "Tus Datos",
                Size = AdaptiveTextSize.Medium,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Bolder,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Left
            };

            
            var _textTitleName = new AdaptiveTextBlock()
            {
                Text = "Tú Nombre",
                Size = AdaptiveTextSize.Small,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Default                
            };

            var _textName = new AdaptiveTextInput()
            {
                 Id = "UserName",
                 Placeholder = "Apellido, Nombre"
            };

            var _textTitleEmail = new AdaptiveTextBlock()
            {
                Text = "Dirección de correo",
                Size = AdaptiveTextSize.Small,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Default
            };

            var _textEmail = new AdaptiveTextInput()
            {
                Id = "Email",
                Placeholder = "correo electronico"
            };

            var _texDatesSubtitle = new AdaptiveTextBlock()
            {
                Text = "Fechas",
                Size = AdaptiveTextSize.Medium,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Bolder,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Left
            };

            var _textTitleDeparture = new AdaptiveTextBlock()
            {
                Text = "Fecha de salida",
                Size = AdaptiveTextSize.Small,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Default
            };

            var _textDeparture = new AdaptiveDateInput()
            {
                Id = "Departure"
            };
            
            var _textTitleArrival = new AdaptiveTextBlock()
            {
                Text = "Fecha de regreso",
                Size = AdaptiveTextSize.Small,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Default
            };

            var _textArrival = new AdaptiveDateInput()
            {
                Id = "Arrival"
            };

            var _texOptionsSubtitle = new AdaptiveTextBlock()
            {
                Text = "Tipo de viaje",
                Size = AdaptiveTextSize.Normal,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Bolder,
                HorizontalAlignment = AdaptiveHorizontalAlignment.Left
            };

            var _textTrasportOpciones = new AdaptiveTextBlock()
            {
                Text = "Tipo de transporte",
                Size = AdaptiveTextSize.Medium,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Default
            };
            //traport
            var _transportChoice = new AdaptiveChoiceSetInput();
            _transportChoice.Id = "TransporChoice";
            _transportChoice.Value = "bus";
            _transportChoice.Style = AdaptiveChoiceInputStyle.Expanded;
            _transportChoice.Choices.Add(new AdaptiveChoice()
            {
                Title = "Omnibus",
                Value = "Omnibus"
            });
            _transportChoice.Choices.Add(new AdaptiveChoice()
            {
                Title = "Avion",
                Value = "Avion"
            });

            var _textClassOpciones = new AdaptiveTextBlock()
            {
                Text = "Clase",
                Size = AdaptiveTextSize.Medium,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Default
            };
            //traport
            var _classChoice = new AdaptiveChoiceSetInput();
            _classChoice.Id = "ClassChoice";
            _classChoice.Value = "Economica";
            _classChoice.Style = AdaptiveChoiceInputStyle.Expanded;
            _classChoice.Choices.Add(new AdaptiveChoice() {               
                  Title = "Economica",
                  Value = "Economica"
            });
            _classChoice.Choices.Add(new AdaptiveChoice()
            {
                Title = "Primera",
                Value = "Primera"
            });

            var _textCashOpciones = new AdaptiveTextBlock()
            {
                Text = "¿Forma de Pago?",
                Size = AdaptiveTextSize.Medium,
                Color = AdaptiveTextColor.Default,
                Weight = AdaptiveTextWeight.Default
            };
            //traport
            var _cashChoice = new AdaptiveChoiceSetInput();
            _cashChoice.Id = "CashChoice";
            _cashChoice.Value = "Efectivo";            
            _cashChoice.Choices.Add(new AdaptiveChoice()
            {
                Title = "Efectivo",
                Value = "Efectivo"
            });
            _cashChoice.Choices.Add(new AdaptiveChoice()
            {
                Title = "Tarjeta de Credito",
                Value = "Tarjeta de Credito"
            });

            var _checkConditions = new AdaptiveToggleInput()
            {
                Id = "checkConditions",
                Value = "accept",
                Title = "Acepto los terminos y condiciones."
            };

            var _submitButton = new AdaptiveSubmitAction()
            {
                Title = "Contratar",
                DataJson = JsonConvert.SerializeObject(new JsonDataAux()
                        {
                            From = TypeCards.FORM.ToString(),                         
                            Action = "MDZFORM"
                        })
            };

            _card.Body.Add(_texDataSubtitle);
            _card.Body.Add(_textTitleName);
            _card.Body.Add(_textName);
            _card.Body.Add(_textTitleEmail);
            _card.Body.Add(_textEmail);
            _card.Body.Add(_texDatesSubtitle);
            _card.Body.Add(_textTitleDeparture);
            _card.Body.Add(_textDeparture);
            _card.Body.Add(_textTitleArrival);
            _card.Body.Add(_textArrival);

            _card.Body.Add(_texOptionsSubtitle);
            _card.Body.Add(_textTrasportOpciones);
            _card.Body.Add(_transportChoice);
            _card.Body.Add(_textClassOpciones);
            _card.Body.Add(_classChoice);
            _card.Body.Add(_textCashOpciones);
            _card.Body.Add(_cashChoice);

            _card.Body.Add(_checkConditions);

            _card.Actions.Add(_submitButton);

            return _card;
        }
    }
}
