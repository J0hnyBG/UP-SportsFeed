﻿using System.Web.Hosting;

namespace SportsFeed.WebClient.ScheduledJobs.Helpers.Contracts
{
    public interface IHostingEnvironmentRegister
    {
        void Register(IRegisteredObject obj);

        void Unregister(IRegisteredObject obj);
    }
}
