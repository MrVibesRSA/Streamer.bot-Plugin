using MrVibesRSA.StreamerbotPlugin.GUI;
using MrVibesRSA.StreamerbotPlugin.Services;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;

namespace MrVibesRSA.StreamerbotPlugin.Actions
{
    public class StreamerBotAction : PluginAction
    {
        private WebSocketProfileManager _profileManager = WebSocketProfileManager.Instance;
        // The name of the action
        public override string Name => "Run Action";

        // A short description what the action can do
        public override string Description => "Allows you to select and execute any available Streamer.bot action.";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Optional; Add if you added CanConfigure; Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new StreamerBotActionConfigurator(this, actionConfigurator);
        }

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            DoStreamerbotAction(Configuration);
        }

        // Optional; Gets called when the action button gets deleted
        public override void OnActionButtonDelete() { }

        // Optional; Gets called when the action button is loaded
        public override void OnActionButtonLoaded() { }

        private void DoStreamerbotAction(string configuration)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(configuration))
                {
                    MacroDeckLogger.Warning(PluginInstance.Main, "No configuration found for Streamer.bot action.");
                    return;
                }

                JObject configObject = JObject.Parse(configuration);

                string profileId = configObject["profileId"]?.ToString();
                string profile = configObject["profile"]?.ToString();
                string actionId = configObject["actionId"]?.ToString();
                string actionArgument = configObject["actionArgument"]?.ToString();

                if (string.IsNullOrWhiteSpace(actionId))
                {
                    MacroDeckLogger.Warning(PluginInstance.Main, "Invalid Streamer.bot action configuration: actionId missing.");
                    return;
                }

                WebSocketService? service = _profileManager.GetServiceByProfileId(profileId);

                if (service == null || !service.IsConnected)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"Streamer.bot profile '{profile}' is not connected.");
                    return;
                }

                service.DoAction(actionId, actionArgument);
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to execute Streamer.bot action: {ex.Message}");
            }
        }
    }
}
