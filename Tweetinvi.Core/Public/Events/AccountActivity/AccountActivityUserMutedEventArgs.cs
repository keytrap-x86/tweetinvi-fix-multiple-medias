﻿using System;
using Tweetinvi.Models;

namespace Tweetinvi.Events
{
    public enum UserMutedRaisedInResultOf
    {
        /// <summary>
        /// The account user has muted another user 
        /// </summary>
        AccountUserMutingAnotherUser,

        /// <summary>
        /// This case should not happen and is here in case Twitter changes when they trigger the Muted event.
        /// If you happen to receive this mode, please report to Tweetinvi your case ideally with the associated json.
        /// </summary>
        Unknown
    }

    public class AccountActivityUserMutedEventArgs : BaseAccountActivityEventArgs<UserMutedRaisedInResultOf>
    {
        public AccountActivityUserMutedEventArgs(AccountActivityEvent<Tuple<IUser, IUser>> eventInfo) : base(eventInfo)
        {
            MutedBy = eventInfo.Args.Item1;
            MutedUser = eventInfo.Args.Item2;

            InResultOf = GetInResultOf();
        }

        /// <summary>
        /// The user who got muted
        /// </summary>
        public IUser MutedUser { get; }

        /// <summary>
        /// The user who performed the action of muting another user
        /// </summary>
        public IUser MutedBy { get; }

        private UserMutedRaisedInResultOf GetInResultOf()
        {
            if (MutedBy.Id == AccountUserId)
            {
                return UserMutedRaisedInResultOf.AccountUserMutingAnotherUser;
            }

            return UserMutedRaisedInResultOf.Unknown;
        }
    }
}
