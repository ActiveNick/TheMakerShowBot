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
                            strRet = mkLuis.entities.Count() > 0 ? GetResource(mkLuis.entities[0].entity) : "";
                            break;
                        case "BuyHardware":
                            strRet = mkLuis.entities.Count() > 0 ? GetStore(mkLuis.entities[0].entity) : "";
                            break;
                        case "Greeting":
                            strRet = "Hi! I'm the Maker Show Bot and I'm here to help you become a maker";
                            break;
                        case "Help":
                            strRet = "Simply ask me questions about different technologies you want to learn or buy";
                            break;
                        case "ThankYou":
                            strRet = "No problem, happy to help";
                            break;
                        case "Complain":
                            strRet = "I'm sorry, I'm doing my best. Try again, maybe?";
                            break;
                        default:
                            strRet = "I'm sorry, but I'm not sure what you're asking me";
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
                case "ardiuno":
                    strRet = "Get started at http://arduino.cc. You should also watch The Maker Show Episode 1 - Blinking an LED... Now What?";
                    break;
                case "raspberry pi":
                case "raspberry":
                case "raspberrypi":
                case "pi":
                case "pie":
                    strRet = "Get started at https://www.raspberrypi.org/. You should also watch The Maker Show Episode 5 - Installing Windows 10 on a Raspberry Pi 2";
                    break;
                case "3d printing":
                case "3d printer":
                case "printers":
                case "3d printers":
                case "printing":
                    strRet = "You should watch The Maker Show Episode 4 - Building and Printing a 3D Model to Fit a Real Component";
                    break;
                case "soldering":
                case "solder":
                    strRet = "You should watch The Maker Show Episode 6 - Soldering Basics";
                    break;
                case "windows 10 iot core":
                case "windows iot":
                case "windows iot core":
                case "iot core":
                    strRet = "Get started at http://dev.windows.com/iot. You should also watch The Maker Show Episode 17 - Coding & GPIO in Windows 10 IoT Core";
                    break;
                case "photon":
                case "photo":
                case "particle photon":
                case "particle":
                case "electron":
                case "particle electron":
                    strRet = "Get started at http://particle.io. You should also watch The Maker Show Episode 7 - The Photon Awakens";
                    break;
                case "wearables":
                case "lillypad":
                    strRet = "You should watch The Maker Show Episode 9 - An Introduction to Wearables";
                    break;
                case "laser cutting":
                case "laser cutter":
                    strRet = "You should watch The Maker Show: Mini - Learning how to Laser Cut";
                    break;
                case "tessel":
                case "tessel 2":
                case "texel":
                    strRet = "Get started at https://www.tessel.io/. You should also watch The Maker Show Episode 18 - Easy IoT with the Tessel 2";
                    break;
                case "esp8266":
                    strRet = "Get started at https://espressif.com/. You also should watch The Maker Show: Episode 16 - The Miniscule ESP8266";
                    break;
                case "alljoyn":
                    strRet = "Get started at https://allseenalliance.org/. You should watch The Maker Show: Episode 11 - Getting Started with AllJoyn Proximity Networks";
                    break;
                case "edison":
                case "intel edison":
                case "inteledison":
                    strRet = "Get started at http://www.intel.com/edison.";
                    break;
                case "coin acceptor":
                case "coinacceptor":
                    strRet = "You should watch The Maker Show: Episode 10 - Adding a Coin Acceptor to Your Arduino Project";
                    break;
                case "neopixel":
                case "neopixels":
                    strRet = "Get started at https://www.adafruit.com/category/168. You should watch The Maker Show: Mini - How to use NeoPixels";
                    break;
                case "hololens":
                case "windows holographic":
                case "windowsholographic":
                case "holographic":
                case "holo lens":
                case "holograms":
                    strRet = "Get started at https://dev.windows.com/holographic to learn about building apps and games for Windows Holographic. While you're there, check out the many tutorials available in the Holographic Academy";
                    break;
                default:
                    strRet = "I'm sorry. I'm not sure how to help you with this one. You should watch The Maker Show: Episode 0 - Meet Your Makers to learn more about the maker world and browse the videos at http://themakershow.io";
                    break;
            }

            return strRet;
        }

        //private async Task<string> GetResource(string strTech)
        private string GetStore(string strTech)
        {
            string strRet = string.Empty;
            bool isMaker = true;

            switch (strTech.ToLower())
            {
                case "arduino":
                case "arduino uno":
                case "arduinouno":
                case "ardiuno":
                    strRet = "Get started at http://arduino.cc.";
                    break;
                case "raspberry pi":
                case "raspberry":
                case "raspberrypi":
                case "pi":
                case "pie":
                    strRet = "Get started at https://www.raspberrypi.org/.";
                    break;
                //case "3d printing":
                //case "3d printer":
                //    strRet = "You should watch The Maker Show Episode 4 - Building and Printing a 3D Model to Fit a Real Component";
                //    break;
                //case "soldering":
                //case "solder":
                //    strRet = "You should watch The Maker Show Episode 6 - Soldering Basics";
                //    break;
                //case "windows 10 iot core":
                //case "windows iot":
                //case "windows iot core":
                //    strRet = "Get started at http://dev.windows.com/iot. You should also watch The Maker Show Episode 17 - Coding & GPIO in Windows 10 IoT Core";
                //    break;
                case "photon":
                case "photo":
                case "particle photon":
                case "particle":
                case "electron":
                case "particle electron":
                    strRet = "Get started at http://particle.io.";
                    break;
                //case "wearables":
                case "lillypad":
                    strRet = "Get started at http://arduino.cc.";
                    break;
                //case "laser cutting":
                //case "laser cutter":
                //    strRet = "You should watch The Maker Show: Mini - Learning how to Laser Cut";
                //    break;
                case "tessel":
                case "tessel 2":
                case "texel":
                    strRet = "Get started at https://www.tessel.io/.";
                    break;
                case "esp8266":
                    strRet = "Get started at https://espressif.com/.";
                    break;
                //case "alljoyn":
                //    strRet = "Get started at https://allseenalliance.org/. You should watch The Maker Show: Episode 11 - Getting Started with AllJoyn Proximity Networks";
                //    break;
                case "edison":
                case "intel edison":
                case "inteledison":
                    strRet = "Get started at http://www.intel.com/edison.";
                    break;
                case "neopixel":
                case "neopixels":
                    strRet = "NeoPixels are made and sold exclusively at http://adafruit.com";
                    isMaker = false;
                    break;
                case "hololens":
                case "windows holographic":
                case "windowsholographic":
                case "holographic":
                case "holo lens":
                case "holograms":
                    strRet = "Get started at https://hololens.com/ to learn about building apps and games for Windows Holographic. While you're there, check out the many tutorials available in the Holographic Academy";
                    isMaker = false;
                    break;
                default:
                    strRet = "I don't have a specific recommendation for this.";
                    break;
            }

            if (isMaker)
            {
                strRet += " I also recommend checking out http://sparkfun.com and http://adafruit.com for all sorts of maker & electronics stuff.";
            }

            return strRet;
        }

        private Activity HandleSystemMessage(Activity activity)
        {
            if (activity.Type == ActivityTypes.Ping)
            {
                
            }
            else if (activity.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (activity.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (activity.Type == ActivityTypes.Typing)
            {
                // Lets your bot indicate whether the user or bot is typing
            }

            return null;
        }
    }
}