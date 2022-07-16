using Autofac;
using Autofac.Util;
using kr.bbon.Xamarin.Forms.Abstractions;
using kr.bbon.Xamarin.Forms.Factories;
using kr.bbon.Xamarin.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace kr.bbon.Xamarin.Forms
{
    public class AppContainerBuilder
    {
        public AppContainerBuilder()
        {
            containerBuilder = new ContainerBuilder();
        }

        public IContainer Build()
        {
            if (AppContainer.Instance.Container == null)
            {
                Register(containerBuilder);

                AppContainer.Instance.Build(containerBuilder);
            }

            return AppContainer.Instance.Container;
        }

        protected virtual void Register(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClientFactory>().As<IHttpClientFactory>();
            builder.RegisterType<AppCenterDiagnosticsService>().As<IAppCenterDiagnosticsService>();

            // Remove JsonService #16
            //builder.RegisterType<JsonService>().As<IJsonService>();
        }

        private readonly ContainerBuilder containerBuilder;

    }
}
