using ArquiteturaPadrao.Infra.CrossCutting.Providers.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ArquiteturaPadrao.Infra.CrossCutting.Providers
{
    public class PathProvider : IPathProvider
    {
        private IHostingEnvironment _hostingEnvironment;

        public PathProvider(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public string MapPathFromContentRoot(string navigatePath, string path)
        {
            return MapPath(_hostingEnvironment.ContentRootPath, navigatePath, path);
        }

        public string MapPathFromWebRoot(string navigatePath, string path)
        {
            return MapPath(_hostingEnvironment.WebRootPath, navigatePath, path);
        }

        private string MapPath(string root, string navigatePath, string path)
        {
            if(!string.IsNullOrEmpty(navigatePath))
                return Path.Combine(root, navigatePath, path);

            return Path.Combine(root, path);
        }
    }
}
