using System;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Interfaces;
using TwitchLib.Communication.Events;
using UnityEngine;

namespace TwitchLib.Unity
{
    public class Client : TwitchClient, ITwitchClient
    {
        #region Events
        /// <summary>
        /// Fires when client connects to Twitch.
        /// </summary>
        public new event AsyncEventHandler<TwitchLib.Client.Events.OnConnectedEventArgs> OnConnected;

        /// <summary>
        /// Fires when client reconnects to Twitch.
        /// </summary>
        public new event AsyncEventHandler<TwitchLib.Client.Events.OnConnectedEventArgs> OnReconnected;

        /// <summary>
        /// Fires when client joins a channel.
        /// </summary>
        public new event AsyncEventHandler<OnJoinedChannelArgs> OnJoinedChannel;

        /// <summary>
        /// Fires on logging in with incorrect details, returns ErrorLoggingInException.
        /// </summary>
        public new event AsyncEventHandler<OnIncorrectLoginArgs> OnIncorrectLogin;

        /// <summary>
        /// Fires when connecting and channel state is changed, returns ChannelState.
        /// </summary>
        public new event AsyncEventHandler<OnChannelStateChangedArgs> OnChannelStateChanged;

        /// <summary>
        /// Fires when a user state is received, returns UserState.
        /// </summary>
        public new event AsyncEventHandler<OnUserStateChangedArgs> OnUserStateChanged;

        /// <summary>
        /// Fires when a new chat message arrives, returns ChatMessage.
        /// </summary>
        public new event AsyncEventHandler<OnMessageReceivedArgs> OnMessageReceived;

        /// <summary>
        /// Fires when a new whisper arrives, returns WhisperMessage.
        /// </summary>
        public new event AsyncEventHandler<OnWhisperReceivedArgs> OnWhisperReceived;

        /// <summary>
        /// Fires when a chat message is sent, returns username, channel and message.
        /// </summary>
        public new event AsyncEventHandler<OnMessageSentArgs> OnMessageSent;

        /// <summary>
        /// Fires when command (uses custom chat command identifier) is received, returns channel, command, ChatMessage, arguments as string, arguments as list.
        /// </summary>
        public new event AsyncEventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;

        /// <summary>
        /// Fires when command (uses custom whisper command identifier) is received, returns command, Whispermessage.
        /// </summary>
        public new event AsyncEventHandler<OnWhisperCommandReceivedArgs> OnWhisperCommandReceived;

        /// <summary>
        /// Fires when a new viewer/chatter joined the channel's chat room, returns username and channel.
        /// </summary>
        public new event AsyncEventHandler<OnUserJoinedArgs> OnUserJoined;

        /// <summary>
        /// Fires when new subscriber is announced in chat, returns Subscriber.
        /// </summary>
        public new event AsyncEventHandler<OnNewSubscriberArgs> OnNewSubscriber;

        /// <summary>
        /// Fires when current subscriber renews subscription, returns ReSubscriber.
        /// </summary>
        public new event AsyncEventHandler<OnReSubscriberArgs> OnReSubscriber;

        /// <summary>
        /// Fires when a current Prime gaming subscriber converts to a paid subscription.
        /// </summary>
        public new event AsyncEventHandler<OnPrimePaidSubscriberArgs> OnPrimePaidSubscriber;

        /// <summary>
        /// Fires when a current gifted subscriber converts to a paid subscription.
        /// </summary>
        public new event AsyncEventHandler<OnContinuedGiftedSubscriptionArgs> OnContinuedGiftedSubscription;

        /// <summary>
        /// Fires when Twitch notifies client of existing users in chat.
        /// </summary>
        public new event AsyncEventHandler<OnExistingUsersDetectedArgs> OnExistingUsersDetected;

        /// <summary>
        /// Fires when a PART message is received from Twitch regarding a particular viewer
        /// </summary>
        public new event AsyncEventHandler<OnUserLeftArgs> OnUserLeft;

        /// <summary>
        /// Fires when bot has disconnected.
        /// </summary>
        public new event AsyncEventHandler<OnDisconnectedArgs> OnDisconnected;

        /// <summary>
        /// Forces when bot suffers conneciton error.
        /// </summary>
        public new event AsyncEventHandler<OnConnectionErrorArgs> OnConnectionError;

        /// <summary>
        /// Fires when a channel's chat is cleared.
        /// </summary>
        public new event AsyncEventHandler<OnChatClearedArgs> OnChatCleared;

        /// <summary>
        /// Fires when a viewer gets timedout by any moderator.
        /// </summary>
        public new event AsyncEventHandler<OnUserTimedoutArgs> OnUserTimedout;

        /// <summary>
        /// Fires when client successfully leaves a channel.
        /// </summary>
        public new event AsyncEventHandler<OnLeftChannelArgs> OnLeftChannel;

        /// <summary>
        /// Fires when a viewer gets banned by any moderator.
        /// </summary>
        public new event AsyncEventHandler<OnUserBannedArgs> OnUserBanned;

