using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.DomainTest.Fixtures
{
    public interface IFixtureService
    {
        TService Get<TService>();
    }
}
