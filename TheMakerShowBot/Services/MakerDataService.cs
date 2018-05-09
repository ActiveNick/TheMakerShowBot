using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheMakerShowBot.Models;

namespace TheMakerShowBot.Services
{
    public static class MakerDataService
    {
        // We don't need an async Task for now since nothing is awaited in this code
        //private async Task<string> GetResource(string strTech)
        public static string GetResource(string strTech)
        {
            // TO DO: Replace this code with a proper fetch from a JSON file or data table

            string strRet = string.Empty;
            strTech = strTech.Replace(" ", ""); // Remove all spaces to make it easier to parse the parameters

            // The switch includes a lot of misspellings to account for common errors that can occur when
            // dealing with input coming from Speech Recognition with Open Dictation.
            switch (strTech.ToLower())
            {
                case "arduino":
                case "adruino":
                case "arduinouno":
                case "ardiuno":
                case "ourdoweknow":
                case "ordoweknow":
                    strRet = "Get started at http://arduino.cc. You should also watch The Maker Show Episode 1 - Blinking an LED... Now What? with Brian Sherwin.";
                    break;
                case "raspberry":
                case "raspberrypi":
                case "raspberrypie":
                case "pi":
                case "pie":
                    strRet = "Get started at https://www.raspberrypi.org/. You should also watch The Maker Show Episode 5 - Installing Windows 10 on a Raspberry Pi, with Kenny Spade.";
                    break;
                case "electricity":
                case "electronics":
                    strRet = "You should watch The Maker Show Episode 1 - Introduction to Electronics, with David Crook. You'll actually learn how to make electricity from lemons!";
                    break;
                case "motors":
                case "motor":
                    strRet = "Sam Stokes covered servo motors in The Maker Show Episode 3, and Bret Stateham covered stepper motors in episode 8.";
                    break;
                case "servomotors":
                case "servomotor":
                case "servo":
                    strRet = "You should watch The Maker Show Episode 3 - Arduino and Servos, with Sam Stokes.";
                    break;
                case "steppermotors":
                case "steppermotor":
                case "stepper":
                    strRet = "You should watch The Maker Show Episode 8 - Driving Your Stepper Motor with an Arduino, with Bret Stateham.";
                    break;
                case "print":
                case "3dprinting":
                case "3d":
                case "3dprinter":
                case "printers":
                case "3dprinters":
                case "printing":
                    strRet = "You should watch The Maker Show Episode 4 - Building and Printing a 3D Model to Fit a Real Component, with Jeremy Foster.";
                    break;
                case "soldering":
                case "solder":
                    strRet = "You should watch The Maker Show Episode 6 - Soldering Basics, with Frank La Vigne.";
                    break;
                case "arduinointerrupts":
                case "interrupts":
                case "electronicinterrupts":
                case "codeinterrupts":
                    strRet = "You should watch The Maker Show Episode 13 - Breaking Out of the Loop, with Jared Bienz.";
                    break;
                case "windows10iotcore":
                case "windowsteniotcore":
                case "windowsiot":
                case "windowsiotcore":
                case "iotcore":
                    strRet = "Get started at http://dev.windows.com/iot. You should also watch The Maker Show Episode 17 - Coding & GPIO in Windows 10 IoT Core, with Nick Landry.";
                    break;
                case "azure":
                case "microsoftazure":
                case "azureiot":
                case "azureiothubs":
                case "azureiothub":
                case "microsoftazureiot":
                case "azureiotsuite":
                case "microsoftazureiotsuite":
                case "microsoftazureiothubs":
                case "microsoftazureiothub":
                    strRet = "Get started at http://azure.com/iot. You should also watch The Maker Show Episode 12 - Connecting Particle Photon to CloudServices, with Paul DeCarlo. Bret Stateham also recorded a six-part series on the Particle Photon Weather Station with Azure.";
                    break;
                case "photon":
                case "photo":
                case "photoin":
                case "particlephoton":
                case "particlefulton":
                case "particle":
                case "fogtown":
                case "particlefogtown":
                case "electron":
                case "particleelectron":
                    strRet = "Get started at http://particle.io. You should also watch The Maker Show Episode 7 - The Photon Awakens, with Nick Landry.";
                    break;
                case "wearables":
                case "lillypad":
                case "lily":
                case "lilly":
                    strRet = "You should watch The Maker Show Episode 9 - An Introduction to Wearables, with Stacey Mulcahy.";
                    break;
                case "lasercutting":
                case "lasercutter":
                case "laser":
                case "cutting":
                case "cutter":
                case "lasercutters":
                    strRet = "You should watch The Maker Show Mini - Learning how to Laser Cut, with David Sheinkopf.";
                    break;
                case "tessel":
                case "tesseltwo":
                case "tessel2":
                case "texel":
                case "texeltwo":
                case "texel2":
                    strRet = "Get started at https://www.tessel.io/. You should also watch The Maker Show Episode 18 - Easy IoT with the Tessel 2, with Jeremy Foster.";
                    break;
                case "esp8266":
                    strRet = "Get started at https://espressif.com/. You also should watch The Maker Show: Episode 16 - The Miniscule ESP8266, with Brian Sherwin.";
                    break;
                case "alljoyn":
                    strRet = "Get started at https://allseenalliance.org/. You should watch The Maker Show: Episode 11 - Getting Started with AllJoyn Proximity Networks, with Nathaniel Rose.";
                    break;
                case "edison":
                case "inteledison":
                    strRet = "Get started at http://www.intel.com/edison. You should also check out Jeremy Foster's blog at http://codefoster.com/edison.";
                    break;
                case "coinslot":
                case "coinacceptor":
                    strRet = "You should watch The Maker Show Episode 10 - Adding a Coin Acceptor to Your Arduino Project, with Rachel Weil.";
                    break;
                case "signalr":
                case "signalare":
                    strRet = "You should watch The Maker Show Episode 14 - SignalR with the Raspberry Pi, with Ian Philpot.";
                    break;
                case "neopixel":
                case "neopixels":
                    strRet = "Get started at https://www.adafruit.com/category/168. You should watch The Maker Show Mini - How to use NeoPixels, with Stacey Mulcahy.";
                    break;
                case "voltageregulators":
                case "regulators":
                case "voltageregulator":
                    strRet = "You should watch The Maker Show Episode 15 - Using Voltage Regulators to Power your Projects, with Bret Stateham.";
                    break;
                case "mechanicalcomputers":
                case "mechanicalcomputer":
                case "gravicomp":
                    strRet = "You should watch The Maker Show Mini - GraviComp Mechanical Computer, with James McCaffrey.";
                    break;
                case "radio":
                case "radiofromscratch":
                case "amradio":
                case "fmradio":
                    strRet = "You should watch The Maker Show Episode 19 - A Radio from Scratch, with Jeremy Foster.";
                    break;
                case "bitcoin":
                case "bitcoinsensor":
                    strRet = "You should watch The Maker Show Mini - Bitcoin Sensor, with Paul DeCarlo.";
                    break;
                case "outside":
                case "outdoor":
                case "outdoors":
                case "outsidehack":
                case "outsidehacks":
                case "outsidegadget":
                case "outsidegadgets":
                case "outsideelectronics":
                case "outdoorhacks":
                case "outdoorhack":
                case "outdoorgadget":
                case "outdoorgadgets":
                case "outdoorelectronics":
                case "rugged":
                case "ruggedgadgets":
                case "offgrid":
                    strRet = "You should watch The Maker Show Episode 20 - IoT in Extreme and Off-Grid Scenarios, with Frank La Vigne.";
                    break;
                case "garage":
                case "automatedgarage":
                case "garagedooropener":
                case "internetconnectedgarage":
                case "internet-connectedgarage":
                case "garageapp":
                case "garageapplication":
                case "garagegadget":
                case "garagegadgets":
                    strRet = "You should watch The Maker Show Episode 21 - Building an iOS app for your Garage, with David Washington.";
                    break;
                case "hololens":
                case "windowsholographic":
                case "holographic":
                case "hollowlens":
                case "wholeolins":
                case "holograms":
                    strRet = "Get started at https://aka.ms/mr to learn about building apps and games for Windows Mixed Reality. While you're there, check out the many tutorials available in the Mixed Reality Academy.";
                    break;
                default:
                    strRet = "I'm sorry. I'm not sure how to help you with this one. You should watch The Maker Show: Episode 0 - Meet Your Makers to learn more about the maker world and browse the videos at http://themakershow.io.";
                    break;
            }

            return strRet;
        }

