using System;
using System.Threading.Tasks;
using WhereAreYouMobile.Services.Common;

namespace WhereAreYouMobile.Abstractions
{
    public interface ILoginAuth0PageRenderService
    {
        void EnableLoginPage();
        Task LoginWrapper(Func<Task<UserLogin>> action);
    }
}