        /// <summary>
        /// Fires when data is either received or sent.
        /// </summary>
        public new event AsyncEventHandler<OnSendReceiveDataArgs> OnSendReceiveData;

        /// <summary>
        /// Fires when a raid notification is detected in chat
        /// </summary>
        public new event AsyncEventHandler<OnRaidNotificationArgs> OnRaidNotification;

        /// <summary>
        /// Fires when a subscription is gifted and announced in chat
        /// </summary>
        public new event AsyncEventHandler<OnGiftedSubscriptionArgs> OnGiftedSubscription;

        /// <summary>Fires when TwitchClient attempts to host a channel it is in.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnSelfRaidError;

        /// <summary>Fires when TwitchClient receives generic no permission error from Twitch.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnNoPermissionError;

        /// <summary>Fires when newly raided channel is mature audience only.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnRaidedChannelIsMatureAudience;

        /// <summary>Fires when the client was unable to join a channel.</summary>
        public new event AsyncEventHandler<OnFailureToReceiveJoinConfirmationArgs> OnFailureToReceiveJoinConfirmation;

        /// <summary>Fires when data is received from Twitch that is not able to be parsed.</summary>
        public new event AsyncEventHandler<OnUnaccountedForArgs> OnUnaccountedFor;

        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnMessageClearedArgs> OnMessageCleared;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnCommunitySubscriptionArgs> OnCommunitySubscription;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnErrorEventArgs> OnError;
        /// <summary>Fires when named event occurs.</summary>
        //public new event AsyncEventHandler<OnVIPsReceivedArgs> OnVIPsReceived;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnAnnouncementArgs> OnAnnouncement;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnMessageThrottledArgs> OnMessageThrottled;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnRequiresVerifiedPhoneNumber;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnFollowersOnly;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnSubsOnly;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnEmoteOnly;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnSuspended;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnBanned;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnSlowMode;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<NoticeEventArgs> OnR9kMode;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnUserIntroArgs> OnUserIntro;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnAnonGiftPaidUpgradeArgs> OnAnonGiftPaidUpgrade;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnUnraidNotificationArgs> OnUnraidNotification;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnRitualArgs> OnRitual;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnBitsBadgeTierArgs> OnBitsBadgeTier;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnCommunityPayForwardArgs> OnCommunityPayForward;
        /// <summary>Fires when named event occurs.</summary>
        public new event AsyncEventHandler<OnStandardPayForwardArgs> OnStandardPayForward;
        #endregion

