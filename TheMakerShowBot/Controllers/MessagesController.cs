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
                        case "WhoIs":
                            strRet = mkLuis.entities.Count() > 0 ? GetPerson(mkLuis.entities[0].entity) : "";
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
            strTech = strTech.Replace(" ", "");

            switch (strTech.ToLower())
            {
                case "arduino":
                case "arduinouno":
                case "ardiuno":
                    strRet = "Get started at http://arduino.cc. You should also watch The Maker Show Episode 1 - Blinking an LED... Now What?";
                    break;
                case "raspberry":
                case "raspberrypi":
                case "pi":
                case "pie":
                    strRet = "Get started at https://www.raspberrypi.org/. You should also watch The Maker Show Episode 5 - Installing Windows 10 on a Raspberry Pi 2";
                    break;
                case "3dprinting":
                case "3dprinter":
                case "printers":
                case "3dprinters":
                case "printing":
                    strRet = "You should watch The Maker Show Episode 4 - Building and Printing a 3D Model to Fit a Real Component";
                    break;
                case "soldering":
                case "solder":
                    strRet = "You should watch The Maker Show Episode 6 - Soldering Basics";
                    break;
                case "windows10iotcore":
                case "windowsteniotcore":
                case "windowsiot":
                case "windowsiotcore":
                case "iotcore":
                    strRet = "Get started at http://dev.windows.com/iot. You should also watch The Maker Show Episode 17 - Coding & GPIO in Windows 10 IoT Core";
                    break;
                case "photon":
                case "photo":
                case "particlephoton":
                case "particle":
                case "electron":
                case "particleelectron":
                    strRet = "Get started at http://particle.io. You should also watch The Maker Show Episode 7 - The Photon Awakens";
                    break;
                case "wearables":
                case "lillypad":
                    strRet = "You should watch The Maker Show Episode 9 - An Introduction to Wearables";
                    break;
                case "lasercutting":
                case "lasercutter":
                    strRet = "You should watch The Maker Show: Mini - Learning how to Laser Cut";
                    break;
                case "tessel":
                case "tessel2":
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
                case "inteledison":
                    strRet = "Get started at http://www.intel.com/edison.";
                    break;
                case "coinslot":
                case "coinacceptor":
                    strRet = "You should watch The Maker Show: Episode 10 - Adding a Coin Acceptor to Your Arduino Project";
                    break;
                case "neopixel":
                case "neopixels":
                    strRet = "Get started at https://www.adafruit.com/category/168. You should watch The Maker Show: Mini - How to use NeoPixels";
                    break;
                case "hololens":
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
            strTech = strTech.Replace(" ", "");
            bool isMaker = true;

            switch (strTech.ToLower())
            {
                case "arduino":
                case "arduinouno":
                case "ardiuno":
                    strRet = "Get started at http://arduino.cc.";
                    break;
                case "raspberry":
                case "raspberrypi":
                case "pi":
                case "pie":
                    strRet = "Get started at https://www.raspberrypi.org/.";
                    break;
                case "photon":
                case "photo":
                case "particlephoton":
                case "particle":
                case "electron":
                case "particleelectron":
                    strRet = "Get started at http://particle.io.";
                    break;
                case "lillypad":
                    strRet = "Get started at http://arduino.cc.";
                    break;
                case "tessel":
                case "tessel2":
                case "texel":
                    strRet = "Get started at https://www.tessel.io/.";
                    break;
                case "esp8266":
                    strRet = "Get started at https://espressif.com/.";
                    break;
                case "edison":
                case "inteledison":
                    strRet = "Get started at http://www.intel.com/edison.";
                    break;
                case "neopixel":
                case "neopixels":
                    strRet = "NeoPixels are made and sold exclusively at http://adafruit.com";
                    isMaker = false;
                    break;
                case "hololens":
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

        private string GetPerson(string strName)
        {
            string strRet = string.Empty;
            strName = strName.Replace(" ", "");

            switch (strName.ToLower())
            {
                case "briansherwin":
                case "sherwin":
                    strRet = "Brian Sherwin is a Microsoft Technical Evangelist based in Columbus, Ohio.";
                    break;
                case "nicklandry":
                case "landry":
                    strRet = "Nick Landry is a Microsoft Technical Evangelist based in New York City.";
                    break;
                case "jeremyfoster":
                case "foster":
                    strRet = "Jeremy Foster is a Microsoft Technical Evangelist based in Seattle, Washington.";
                    break;
                case "bretstateham":
                case "stateham":
                    strRet = "Bret Stateham is a Microsoft Technical Evangelist based in San Diego.";
                    break;
                case "staceymulcahy":
                case "mulcahy":
                    strRet = "Stacey Mulcahy is a Program Manager for the Microsoft Garage in Vancouver, British Columbia.";
                    break;
                case "rachelweil":
                case "weil":
                    strRet = "Rachel Weil is a Microsoft Technical Evangelist based in Austin, Texas.";
                    break;
                case "pauldecarlo":
                case "decarlo":
                case "carlo":
                    strRet = "Paul De Carlo is a Microsoft Technical Evangelist based in Houston, Texas.";
                    break;
                case "davidwashington":
                case "washington":
                    strRet = "David Washington is a Director of Technical Evangelist at Microsoft in Minnesota.";
                    break;
                case "franklavigne":
                case "lavigne":
                case "vigne":
                    strRet = "Frank La Vigne is a Microsoft Technical Evangelist based in Washington, DC.";
                    break;
                case "jamesmccaffrey":
                case "mccaffrey":
                    strRet = "James McCaffrey is a Senior Researcher  at Microsoft Research in Redmond, Washington.";
                    break;
                case "jaredbienz":
                case "bienz":
                    strRet = "Jared Bienz is a Microsoft Technical Evangelist based in Houston, Texas.";
                    break;
                case "nathanielrose":
                case "rose":
                    strRet = "Nathaniel Rose is a Microsoft Technical Evangelist based in San Francisco, California.";
                    break;
                case "kennyspade":
                case "spade":
                    strRet = "Kenny Spade is a Program Manager for the Microsoft Garage in Silicon Valley, California.";
                    break;
                case "davidcrook":
                case "crook":
                    strRet = "David Crook is a Microsoft Technical Evangelist based in Miami, Florida.";
                    break;
                case "samstokes":
                case "stokes":
                    strRet = "Sam Stokes is a former Microsoft Technical Evangelist now working on the Mars Habitat mission at UCLA in Southern California.";
                    break;
                //case "":
                //case "":
                //    strRet = " is a Microsoft Technical Evangelist based in .";
                //    break;
                default:
                    strRet = "I don't have any information about " + strName + ".";
                    break;
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