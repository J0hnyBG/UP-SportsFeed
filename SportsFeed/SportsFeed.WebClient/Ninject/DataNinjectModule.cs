﻿using FluentScheduler;

using Microsoft.AspNet.SignalR.Hubs;

using Ninject.Activation;
using Ninject.Modules;
using Ninject.Web.Common;

using SportsFeed.Data;
using SportsFeed.Data.Contracts;

namespace SportsFeed.WebClient.Ninject
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ISportsFeedDbContext>().To<SportsFeedDbContext>()
                .When(x => DoesNotHaveAncestorAssignableFrom<IHub>(x) && DoesNotHaveAncestorAssignableFrom<IJob>(x))
                .InRequestScope();

            this.Bind<ISportsFeedDbContext>().To<SportsFeedDbContext>()
                .When(HasAncestorAssignableFrom<IHub>)
                .InTransientScope();

            this.Bind<ISportsFeedDbContext>().To<SportsFeedDbContext>()
                .When(HasAncestorAssignableFrom<IJob>)
                .InSingletonScope();
        }

        private static bool HasAncestorAssignableFrom<T>(IRequest request)
        {
            while (true)
            {
                if (request == null)
                {
                    return false;
                }

                if (typeof(T).IsAssignableFrom(request.Service))
                {
                    return true;
                }

                request = request.ParentRequest;
            }
        }

        private static bool DoesNotHaveAncestorAssignableFrom<T>(IRequest request)
        {
            return !HasAncestorAssignableFrom<T>(request);
        }
    }
}
