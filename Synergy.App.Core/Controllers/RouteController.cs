using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Synergy.App.Core.Controllers;

[Route("api/debug/routes")]
[ApiController]
public class RouteController(IEnumerable<EndpointDataSource> endpointSources) : ControllerBase
{
    [HttpGet]
    public IActionResult GetRoutes()
    {
        var endpoints = endpointSources.SelectMany(es => es.Endpoints);

        var data = new List<dynamic>();
        foreach (var endpoint in endpoints)
        {
            var routePattern = endpoint is RouteEndpoint re ? re.RoutePattern.RawText : endpoint.DisplayName;

            var authMetadata = endpoint.Metadata.GetOrderedMetadata<IAuthorizeData>();
            var allowAnon = endpoint.Metadata.GetMetadata<IAllowAnonymous>();

            var roles = string.Join(", ",
                authMetadata?.Where(a => !string.IsNullOrWhiteSpace(a.Roles)).Select(a => a.Roles) ?? []);
            var policies = string.Join(", ",
                authMetadata?.Where(a => !string.IsNullOrWhiteSpace(a.Policy)).Select(a => a.Policy) ?? []);
            var anonymous = allowAnon != null;
            data.Add(new
            {
                routePattern,
                roles,
                policies,
                anonymous,
            });
        }

        return Ok(data);
    }
}