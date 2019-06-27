namespace ArquiteturaPadrao.Infra.CrossCutting.Providers.Interfaces
{
    public interface IPathProvider
    {
        string MapPathFromContentRoot(string navigatePath, string path);

        string MapPathFromWebRoot(string navigatePath, string path);
    }
}