        public Client() : base(null)
        {
            ThreadDispatcher.EnsureCreated();

            base.OnConnected += (object sender, TwitchLib.Client.Events.OnConnectedEventArgs e) => { ThreadDispatcher.Enqueue(() => OnConnected?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnReconnected += (object sender, TwitchLib.Client.Events.OnConnectedEventArgs e) => { ThreadDispatcher.Enqueue(() => OnReconnected?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnJoinedChannel += (object sender, OnJoinedChannelArgs e) => { ThreadDispatcher.Enqueue(() => OnJoinedChannel?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnIncorrectLogin += (object sender, OnIncorrectLoginArgs e) => { ThreadDispatcher.Enqueue(() => OnIncorrectLogin?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnChannelStateChanged += (object sender, OnChannelStateChangedArgs e) => { ThreadDispatcher.Enqueue(() => OnChannelStateChanged?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnUserStateChanged += (object sender, OnUserStateChangedArgs e) => { ThreadDispatcher.Enqueue(() => OnUserStateChanged?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnMessageReceived += (object sender, OnMessageReceivedArgs e) => { ThreadDispatcher.Enqueue(() => OnMessageReceived?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnWhisperReceived += (object sender, OnWhisperReceivedArgs e) => { ThreadDispatcher.Enqueue(() => OnWhisperReceived?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnMessageSent += (object sender, OnMessageSentArgs e) => { ThreadDispatcher.Enqueue(() => OnMessageSent?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnChatCommandReceived += (object sender, OnChatCommandReceivedArgs e) => { ThreadDispatcher.Enqueue(() => OnChatCommandReceived?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnWhisperCommandReceived += (object sender, OnWhisperCommandReceivedArgs e) => { ThreadDispatcher.Enqueue(() => OnWhisperCommandReceived?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnUserJoined += (object sender, OnUserJoinedArgs e) => { ThreadDispatcher.Enqueue(() => OnUserJoined?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnNewSubscriber += (object sender, OnNewSubscriberArgs e) => { ThreadDispatcher.Enqueue(() => OnNewSubscriber?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnReSubscriber += (object sender, OnReSubscriberArgs e) => { ThreadDispatcher.Enqueue(() => OnReSubscriber?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnPrimePaidSubscriber += (object sender, OnPrimePaidSubscriberArgs e) => { ThreadDispatcher.Enqueue(() => OnPrimePaidSubscriber?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnContinuedGiftedSubscription += (object sender, OnContinuedGiftedSubscriptionArgs e) => { ThreadDispatcher.Enqueue(() => OnContinuedGiftedSubscription?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnExistingUsersDetected += (object sender, OnExistingUsersDetectedArgs e) => { ThreadDispatcher.Enqueue(() => OnExistingUsersDetected?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnUserLeft += (object sender, OnUserLeftArgs e) => { ThreadDispatcher.Enqueue(() => OnUserLeft?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnDisconnected += (object sender, OnDisconnectedArgs e) => { ThreadDispatcher.Enqueue(() => OnDisconnected?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnConnectionError += (object sender, OnConnectionErrorArgs e) => { ThreadDispatcher.Enqueue(() => OnConnectionError?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnChatCleared += (object sender, OnChatClearedArgs e) => { ThreadDispatcher.Enqueue(() => OnChatCleared?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnUserTimedout += (object sender, OnUserTimedoutArgs e) => { ThreadDispatcher.Enqueue(() => OnUserTimedout?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnLeftChannel += (object sender, OnLeftChannelArgs e) => { ThreadDispatcher.Enqueue(() => OnLeftChannel?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnUserBanned += (object sender, OnUserBannedArgs e) => { ThreadDispatcher.Enqueue(() => OnUserBanned?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnSendReceiveData += (object sender, OnSendReceiveDataArgs e) => { ThreadDispatcher.Enqueue(() => OnSendReceiveData?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnRaidNotification += (object sender, OnRaidNotificationArgs e) => { ThreadDispatcher.Enqueue(() => OnRaidNotification?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnGiftedSubscription += (object sender, OnGiftedSubscriptionArgs e) => { ThreadDispatcher.Enqueue(() => OnGiftedSubscription?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnRaidedChannelIsMatureAudience += (object sender, NoticeEventArgs arg) => { ThreadDispatcher.Enqueue(() => OnRaidedChannelIsMatureAudience?.Invoke(sender, arg)); return Task.CompletedTask; };
            base.OnFailureToReceiveJoinConfirmation += (object sender, OnFailureToReceiveJoinConfirmationArgs e) => { ThreadDispatcher.Enqueue(() => OnFailureToReceiveJoinConfirmation?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnUnaccountedFor += (object sender, OnUnaccountedForArgs e) => { ThreadDispatcher.Enqueue(() => OnUnaccountedFor?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnSelfRaidError += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnSelfRaidError?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnNoPermissionError += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnNoPermissionError?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnMessageCleared += (object sender, OnMessageClearedArgs e) => { ThreadDispatcher.Enqueue(() => OnMessageCleared?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnCommunitySubscription += (object sender, OnCommunitySubscriptionArgs e) => { ThreadDispatcher.Enqueue(() => OnCommunitySubscription?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnError += (object sender, OnErrorEventArgs e) => { ThreadDispatcher.Enqueue(() => OnError?.Invoke(sender, e)); return Task.CompletedTask; };
            //base.OnVIPsReceived += (object sender, OnVIPsReceivedArgs e) => { ThreadDispatcher.Enqueue(() => OnVIPsReceived?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnAnnouncement += (object sender, OnAnnouncementArgs e) => { ThreadDispatcher.Enqueue(() => OnAnnouncement?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnMessageThrottled += (object sender, OnMessageThrottledArgs e) => { ThreadDispatcher.Enqueue(() => OnMessageThrottled?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnRequiresVerifiedPhoneNumber += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnRequiresVerifiedPhoneNumber?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnFollowersOnly += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnFollowersOnly?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnSubsOnly += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnSubsOnly?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnEmoteOnly += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnEmoteOnly?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnSuspended += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnSuspended?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnBanned += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnBanned?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnSlowMode += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnSlowMode?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnR9kMode += (object sender, NoticeEventArgs e) => { ThreadDispatcher.Enqueue(() => OnR9kMode?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnUserIntro += (object sender, OnUserIntroArgs e) => { ThreadDispatcher.Enqueue(() => OnUserIntro?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnAnonGiftPaidUpgrade += (object sender, OnAnonGiftPaidUpgradeArgs e) => { ThreadDispatcher.Enqueue(() => OnAnonGiftPaidUpgrade?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnUnraidNotification += (object sender, OnUnraidNotificationArgs e) => { ThreadDispatcher.Enqueue(() => OnUnraidNotification?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnRitual += (object sender, OnRitualArgs e) => { ThreadDispatcher.Enqueue(() => OnRitual?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnBitsBadgeTier += (object sender, OnBitsBadgeTierArgs e) => { ThreadDispatcher.Enqueue(() => OnBitsBadgeTier?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnCommunityPayForward += (object sender, OnCommunityPayForwardArgs e) => { ThreadDispatcher.Enqueue(() => OnCommunityPayForward?.Invoke(sender, e)); return Task.CompletedTask; };
            base.OnStandardPayForward += (object sender, OnStandardPayForwardArgs e) => { ThreadDispatcher.Enqueue(() => OnStandardPayForward?.Invoke(sender, e)); return Task.CompletedTask; };
        }

        private new void HandleNotInitialized()
        {
            ThreadDispatcher.Enqueue(() => throw new ClientNotInitializedException("The twitch client has not been initialized and cannot be used. Please call Initialize();"));
        }
    } 
}

