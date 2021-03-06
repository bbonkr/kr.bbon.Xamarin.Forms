﻿using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace kr.bbon.Xamarin.Forms
{
    /// <summary>
    /// IoC 컨테이너 접근을 제공합니다.
    /// </summary>
    public class AppContainer
    {
        private AppContainer() { }

        public static AppContainer Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new AppContainer();
                        }
                    }
                }
                return instance;
            }
        }

        public IContainer Container { get => container; }

        public void Build(ContainerBuilder builder)
        {
            container = builder.Build();
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public T Resolve<T>(params Parameter[] parameters)
        {
            return container.Resolve<T>(parameters);
        }

        public T Resolve<T>(IEnumerable<Parameter> parameters)
        {
            return container.Resolve<T>(parameters);
        }

        private IContainer container = null;
        private static AppContainer instance = null;
        private static object sync = new object();
    }
}
