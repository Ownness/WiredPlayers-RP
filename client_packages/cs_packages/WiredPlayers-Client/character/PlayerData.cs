﻿using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System;

namespace WiredPlayers_Client.character
{
    class PlayerData : Events.Script
    {
        private static Player target = null;

        public PlayerData()
        {
            Events.Add("showPlayerData", ShowPlayerDataEvent);
            Events.Add("showPropertiesData", ShowPropertiesDataEvent);
            Events.Add("showVehiclesData", ShowVehiclesDataEvent);
            Events.Add("showExtendedData", ShowExtendedDataEvent);
            Events.Add("retrievePanelData", RetrievePanelDataEvent);
        }

        private void ShowPlayerDataEvent(object[] args)
        {
            // Get the data from the input
            string name = args[1].ToString();
            string age = args[2].ToString();
            string sex = args[3].ToString();
            string money = args[4].ToString();
            string bank = args[5].ToString();
            string job = args[6].ToString();
            string rank = args[7].ToString();

            if (args[0] != null)
            {
                // Get the player
                int playerId = Convert.ToInt32(args[0]);
                target = Entities.Players.GetAtRemote((ushort)playerId);
            }

            if (Browser.customBrowser == null)
            {
                // Create the window with the basic data
                Browser.CreateBrowserEvent(new object[] { "package://statics/html/playerData.html", "initializePlayerData", name, age, sex, money, bank, job, rank, args[8].ToString() });
            }
            else
            {
                // Update the window
                Browser.ExecuteFunctionEvent(new object[] { "populateBasicData", name, age, sex, money, bank, job, rank });
            }
        }

        private void ShowPropertiesDataEvent(object[] args)
        {
            // Update the window
            Browser.ExecuteFunctionEvent(new object[] { "populatePropertiesData", args[0].ToString(), args[1].ToString() });
        }

        private void ShowVehiclesDataEvent(object[] args)
        {
            // Update the window
            Browser.ExecuteFunctionEvent(new object[] { "populateVehiclesData", args[0].ToString(), args[1].ToString() });
        }

        private void ShowExtendedDataEvent(object[] args)
        {
            // Update the window
            Browser.ExecuteFunctionEvent(new object[] { "populateExtendedData", args[0].ToString() });
        }

        private void RetrievePanelDataEvent(object[] args)
        {
            // Call the event from the parameters
            Events.CallRemote(args[0].ToString(), target);
        }
    }
}
