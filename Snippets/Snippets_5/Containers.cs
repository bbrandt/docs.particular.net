﻿using Autofac;
using Castle.Windsor;
using Microsoft.Practices.Unity;
using Ninject;
using NServiceBus;
using Spring.Context.Support;

public class Containers
{
    public void Simple()
    {
        IWindsorContainer windsorContainer = null;
        ILifetimeScope lifetimeScope = null;
        IKernel ninjectKernel = null;
        GenericApplicationContext springApplicationContext = null;
        StructureMap.IContainer structureMapContainer = null;
        UnityContainer unityContainer = null;

        #region Containers

        BusConfiguration busConfiguration = new BusConfiguration();

        // Autofac
        busConfiguration.UseContainer<AutofacBuilder>();
        // Autofac with a container instance
        busConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(lifetimeScope));

        // Castle
        busConfiguration.UseContainer<WindsorBuilder>();
        // Castle with a container instance
        busConfiguration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(windsorContainer));

        // Ninject
        busConfiguration.UseContainer<NinjectBuilder>();
        // Ninject with a container instance 
        busConfiguration.UseContainer<NinjectBuilder>(c => c.ExistingKernel(ninjectKernel));

        // Unity
        busConfiguration.UseContainer<UnityBuilder>();
        // Unity with a container instance 
        busConfiguration.UseContainer<UnityBuilder>(c => c.UseExistingContainer(unityContainer));

        // Spring
        busConfiguration.UseContainer<SpringBuilder>();
        // Spring with an instance 
        busConfiguration.UseContainer<SpringBuilder>(c => c.ExistingApplicationContext(springApplicationContext));

        // StructureMap
        busConfiguration.UseContainer<StructureMapBuilder>();
        // StructureMap with a container instance 
        busConfiguration.UseContainer<StructureMapBuilder>(c => c.ExistingContainer(structureMapContainer));
        
        #endregion
    }

}
