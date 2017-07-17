using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using TheMakerShowBot.Models;
using TheMakerShowBot.Services;

namespace TheMakerShowBot.Dialogs
{
    [LuisModel("e86d239e-7bd2-40ac-8881-03fbd8aa0d29", "26e9e3fec3904829983742aa809770e6")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        private const string EntityHardwareType = "HardwareType";

        private const string EntityPerson = "Person";

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string phrase = $"I'm sorry, but I'm not sure what you're asking me. Type 'help' if you need assistance.";

            var message = context.MakeMessage();
            message.Text = phrase;
            message.Speak = phrase;

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("LearnTopic")]
        public async Task LearnTopic(IDialogContext context, LuisResult result)
        {
            EntityRecommendation hardwareEntityRecommendation;
            string hardware = "";

            if (result.TryFindEntity(EntityHardwareType, out hardwareEntityRecommendation))
            {
                hardware = hardwareEntityRecommendation.Entity;
            }

            string phrase = MakerDataService.GetResource(hardware);

            var message = context.MakeMessage();
            message.Text = phrase;
            message.Speak = phrase;

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("BuyHardware")]
        public async Task BuyHardware(IDialogContext context, LuisResult result)
        {
            EntityRecommendation hardwareEntityRecommendation;
            string hardware = "";

            if (result.TryFindEntity(EntityHardwareType, out hardwareEntityRecommendation))
            {
                hardware = hardwareEntityRecommendation.Entity;
            }

            string phrase = MakerDataService.GetStore(hardware);

            var message = context.MakeMessage();
            message.Text = phrase;
            message.Speak = phrase;

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("WhoIs")]
        public async Task WhoIs(IDialogContext context, LuisResult result)
        {
            EntityRecommendation hardwareEntityPerson;
            string person = "";

            if (result.TryFindEntity(EntityPerson, out hardwareEntityPerson))
            {
                person = hardwareEntityPerson.Entity;
            }

            Person maker = MakerDataService.GetPerson(person);

            var message = context.MakeMessage();

            if (maker != null)
            {
                // Create a new Hero Card showing the person's Name, Title, Locastion, Photo and a Twitter profile button
                var heroCard = new HeroCard
                {
                    Title = maker.Name,
                    Subtitle = maker.Title,
                    Text = $"Location: {maker.Location}",
                    Images = new List<CardImage> { new CardImage(maker.ImageUrl) },
                    Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Twitter profile", value: maker.TwitterUrl) }
                };
                var attachment = heroCard.ToAttachment();
                message.Attachments.Add(attachment);

                // Set the text to be spoken out loud since we don;t want to just read the Hero Card data
                message.Speak = $"{maker.Name} is a {maker.Title} based in {maker.Location}.";
            }
            else
            {
                message.Text = $"I'm sorry. I don't have any information about {person}";
            }

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            string phrase = "Hi! I'm the Maker Show Bot and I'm here to help you become a maker";

            var message = context.MakeMessage();
            message.Text = phrase;
            message.Speak = phrase;

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            string phrase = "Simply ask me questions about different technologies you want to learn or buy";

            var message = context.MakeMessage();
            message.Text = phrase;
            message.Speak = phrase;

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ThankYou")]
        public async Task ThankYou(IDialogContext context, LuisResult result)
        {
            string phrase = "No problem, happy to help";

            var message = context.MakeMessage();
            message.Text = phrase;
            message.Speak = phrase;

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Complain")]
        public async Task Complain(IDialogContext context, LuisResult result)
        {
            string phrase = "I'm sorry, I'm doing my best. Try again, maybe?";

            var message = context.MakeMessage();
            message.Text = phrase;
            message.Speak = phrase;

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        // Implementation is not complete on this intent,
        // this was for a live demo at a conference
        [LuisIntent("Compare")]
        public async Task Compare(IDialogContext context, LuisResult result)
        {
            EntityRecommendation hardwareEntityRecommendation;
            string hardware1 = "";
            string hardware2 = "that other one";

            if (result.TryFindEntity(EntityHardwareType, out hardwareEntityRecommendation))
            {
                hardware1 = hardwareEntityRecommendation.Entity;

                if ((result.Entities.Count == 2) && (result.Entities[1].Type == EntityHardwareType))
                {
                    hardware2 = result.Entities[1].Entity;
                }
            }

            string phrase = $"Sorry, I don't really know exactly the differences between {hardware1} and {hardware2}.";

            var message = context.MakeMessage();
            message.Text = phrase;
            message.Speak = phrase;

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }
    }
}