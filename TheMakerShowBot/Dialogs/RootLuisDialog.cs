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
            string message = $"I'm sorry, but I'm not sure what you're asking me. Type 'help' if you need assistance.";

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

            string message = MakerDataService.GetResource(hardware);

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

            string message = MakerDataService.GetStore(hardware);

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

            string message = MakerDataService.GetPerson(person);

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            string message = "Hi! I'm the Maker Show Bot and I'm here to help you become a maker";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            string message = "Simply ask me questions about different technologies you want to learn or buy";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ThankYou")]
        public async Task ThankYou(IDialogContext context, LuisResult result)
        {
            string message = "No problem, happy to help";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Complain")]
        public async Task Complain(IDialogContext context, LuisResult result)
        {
            string message = "I'm sorry, I'm doing my best. Try again, maybe?";

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

            string message = $"Sorry, I don't really know exactly the differences between {hardware1} and {hardware2}.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }
    }
}