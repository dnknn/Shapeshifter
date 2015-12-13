﻿namespace Shapeshifter.WindowsDesktop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Autofac;

    using NSubstitute;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    static class Extensions
    {
        static readonly IDictionary<Type, object> fakeCache;

        static Extensions()
        {
            fakeCache = new Dictionary<Type, object>();
        }

        internal static void ClearCache()
        {
            fakeCache.Clear();
        }

        public static void AssertWait(Action expression)
        {
            AssertWait(10000, expression);
        }

        public static void AssertWait(int timeout, Action expression)
        {
            var time = DateTime.Now;

            var exceptions = new List<AssertFailedException>();
            while (
                ((DateTime.Now - time).TotalMilliseconds < timeout) || 
                (exceptions.Count == 0))
            {
                try
                {
                    expression();
                    return;
                }
                catch (AssertFailedException ex)
                {
                    if (exceptions.All(x => x.Message != ex.Message))
                    {
                        exceptions.Add(ex);
                    }
                }

                Thread.Sleep(1);
            }

            if (exceptions.Count > 1)
            {
                throw new AggregateException(exceptions);
            }

            throw exceptions.First();
        }

        public static TInterface WithFakeSettings<TInterface>(this TInterface item, Action<TInterface> method)
        {
            method(item);
            return item;
        }

        public static TInterface RegisterFake<TInterface>(this ContainerBuilder builder)
            where TInterface : class
        {
            if (fakeCache.ContainsKey(typeof (TInterface)))
            {
                return (TInterface) fakeCache[typeof (TInterface)];
            }

            var fake = Substitute.For<TInterface>();
            return Register(builder, fake);
        }

        static TInterface Register<TInterface>(ContainerBuilder builder, TInterface fake) where TInterface : class
        {
            fakeCache.Add(typeof (TInterface), fake);
            builder.Register(c => fake)
                   .As<TInterface>();
            return fake;
        }

        public static void IgnoreAwait(this Task task) { }
    }
}