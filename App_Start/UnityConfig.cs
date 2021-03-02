using GymDataAccess.Infrastructure;
using GymDataAccess.UnitOfWork;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace DynamoFitness
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

           

            container.RegisterType<IConnectionfactory, Connectionfactory>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}