        //private async Task<string> GetResource(string strTech)
        public static string GetStore(string strTech)
        {
            string strRet = string.Empty;
            strTech = strTech.Replace(" ", ""); // Remove all spaces to make it easier to parse the parameters
            bool isMaker = true;

            // The switch includes a lot of misspellings to account for common errors that can occur when
            // dealing with input coming from Speech Recognition with Open Dictation.
            switch (strTech.ToLower())
            {
                case "arduino":
                case "arduinouno":
                case "ardiuno":
                case "ourdoweknow":
                case "ordoweknow":
                    strRet = "Get started at http://arduino.cc.";
                    break;
                case "raspberry":
                case "raspberrypi":
                case "raspberrypie":
                case "pi":
                case "pie":
                    strRet = "Get started at https://www.raspberrypi.org/.";
                    break;
                case "photon":
                case "photo":
                case "photoin":
                case "particlephoton":
                case "particlefulton":
                case "particle":
                case "fogtown":
                case "particlefogtown":
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
                case "hollowlens":
                case "wholeolins":
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

        public static Person GetPerson(string strName)
        {
            string strRet = string.Empty;
            strName = strName.Replace(" ", ""); // Remove all spaces to make it easier to parse the parameters

            Person maker = new Person();

            // The switch includes a lot of misspellings to account for common errors that can occur when
            // dealing with input coming from Speech Recognition with Open Dictation.
            switch (strName.ToLower())
            {
                case "briansherwin":
                case "brianshwerwin":
                case "sherwin":
                    maker.Name = "Brian Sherwin";
                    maker.Title = "Microsoft Technical Evangelist";
                    maker.Location = "Columbus, OH";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/473558593250148353/u7jMVvfk_400x400.jpeg";
                    maker.TwitterUrl = "https://twitter.com/bsherwin";

                    //strRet = "Brian Sherwin is a Microsoft Technical Evangelist based in Columbus, Ohio.";
                    break;
                case "nicklandry":
                case "nicklaundry":
                case "landry":
                    maker.Name = "Nick Landry";
                    maker.Title = "Microsoft Software Engineer";
                    maker.Location = "New York, NY";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/877892100783828992/Fq8KQiDA_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/ActiveNick";

                    //strRet = "Nick Landry is a Microsoft Technical Evangelist based in New York City.";
                    break;
                case "jeremyfoster":
                case "foster":
                    maker.Name = "Jeremy Foster";
                    maker.Title = "Microsoft Software Development Engineer";
                    maker.Location = "Seattle, WA";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/851478701023805441/J-Sc3ZV2_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/codefoster";

                    //strRet = "Jeremy Foster is a Microsoft Technical Evangelist based in Seattle, Washington.";
                    break;
                case "bretstateham":
                case "bretstatehim":
                case "bretstate":
                case "brettstateham":
                case "brettstatehim":
                case "brettstate":
                case "stateham":
                case "bret":
                case "brett":
                case "bretstadium":
                case "brettstadium":
                    maker.Name = "Bret Stateham";
                    maker.Title = "Microsoft Software Development Engineer";
                    maker.Location = "San Diego, CA";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/562664058/BSHS01_Retouched_256_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/bretstateham";

                    //strRet = "Bret Stateham is a Microsoft Technical Evangelist based in San Diego.";
                    break;
                case "staceymulcahy":
                case "stacymulcahy":
                case "staceymulcahey":
                case "stacymulcahey":
                case "stacey":
                case "stacy":
                case "mulcahy":
                case "mulcahey":
                    maker.Name = "Stacey Mulcahy";
                    maker.Title = "Program Manager, Microsoft Garage";
                    maker.Location = "Vancouver, BC";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/596112741120638977/ggVL3fkj_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/bitchwhocodes";

                    //strRet = "Stacey Mulcahy is a Program Manager for the Microsoft Garage in Vancouver, British Columbia.";
                    break;
                case "rachelweil":
                case "rachelveil":
                case "rachel":
                case "rachelwhile":
                case "while":
                case "weil":
                    maker.Name = "Rachel Weil";
                    maker.Title = "Microsoft Technical Evangelist";
                    maker.Location = "Austin, TX";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/860923782961168384/sKm4ZHxy_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/partytimeHXLNT";

                    //strRet = "Rachel Weil is a Microsoft Technical Evangelist based in Austin, Texas.";
                    break;
                case "pauldecarlo":
                case "decarlo":
                case "carlo":
                    maker.Name = "Paul DeCarlo";
                    maker.Title = "Microsoft Software Development Engineer";
                    maker.Location = "Houston, TX";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/771055098848022528/MVRQJ3If_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/pjdecarlo";

                    //strRet = "Paul DeCarlo is a Microsoft Technical Evangelist based in Houston, Texas.";
                    break;
                case "davidwashington":
                case "washington":
                    maker.Name = "David Washington";
                    maker.Title = "Microsoft Software Engineering Lead";
                    maker.Location = "Saint Paul, MN";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/378800000227509297/a60236af1fe6d45f29e1c8fcd7405b58_400x400.jpeg";
                    maker.TwitterUrl = "https://twitter.com/dwcares";

                    //strRet = "David Washington is a Director of Technical Evangelism at Microsoft in Minnesota.";
                    break;
                case "franklavigne":
                case "franklavigna":
                case "lavigna":
                case "lavigne":
                case "vigne":
                    maker.Name = "Frank La Vigne";
                    maker.Title = "Microsoft Technical Solutions Professional";
                    maker.Location = "Washington, DC";
                    maker.ImageUrl = "https://media.licdn.com/mpr/mpr/shrinknp_400_400/AAEAAQAAAAAAAAtwAAAAJDVjZDk3MzE4LTFkMmYtNDU2Zi04MzA2LTY0NjcwMzNkZmJmNQ.jpg";
                    maker.TwitterUrl = "https://twitter.com/tableteer";

                    //strRet = "Frank La Vigne is a Microsoft Technical Evangelist based in Washington, DC.";
                    break;
                case "jamesmccaffrey":
                case "mccaffrey":
                    maker.Name = "James McCaffrey";
                    maker.Title = "Senior Researcher at Microsoft Research";
                    maker.Location = "Redmond, WA";
                    maker.ImageUrl = "https://sec.ch9.ms/sessions/build/2013/JamesMcCaffrey.jpg";
                    maker.TwitterUrl = "https://twitter.com/MSFTResearch";

                    //strRet = "James McCaffrey is a Senior Researcher at Microsoft Research in Redmond, Washington.";
                    break;
                case "jaredbienz":
                case "bienz":
                    maker.Name = "Jared Bienz";
                    maker.Title = "Microsoft Software Engineer";
                    maker.Location = "Houston, TX";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/1267332786/00_-_White_Wall_Square_Face_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/jbienz";

                    //strRet = "Jared Bienz is a Microsoft Technical Evangelist based in Houston, Texas.";
                    break;
                case "nathanielrose":
                case "rose":
                    maker.Name = "Nathaniel Rose";
                    maker.Title = "Microsoft Software Development Engineer";
                    maker.Location = "San Francisco, CA";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/469225813154336770/uzqMx93S_400x400.jpeg";
                    maker.TwitterUrl = "https://twitter.com/naterose2";

                    //strRet = "Nathaniel Rose is a Microsoft Technical Evangelist based in San Francisco, California.";
                    break;
                case "kenny":
                case "kenny'spaid":
                case "kennyspade":
                case "spade":
                    maker.Name = "Kenny Spade";
                    maker.Title = "Program Manager for the Microsoft Garage";
                    maker.Location = "Silicon valley, CA";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/392114511/4531_104097427649_604992649_2692227_3915030_n_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/kennyspade";

                    //strRet = "Kenny Spade is a Program Manager for the Microsoft Garage in Silicon Valley, California.";
                    break;
                case "davidcrook":
                case "crook":
                    maker.Name = "David Crook";
                    maker.Title = "Microsoft Software Development Engineer";
                    maker.Location = "Deerfield Beach, Florida";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/701998221741133824/Al7sZ3QD_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/data4bots";

                    //strRet = "David Crook is a Microsoft Technical Evangelist based in Miami, Florida.";
                    break;
                case "samstokes":
                case "stokes":
                    maker.Name = "Sam Stokes";
                    maker.Title = "Former Microsoft Technical Evangelist";
                    maker.Location = "Dana Point, CA";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/58772808/sam_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/socalsam";

                    //strRet = "Sam Stokes is a former Microsoft Technical Evangelist now working on the Mars Habitat mission at UCLA in Southern California.";
                    break;
                case "davidsheinkopf":
                case "davesheinkopf":
                case "sheinkopf":
                    maker.Name = "David Sheinkopf";
                    maker.Title = "Electronics Teacher & Director of Education at Pioneer Works Center for Art and Innovation";
                    maker.Location = "Brooklyn, NY";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/3364417376/71be72f79112703e2fed955e954563b2_400x400.jpeg";
                    maker.TwitterUrl = "https://twitter.com/";

                    //strRet = "David Sheinkopf is an Electronics Teacher and the Director of Education at Pioneer Works Center for Art and Innovation in Brooklyn, New York.";
                    break;
                case "ianphilpot":
                case "philpot":
                    maker.Name = "Ian Philpot";
                    maker.Title = "Microsoft Technical Evangelist";
                    maker.Location = "Atlanta, GA";
                    maker.ImageUrl = "https://pbs.twimg.com/profile_images/829692939056070656/10LZD60A_400x400.jpg";
                    maker.TwitterUrl = "https://twitter.com/tripdubroot";

                    //strRet = "Ian Philpot is a Microsoft Technical Evangelist based in Atlanta, Georgia.";
                    break;
                default:
                    maker = null;
                    //strRet = "I'm sorry. I don't have any information about " + strName + ".";
                    break;
            }

            return maker;
        }
    }
}