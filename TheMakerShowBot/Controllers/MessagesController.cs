using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace TheMakerShowBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            if (activity.Type == ActivityTypes.Message)
            {               
                // This is the reply from the original template code, kept here for reference
                // int length = (activity.Text ?? string.Empty).Length;
                // Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
                // await connector.Conversations.ReplyToActivityAsync(reply);

                // Parse the user's meaning via Language Understanding (LUIS) in Cognitive Services
                MakerLUIS mkLuis = await LUISMakerShowClient.ParseUserInput(activity.Text);
                string strRet = string.Empty;
                string strTech = activity.Text;

                if (mkLuis.intents.Count() > 0)
                {
                    switch (mkLuis.intents[0].intent)
                    {
                        case "LearnTopic":
                            strRet = GetResource(mkLuis.entities[0].entity);
                            break;
                        case "BuyHardware":
                            strRet = GetStore(mkLuis.entities[0].entity);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    strRet = "I'm sorry but I don't understand what you're asking";
                }

                Activity reply = activity.CreateReply(strRet);

                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        // We don;t need an async Task for now since nothing is awaited in this code
        //private async Task<string> GetResource(string strTech)
        private string GetResource(string strTech)
        {
            // TO DO: Replace this code with a proper fetch from a JSON file or data table

            string strRet = string.Empty;

            switch (strTech.ToLower())
            {
                case "arduino":
                case "arduino uno":
                case "arduinouno":
                    strRet = "You should watch The Maker Show Episode 1 - Blinking an LED... Now What?";
                    break;
                case "raspberry pi":
                case "raspberrypi":
                    strRet = "You should watch The Maker Show Episode 5 - Installing Windows 10 on a Raspberry Pi 2";
                    break;
                case "3d printing":
                case "3d printer":
                    strRet = "You should watch The Maker Show Episode 4 - Building and Printing a 3D Model to Fit a Real Component";
                    break;
                case "soldering":
                    strRet = "You should watch The Maker Show Episode 6 - Soldering Basics";
                    break;
                case "windows 10 iot core":
                case "windows iot":
                case "windows iot core":
                    strRet = "You should watch The Maker Show Episode 17 - Coding & GPIO in Windows 10 IoT Core";
                    break;
                case "photon":
                case "particle photon":
                case "particle":
                    strRet = "You should watch The Maker Show Episode 7 - The Photon Awakens";
                    break;
                case "wearables":
                case "lillypad":
                    strRet = "You should watch The Maker Show Episode 9 - An Introduction to Wearables";
                    break;
                case "laser cutting":
                    strRet = "You should watch The Maker Show: Mini - Learning how to Laser Cut";
                    break;
                case "tessel":
                case "tessel 2":
                    strRet = "You should watch The Maker Show Episode 18 - Easy IoT with the Tessel 2";
                    break;
                //case "":
                //    strRet = "You should watch The Maker Show Episode";
                //    break;
                //case "":
                //    strRet = "You should watch The Maker Show Episode";
                //    break;
                default:
                    break;
            }

            return strRet;
        }

        //private async Task<string> GetResource(string strTech)
        private string GetStore(string strTech)
        {
            string strRet = string.Empty;

            return strRet;
        }

        private Activity HandleSystemMessage(Activity activity)
        {
            if (activity.Type == ActivityTypes.Ping)
            {
                Activity reply = activity.CreateReply();
                reply.Type = ActivityTypes.Ping;
                return reply;
            }
            else if (activity.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (activity.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Lets your bot know if a bot has been added or removed as a contact for a user
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                // Replaces Bot/User Added/Removed To/From Conversation with a single method
            }
            else if (activity.Type == ActivityTypes.Typing)
            {
                // Lets your bot indicate whether the user or bot is typing
            }

            return null;
        }
    }
}