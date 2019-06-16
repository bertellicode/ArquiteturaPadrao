using System.Threading.Tasks;

namespace ArquiteturaPadrao.Infra.CrossCutting.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
