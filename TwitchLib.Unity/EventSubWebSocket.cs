using Microsoft.Extensions.Logging;
using System;
using TwitchLib.EventSub.Websockets;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Stream;
using TwitchLib.EventSub.Websockets.Core.EventArgs.User;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Core;
using System.Threading.Tasks;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace TwitchLib.Unity
{
    public class EventSubWebSocket : EventSubWebsocketClient
    {
        #region Events
        /// <summary>
        /// Event that triggers when the websocket was successfully connected
        /// </summary>
        public new event AsyncEventHandler<WebsocketConnectedArgs> WebsocketConnected;
        /// <summary>
        /// Event that triggers when the websocket disconnected
        /// </summary>
        public new event AsyncEventHandler WebsocketDisconnected;
        /// <summary>
        /// Event that triggers when an error occurred on the websocket
        /// </summary>
        public new event AsyncEventHandler<ErrorOccuredArgs> ErrorOccurred;
        /// <summary>
        /// Event that triggers when the websocket was successfully reconnected
        /// </summary>
        public new event AsyncEventHandler WebsocketReconnected;

        /// <summary>
        /// Event that triggers on "channel.ad_break.begin" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelAdBreakBeginArgs> ChannelAdBreakBegin;

        /// <summary>
        /// Event that triggers on "channel.ban" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelBanArgs> ChannelBan;

        /// <summary>
        /// Event that triggers on "channel.charity_campaign.start" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelCharityCampaignStartArgs> ChannelCharityCampaignStart;
        /// <summary>
        /// Event that triggers on "channel.charity_campaign.donate" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelCharityCampaignDonateArgs> ChannelCharityCampaignDonate;
        /// <summary>
        /// Event that triggers on "channel.charity_campaign.progress" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelCharityCampaignProgressArgs> ChannelCharityCampaignProgress;
        /// <summary>
        /// Event that triggers on "channel.charity_campaign.stop" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelCharityCampaignStopArgs> ChannelCharityCampaignStop;
        /// <summary>
        /// Event that triggers on channel.chat.message notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelChatMessageArgs> ChannelChatMessage;
        /// <summary>
        /// Event that triggers on "channel.cheer" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelCheerArgs> ChannelCheer;
        /// <summary>
        /// Event that triggers on "channel.follow" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelFollowArgs> ChannelFollow;

        /// <summary>
        /// Event that triggers on "channel.goal.begin" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelGoalBeginArgs> ChannelGoalBegin;
        /// <summary>
        /// Event that triggers on "channel.goal.end" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelGoalEndArgs> ChannelGoalEnd;
        /// <summary>
        /// Event that triggers on "channel.goal.progress" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelGoalProgressArgs> ChannelGoalProgress;

        /// <summary>
        /// Event that triggers on "channel.guest_star_guest.update" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelGuestStarGuestUpdateArgs> ChannelGuestStarGuestUpdate;
        /// <summary>
        /// Event that triggers on "channel.guest_star_session.begin" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelGuestStarSessionBegin> ChannelGuestStarSessionBegin;
        /// <summary>
        /// Event that triggers on "channel.guest_star_guest.update" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelGuestStarSessionEnd> ChannelGuestStarSessionEnd;
        /// <summary>
        /// Event that triggers on "channel.guest_star_settings.update" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelGuestStarSettingsUpdateArgs> ChannelGuestStarSettingsUpdate;
        /// <summary>
        /// Event that triggers on "channel.guest_star_slot.update" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelGuestStarSlotUpdateArgs> ChannelGuestStarSlotUpdate;

        /// <summary>
        /// Event that triggers on "channel.hype_train.begin" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelHypeTrainBeginArgs> ChannelHypeTrainBegin;
        /// <summary>
        /// Event that triggers on "channel.hype_train.end" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelHypeTrainEndArgs> ChannelHypeTrainEnd;
        /// <summary>
        /// Event that triggers on "channel.hype_train.progress" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelHypeTrainProgressArgs> ChannelHypeTrainProgress;

        /// <summary>
        /// Event that triggers on "channel.moderator.add" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelModeratorArgs> ChannelModeratorAdd;
        /// <summary>
        /// Event that triggers on "channel.moderator.remove" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelModeratorArgs> ChannelModeratorRemove;

        /// <summary>
        /// Event that triggers on "channel.vip.add" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelVipArgs> ChannelVipAdd;
        /// <summary>
        /// Event that triggers on "channel.vip.remove" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelVipArgs> ChannelVipRemove;

        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward.add" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPointsCustomRewardArgs> ChannelPointsCustomRewardAdd;
        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward.remove" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPointsCustomRewardArgs> ChannelPointsCustomRewardRemove;
        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward.update" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPointsCustomRewardArgs> ChannelPointsCustomRewardUpdate;

        /// <summary>
        /// Event that triggers on "channel.channel_points_automatic_reward_redemption.add" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPointsAutomaticRewardRedemptionArgs> ChannelPointsAutomaticRewardRedemptionAdd;

        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward_redemption.add" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPointsCustomRewardRedemptionArgs> ChannelPointsCustomRewardRedemptionAdd;
        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward_redemption.update" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPointsCustomRewardRedemptionArgs> ChannelPointsCustomRewardRedemptionUpdate;

        /// <summary>
        /// Event that triggers on "channel.poll.begin" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPollBeginArgs> ChannelPollBegin;
        /// <summary>
        /// Event that triggers on "channel.poll.end" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPollEndArgs> ChannelPollEnd;
        /// <summary>
        /// Event that triggers on "channel.poll.progress" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPollProgressArgs> ChannelPollProgress;

        /// <summary>
        /// Event that triggers on "channel.prediction.begin" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPredictionBeginArgs> ChannelPredictionBegin;
        /// <summary>
        /// Event that triggers on "channel.prediction.end" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPredictionEndArgs> ChannelPredictionEnd;
        /// <summary>
        /// Event that triggers on "channel.prediction.lock" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPredictionLockArgs> ChannelPredictionLock;
        /// <summary>
        /// Event that triggers on "channel.prediction.progress" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelPredictionProgressArgs> ChannelPredictionProgress;

        /// <summary>
        /// Event that triggers on "channel.raid" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelRaidArgs> ChannelRaid;

        /// <summary>
        /// Event that triggers on "channel.shield_mode.begin" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelShieldModeBeginArgs> ChannelShieldModeBegin;
        /// <summary>
        /// Event that triggers on "channel.shield_mode.end" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelShieldModeEndArgs> ChannelShieldModeEnd;

        /// <summary>
        /// Event that triggers on "channel.shoutout.create" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelShoutoutCreateArgs> ChannelShoutoutCreate;
        /// <summary>
        /// Event that triggers on "channel.shoutout.receive" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelShoutoutReceiveArgs> ChannelShoutoutReceive;

        /// <summary>
        /// Event that triggers on "channel.subscribe" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelSubscribeArgs> ChannelSubscribe;
        /// <summary>
        /// Event that triggers on "channel.subscription.end" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelSubscriptionEndArgs> ChannelSubscriptionEnd;
        /// <summary>
        /// Event that triggers on "channel.subscription.gift" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelSubscriptionGiftArgs> ChannelSubscriptionGift;
        /// <summary>
        /// Event that triggers on "channel.subscription.message" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelSubscriptionMessageArgs> ChannelSubscriptionMessage;

        /// <summary>
        /// Event that triggers on "channel.suspicious_user.message" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelSuspiciousUserMessageArgs> ChannelSuspiciousUserMessage;

        /// <summary>
        /// Event that triggers on "channel.suspicious_user.update" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelSuspiciousUserUpdateArgs> ChannelSuspiciousUserUpdate;

        /// <summary>
        /// Event that triggers on "channel.warning.acknowledge" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelWarningAcknowledgeArgs> ChannelWarningAcknowledge;

        /// <summary>
        /// Event that triggers on "channel.warning.send" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelWarningSendArgs> ChannelWarningSend;

        /// <summary>
        /// Event that triggers on "channel.unban" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelUnbanArgs> ChannelUnban;

        /// <summary>
        /// Event that triggers on "channel.update" notifications
        /// </summary>
        public new event AsyncEventHandler<ChannelUpdateArgs> ChannelUpdate;

        /// <summary>
        /// Event that triggers on "stream.offline" notifications
        /// </summary>
        public new event AsyncEventHandler<StreamOfflineArgs> StreamOffline;
        /// <summary>
        /// Event that triggers on "stream.online" notifications
        /// </summary>
        public new event AsyncEventHandler<StreamOnlineArgs> StreamOnline;

        /// <summary>
        /// Event that triggers on "user.update" notifications
        /// </summary>
        public new event AsyncEventHandler<UserUpdateArgs> UserUpdate;

        /// <summary>
        /// Event that triggers on "user.whisper.message" notifications
        /// </summary>
        public new event AsyncEventHandler<UserWhisperMessageArgs> UserWhisperMessage;
        #endregion

        public EventSubWebSocket(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            ThreadDispatcher.EnsureCreated();

            base.WebsocketConnected += (object sender, WebsocketConnectedArgs e) => { ThreadDispatcher.Enqueue(() => WebsocketConnected?.Invoke(sender, e)); return Task.CompletedTask; };
            base.WebsocketDisconnected += (object sender, EventArgs e) => { ThreadDispatcher.Enqueue(() => WebsocketDisconnected?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ErrorOccurred += (object sender, ErrorOccuredArgs e) => { ThreadDispatcher.Enqueue(() => ErrorOccurred?.Invoke(sender, e)); return Task.CompletedTask; };
            base.WebsocketReconnected += (object sender, EventArgs e) => { ThreadDispatcher.Enqueue(() => WebsocketReconnected?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelAdBreakBegin += (object sender, ChannelAdBreakBeginArgs e) => { ThreadDispatcher.Enqueue(() => ChannelAdBreakBegin?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelBan += (object sender, ChannelBanArgs e) => { ThreadDispatcher.Enqueue(() => ChannelBan?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelCharityCampaignStart += (object sender, ChannelCharityCampaignStartArgs e) => { ThreadDispatcher.Enqueue(() => ChannelCharityCampaignStart?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelCharityCampaignDonate += (object sender, ChannelCharityCampaignDonateArgs e) => { ThreadDispatcher.Enqueue(() => ChannelCharityCampaignDonate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelCharityCampaignProgress += (object sender, ChannelCharityCampaignProgressArgs e) => { ThreadDispatcher.Enqueue(() => ChannelCharityCampaignProgress?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelCharityCampaignStop += (object sender, ChannelCharityCampaignStopArgs e) => { ThreadDispatcher.Enqueue(() => ChannelCharityCampaignStop?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelChatMessage += (object sender, ChannelChatMessageArgs e) => { ThreadDispatcher.Enqueue(() => ChannelChatMessage?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelCheer += (object sender, ChannelCheerArgs e) => { ThreadDispatcher.Enqueue(() => ChannelCheer?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelFollow += (object sender, ChannelFollowArgs e) => { ThreadDispatcher.Enqueue(() => ChannelFollow?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelGoalBegin += (object sender, ChannelGoalBeginArgs e) => { ThreadDispatcher.Enqueue(() => ChannelGoalBegin?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelGoalEnd += (object sender, ChannelGoalEndArgs e) => { ThreadDispatcher.Enqueue(() => ChannelGoalEnd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelGoalProgress += (object sender, ChannelGoalProgressArgs e) => { ThreadDispatcher.Enqueue(() => ChannelGoalProgress?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelGuestStarGuestUpdate += (object sender, ChannelGuestStarGuestUpdateArgs e) => { ThreadDispatcher.Enqueue(() => ChannelGuestStarGuestUpdate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelGuestStarSessionBegin += (object sender, ChannelGuestStarSessionBegin e) => { ThreadDispatcher.Enqueue(() => ChannelGuestStarSessionBegin?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelGuestStarSessionEnd += (object sender, ChannelGuestStarSessionEnd e) => { ThreadDispatcher.Enqueue(() => ChannelGuestStarSessionEnd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelGuestStarSettingsUpdate += (object sender, ChannelGuestStarSettingsUpdateArgs e) => { ThreadDispatcher.Enqueue(() => ChannelGuestStarSettingsUpdate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelGuestStarSlotUpdate += (object sender, ChannelGuestStarSlotUpdateArgs e) => { ThreadDispatcher.Enqueue(() => ChannelGuestStarSlotUpdate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelHypeTrainBegin += (object sender, ChannelHypeTrainBeginArgs e) => { ThreadDispatcher.Enqueue(() => ChannelHypeTrainBegin?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelHypeTrainEnd += (object sender, ChannelHypeTrainEndArgs e) => { ThreadDispatcher.Enqueue(() => ChannelHypeTrainEnd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelHypeTrainProgress += (object sender, ChannelHypeTrainProgressArgs e) => { ThreadDispatcher.Enqueue(() => ChannelHypeTrainProgress?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelModeratorAdd += (object sender, ChannelModeratorArgs e) => { ThreadDispatcher.Enqueue(() => ChannelModeratorAdd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelModeratorRemove += (object sender, ChannelModeratorArgs e) => { ThreadDispatcher.Enqueue(() => ChannelModeratorRemove?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelVipAdd += (object sender, ChannelVipArgs e) => { ThreadDispatcher.Enqueue(() => ChannelVipAdd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelVipRemove += (object sender, ChannelVipArgs e) => { ThreadDispatcher.Enqueue(() => ChannelVipRemove?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPointsCustomRewardAdd += (object sender, ChannelPointsCustomRewardArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPointsCustomRewardAdd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPointsCustomRewardRemove += (object sender, ChannelPointsCustomRewardArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPointsCustomRewardRemove?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPointsCustomRewardUpdate += (object sender, ChannelPointsCustomRewardArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPointsCustomRewardUpdate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPointsAutomaticRewardRedemptionAdd += (object sender, ChannelPointsAutomaticRewardRedemptionArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPointsAutomaticRewardRedemptionAdd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPointsCustomRewardRedemptionAdd += (object sender, ChannelPointsCustomRewardRedemptionArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPointsCustomRewardRedemptionAdd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPointsCustomRewardRedemptionUpdate += (object sender, ChannelPointsCustomRewardRedemptionArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPointsCustomRewardRedemptionUpdate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPollBegin += (object sender, ChannelPollBeginArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPollBegin?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPollEnd += (object sender, ChannelPollEndArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPollEnd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPollProgress += (object sender, ChannelPollProgressArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPollProgress?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPredictionBegin += (object sender, ChannelPredictionBeginArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPredictionBegin?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPredictionEnd += (object sender, ChannelPredictionEndArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPredictionEnd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPredictionLock += (object sender, ChannelPredictionLockArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPredictionLock?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelPredictionProgress += (object sender, ChannelPredictionProgressArgs e) => { ThreadDispatcher.Enqueue(() => ChannelPredictionProgress?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelRaid += (object sender, ChannelRaidArgs e) => { ThreadDispatcher.Enqueue(() => ChannelRaid?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelShieldModeBegin += (object sender, ChannelShieldModeBeginArgs e) => { ThreadDispatcher.Enqueue(() => ChannelShieldModeBegin?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelShieldModeEnd += (object sender, ChannelShieldModeEndArgs e) => { ThreadDispatcher.Enqueue(() => ChannelShieldModeEnd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelShoutoutCreate += (object sender, ChannelShoutoutCreateArgs e) => { ThreadDispatcher.Enqueue(() => ChannelShoutoutCreate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelShoutoutReceive += (object sender, ChannelShoutoutReceiveArgs e) => { ThreadDispatcher.Enqueue(() => ChannelShoutoutReceive?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelSubscribe += (object sender, ChannelSubscribeArgs e) => { ThreadDispatcher.Enqueue(() => ChannelSubscribe?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelSubscriptionEnd += (object sender, ChannelSubscriptionEndArgs e) => { ThreadDispatcher.Enqueue(() => ChannelSubscriptionEnd?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelSubscriptionGift += (object sender, ChannelSubscriptionGiftArgs e) => { ThreadDispatcher.Enqueue(() => ChannelSubscriptionGift?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelSubscriptionMessage += (object sender, ChannelSubscriptionMessageArgs e) => { ThreadDispatcher.Enqueue(() => ChannelSubscriptionMessage?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelSuspiciousUserMessage += (object sender, ChannelSuspiciousUserMessageArgs e) => { ThreadDispatcher.Enqueue(() => ChannelSuspiciousUserMessage?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelSuspiciousUserUpdate += (object sender, ChannelSuspiciousUserUpdateArgs e) => { ThreadDispatcher.Enqueue(() => ChannelSuspiciousUserUpdate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelWarningAcknowledge += (object sender, ChannelWarningAcknowledgeArgs e) => { ThreadDispatcher.Enqueue(() => ChannelWarningAcknowledge?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelWarningSend += (object sender, ChannelWarningSendArgs e) => { ThreadDispatcher.Enqueue(() => ChannelWarningSend?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelUnban += (object sender, ChannelUnbanArgs e) => { ThreadDispatcher.Enqueue(() => ChannelUnban?.Invoke(sender, e)); return Task.CompletedTask; };
            base.ChannelUpdate += (object sender, ChannelUpdateArgs e) => { ThreadDispatcher.Enqueue(() => ChannelUpdate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.StreamOffline += (object sender, StreamOfflineArgs e) => { ThreadDispatcher.Enqueue(() => StreamOffline?.Invoke(sender, e)); return Task.CompletedTask; };
            base.StreamOnline += (object sender, StreamOnlineArgs e) => { ThreadDispatcher.Enqueue(() => StreamOnline?.Invoke(sender, e)); return Task.CompletedTask; };
            base.UserUpdate += (object sender, UserUpdateArgs e) => { ThreadDispatcher.Enqueue(() => UserUpdate?.Invoke(sender, e)); return Task.CompletedTask; };
            base.UserWhisperMessage += (object sender, UserWhisperMessageArgs e) => { ThreadDispatcher.Enqueue(() => UserWhisperMessage?.Invoke(sender, e)); return Task.CompletedTask; };
        }

    }
}
