using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Testing.Microsoft;
using Spring.Context;
using Spring.Context.Support;

namespace DalTest
{
    public abstract class BaseTest : AbstractDependencyInjectionSpringContextTests
    {
        protected IApplicationContext Context
        {
            get
            {
                return ContextRegistry.GetContext();
            }
        }
        protected override string[] ConfigLocations
        {
            get
            {
                //return new string[] { 
                //    "assembly://DalTest/DalTest.Config.Spring/context.config",
                //    //"assembly://DalTest/DalTest.Config.Spring/entity.config",
                //    "assembly://DalTest/DalTest.Config.Spring/dal.config"};

                return new string[] { 
                    "Config/Spring/context.config",
                    //"assembly://DalTest/DalTest.Config.Spring/entity.config",
                    "Config/Spring/dal.config"};
            }
        }
    }
}
