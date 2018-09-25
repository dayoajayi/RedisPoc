using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RedisPoc.Web
{
    [Produces("application/json")]
    [Route("api/Foo")]
    public class FooController : Controller
    {
        private IFoo _foo;

        public FooController(IFoo foo)
        {
            _foo = foo;
        }

        public async Task Get (string value)
        {
            await _foo.Fah(value);
        }
    }
}