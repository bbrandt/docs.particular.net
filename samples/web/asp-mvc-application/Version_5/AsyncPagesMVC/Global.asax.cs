﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using NServiceBus;

public class MvcApplication : HttpApplication
{
    IBus bus;

    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapRoute(
            "Default", // Route name
            "{controller}/{action}/{id}", // URL with parameters
            new
            {
                controller = "Home",
                action = "SendLinks",
                id = UrlParameter.Optional
            } // Parameter defaults

            );
    }

    public override void Dispose()
    {
        if (bus != null)
        {
            bus.Dispose();
        }
        base.Dispose();
    }

    protected void Application_Start()
    {
        #region ApplicationStart

        ContainerBuilder builder = new ContainerBuilder();

        // Register your MVC controllers.
        builder.RegisterControllers(typeof(MvcApplication).Assembly);

        // Set the dependency resolver to be Autofac.
        IContainer container = builder.Build();

        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.Mvc.WebApplication");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.EnableInstallers();


        var startableBus = Bus.Create(busConfiguration);
        bus = startableBus.Start();

        AreaRegistration.RegisterAllAreas();
        RegisterRoutes(RouteTable.Routes);

        #endregion
    }
}